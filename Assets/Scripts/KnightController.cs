using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    //[SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    //public CrystalGlow crystalGlow;

    //[Header("Pushing Settings")]
    //[SerializeField] private float pushForce = 5f; // ���� ��������

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    //bool isPushing = false;
    private float gravity = -9.81f;


    private Animator animator;
    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    //private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            groundCheck = transform;
        }
    }

    private void Update()
    {
        // �������� ���������� �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // ��������� �����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // ����������� ���������
        Vector3 moveDirection = new Vector3(-vertical, 0, horizontal).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // ������� ��������� � ����������� �������� (������������ ���������� ����)
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // �������� ������ (��� � ���������� �����������)
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // ������
        //if (isGrounded && Input.GetButtonDown("Jump"))
        //{
        //    velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        //}

        // ����������
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //��������
        if (animator != null)
        {
            float animationSpeed = moveDirection.magnitude;
            animator.SetFloat(MoveSpeedHash, animationSpeed);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("isAttack");
            }
            //animator.SetBool(IsGroundedHash, isGrounded);
        }
        
    }
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Crystal"))
    //    {
    //        crystalGlow.ToggleGlow();
    //    }
    //}
}






// ����������� ���������
//Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

//if (moveDirection.magnitude >= 0.1f)
//{
//    // ������� ��������� � ����������� ��������
//    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
//    float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
//    transform.rotation = Quaternion.Euler(0f, angle, 0f);

//    // �������� ������
//    Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//    characterController.Move(moveVector.normalized * moveSpeed * Time.deltaTime);
//}

//private void OnControllerColliderHit(ControllerColliderHit hit)
//{
//    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

//    // ���� ������ ����� Rigidbody � �� ��������� (isKinematic = false)
//    if (rb != null && !rb.isKinematic)
//    {

//        // ��������� ���� � ����������� �������� ���������
//        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
//        rb.AddForce(pushDirection * pushForce, ForceMode.Force);
//        isPushing = true;
//        animator.SetBool("IsPushing", isPushing);

//    }
//    else
//    {
//        isPushing = false;
//        animator.SetBool("IsPushing", isPushing);
//    }
//}