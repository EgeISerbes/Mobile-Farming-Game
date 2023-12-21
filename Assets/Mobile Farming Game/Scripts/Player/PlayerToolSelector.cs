using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerToolSelector : MonoBehaviour
{
    public enum Tool
    {
        None, Sow, Water, Harvest
    }
    private Tool _activeTool;

    [Header(" Elements ")]
    [SerializeField] private Image[] _toolImages;

    [Header(" Settings ")]
    [SerializeField] private Color _selectedToolColor;

    [Header(" Actions ")]
    public static Action<Tool> onToolSelected;
    // Start is called before the first frame update
    void Start()
    {
        SelectTool(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectTool(int toolIndex)
    {
        _activeTool = (Tool)toolIndex;
        Debug.Log("Active Tool :" + _activeTool);

        for (int i = 0; i < _toolImages.Length; i++)
        {
            _toolImages[i].color = i == toolIndex ? _selectedToolColor : Color.white;
        }
        onToolSelected?.Invoke(_activeTool);
    }
    public bool CanSow()
    {
        return _activeTool == Tool.Sow;
    }
    public bool CanWater()
    {
        return _activeTool == Tool.Water;
    }
    public bool CanHarvest()
    {
        return _activeTool == Tool.Harvest;
    }
}
