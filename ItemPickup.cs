using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public SimpleInventory inventory; // Ссылка на инвентарь
    public float pickupDistance = 100f; // Дальность луча для подбора предмета

    void Update()
    {
        // Проверяем, нажата ли клавиша E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Создаём луч из центра экрана
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // Проверяем, попал ли луч в предмет
            if (Physics.Raycast(ray, out hit, pickupDistance))
            {
                if (hit.collider.CompareTag("Item")) // Если луч попал в объект с тегом "Item"
                {
                    // Получаем компонент ItemData
                    ItemData itemData = hit.collider.GetComponent<ItemData>();
                    if (itemData != null && itemData.itemPrefab != null) // Проверяем, что компонент и префаб существуют
                    {
                        // Добавляем префаб в инвентарь
                        inventory.AddItem(itemData.itemPrefab);
                        Destroy(hit.collider.gameObject); // Уничтожаем предмет на сцене
                        Debug.Log($"Предмет {hit.collider.name} подобран.");
                    }
                    else
                    {
                        Debug.LogError("У предмета отсутствует компонент ItemData или префаб не назначен.");
                    }
                }
            }
        }
    }
}