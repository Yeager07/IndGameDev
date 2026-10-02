using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dice : MonoBehaviour
{
    [SerializeField] private DiceInputs diceInputs;
    [SerializeField] private int dropForce;
    [SerializeField] private int torqueForce;
    [SerializeField] private Transform normalsChecker;
    [SerializeField] private Vector3[] direction = new Vector3[7];


    private Rigidbody rb;
    private Dictionary<string, int> score = new Dictionary<string, int>()
    {
        { "Left", 1 },
        { "Down", 5 },
        { "Right", 6 },
        { "Back",  4 },
        { "Up", 2 },
        { "Front", 3 }
    };
    private bool isCollinear;
    private string previousCollinearVector = "Up";
    public int currentScore = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        dropForce = UnityEngine.Random.Range(3, 8);
        torqueForce = UnityEngine.Random.Range(-20, 20);
        
        diceInputs.dropEvent.AddListener(OnDrop);
        diceInputs.rotateEvent.AddListener(OnRotate);
    }

    /*private void Update()
    {
        OnDrop();
    }*/

    private void FixedUpdate()
    {
        if(CheckNormal() == previousCollinearVector)
        currentScore = score[previousCollinearVector];
        else
        previousCollinearVector = CheckNormal();
    }

    private void OnDrop()
    {
        rb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
        rb.AddForce(direction[UnityEngine.Random.Range(0, 2)] * dropForce, ForceMode.Impulse);
        dropForce = UnityEngine.Random.Range(3, 5);
        return;
    }

    private void OnRotate()
    {
        rb.AddTorque(direction[UnityEngine.Random.Range(0, 2)] * torqueForce);
        torqueForce = UnityEngine.Random.Range(-20, 20);
    }

    private string CheckNormal()
    {
        float dotProduct;
        string collinearVector = "Up";

        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            dotProduct = Vector3.Dot(child.up, normalsChecker.up);
            
            if(dotProduct > 0.9)
            collinearVector = child.name;
        }

        return collinearVector;
    }
}
