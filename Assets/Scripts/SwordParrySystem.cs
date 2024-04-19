using UnityEngine;

public class SwordParrySystem : MonoBehaviour
{
    [SerializeField] private GameObject parryCollider;

    [SerializeField] private float rechargeTime = .2f;

    private ParryParent parryParent;

    private bool m1, m2;

    public bool gravityBlade, earthBlade;

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

            if (gravityBlade)
            {
                if (!FindObjectOfType<TarodevController.PlayerController>().antiGrav)
                    FindObjectOfType<TarodevController.PlayerController>().antiGrav = true;
                else
                    FindObjectOfType<TarodevController.PlayerController>().antiGrav = false;
            }
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
        if (earthBlade && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            FindObjectOfType<TarodevController.PlayerController>().ExecuteJump();
    }
    private void ParryObject(GameObject go)
    {
        if (gravityBlade)
            return;

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRot = Quaternion.AngleAxis(angle, Vector3.forward);

        go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        go.transform.rotation = desiredRot;

        go.GetComponent<Rigidbody2D>().AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * go.GetComponent<Projectile>().speed * 2, ForceMode2D.Impulse);

    }
}
