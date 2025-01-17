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
    [SerializeField] private Animator animator;
    [SerializeField] private InventoryComponent inventoryComponent;
    private Camera mainCamera;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 8f;
    [SerializeField] private float animTurnSpeed = 15f;
    private Vector2 moveInput;
    private Vector2 aimInput;

    private float animatorTurnSpeed;

    private void Start()
    {
        LoadComponent();

        moveStick.onStickValueUpdated += MoveStickUpdated;
        aimStick.onStickValueUpdated += AimStickUpdated;
        aimStick.onStickTaped += StartSwitchWeapon;
    }

    private void LoadComponent()
    {
        mainCamera = Camera.main;
        LoadCharacterController();
        LoadCameraController();
        LoadAnimator();
        LoadInventoryComponent();
    }

    private void AimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;

        if (aimInput.magnitude > 0)
            animator.SetBool("attacking", true);
        else
            animator.SetBool("attacking", false);

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

        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);
    }

    private void UpdateCamera()
    {
        if (moveInput.sqrMagnitude != 0 && aimInput.sqrMagnitude == 0 && cameraController != null)
            cameraController.AddYawInput(moveInput.x);
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0;

        if (aimDir.sqrMagnitude != 0)
        {
            Quaternion prevRot = transform.rotation;

            float turnLerp = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerp);

            Quaternion currentRot = transform.rotation;
            float dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotDelta = Quaternion.Angle(prevRot, currentRot) * dir;
            currentTurnSpeed = rotDelta / Time.deltaTime;
        }

        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    private Vector3 StickInputToWorldDir(Vector2 inputValue)
    {
        Vector3 rightDir = mainCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return inputValue.x * rightDir + inputValue.y * upDir;
    }

    private void StartSwitchWeapon()
    {
        animator.SetTrigger("switchWeapon");
    }

    private void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    #region Components Link

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

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    private void LoadInventoryComponent()
    {
        if (inventoryComponent != null) return;
        inventoryComponent = GetComponent<InventoryComponent>();
        Debug.Log(transform.name + ": LoadInventoryComponent", gameObject);
    }

    #endregion
}
