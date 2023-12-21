using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [Header(" Data ")]
    [SerializeField] private CropData[] _cropDatas;

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sprite GetCropSpriteFromCropType(CropType cropType)
    {
        for (int i = 0; i < _cropDatas.Length; i++)
        {
            if (_cropDatas[i].cropType == cropType)
            {
                return _cropDatas[i].icon;
            }
        }
        Debug.LogError("No Crop Data found of that type");
        return null;
    }

    public int GetCropPriceFromCropType(CropType cropType)
    {
        for (int i = 0; i < _cropDatas.Length; i++)
        {
            if (_cropDatas[i].cropType == cropType)
            {
                return _cropDatas[i].price;
            }
        }
        Debug.LogError("No Crop Data found of that type");
        return 0;
    }
}
