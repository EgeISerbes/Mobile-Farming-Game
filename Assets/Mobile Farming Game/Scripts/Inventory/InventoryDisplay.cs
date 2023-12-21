using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _uiCropContainerParentTR;
    [SerializeField] private UICropContainer _uiCropContainerPrefab;

    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();
        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer cropContainer = Instantiate(_uiCropContainerPrefab, _uiCropContainerParentTR);
            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);

            cropContainer.Configure(cropIcon, items[i].amount);
        }
    }
    //public void UpdateDisplay(Inventory inventory)
    //{
    //    InventoryItem[] items = inventory.GetInventoryItems();

    //    while(_uiCropContainerParentTR.childCount>0)
    //    {
    //        Transform container = _uiCropContainerParentTR.GetChild(0);
    //        container.SetParent(null);
    //        Destroy(container.gameObject);
    //    }

    //    Configure(inventory);

    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        UICropContainer cropContainer = Instantiate(_uiCropContainerPrefab, _uiCropContainerParentTR);
    //        Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);

    //        cropContainer.Configure(cropIcon, items[i].amount);
    //    }
    //}

    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();
        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer containerInstance;
            if (i < _uiCropContainerParentTR.childCount)
            {
                containerInstance = _uiCropContainerParentTR.GetChild(i).GetComponent<UICropContainer>();
                containerInstance.gameObject.SetActive(true);

            }
            else
            {
                containerInstance = Instantiate(_uiCropContainerPrefab, _uiCropContainerParentTR);

            }
            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configure(cropIcon, items[i].amount);
        }

        int remainingContainers = _uiCropContainerParentTR.childCount - items.Length;
        if (remainingContainers <= 0) return;
        else
        {
            for (int i = 0; i < remainingContainers; i++)
            {
                _uiCropContainerParentTR.GetChild(items.Length + i).gameObject.SetActive(false);
            }
        }
    }
}
