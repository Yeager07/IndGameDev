using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.Math;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnTotalScoreChanged;
    public event Action<int> OnCurrentScoreChanged;
    public event Action<int, int, int> OnTargetsChanged;
    public event Action<int> OnThrowCountChanged;
    public event Action<GameResult> OnGameEnded;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] public DiceInputs diceInputs;
 
    public int totalScore;
    public int currentScore;
    private int winScore;
    private int loseScore;
    private int drawScore;
    private int cubesCount = 3;
    private int throwCount = 0;
    private bool isRolling;
    private bool isGameOver;
    private readonly HashSet<Dice> rollingDice = new();

    private void Start()
    {
        diceInputs.dropEvent.AddListener(RollDice);
        RestartGame();
    }

    private void OnDestroy()
    {
        diceInputs.dropEvent.RemoveListener(RollDice);
    }

    public void CountAllScore()
    {
        currentScore = 0;
        
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Dice child = transform.GetChild(i).GetComponent<Dice>();
            currentScore += child.currentScore;
        }
    }

    private void InitializeCubes(int count)
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.TryGetComponent(out Dice oldDie))
                oldDie.OnStopped -= HendleDieStopped;
            Destroy(child.gameObject);
        }

        for(int i = 1; i <= cubesCount; i++)
        {
            Dice dice = Instantiate(cubePrefab, new Vector3(i * 1.5f, 1.0f, i * 1.0f), Quaternion.identity).GetComponent<Dice>();
            dice.transform.parent = transform;
            dice.OnStopped += HendleDieStopped;
        }
    }

    public void RestartGame()
    {
        isRolling = false;
        isGameOver = false;
        rollingDice.Clear();

        totalScore = 0;
        currentScore = 0;
        throwCount = 0;

        InitializeCubes(cubesCount);
        GenerateTarget();

        OnTotalScoreChanged?.Invoke(totalScore);
        OnCurrentScoreChanged?.Invoke(currentScore);
        OnTargetsChanged?.Invoke(winScore, loseScore, drawScore);
    }

    public void GenerateTarget()
    {
        winScore = UnityEngine.Random.Range(cubesCount*cubesCount*3, cubesCount*cubesCount*5);
        loseScore = UnityEngine.Random.Range(1, cubesCount*cubesCount*3/2);
        drawScore = UnityEngine.Random.Range(cubesCount*cubesCount*3/2, cubesCount*cubesCount*4);
    }

    public void RollDice()
    {
        if(isRolling || isGameOver)
        return;
        
        isRolling = true;
        rollingDice.Clear();

        foreach (Transform child in transform)
        {
            var die = child.GetComponent<Dice>();
            rollingDice.Add(die);
            die.StartRoll();
        }

        throwCount += 1;
        OnThrowCountChanged?.Invoke(throwCount);
    }

    private void HendleDieStopped(Dice dice)
    {
        if(!rollingDice.Contains(dice))
        return;

        rollingDice.Remove(dice);

        if(rollingDice.Count > 0)
        return;

        isRolling = false;

        CountAllScore();

        totalScore += currentScore;

        if(throwCount == cubesCount)
        {
            isGameOver = true;
            CheckGameEnd();
        }

        OnCurrentScoreChanged?.Invoke(currentScore);
        OnTotalScoreChanged?.Invoke(totalScore);
    }

    private void CheckGameEnd()
    {
        GameResult result;
        
        if(totalScore >= winScore)
        result = GameResult.Win;
        
        else if(totalScore <= loseScore)
        result = GameResult.Lose;
        
        else if(totalScore <= winScore && totalScore > loseScore) 
        result = GameResult.Draw;
        
        else return;

        OnGameEnded?.Invoke(result);
    }

    public void SetDiceCount(int count)
    {
        cubesCount = Mathf.Max(1, count);
    }

    public void SetWinScore(int score)
    {
        winScore = score;
        OnTargetsChanged?.Invoke(winScore, loseScore, drawScore);
    }

    public void SetLoseScore(int score)
    {
        loseScore = score;
        OnTargetsChanged?.Invoke(winScore, loseScore, drawScore);
    }

    public void SetDrawScore(int score)
    {
        drawScore = score;
        OnTargetsChanged?.Invoke(winScore, loseScore, drawScore);
    }
}

public enum GameResult { Win, Lose, Draw }