using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float fireOnTime = 3f;
    [SerializeField] private float fireOffTime = 3f;
    [SerializeField] private GameObject hitPoint;

    private float lastFireTime;
    private float lastFireOffTime;

    private bool isFireOn;

    private Animator animator; 

    private void Start()
    {
        animator = GetComponent<Animator>();

        lastFireTime = Time.time;
        hitPoint.SetActive(false);
        isFireOn = false;
    }

    private void Update()
    {
        if (!isFireOn)
        {
            if (Time.time >= lastFireTime + fireOffTime)
            {
                animator.SetBool("IsOn", true);
            }
        }
        else
        {
            if (Time.time >= lastFireTime + fireOnTime)
            {
                animator.SetBool("IsOn", false);
                hitPoint.SetActive(false);
                isFireOn = false;
                lastFireTime = Time.time;
            }
        }
    }

    private void FireOn()
    {
        Debug.Log("Fire ON");
        hitPoint.SetActive(true);
        isFireOn = true;
        lastFireTime = Time.time;
    }
}
