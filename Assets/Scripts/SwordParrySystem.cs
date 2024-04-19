using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParrySystem : MonoBehaviour
{
    [SerializeField] private GameObject parryCollider;

    [SerializeField] private float rechargeTime = .2f;

    private bool m1, m2;
    private void Update()
    {
        HandleInput();
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
            parryCollider.SetActive(true);
        else
            parryCollider.SetActive(false);
    }
}
