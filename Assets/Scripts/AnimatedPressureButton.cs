using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedPressureButton : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private float activationForce = 5000f;//50 // ����������� ���� ��� ��������� (� ��������)
    [SerializeField] private Vector3 forceDirection = Vector3.down; // ����������� ���� (�� ��������� ����)
    [SerializeField] private string pressAnimationTrigger = "Press"; // ��� �������� ���������
    [SerializeField] private Animator animatorStoneDown;

    private bool isActivated = false;
    private Animator buttonAnimator;

    private void Start()
    {
        buttonAnimator = GetComponent<Animator>();

        // ��������� ���������, ���� �����������
        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActivated) return;

        // ������������ ���� �����������
        float impactForce = CalculateImpactForce(collision);

        if (impactForce >= activationForce)
        {
            ActivateButton();
        }
    }

    private float CalculateImpactForce(Collision collision)
    {
        Rigidbody otherRb = collision.collider.attachedRigidbody;
        if (otherRb == null) return 0f;

        // ���������� ������� �� ������ �����������
        Vector3 forceVector = Vector3.Project(collision.impulse / Time.fixedDeltaTime, forceDirection);
        return forceVector.magnitude;
    }

    private void ActivateButton()
    {
        isActivated = true;

        buttonAnimator.SetTrigger(pressAnimationTrigger);       

        animatorStoneDown.SetTrigger("activate");
    }
    
}
