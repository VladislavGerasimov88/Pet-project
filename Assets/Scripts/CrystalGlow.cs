using UnityEngine;
using System.Collections;

public class CrystalGlow : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private float targetIntensity = 3f; // ������� �������������
    [SerializeField] private float transitionTime = 1f;  // ����� �������� � ��������

    private Material material;
    private Color originalEmissionColor;
    private Coroutine glowCoroutine;
    private bool isGlowing = false;
    private bool Check;
    private bool playerInTrigger = false;

    public PlayerPoint playerPoint;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalEmissionColor = material.GetColor("_EmissionColor");
    }

    void Update()
    {
        if (playerInTrigger && Input.GetMouseButtonDown(0))
        {
            Debug.Log($"����! isGlowing: {isGlowing}, Time: {Time.time}");
            ToggleGlow();
        }
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
            playerPoint.crystalPoint += 1;
        }
    }

    private IEnumerator AdjustEmission(bool glowEnabled)
    {
        yield return new WaitForSeconds(1f);
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
