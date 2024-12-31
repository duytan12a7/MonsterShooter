using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed = 20f;
    private Vector2 moveInput;

    private void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
    }

    private void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    private void Update()
    {
        characterController.Move(new Vector3(moveInput.x, 0f, moveInput.y) * Time.deltaTime * moveSpeed);
    }
}
