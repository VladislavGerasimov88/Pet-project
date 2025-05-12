using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperOpenChest : MonoBehaviour
{
    private bool playerInTrigger;

    [SerializeField] private CoinsCounter coinsCounter;
    [SerializeField] private GameObject helper;
    [SerializeField] private Animator animChest;

    
    void Update()
    {
        if (playerInTrigger)
        {
            if (helper!=null)
            {
                helper.gameObject.SetActive(true);
            }
        }
        if (!playerInTrigger)
        {
            helper.gameObject.SetActive(false);
        }
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            animChest.SetTrigger("activate");
            //helper.gameObject.SetActive(false);
            Destroy(helper);
            StartCoroutine(AdditionCoins());           
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
    IEnumerator AdditionCoins()
    {        
        yield return new WaitForSeconds(1.9f);

        CoinsCounter.CoinCount += 3;
        coinsCounter.UpdateCoinText();
        Destroy(gameObject);
    }
}
