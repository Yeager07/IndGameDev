using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Inputs inputs;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float mouseSens;

    private Rigidbody rb;
    private Camera mainCamera;

    private float xRotation = 0;
    private float yRotation = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        inputs.shootEvent.AddListener(OnShoot);
        inputs.jumpEvent.AddListener(OnJump);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnLook();
    }

    private void OnMove()
    {
        rb.AddRelativeForce(new Vector3(inputs.move.x, 0, inputs.move.y) * speed);
    }

    private void OnLook()
    {
        xRotation -= inputs.look.y;
        xRotation = Mathf.Clamp(xRotation, -60.0f, 30.0f);

        yRotation += inputs.look.x;

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }

    private void OnShoot()
    {
        print("Выстрел");
    }

    private void OnJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
