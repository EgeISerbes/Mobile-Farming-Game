using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerSowAbility : MonoBehaviour
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
        SeedParticles.onSeedsCollided += SeedCollidedCallback;

        CropField.OnFullySown += CropFieldFullySownCallback;
        PlayerToolSelector.onToolSelected += ToolSelectedCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        SeedParticles.onSeedsCollided -= SeedCollidedCallback;
        CropField.OnFullySown -= CropFieldFullySownCallback;
        PlayerToolSelector.onToolSelected -= ToolSelectedCallback;

    }
    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanSow())
            {
            _playerAnimator.StopSowAnimation();
        }
        
    }
    private void CropFieldFullySownCallback(CropField cropField)
    {
        if (cropField != _currentCropField) return;
        _playerAnimator.StopSowAnimation();

    }
    private void SeedCollidedCallback(Vector3[] seedPositions)
    {
        if (_currentCropField == null) return;

        _currentCropField.SeedsCollidedCallback(seedPositions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            EnteredCropField(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            EnteredCropField(other);
        }
    }
    void EnteredCropField(Collider collider)
    {
        if (_playerToolSelector.CanSow())
        {
            _playerAnimator.PlaySowAnimation();
            _currentCropField = collider.GetComponent<CropField>();
        }
    }
  
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CropField"))
        {
            _playerAnimator.StopSowAnimation();
            _currentCropField = null;
        }
    }
}
