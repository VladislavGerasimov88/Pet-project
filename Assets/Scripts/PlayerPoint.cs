using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public int points;
    public int platePoints;
    public int crystalPoint;

    [SerializeField] private Animator animatorPeaks;
    [SerializeField] private Animator animatorFirstPlate;
    [SerializeField] private Animator animatorFirstDoor;
    [SerializeField] private Animator animatorStoneDoor;
    [SerializeField] private Animator animatorGrid;
    [SerializeField] private Animator animatorMainDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (points == 2)
        {
            StartCoroutine(ExecuteAfterDelay());
        }
        if (points ==3)
        {
            animatorFirstPlate.SetTrigger("activate");
            StartCoroutine(OpenDoor());
        }
        if (platePoints == 4)
        {
            animatorStoneDoor.SetTrigger("activate");
        }
        if (points==8)
        {
            StartCoroutine(OpenMainDoor());
        }
        if (crystalPoint==3)
        {
            StartCoroutine(OpenGrid());
        }
    }
    IEnumerator ExecuteAfterDelay()
    {
        // ��� 1 �������
        yield return new WaitForSeconds(1f);

        animatorPeaks.SetTrigger("activate");
    }
    IEnumerator OpenDoor()
    {
        // ��� 1 �������
        yield return new WaitForSeconds(1f);

        animatorFirstDoor.SetTrigger("activate");
    }
    IEnumerator OpenGrid()
    {
        // ��� 1 �������
        yield return new WaitForSeconds(1f);

        animatorGrid.SetTrigger("inActiv");
    }
    IEnumerator OpenMainDoor()
    {
        // ��� 1 �������
        yield return new WaitForSeconds(1f);

        animatorMainDoor.SetTrigger("activate");
    }
}
