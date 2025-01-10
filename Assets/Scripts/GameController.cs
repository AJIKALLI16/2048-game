using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI playerIDText;
    [SerializeField]
    private Button resetButton;
    [SerializeField]
    private int ID;

    private const string PlayerIDKey = "PlayerID";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartGame();
        PlayerID();
    }

    public void StartGame()
    {
        gameResult.text = "";

        SetPoints(0);
        GameStarted = true;

        Field.Instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You WIN!!!!!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "You LOSE!!!!!";
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    public void SetPoints(int points)
    {
        Points = points;
        pointsText.text = points.ToString();
    }

    public void PlayerID()
    {
        if (PlayerPrefs.HasKey(PlayerIDKey))
            playerIDText.text = PlayerPrefs.GetString(PlayerIDKey);
        else
            GenerateNewID();

        resetButton.onClick.AddListener(ResetID);
    }

    public void GenerateNewID()
    {
        string newID = Random.Range(100000, 999999).ToString();
        playerIDText.text = newID;

        PlayerPrefs.SetString(PlayerIDKey, newID);
        PlayerPrefs.Save();
    }

    public void ResetID()
    {
        if (string.IsNullOrEmpty(playerIDText.text))
        {
            GenerateNewID();
            StartGame();
        }
        else
        {
            GenerateNewID();
            StartGame();
        }
    }
}
