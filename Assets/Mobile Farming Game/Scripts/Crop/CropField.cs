using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CropField : MonoBehaviour
{
    [Header(" Elements ")]
    private List<CropTile> _cropTiles = new List<CropTile>();
    [SerializeField] private Transform _tilesParentTR;
    // Start is called before the first frame update
    [Header(" Settings ")]
    [SerializeField] private CropData _cropData;
    private int _tilesSown = 0;
    private int _tilesWatered = 0;
    private int _tilesHarvested = 0;
    private TileFieldState _fieldState = TileFieldState.Empty;

    [Header(" Actions ")]
    public static Action<CropField> OnFullySown;
    public static Action<CropField> OnFullyWatered;
    public static Action<CropField> OnFullyHarvested;
    void Start()
    {
        StoreTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InstantlySowTiles();
        }
    }
    private void StoreTiles()
    {
        for (int i = 0; i < _tilesParentTR.childCount; i++)
        {
            _cropTiles.Add(_tilesParentTR.GetChild(i).GetComponent<CropTile>());
        }
    }
    public void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);
            if (closestCropTile == null) continue;

            if (closestCropTile.IsEmpty()) Sow(closestCropTile);
        }
    }
    public void WaterCollidedCallback(Vector3[] waterPositions)
    {
        for (int i = 0; i < waterPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(waterPositions[i]);
            if (closestCropTile == null) continue;

            if (closestCropTile.IsSown()) Water(closestCropTile);
        }
    }

    private void Sow(CropTile cropTile)
    {   
        cropTile.Sow(_cropData);
        _tilesSown++;
        if(_tilesSown >= _cropTiles.Count)
        {
            FieldFullySown();
        }
    }
    private  void Water(CropTile cropTile)
    {
        cropTile.Water();
        _tilesWatered++;
        if (_tilesWatered >= _cropTiles.Count)
        {
            FieldFullyWatered();
        }
    }
    public void Harvest(Transform harvestSphere)
    {
        float sphereRadius = harvestSphere.localScale.x;
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            if(_cropTiles[i].IsEmpty())
            {
                continue;
            }
            float distanceCropTileSphere = Vector3.Distance(harvestSphere.position, _cropTiles[i].transform.position);
            if(distanceCropTileSphere <= sphereRadius)
            {
                HarvestTile(_cropTiles[i]);
            }
        }
    }
    private void HarvestTile(CropTile cropTile)
    {
        cropTile.Harvest();
        _tilesHarvested++;
        if(_tilesHarvested>= _cropTiles.Count)
        {
            FieldFullyHarvested();
        }
    }
    private void FieldFullyHarvested()
    {
        _tilesSown = 0;
        _tilesWatered = 0;
        _tilesHarvested = 0;

        _fieldState = TileFieldState.Empty;
        OnFullyHarvested?.Invoke(this);
    }
    private void FieldFullySown()
    {
        Debug.Log("Field fully Sown");
        _fieldState = TileFieldState.Sown;
        OnFullySown?.Invoke(this);
    }
    private void FieldFullyWatered()
    {
        Debug.Log("Field fully Watered");
        _fieldState = TileFieldState.Watered;
        OnFullyWatered?.Invoke(this);
    }
    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = 9999;
        int closestCropTileIndex = -1;

        for (int i = 0; i < _cropTiles.Count; i++)
        {
            CropTile cropTile = _cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }

        }
        if (closestCropTileIndex == -1)
        {
            return null; ;
        }
        return _cropTiles[closestCropTileIndex];
    }

    [NaughtyAttributes.Button]
    private void InstantlySowTiles()
    {
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Sow(_cropTiles[i]);
        }
    }
    [NaughtyAttributes.Button]
    private void InstantlyWaterTiles()
    {
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Water(_cropTiles[i]);
        }
    }
    
    public bool IsEmpty()
    {
        return _fieldState == TileFieldState.Empty;
    }
    public bool IsWatered()
    {
        return _fieldState == TileFieldState.Watered;
    }
    public bool IsSown()
    {
        return _fieldState == TileFieldState.Sown;
    }
}
