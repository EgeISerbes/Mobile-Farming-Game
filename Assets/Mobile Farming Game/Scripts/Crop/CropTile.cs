using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
    

public class CropTile : MonoBehaviour
{   
    private TileFieldState _cropTileState;

    [Header(" Elements ")]
    [SerializeField] private Transform _cropParent;
    [SerializeField] private MeshRenderer _tileRenderer;
    private Crop _crop;
    private CropData _cropData;

    [Header(" Event ")]
    public static Action<CropType> onCropHarvested;
    // Start is called before the first frame update
    void Start()
    {
        _cropTileState = TileFieldState.Empty;
    }

    public void Sow(CropData cropData)
    {
        _cropTileState = TileFieldState.Sown;

        _crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, _cropParent);

        this._cropData = cropData;
    }
    public void Water()
    {
        _cropTileState = TileFieldState.Watered;
        
        _crop.ScaleUp();

        _tileRenderer.gameObject.LeanColor(Color.white * .3f, 1);
        
    }
    public void Harvest()
    {
        _cropTileState = TileFieldState.Empty;
        _crop.ScaleDown();
        _tileRenderer.gameObject.LeanColor(Color.white, 1);
        onCropHarvested?.Invoke(_cropData.cropType);
    }
    IEnumerator ColorTileCoroutine()
    {
        float duration = 1f;
        float timer = 0f;
         
        while(timer<duration)
        {
            float t = timer / duration;
            _tileRenderer.material.color = Color.Lerp(Color.white, Color.white * .3f, t);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsEmpty()
    {
        return _cropTileState == TileFieldState.Empty;
    }
    public bool IsSown()
    {
        return _cropTileState == TileFieldState.Sown;
    }
}
