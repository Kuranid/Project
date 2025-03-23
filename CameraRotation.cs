using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Чувствительность мыши
    public Transform playerBody; // Ссылка на объект, к которому привязана камера (например, персонаж)

    private float xRotation = 0f;

    void Start()
    {
        // Блокировка курсора мыши в центре экрана и его скрытие
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Получение ввода от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вращение камеры по вертикали (ось X)
        xRotation -= mouseY; // Инвертируем mouseY, чтобы движение мыши вверх опускало камеру
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем угол поворота по вертикали

        // Применяем поворот по оси X к камере
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращение персонажа (или объекта, к которому привязана камера) по горизонтали (ось Y)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}