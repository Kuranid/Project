using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Настройки VSync и FPS")]
    public bool enableVSync = true; // Включить VSync
    public int targetFPS = 60; // Ограничение FPS

    [Header("Настройки мыши")]
    public float mouseSensitivity = 100f; // Чувствительность мыши

    [Header("Настройки экрана")]
    public int screenWidth = 1920; // Ширина экрана
    public int screenHeight = 1080; // Высота экрана
    public int refreshRate = 60; // Частота обновления монитора (герцовка)
    public bool fullscreen = true; // Полноэкранный режим

    [Header("Настройки фонарика")]
    public float flashlightIntensity = 3f; // Яркость света фонарика

    [Header("Ссылки")]
    public CameraRotation cameraRotation; // Ссылка на скрипт вращения камеры

    void Start()
    {
        // Применяем настройки VSync и FPS
        ApplyVSyncAndFPS();

        // Применяем настройки экрана
        ApplyScreenSettings();

        // Применяем чувствительность мыши
        if (cameraRotation != null)
        {
            cameraRotation.mouseSensitivity = mouseSensitivity;
        }
    }

    void ApplyVSyncAndFPS()
    {
        // Включаем или выключаем VSync
        QualitySettings.vSyncCount = enableVSync ? 1 : 0;

        // Устанавливаем целевой FPS
        Application.targetFrameRate = enableVSync ? -1 : targetFPS; // Если VSync включён, FPS игнорируется
    }

    void ApplyScreenSettings()
    {
        // Устанавливаем разрешение и частоту обновления
        Screen.SetResolution(screenWidth, screenHeight, fullscreen, refreshRate);
    }

    void Update()
    {
        // Обновляем чувствительность мыши в реальном времени (если нужно)
        if (cameraRotation != null)
        {
            cameraRotation.mouseSensitivity = mouseSensitivity;
        }
    }
}