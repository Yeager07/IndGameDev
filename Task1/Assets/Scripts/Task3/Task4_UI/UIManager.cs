using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private UnityAction<string> winHandler;
    private UnityAction<string> loseHandler;
    private UnityAction<string> drawHandler;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI throwCountText;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI scoreToWinText;
    [SerializeField] private TextMeshProUGUI scoreToLoseText;
    [SerializeField] private TextMeshProUGUI scoreToDrawText;
    [SerializeField] private TMP_InputField inputCubesCount;
    [SerializeField] private TMP_InputField inputScoreToWin;
    [SerializeField] private TMP_InputField inputScoreToLose;
    [SerializeField] private TMP_InputField inputScoreToDraw;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button restartResultButton;

    private void Start()
    {
        resultPanel.SetActive(false);
        scorePanel.SetActive(true);
    }

    private void OnEnable()
    {
        winHandler = v => ParseAndApply(v, gameManager.SetWinScore);
        loseHandler = v => ParseAndApply(v, gameManager.SetLoseScore);
        drawHandler = v => ParseAndApply(v, gameManager.SetDrawScore);

        gameManager.OnTotalScoreChanged += UpdateTotalScore;
        gameManager.OnCurrentScoreChanged += UpdateCurrentScore;
        gameManager.OnThrowCountChanged += UpdateThrowCount;
        gameManager.OnTargetsChanged += UpdateTargets;
        gameManager.OnGameEnded += ShowResults;

        restartButton.onClick.AddListener(gameManager.RestartGame);
        restartResultButton.onClick.AddListener(OnRestartGameClicked);

        inputCubesCount.onEndEdit.AddListener(OnDiceCount);
        inputScoreToWin.onEndEdit.AddListener(winHandler);
        inputScoreToLose.onEndEdit.AddListener(loseHandler);
        inputScoreToDraw.onEndEdit.AddListener(drawHandler);
    }

    private void OnDisable()
    {
        gameManager.OnTotalScoreChanged -= UpdateTotalScore;
        gameManager.OnCurrentScoreChanged -= UpdateCurrentScore;
        gameManager.OnThrowCountChanged -= UpdateThrowCount;
        gameManager.OnTargetsChanged -= UpdateTargets;
        gameManager.OnGameEnded -= ShowResults;

        restartButton.onClick.RemoveListener(gameManager.RestartGame);

        inputCubesCount.onEndEdit.RemoveListener(OnDiceCount);
        inputScoreToWin.onEndEdit.RemoveListener(winHandler);
        inputScoreToLose.onEndEdit.RemoveListener(loseHandler);
        inputScoreToDraw.onEndEdit.RemoveListener(drawHandler);
    }

    private void UpdateTotalScore(int totalScore)
    {
        totalScoreText.text = "Total Score: " + totalScore.ToString();
    }

    private void UpdateThrowCount(int throwCount)
    {
        throwCountText.text = "Throw Count: " + throwCount.ToString();
    }

    private void UpdateCurrentScore(int currentScore)
    {
        currentScoreText.text = "Current Score: " + currentScore.ToString();
    }

    private void UpdateTargets(int winScore, int loseScore, int drawScore)
    {
        scoreToWinText.text = "Score to Win: " + winScore.ToString();
        scoreToLoseText.text = "Score to Lose: " + loseScore.ToString();
        scoreToDrawText.text = "Score to Draw: " + drawScore.ToString();
    }

    private void ShowResults(GameResult result)
    {
        resultPanel.SetActive(true);
        scorePanel.SetActive(false);
        resultText.text = result switch
        {
            GameResult.Win  => "Win!",
            GameResult.Lose => "Lose!",
            GameResult.Draw => "Draw!",
            _ => ""
        };
    }

    private void OnDiceCount(string text)
    {
        if(int.TryParse(text, out int value))
        gameManager.SetDiceCount(value);
    }

    private void ParseAndApply(string text, Action<int> setter)
    {
        if(int.TryParse(text, out int value))
        setter(value);
    }

    private void OnRestartGameClicked()
    {
        resultPanel.SetActive(false);
        scorePanel.SetActive(true);
        gameManager.RestartGame();
    }
}
