using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chunk : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject _unlockedElements;
    [SerializeField] private GameObject _lockedElements;
    [SerializeField] private TextMeshPro _priceText;

    [Header(" Settings ")]
    [SerializeField] private int _initialPrice;
    // Start is called before the first frame update
    void Start()
    {
        _priceText.text = _initialPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryUnlock()
    {
        
    }
}
