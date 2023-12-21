using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    [Header(" Settings ")]
    private int _coins;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
        UpdateCoinContainers();


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateCoinContainers()
    {
        GameObject[] coinContainers = GameObject.FindGameObjectsWithTag("CoinAmount");

        foreach (GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = _coins.ToString();
        }
    }
    public void AddCoins(int amount)
    {
        _coins += amount;
        UpdateCoinContainers();
        Debug.Log("We now have " + _coins + " coins");
        SaveData();
    }
    private void LoadData()
    {
        _coins = PlayerPrefs.GetInt("Coins");
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }
}
