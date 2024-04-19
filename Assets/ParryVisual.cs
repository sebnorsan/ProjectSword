using UnityEngine;

public class ParryVisual : MonoBehaviour
{
    private Animator anim;
    private int swing = 1;

    private void OnEnable()
    {
        if (swing == 1)
        {
            anim.SetTrigger("Swing1");
            swing = 0;
        }
        else
        {
            anim.SetTrigger("Swing2");
            swing = 1;
        }
    }
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

}
