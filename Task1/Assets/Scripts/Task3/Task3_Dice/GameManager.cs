using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.Math;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalScore = 0;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int cubesCount = 3;
    [SerializeField] public DiceInputs diceInputs;
 
    private void Awake()
    {
        InitializeCubes();
    }

    public void CountAllScore()
    {
        totalScore = 0;
        
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Dice child = transform.GetChild(i).GetComponent<Dice>();
            totalScore += child.currentScore;
        }
    }

    private void InitializeCubes()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        for(int i = 1; i <= cubesCount; i++)
        {
            GameObject dice = Instantiate(cubePrefab, new Vector3(i * 1.5f, 1.0f, i * 1.0f), Quaternion.identity);
            dice.transform.parent = transform;
        }
    }
}
