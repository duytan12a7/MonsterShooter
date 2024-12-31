using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float turnSpeed = 30f;
    private Vector2 moveInput;
    private Camera mainCamera;

    private void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
        mainCamera = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    private void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    private void Update()
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 moveDir = moveInput.x * rightDir + moveInput.y * upDir;
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        if (moveInput.magnitude != 0)
        {
            float turnLerp = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), turnLerp);

            cameraController?.AddYawInput(moveInput.x);
        }

    }
}
