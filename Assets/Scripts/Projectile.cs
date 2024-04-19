using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    private void Awake()
    {
        Vector2 dir = GameObject.Find("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRot = Quaternion.AngleAxis(angle, Vector3.forward);

        gameObject.transform.rotation = desiredRot;

        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }
}
