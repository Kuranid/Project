using UnityEngine;

public class SimpleInventory : MonoBehaviour
{
    [Header("��������� ���������")]
    public GameObject[] items = new GameObject[4]; // ������ ���������
    public Transform hand; // �����, ��� ����� ���������� ��������

    private int currentItemIndex = -1; // ������ �������� �������� (-1 ��������, ��� ��������� ����)

    void Update()
    {
        // ������������ ����� ���������� � ������� ������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // �������� �������� ���������
        if (scroll < 0) // ��������� ����
        {
            NextItem();
        }
        else if (scroll > 0) // ��������� �����
        {
            PreviousItem(); // ������������ �� ���������� �������
        }

        // ��������� ������� � ������� �������� ��������
        if (currentItemIndex != -1 && items[currentItemIndex] != null && items[currentItemIndex].activeSelf)
        {
            items[currentItemIndex].transform.position = hand.position;
            items[currentItemIndex].transform.rotation = hand.rotation;

            // ���� ������� ������ ���� ��������������, ��������� �������������� �������
            ItemData itemData = items[currentItemIndex].GetComponent<ItemData>();
            if (itemData != null && itemData.isHorizontal)
            {
                items[currentItemIndex].transform.Rotate(90, 0, 0); // ������������ �� 90 �������� �� ��� X
            }
        }
    }

    // ���������� �������� � ���������
    public void AddItem(GameObject itemPrefab)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) // ���� ���� ������
            {
                // ������ ����� ��������� �������� �� �������
                GameObject newItem = Instantiate(itemPrefab, hand.position, hand.rotation);
                newItem.SetActive(false); // �������� ������� �� ��� �����������
                items[i] = newItem; // ��������� ������� � ���������
                Debug.Log($"������� {newItem.name} �������� � ���� {i}.");

                // ���� ��������� ��� ����, ���������� ����������� �������
                if (currentItemIndex == -1)
                {
                    currentItemIndex = i;
                    ShowCurrentItem();
                }
                return;
            }
        }

        Debug.Log("��������� �����!");
    }

    // ������������ �� ��������� �������
    public void NextItem()
    {
        if (items.Length == 0 || currentItemIndex == -1) return; // ���� ��������� ����

        // �������� ������� ������� (���� �� ����������)
        if (items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(false);
        }

        // ���� ��������� �������
        for (int i = 1; i <= items.Length; i++)
        {
            int nextIndex = (currentItemIndex + i) % items.Length; // ������� � ���������� �����
            if (items[nextIndex] != null) // ���� ���� �� ������
            {
                currentItemIndex = nextIndex;
                ShowCurrentItem(); // ���������� ����� �������
                return;
            }
        }

        // ���� ��������� ����
        currentItemIndex = -1;
        Debug.Log("��������� ����.");
    }

    // ������������ �� ���������� �������
    public void PreviousItem()
    {
        if (items.Length == 0 || currentItemIndex == -1) return; // ���� ��������� ����

        // �������� ������� ������� (���� �� ����������)
        if (items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(false);
        }

        // ���� ���������� �������
        for (int i = 1; i <= items.Length; i++)
        {
            int prevIndex = (currentItemIndex - i + items.Length) % items.Length; // ������� � ����������� �����
            if (items[prevIndex] != null) // ���� ���� �� ������
            {
                currentItemIndex = prevIndex;
                ShowCurrentItem(); // ���������� ����� �������
                return;
            }
        }

        // ���� ��������� ����
        currentItemIndex = -1;
        Debug.Log("��������� ����.");
    }

    // ����� �������� ��������
    private void ShowCurrentItem()
    {
        if (currentItemIndex != -1 && items[currentItemIndex] != null)
        {
            items[currentItemIndex].SetActive(true); // ���������� �������
            items[currentItemIndex].transform.position = hand.position; // ���������� � ����� hand
            items[currentItemIndex].transform.rotation = hand.rotation; // ������������ ��� hand

            // ���� ������� ������ ���� ��������������, ��������� �������������� �������
            ItemData itemData = items[currentItemIndex].GetComponent<ItemData>();
            if (itemData != null && itemData.isHorizontal)
            {
                items[currentItemIndex].transform.Rotate(90, 0, 0); // ������������ �� 90 �������� �� ��� X
            }

            Debug.Log($"������� �������: {items[currentItemIndex].name}");
        }
    }

    // �������� �������� ��������
    public void RemoveCurrentItem()
    {
        if (currentItemIndex != -1 && items[currentItemIndex] != null)
        {
            Debug.Log($"������� {items[currentItemIndex].name} �����.");
            Destroy(items[currentItemIndex]); // ���������� �������
            items[currentItemIndex] = null; // ������� ����
            NextItem(); // ������������� �� ��������� �������
        }
    }
}