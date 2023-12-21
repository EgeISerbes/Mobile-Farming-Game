using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyerInteractor : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private InventoryManager _inventoryManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buyer"))
        {
            SellCrops();
        }
    }

    
    private void SellCrops()
    {
        Inventory inventory = _inventoryManager.GetInventory();
        InventoryItem[] inventoryItems = inventory.GetInventoryItems();

        int coinsEarned = 0;

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            int price = DataManager.instance.GetCropPriceFromCropType(inventoryItems[i].cropType);
            coinsEarned +=  price * inventoryItems[i].amount;
        }
        CashManager.instance.AddCoins(coinsEarned);
        _inventoryManager.ClearInventory();
    }
}
