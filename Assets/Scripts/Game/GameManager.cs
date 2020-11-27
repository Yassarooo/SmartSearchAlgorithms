using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Complexity {
    Easy,
    Hard
}

public class GameManager : MonoBehaviour {
    [SerializeField]
    public int hearts = 7;
    public int GetHearts => hearts;
    // [SerializeField]
    // private int score = 0;
    // public int GetScore => score;

    public static int startLives = 7;
    //private int startScore = 0;

    private Board board;

    private void Awake () {
        board = FindObjectOfType<Board> ();

    }

    void Start () {
        startLives = hearts;
        //startScore = score;
        board.Init (this);
    }

    // public void GameOver()
    // {
    // GameIsOver = true;

    //bool isInHighScores = IsInHighScores();
    // if (isInHighScores)
    // {
    //     SaveResult();
    // }

    //uIManager.ShowGameOverPanel(isInHighScores);
    // }

    public void RestartGame () {
        SceneManager.LoadScene (0);
    }
}