using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedPressureButton : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float activationForce = 5000f;//50 // Необходимая сила для активации (в ньютонах)
    [SerializeField] private Vector3 forceDirection = Vector3.down; // Направление силы (по умолчанию вниз)
    [SerializeField] private string pressAnimationTrigger = "Press"; // Имя триггера аниматора
    [SerializeField] private Animator animatorStoneDown;

    private bool isActivated = false;
    private Animator buttonAnimator;

    private void Start()
    {
        buttonAnimator = GetComponent<Animator>();

        // Добавляем коллайдер, если отсутствует
        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActivated) return;

        // Рассчитываем силу воздействия
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

        // Проецируем импульс на нужное направление
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
