using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlate : MonoBehaviour
{
    public PlayerPoint playerPoint;
    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPoint.points += 1;
            Destroy(boxCollider);
        }
    }
}
