using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPushing : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Animator anim = other.GetComponent<Animator>();
        if (anim != null && other.CompareTag("Player"))
        {
            anim.SetBool("IsPushing", true);
            rb.isKinematic = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Animator anim = other.GetComponent<Animator>();
        if (anim != null && other.CompareTag("Player"))
        {
            anim.SetBool("IsPushing", false);
            rb.isKinematic = true;
        }
    }
}
