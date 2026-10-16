using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dice : MonoBehaviour
{
    public event Action<Dice> OnStopped;
    
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

    private DiceInputs diceInputs;
    private GameManager gameManager;

    public int currentScore = 0;
    public bool isRolling = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        dropForce = UnityEngine.Random.Range(3, 8);
        torqueForce = UnityEngine.Random.Range(-20, 20);
    }

    private void Start()
    {
        gameManager = transform.parent.GetComponent<GameManager>();
        diceInputs = gameManager.diceInputs;
    }

    private void FixedUpdate()
    {
        if(!isRolling)
        return;

        if(!rb.IsSleeping())
        return;

        isRolling = false;
        OnStopped?.Invoke(this);
    }

    public void StartRoll()
    {
        rb.WakeUp();
        isRolling = true;

        rb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
        rb.AddForce(direction[UnityEngine.Random.Range(0, 2)] * dropForce, ForceMode.Impulse);

        rb.AddTorque(direction[UnityEngine.Random.Range(0, 2)] * torqueForce);

        dropForce = UnityEngine.Random.Range(3, 5);
        torqueForce = UnityEngine.Random.Range(-20, 20);
    }

    public void CheckNormal(Transform side)
    {
        currentScore = score[side.name];
    }
}
