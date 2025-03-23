using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour
{
    [Header("Прицел")]
    public Texture2D crosshairTexture; // Текстура прицела
    public Vector2 crosshairSize = new Vector2(20, 20); // Размер прицела
    public Color crosshairColor = Color.white; // Цвет прицела

    [Header("Луч")]
    public float raycastRange = 100f; // Дальность луча
    public LayerMask targetLayer; // Слой, на котором будут обнаруживаться объекты

    [Header("Эффект")]
    public GameObject blackSpotPrefab; // Префаб чёрного пятна
    public float spotFadeDuration = 30f; // Время исчезновения пятна (в секундах)
    public int maxSpots = 30; // Максимальное количество пятен

    private Queue<GameObject> spotsQueue = new Queue<GameObject>(); // Очередь для хранения пятен

    void OnGUI()
    {
        // Отрисовка прицела в центре экрана
        if (crosshairTexture != null)
        {
            Vector2 center = new Vector2(Screen.width / 2 - crosshairSize.x / 2, Screen.height / 2 - crosshairSize.y / 2);
            GUI.color = crosshairColor;
            GUI.DrawTexture(new Rect(center, crosshairSize), crosshairTexture);
        }
    }

    void Update()
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0)) // 0 = ЛКМ
        {
            // Проверяем, назначена ли камера
            if (Camera.main == null)
            {
                Debug.LogError("Камера не найдена! Убедись, что на сцене есть камера с тегом MainCamera.");
                return;
            }

            // Создаём луч из центра экрана
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // Проверяем, попал ли луч в объект
            if (Physics.Raycast(ray, out hit, raycastRange, targetLayer))
            {
                Debug.Log("Обнаружен объект: " + hit.collider.name);

                // Проверяем, назначен ли префаб
                if (blackSpotPrefab == null)
                {
                    Debug.LogError("Префаб чёрного пятна не назначен!");
                    return;
                }

                // Если достигнуто максимальное количество пятен, удаляем самое старое
                if (spotsQueue.Count >= maxSpots)
                {
                    GameObject oldestSpot = spotsQueue.Dequeue(); // Удаляем самое старое пятно из очереди
                    if (oldestSpot != null)
                    {
                        Destroy(oldestSpot); // Уничтожаем его
                    }
                }

                // Создаём новое пятно на объекте
                GameObject newSpot = Instantiate(blackSpotPrefab, hit.point, Quaternion.identity);
                newSpot.transform.parent = hit.transform; // Привязываем пятно к объекту

                // Добавляем новое пятно в очередь
                spotsQueue.Enqueue(newSpot);

                // Запускаем корутину для удаления пятна через указанное время
                StartCoroutine(RemoveSpot(newSpot, spotFadeDuration));
            }
        }
    }

    // Корутина для удаления пятна через указанное время
    private System.Collections.IEnumerator RemoveSpot(GameObject spot, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждём указанное время
        if (spot != null)
        {
            Destroy(spot); // Удаляем пятно
        }
    }
}