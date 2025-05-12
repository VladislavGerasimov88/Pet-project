using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    private Animator anim;
    private SphereCollider sphereCollider;

    public static int CoinCount = 0;    
    public static TMP_Text CoinText; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        if (CoinText == null)
        {
            CoinText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        }

        // Обновляем отображение счёта при старте
        UpdateCoinText();
    }

    // При сборе монеты
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("activate");
            CoinCount++; // Увеличиваем счёт
            UpdateCoinText(); // Обновляем UI
            Destroy(sphereCollider);
            Destroy(gameObject, 2f); // Уничтожаем монетку
        }
    }

    // Метод для обновления текста счёта
    public void UpdateCoinText()
    {
        if (CoinText != null)
        {
            CoinText.text = $"{CoinCount}";
        }
    }
}
