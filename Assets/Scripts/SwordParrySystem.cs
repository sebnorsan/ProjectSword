using UnityEngine;

public class SwordParrySystem : MonoBehaviour
{
    [SerializeField] private GameObject parryCollider;

    [SerializeField] private float rechargeTime = .2f;

    private ParryParent parryParent;

    private bool m1, m2;

    private void Awake()
    {
        parryParent = GetComponentInChildren<ParryParent>();
    }

    private void Update()
    {
        HandleInput();
        if (m2)
            Parry();
    }
    private void HandleInput()
    {
        m1 = Input.GetKeyDown(KeyCode.Mouse0);
        m2 = Input.GetKeyDown(KeyCode.Mouse1);
    }

    private void Parry()
    {
        if (!parryCollider.activeSelf)
        {
            ParryToggle();
            Invoke(nameof(ParryToggle), rechargeTime);
        }
    }

    public void ParryToggle()
    {
        if (!parryCollider.activeSelf)
        {
            parryParent.enabled = false;
            parryCollider.SetActive(true);
        }
        else
        {
            parryParent.enabled = true;
            parryCollider.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Parryable"))
            ParryObject(collision.gameObject);
    }
    private void ParryObject(GameObject go)
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRot = Quaternion.AngleAxis(angle, Vector3.forward);

        go.transform.rotation = desiredRot;

        go.GetComponent<Rigidbody2D>().velocity = -go.GetComponent<Rigidbody2D>().velocity;
    }
}
