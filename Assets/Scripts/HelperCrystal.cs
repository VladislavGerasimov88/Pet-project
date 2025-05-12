using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperCrystal : MonoBehaviour
{
    private bool playerInTrigger;

    [SerializeField] private GameObject helper;

    void Update()
    {
        if (playerInTrigger)
        {
            helper.gameObject.SetActive(true);
        }
        if (!playerInTrigger)
        {
            helper.gameObject.SetActive(false);
        }
        if (playerInTrigger && Input.GetMouseButtonDown(0))
        {
            helper.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
