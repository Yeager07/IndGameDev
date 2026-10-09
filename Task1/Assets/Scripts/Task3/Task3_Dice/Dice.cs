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
    [SerializeField] private Vector3[] direction = new Vector3[7];

    private Rigidbody rb;
    private Dictionary<string, int> score = new Dictionary<string, int>()
    {
        { "Left", 6 },
        { "Down", 2 },
        { "Right", 1 },
        { "Back",  3 },
        { "Up", 5 },
        { "Front", 4 }
    };
    public int currentScore = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        dropForce = UnityEngine.Random.Range(3, 8);
        torqueForce = UnityEngine.Random.Range(-20, 20);
        
        diceInputs.dropEvent.AddListener(OnDrop);
        diceInputs.rotateEvent.AddListener(OnRotate);
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

    public void CheckNormal(Transform side)
    {
        currentScore = score[side.name];
        transform.parent.gameObject.GetComponent<CountScore>().CountAllScore();
    }
}
