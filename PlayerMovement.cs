using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ������� �������� ������������
    public float sprintSpeed = 8f; // �������� ��� �������
    public float jumpForce = 5f; // ���� ������
    public float gravity = -9.81f; // ����������

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ��������, ��������� �� �������� �� �����, � ������� CharacterController.isGrounded
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ��������� ����, ����� �������� "��������" � �����
        }

        // ��������� ����� � ����������
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // ���������� ����������� ��������
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ���������� �������, ���� ����� Shift
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // ����������� ���������
        controller.Move(move * currentSpeed * Time.deltaTime);

        // ������, ���� �������� �� ����� � ������ ������� Space
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // ���������� ����������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}