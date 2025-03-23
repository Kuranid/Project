using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("��������� ��������")]
    public Light flashlightLight; // �������� ����� (Spotlight)
    public Transform lightOrigin; // �����, ������ ������� ����

    private bool isOn = false; // ������� �� �������
    private GameSettings gameSettings; // ������ �� ��������� ����
    private SimpleInventory inventory; // ������ �� ���������

    void Start()
    {
        // ���������, �������� �� �������� �����
        if (flashlightLight == null)
        {
            Debug.LogError("�������� ����� �� ��������!");
            return;
        }

        // �������� ������ �� GameSettings
        gameSettings = FindObjectOfType<GameSettings>();
        if (gameSettings == null)
        {
            Debug.LogError("GameSettings �� ������!");
            return;
        }

        // �������� ������ �� ���������
        inventory = FindObjectOfType<SimpleInventory>();
        if (inventory == null)
        {
            Debug.LogError("��������� �� ������!");
            return;
        }

        // �������������
        flashlightLight.enabled = false; // ��������� ���� ��� ������
        flashlightLight.intensity = gameSettings.flashlightIntensity; // ������������� ������� �� GameSettings
    }

    void Update()
    {
        // ���������, ��������� �� ������� � ���������
        if (!IsInInventory()) return;

        // ���������/���������� �������� �� ���
        if (Input.GetMouseButtonDown(0)) // ���
        {
            ToggleFlashlight();
        }

        // ��������� ������� � ����������� �����
        if (flashlightLight != null && lightOrigin != null)
        {
            flashlightLight.transform.position = lightOrigin.position;
            flashlightLight.transform.rotation = lightOrigin.rotation;
        }

        // ��������� ������� ����� �� GameSettings
        if (flashlightLight != null && gameSettings != null)
        {
            flashlightLight.intensity = gameSettings.flashlightIntensity;
        }
    }

    // ��������, ��������� �� ������� � ���������
    private bool IsInInventory()
    {
        if (inventory == null) return false;

        // ���������, ���� �� ������� � ���������
        foreach (GameObject item in inventory.items)
        {
            if (item != null && item == gameObject) // ���� ������� ������ � ���������
            {
                return true;
            }
        }

        return false;
    }

    // ���������/���������� ��������
    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;

        if (isOn)
        {
            Debug.Log("������� �������.");
        }
        else
        {
            Debug.Log("������� ��������.");
        }
    }
}