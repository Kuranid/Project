using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Базовая скорость передвижения
    public float sprintSpeed = 8f; // Скорость при спринте
    public float jumpForce = 5f; // Сила прыжка
    public float gravity = -9.81f; // Гравитация

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверка, находится ли персонаж на земле, с помощью CharacterController.isGrounded
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Небольшая сила, чтобы персонаж "прилипал" к земле
        }

        // Получение ввода с клавиатуры
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Вычисление направления движения
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Применение спринта, если зажат Shift
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Перемещение персонажа
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Прыжок, если персонаж на земле и нажата клавиша Space
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Применение гравитации
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}