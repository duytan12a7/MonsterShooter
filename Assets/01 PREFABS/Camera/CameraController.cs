using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTrans;
    [SerializeField] private float turnSpeed = 50f;

    private void Start()
    {
        LoadFollowTrans();
    }

    private void LoadFollowTrans()
    {
        if (followTrans != null) return;

        followTrans = GameObject.FindWithTag("Player").transform;
        Debug.Log(transform.name + " : LoadFollowTrans", gameObject);
    }

    private void LateUpdate()
    {
        transform.position = followTrans.position;
    }

    public void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
    }
}
