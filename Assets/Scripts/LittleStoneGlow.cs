using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleStoneGlow : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private float targetIntensity = 3f; // ������� �������������
    [SerializeField] private float transitionTime = 1f;  // ����� �������� � ��������

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

    // ��������� ���� ����� ��� ������� ������
    public void ToggleGlow()
    {
        if (Check == false)
        {
            isGlowing = !isGlowing;
            Check = true;
            // ������������� ���������� ��������, ���� ����
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }

            // ��������� ����� ��������
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

        // �������� ������ ���������
        material.SetColor("_EmissionColor", originalEmissionColor * targetValue);
        DynamicGI.UpdateEnvironment();
    }
    
}
