using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Illustration_Part_Demo;
using UnityEngine;
using UnityEngine.UI;

public class IllustrationPart : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private int x;
    [SerializeField] private int y;

    public int X => x;
    public int Y => y;
    
    public void SetData(IllustrationData.IllustrationPartData data)
    {
        _image.sprite = data.image;

        x = data.x;
        y = data.y;
    }

    public bool CompareIndicesTo(IllustrationPart part)
    {
        return x.Equals(part.x) && y.Equals(part.y);
    }
}
