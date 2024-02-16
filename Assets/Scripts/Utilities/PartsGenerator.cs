using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Illustration_Part_Demo;
using UnityEngine;
using UnityEngine.Serialization;

public class PartsGenerator : MonoBehaviour
{
    private IllustrationData Data => IllustrationCore.Instance.Data;
    [SerializeField] private IllustrationPart _partPrefab;
    [SerializeField] private bool _gridifyPosition;

    private void Clear()
    {
        if (transform.childCount == 0) return;
        
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
    }

    public void Generate()
    {
        Clear();

        CheckData();
        
        var sizeX = Data.sizeX;
        var sizeY = Data.sizeY;

        for (int i = 1; i <= sizeX; i++)
        {
            for (int j = 1; j <= sizeY; j++)
            {
                var part = Instantiate(_partPrefab, transform) as IllustrationPart;
                
                part.SetData(Data.partDates.FirstOrDefault(x => x.x.Equals(j) && x.y.Equals(i)));
                if (_gridifyPosition) SetPosition(part, j, i);
            }
        }
    }

    private void SetPosition(IllustrationPart part, int x, int y)
    {
        var halfSizeX = Data.sizeX / 2.0f;
        var halfSizeY = Data.sizeY / 2.0f;
        var partSize = Data.partSize;
        var offset = Data.partsSpace;

        var relative = new Vector2((x - 1) * partSize, -(y - 1) * partSize) + new Vector2((x - 1) * offset, -(y - 1) * offset);
        var centerOffset = new Vector2(-(halfSizeX) * partSize + -(halfSizeX - 1) * offset, (halfSizeY) * partSize + (halfSizeY - 1) * offset);
        var oneBlockOffset = new Vector2(partSize * 0.5f + (-offset), -partSize * 0.5f + (offset));

        (part.transform as RectTransform).anchoredPosition = relative + centerOffset + oneBlockOffset;
    }

    private void CheckData()
    {
        if (!Data.IsValid()) throw new Exception("Data has wrong configuration!");
    }
}
