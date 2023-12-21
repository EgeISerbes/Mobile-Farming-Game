using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerHarvestAbility : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _harvestSphere;
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;
    [Header(" Settings ")]
    private CropField _currentCropField;
    private bool _canHarvest;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();
        //WaterParticles.onWaterCollided += WaterCollidedCallback;

        CropField.OnFullyHarvested += CropFieldFullyHarvestedCallback;
        PlayerToolSelector.onToolSelected += ToolSelectedCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        //WaterParticles.onWaterCollided -= WaterCollidedCallback;
        CropField.OnFullyHarvested -= CropFieldFullyHarvestedCallback;
        PlayerToolSelector.onToolSelected -= ToolSelectedCallback;

    }
    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanHarvest()) _playerAnimator.StopHarvestAnimation();

    }
    private void CropFieldFullyHarvestedCallback(CropField cropField)
    {
        if (cropField != _currentCropField) return;
        _playerAnimator.StopHarvestAnimation();

    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(other);
        }
    }
    void EnteredCropField(Collider collider)
    {
        if (_playerToolSelector.CanHarvest())
        {
            if (_currentCropField == null)
            {
                _currentCropField = collider.GetComponent<CropField>();

            }
            _playerAnimator.PlayHarvestAnimation();
            if(_canHarvest)
            {
                _currentCropField.Harvest(_harvestSphere);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            EnteredCropField(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CropField"))
        {
            _playerAnimator.StopHarvestAnimation();
            _currentCropField = null;
        }
    }


    public void HarvestingStartedCallback()
    {
        _canHarvest = true;
    }
    public void HarvestingStopCallback()
    {
        _canHarvest = false;

    }
}
