using System.Collections;
using UnityEngine;

public class StoneGlow : MonoBehaviour
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
    public ParticleSystem myParticleSystem;
    public LittleStoneGlow littleStoneGlow;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalEmissionColor = material.GetColor("_EmissionColor");
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"����! isGlowing: {isGlowing}, Time: {Time.time}");
            ToggleGlow();
            if (littleStoneGlow != null)
            {
                littleStoneGlow.ToggleGlow();
            }
        }
    }

    public void ToggleGlow()
    {
        if (Check == false)
        {
            isGlowing = !isGlowing;
            Check = true;
            myParticleSystem.Play();
            // ������������� ���������� ��������, ���� ����
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }

            // ��������� ����� ��������
            glowCoroutine = StartCoroutine(AdjustEmission(isGlowing));
            playerPoint.points += 1;
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
