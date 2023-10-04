using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerColor : MonoBehaviour
{
    public int maxItemsPerRow = 3; // Her s�radaki maksimum ��e say�s�

    private void Start()
    {
        UpdateLayout();
    }

    private void UpdateLayout()
    {
        int childCount = transform.childCount;

        // Maksimum ��e say�s�na ula��ld���nda yeni bir sat�r ekleyin
        for (int i = 0; i < childCount; i += maxItemsPerRow)
        {
            int endIndex = Mathf.Min(i + maxItemsPerRow, childCount);
            SetChildrenToHorizontalLayout(i, endIndex);
        }
    }

    private void SetChildrenToHorizontalLayout(int startIndex, int endIndex)
    {
        HorizontalLayoutGroup horizontalLayout = gameObject.GetComponent<HorizontalLayoutGroup>();
        if (horizontalLayout == null)
        {
            horizontalLayout = gameObject.AddComponent<HorizontalLayoutGroup>();
            horizontalLayout.childControlWidth = true;
            horizontalLayout.childForceExpandWidth = true;
        }

        for (int i = startIndex; i < endIndex; i++)
        {
            Transform child = transform.GetChild(i);
            child.SetParent(transform.parent);
            child.SetParent(transform);
        }
    }
}
