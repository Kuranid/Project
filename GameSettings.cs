using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("��������� VSync � FPS")]
    public bool enableVSync = true; // �������� VSync
    public int targetFPS = 60; // ����������� FPS

    [Header("��������� ����")]
    public float mouseSensitivity = 100f; // ���������������� ����

    [Header("��������� ������")]
    public int screenWidth = 1920; // ������ ������
    public int screenHeight = 1080; // ������ ������
    public int refreshRate = 60; // ������� ���������� �������� (��������)
    public bool fullscreen = true; // ������������� �����

    [Header("��������� ��������")]
    public float flashlightIntensity = 3f; // ������� ����� ��������

    [Header("������")]
    public CameraRotation cameraRotation; // ������ �� ������ �������� ������

    void Start()
    {
        // ��������� ��������� VSync � FPS
        ApplyVSyncAndFPS();

        // ��������� ��������� ������
        ApplyScreenSettings();

        // ��������� ���������������� ����
        if (cameraRotation != null)
        {
            cameraRotation.mouseSensitivity = mouseSensitivity;
        }
    }

    void ApplyVSyncAndFPS()
    {
        // �������� ��� ��������� VSync
        QualitySettings.vSyncCount = enableVSync ? 1 : 0;

        // ������������� ������� FPS
        Application.targetFrameRate = enableVSync ? -1 : targetFPS; // ���� VSync �������, FPS ������������
    }

    void ApplyScreenSettings()
    {
        // ������������� ���������� � ������� ����������
        Screen.SetResolution(screenWidth, screenHeight, fullscreen, refreshRate);
    }

    void Update()
    {
        // ��������� ���������������� ���� � �������� ������� (���� �����)
        if (cameraRotation != null)
        {
            cameraRotation.mouseSensitivity = mouseSensitivity;
        }
    }
}