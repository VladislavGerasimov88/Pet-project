using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStoneGlow : MonoBehaviour
{
    [Header("Настройки свечения")]
    [SerializeField] private float targetIntensity = 3f; // Целевая интенсивность
    [SerializeField] private float transitionTime = 1f;  // Время перехода в секундах

    private Material material;
    private Color originalEmissionColor;
    private Coroutine glowCoroutine;
    private bool isGlowing = false;
    private bool Check;


    void Start()
    {
        material = GetComponent<Renderer>().material; //GetComponent<Renderer>().sharedMaterial;
        originalEmissionColor = material.GetColor("_EmissionColor");
    }

    // Вызывайте этот метод при нажатии кнопки
    public void ToggleGlow()
    {
        if (Check == false)
        {
            isGlowing = !isGlowing;
            Check = true;
            // Останавливаем предыдущую корутину, если была
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }

            // Запускаем новую корутину
            glowCoroutine = StartCoroutine(AdjustEmission(isGlowing));
        }
    }

    private IEnumerator AdjustEmission(bool glowEnabled)
    {
        yield return new WaitForSeconds(0.3f);
        float startIntensity = material.GetColor("_EmissionColor").maxColorComponent;
        float targetValue = glowEnabled ? targetIntensity : 1f;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionTime);
            float currentIntensity = Mathf.Lerp(startIntensity, targetValue, t);

            material.SetColor("_EmissionColor", originalEmissionColor * currentIntensity);
            DynamicGI.UpdateEnvironment();

            yield return null;
        }

        // Финишная точная установка
        material.SetColor("_EmissionColor", originalEmissionColor * targetValue);
        DynamicGI.UpdateEnvironment();
    }
    
}
