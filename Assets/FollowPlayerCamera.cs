using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerToFollow;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = .1f;
    private Vector3 vel = Vector3.zero;

    private void Start()
    {
        playerToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 targetPos = playerToFollow.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);
    }
}
