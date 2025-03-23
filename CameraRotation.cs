using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f; // ���������������� ����
    public Transform playerBody; // ������ �� ������, � �������� ��������� ������ (��������, ��������)

    private float xRotation = 0f;

    void Start()
    {
        // ���������� ������� ���� � ������ ������ � ��� �������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ��������� ����� �� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �������� ������ �� ��������� (��� X)
        xRotation -= mouseY; // ����������� mouseY, ����� �������� ���� ����� �������� ������
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������������ ���� �������� �� ���������

        // ��������� ������� �� ��� X � ������
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �������� ��������� (��� �������, � �������� ��������� ������) �� ����������� (��� Y)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}