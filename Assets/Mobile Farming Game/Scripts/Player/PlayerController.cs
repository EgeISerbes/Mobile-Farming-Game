using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("   Elements    ")]
    [SerializeField] private MobileJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();

    }
    private void ManageMovement()
    {
        Vector3 moveVector = _joystick.GetMoveVector() * _moveSpeed * Time.deltaTime / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        _characterController.Move(moveVector);

        _playerAnimator.ManageAnimations(moveVector);
    }
}
