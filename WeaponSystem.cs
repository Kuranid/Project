using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour
{
    [Header("������")]
    public Texture2D crosshairTexture; // �������� �������
    public Vector2 crosshairSize = new Vector2(20, 20); // ������ �������
    public Color crosshairColor = Color.white; // ���� �������

    [Header("���")]
    public float raycastRange = 100f; // ��������� ����
    public LayerMask targetLayer; // ����, �� ������� ����� �������������� �������

    [Header("������")]
    public GameObject blackSpotPrefab; // ������ ������� �����
    public float spotFadeDuration = 30f; // ����� ������������ ����� (� ��������)
    public int maxSpots = 30; // ������������ ���������� �����

    private Queue<GameObject> spotsQueue = new Queue<GameObject>(); // ������� ��� �������� �����

    void OnGUI()
    {
        // ��������� ������� � ������ ������
        if (crosshairTexture != null)
        {
            Vector2 center = new Vector2(Screen.width / 2 - crosshairSize.x / 2, Screen.height / 2 - crosshairSize.y / 2);
            GUI.color = crosshairColor;
            GUI.DrawTexture(new Rect(center, crosshairSize), crosshairTexture);
        }
    }

    void Update()
    {
        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButtonDown(0)) // 0 = ���
        {
            // ���������, ��������� �� ������
            if (Camera.main == null)
            {
                Debug.LogError("������ �� �������! �������, ��� �� ����� ���� ������ � ����� MainCamera.");
                return;
            }

            // ������ ��� �� ������ ������
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // ���������, ����� �� ��� � ������
            if (Physics.Raycast(ray, out hit, raycastRange, targetLayer))
            {
                Debug.Log("��������� ������: " + hit.collider.name);

                // ���������, �������� �� ������
                if (blackSpotPrefab == null)
                {
                    Debug.LogError("������ ������� ����� �� ��������!");
                    return;
                }

                // ���� ���������� ������������ ���������� �����, ������� ����� ������
                if (spotsQueue.Count >= maxSpots)
                {
                    GameObject oldestSpot = spotsQueue.Dequeue(); // ������� ����� ������ ����� �� �������
                    if (oldestSpot != null)
                    {
                        Destroy(oldestSpot); // ���������� ���
                    }
                }

                // ������ ����� ����� �� �������
                GameObject newSpot = Instantiate(blackSpotPrefab, hit.point, Quaternion.identity);
                newSpot.transform.parent = hit.transform; // ����������� ����� � �������

                // ��������� ����� ����� � �������
                spotsQueue.Enqueue(newSpot);

                // ��������� �������� ��� �������� ����� ����� ��������� �����
                StartCoroutine(RemoveSpot(newSpot, spotFadeDuration));
            }
        }
    }

    // �������� ��� �������� ����� ����� ��������� �����
    private System.Collections.IEnumerator RemoveSpot(GameObject spot, float delay)
    {
        yield return new WaitForSeconds(delay); // ��� ��������� �����
        if (spot != null)
        {
            Destroy(spot); // ������� �����
        }
    }
}