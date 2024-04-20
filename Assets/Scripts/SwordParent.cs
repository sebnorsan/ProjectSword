using UnityEngine;

public class SwordParent : MonoBehaviour
{
    //[SerializeField] private Transform sprite;

    private void Update()
    {
        //if (sprite.localScale.x == -1)
        //    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        //else
        //    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 0, 0, transform.rotation.w), 5f * Time.deltaTime);

        if (Input.GetMouseButtonUp(1))
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion desiredRot = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = desiredRot;
        }
    }
}
