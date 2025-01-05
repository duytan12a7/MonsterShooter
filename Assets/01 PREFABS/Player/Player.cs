using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private JoyStick moveStick;
    [SerializeField] private JoyStick aimStick;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraController cameraController;
    private Camera mainCamera;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float turnSpeed = 30f;
    private Vector2 moveInput;
    private Vector2 aimInput;

    private void Start()
    {
        LoadComponent();

        moveStick.onStickValueUpdated += MoveStickUpdated;
        aimStick.onStickValueUpdated += AimStickUpdated;
    }

    private void LoadComponent()
    {
        mainCamera = Camera.main;
        LoadCharacterController();
        LoadCameraController();
    }

    private void AimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
    }

    private void MoveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    private void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDir = StickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

        Vector3 aimDir = aimInput.sqrMagnitude > 0 ? StickInputToWorldDir(aimInput) : moveDir;

        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        // player is move but not aiming and cameraController exists
        if (moveInput.sqrMagnitude != 0 && aimInput.sqrMagnitude == 0 && cameraController != null)
            cameraController.AddYawInput(moveInput.x);
    }

    private void RotateTowards(Vector3 aimDir)
    {
        if (aimDir.sqrMagnitude != 0)
        {
            float turnLerp = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerp);
        }
    }

    private Vector3 StickInputToWorldDir(Vector2 inputValue)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return inputValue.x * rightDir + inputValue.y * upDir;
    }

    private void LoadCharacterController()
    {
        if (characterController != null) return;
        characterController = GetComponent<CharacterController>();
        Debug.Log(transform.name + ": LoadCharacterController", gameObject);
    }

    private void LoadCameraController()
    {
        if (cameraController != null) return;
        cameraController = FindObjectOfType<CameraController>();
        Debug.Log(transform.name + ": LoadCameraController", gameObject);
    }
}
