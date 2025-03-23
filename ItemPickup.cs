using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public SimpleInventory inventory; // ������ �� ���������
    public float pickupDistance = 100f; // ��������� ���� ��� ������� ��������

    void Update()
    {
        // ���������, ������ �� ������� E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ������ ��� �� ������ ������
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // ���������, ����� �� ��� � �������
            if (Physics.Raycast(ray, out hit, pickupDistance))
            {
                if (hit.collider.CompareTag("Item")) // ���� ��� ����� � ������ � ����� "Item"
                {
                    // �������� ��������� ItemData
                    ItemData itemData = hit.collider.GetComponent<ItemData>();
                    if (itemData != null && itemData.itemPrefab != null) // ���������, ��� ��������� � ������ ����������
                    {
                        // ��������� ������ � ���������
                        inventory.AddItem(itemData.itemPrefab);
                        Destroy(hit.collider.gameObject); // ���������� ������� �� �����
                        Debug.Log($"������� {hit.collider.name} ��������.");
                    }
                    else
                    {
                        Debug.LogError("� �������� ����������� ��������� ItemData ��� ������ �� ��������.");
                    }
                }
            }
        }
    }
}