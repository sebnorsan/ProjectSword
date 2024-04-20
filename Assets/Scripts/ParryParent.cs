using UnityEngine;

public class ParryParent : MonoBehaviour
{
    //[SerializeField] private Transform sprite;

    private Vector2 pointerPos;

    private void Update()
    {
        //if (sprite.localScale.x == -1)
        //    transform.GetChild(0).transform.localScale = new Vector3(transform.GetChild(0).transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
        //if ((sprite.localScale.x == -1))
        //    transform.GetChild(0).transform.localScale = new Vector3(transform.GetChild(0).transform.localScale.x * -1, transform.GetChild(0).transform.localScale.y, transform.GetChild(0).transform.localScale.z);

        Vector2 direction = (pointerPos - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y *= -1;
        else
            scale.y *= 1;

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion desiredRot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = desiredRot;
    }
}
