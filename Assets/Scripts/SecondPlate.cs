using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlate : MonoBehaviour
{
    private Animator animator;

    public PlayerPoint playerPoint;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //animator.SetTrigger("activate");
        animator.SetInteger("activate", 5);
        playerPoint.platePoints += 1;
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetInteger("activate", 0);
        playerPoint.platePoints -= 1;
    }
}
