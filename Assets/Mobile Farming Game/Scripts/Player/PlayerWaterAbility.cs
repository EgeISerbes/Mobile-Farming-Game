using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerWaterAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;
    [Header(" Settings ")]
    private CropField _currentCropField;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();
        WaterParticles.onWaterCollided += WaterCollidedCallback;

        CropField.OnFullyWatered += CropFieldFullyWateredCallback;
        PlayerToolSelector.onToolSelected += ToolSelectedCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        WaterParticles.onWaterCollided -= WaterCollidedCallback;
        CropField.OnFullyWatered -= CropFieldFullyWateredCallback;
        PlayerToolSelector.onToolSelected -= ToolSelectedCallback;

    }
    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanWater()) _playerAnimator.StopWaterAnimation();

    }
    private void CropFieldFullyWateredCallback(CropField cropField)
    {
        if (cropField != _currentCropField) return;
        _playerAnimator.StopWaterAnimation();

    }
    private void WaterCollidedCallback(Vector3[] waterPositions)
    {
        if (_currentCropField == null) return;

        _currentCropField.WaterCollidedCallback(waterPositions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(other);
        }
    }
    void EnteredCropField(Collider collider)
    {
        if (_playerToolSelector.CanWater())
        {
            _playerAnimator.PlayWaterAnimation();
            if (_currentCropField == null)
            {
                _currentCropField = collider.GetComponent<CropField>();

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            EnteredCropField(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CropField"))
        {
            _playerAnimator.StopWaterAnimation();
            _currentCropField = null;
        }
    }
}
