using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    public Text description, HeartsTxt;
    public static UiManager instance;
    public GameObject GameOverPanel, GoalPanel, AlgoPanel;
    public GameObject BtnAlgo, BtnHideAlgo;

    #region     
    public UiManager () {
        instance = this;
    }
    #endregion
    public void GenerateTxt (int phase) {
        if (phase == 1) {
            SetDescription ("Creating Tiles");
        }
        if (phase == 2) {
            SetDescription ("Choose Start Pos");
        }
        if (phase == 3) {
            SetDescription ("Choose Goal Pos");
        }
        if (phase == 4) {
            SetDescription ("Playing");
        }
        if (phase == 5) {
            SetDescription ("Invalid Move !");
        }
        if (phase == 6) {
            SetDescription ("Visited Tile !");
        }
    }

    private void SetDescription (string txt) {
        description.text = txt;
    }

    public void ShowGameOverPanel (bool y) {
        if (y) {
            GameOverPanel.SetActive (true);
        } else {
            GameOverPanel.SetActive (false);
        }
    }

    public void ShowGoalPanel (bool y) {
        if (y) {
            GoalPanel.SetActive (true);
        } else {
            GoalPanel.SetActive (false);
        }
    }
    public void ShowAlgoPanel (bool y) {
        if (y) {
            AlgoPanel.SetActive (true);
            BtnAlgo.SetActive (false);
            BtnHideAlgo.SetActive (true);
        } else {
            AlgoPanel.SetActive (false);
            BtnAlgo.SetActive (true);
            BtnHideAlgo.SetActive (false);

        }
    }

    public void UpdateMoves (int moves) {
        HeartsTxt.text = moves.ToString ();
    }

}