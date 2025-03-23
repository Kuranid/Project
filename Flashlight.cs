using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [Header("Настройки фонарика")]
    public Light flashlightLight; // Источник света (Spotlight)
    public Transform lightOrigin; // Точка, откуда исходит свет

    private bool isOn = false; // Включён ли фонарик
    private GameSettings gameSettings; // Ссылка на настройки игры
    private SimpleInventory inventory; // Ссылка на инвентарь

    void Start()
    {
        // Проверяем, назначен ли источник света
        if (flashlightLight == null)
        {
            Debug.LogError("Источник света не назначен!");
            return;
        }

        // Получаем ссылку на GameSettings
        gameSettings = FindObjectOfType<GameSettings>();
        if (gameSettings == null)
        {
            Debug.LogError("GameSettings не найден!");
            return;
        }

        // Получаем ссылку на инвентарь
        inventory = FindObjectOfType<SimpleInventory>();
        if (inventory == null)
        {
            Debug.LogError("Инвентарь не найден!");
            return;
        }

        // Инициализация
        flashlightLight.enabled = false; // Выключаем свет при старте
        flashlightLight.intensity = gameSettings.flashlightIntensity; // Устанавливаем яркость из GameSettings
    }

    void Update()
    {
        // Проверяем, находится ли фонарик в инвентаре
        if (!IsInInventory()) return;

        // Включение/выключение фонарика по ЛКМ
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            ToggleFlashlight();
        }

        // Обновляем позицию и направление света
        if (flashlightLight != null && lightOrigin != null)
        {
            flashlightLight.transform.position = lightOrigin.position;
            flashlightLight.transform.rotation = lightOrigin.rotation;
        }

        // Обновляем яркость света из GameSettings
        if (flashlightLight != null && gameSettings != null)
        {
            flashlightLight.intensity = gameSettings.flashlightIntensity;
        }
    }

    // Проверка, находится ли фонарик в инвентаре
    private bool IsInInventory()
    {
        if (inventory == null) return false;

        // Проверяем, есть ли фонарик в инвентаре
        foreach (GameObject item in inventory.items)
        {
            if (item != null && item == gameObject) // Если фонарик найден в инвентаре
            {
                return true;
            }
        }

        return false;
    }

    // Включение/выключение фонарика
    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;

        if (isOn)
        {
            Debug.Log("Фонарик включён.");
        }
        else
        {
            Debug.Log("Фонарик выключен.");
        }
    }
}