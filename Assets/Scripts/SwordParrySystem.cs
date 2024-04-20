using UnityEngine;

public class SwordParrySystem : MonoBehaviour
{
    [SerializeField] private GameObject parryCollider;

    [SerializeField] private float rechargeTime = .2f;
    [SerializeField] private float reloadTime = .2f;
    private bool canSwing = true;

    private ParryParent parryParent;

    private bool m1up,m1down, m2down, m2up;

    public bool gravityBlade, earthBlade;

    [SerializeField] private Animator lightAnim;
    private Animator parentAnim;
    private TarodevController.PlayerController player;

    [SerializeField] private ParticleSystem sparks;

    private void Awake()
    {
        parryParent = GetComponentInChildren<ParryParent>();
        parentAnim = gameObject.GetComponent<Animator>();

        player = FindObjectOfType<TarodevController.PlayerController>();
    }

    private void Update()
    {
        HandleInput();

        if (canSwing)
        {
            //if (m1down)
            //    LoadParry();
            if (m1down)
            {
                earthBlade = true;
                Parry();
            }

            if (m2down)
                LoadParry();
            if (m2up)
                Parry();
        }
    }
    private void HandleInput()
    {
        m1down = Input.GetKeyDown(KeyCode.Mouse0);
        m1up = Input.GetKeyUp(KeyCode.Mouse0);
        m2down = Input.GetKeyDown(KeyCode.Mouse1);
        m2up = Input.GetKeyUp(KeyCode.Mouse1);
    }

    private void LoadParry()
    {
        if (!parryCollider.activeSelf)
            lightAnim.SetTrigger("Load");
    }
    private void Parry()
    {
        if (!parryCollider.activeSelf)
        {
            if (canSwing)
            {
                canSwing = false;
                Invoke(nameof(ResetSwing), reloadTime);
            }

            if (!earthBlade)
                lightAnim.SetTrigger("Load");

            ParryToggle();
            Invoke(nameof(ParryToggle), rechargeTime);

            if (!gravityBlade)
            {
                if (parentAnim.GetInteger("FutsuSwing") > 0)
                {
                    parentAnim.SetInteger("FutsuSwing", 0);
                    parentAnim.SetTrigger("Swing1");

                }
                else
                {
                    parentAnim.SetInteger("FutsuSwing", 1);
                    parentAnim.SetTrigger("Swing2");
                }
                    
            }
        }
    }

    private void ResetSwing()
    {
        canSwing = true;
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
        {
            FindObjectOfType<TarodevController.PlayerController>().ExecuteJump();
            earthBlade = false;
        }

        sparks.Play();

        //FindObjectOfType<TarodevController.PlayerController>().ExecuteJump();
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
