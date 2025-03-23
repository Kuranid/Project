using UnityEngine;

public class SimpleInventory : MonoBehaviour
{
    [Header("Настройки инвентаря")]
    public GameObject[] items = new GameObject[4]; // Массив предметов
    public Transform hand; // Точка, где будут появляться предметы

    private int currentItemIndex = -1; // Индекс текущего предмета (-1 означает, что инвентарь пуст)

    void Update()
    {
        // Переключение между предметами с помощью колёсика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Получаем значение прокрутки
        if (scroll < 0) // Прокрутка вниз
        {
            NextItem();
        }
        else if (scroll > 0) // Прокрутка вверх
        {
            PreviousItem(); // Переключение на предыдущий предмет
        }

        // Обновляем позицию и поворот текущего предмета
        if (currentItemIndex != -1 && items[currentItemIndex] != null && items[currentItemIndex].activeSelf)
        {
            items[currentItemIndex].transform.position = hand.position;
            items[currentItemIndex].transform.rotation = hand.rotation;

            // Если предмет должен быть горизонтальным, применяем дополнительный поворот
            ItemData itemData = items[currentItemIndex].GetComponent<ItemData>();
            if (itemData != null && itemData.isHorizontal)
            {
                items[currentItemIndex].transform.Rotate(90, 0, 0); // Поворачиваем на 90 градусов по оси X
            }
        }
    }

    // Добавление предмета в инвентарь
    public void AddItem(GameObject itemPrefab)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) // Если слот пустой
            {
                // Создаём новый экземпляр предмета из префаба
                GameObject newItem = Instantiate(itemPrefab, hand.position, hand.rotation);
                newItem.SetActive(false); // Скрываем предмет до его отображения
                items[i] = newItem; // Добавляем предмет в инвентарь
                Debug.Log($"Предмет {newItem.name} добавлен в слот {i}.");

                // Если инвентарь был пуст, показываем добавленный предмет
                if (currentItemIndex == -1)
                {
                    currentItemIndex = i;
                    ShowCurrentItem();
                }
                return;
            }
        }

        Debug.Log("Инвентарь полон!");
    }

    // Переключение на следующий предмет
    public void NextItem()
    {
        if (items.Length == 0 || currentItemIndex == -1) return; // Если инвентарь пуст

        // Скрываем текущий предмет (если он существует)
        if (items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(false);
        }

        // Ищем следующий предмет
        for (int i = 1; i <= items.Length; i++)
        {
            int nextIndex = (currentItemIndex + i) % items.Length; // Переход к следующему слоту
            if (items[nextIndex] != null) // Если слот не пустой
            {
                currentItemIndex = nextIndex;
                ShowCurrentItem(); // Показываем новый предмет
                return;
            }
        }

        // Если инвентарь пуст
        currentItemIndex = -1;
        Debug.Log("Инвентарь пуст.");
    }

    // Переключение на предыдущий предмет
    public void PreviousItem()
    {
        if (items.Length == 0 || currentItemIndex == -1) return; // Если инвентарь пуст

        // Скрываем текущий предмет (если он существует)
        if (items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(false);
        }

        // Ищем предыдущий предмет
        for (int i = 1; i <= items.Length; i++)
        {
            int prevIndex = (currentItemIndex - i + items.Length) % items.Length; // Переход к предыдущему слоту
            if (items[prevIndex] != null) // Если слот не пустой
            {
                currentItemIndex = prevIndex;
                ShowCurrentItem(); // Показываем новый предмет
                return;
            }
        }

        // Если инвентарь пуст
        currentItemIndex = -1;
        Debug.Log("Инвентарь пуст.");
    }

    // Показ текущего предмета
    private void ShowCurrentItem()
    {
        if (currentItemIndex != -1 && items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(true); // Показываем предмет
            items[currentItemIndex].transform.position = hand.position; // Перемещаем в точку hand
            items[currentItemIndex].transform.rotation = hand.rotation; // Поворачиваем как hand

            // Если предмет должен быть горизонтальным, применяем дополнительный поворот
            ItemData itemData = items[currentItemIndex].GetComponent<ItemData>();
            if (itemData != null && itemData.isHorizontal)
            {
                items[currentItemIndex].transform.Rotate(90, 0, 0); // Поворачиваем на 90 градусов по оси X
            }

            Debug.Log($"Текущий предмет: {items[currentItemIndex].name}");
        }
    }

    // Удаление текущего предмета
    public void RemoveCurrentItem()
    {
        if (currentItemIndex != -1 && items[currentItemIndex] != null)
        {
            Debug.Log($"Предмет {items[currentItemIndex].name} удалён.");
            Destroy(items[currentItemIndex]); // Уничтожаем предмет
            items[currentItemIndex] = null; // Очищаем слот
            NextItem(); // Переключаемся на следующий предмет
        }
    }
}