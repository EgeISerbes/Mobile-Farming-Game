using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICropContainer : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(Sprite icon , int amount)
    {
        _iconImage.sprite = icon;
        _amountText.text = amount.ToString();
    }
    public void UpdateDisplay(int amount)
    {
        _amountText.text = amount.ToString();
    }
}
