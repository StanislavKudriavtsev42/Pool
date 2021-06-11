using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerOneName;
    public TextMeshProUGUI playerTwoName;
    public TextMeshProUGUI playerOneBallType;
    public TextMeshProUGUI playerTwoBallType;
    public TextMeshProUGUI winText;
    public GameObject panel;

    public GameObject[] balls;
    public bool ballsAreStationary = true;
    public CueOrbit orbitScript;
    public bool allowRotation = true;
    public Dictionary<string, string> players = new Dictionary<string, string>();
    public int stripes = 7;
    public int solids = 7;
    public bool firstPlayerTurn;
    public string currentTurn;
    public string nextTurn;
    public int turnCount = -1;
    public bool victoryConditionIsMet = false;
    public bool ballWasScored;

    private void Start()
    {
        string nameOne = InfoPasser.firstPlayerName;
        string nameTwo = InfoPasser.secondPlayerName;
        playerOneName.text = nameOne;
        playerTwoName.text = nameTwo;
        players.Add(nameOne, "");
        players.Add(nameTwo, "");
        currentTurn = nameTwo;
        nextTurn = nameOne;
    }
    void Update()
    {
        bool ballsBool = true;
        foreach(GameObject ball in balls)
        {
            ballsBool &= ball.GetComponent<BallSpeedRegulator>().IsStationary();
        }
        if (ballsAreStationary == true && ballsBool == false)
        {
            orbitScript.MoveToHomePosition();
        }
        else if (ballsAreStationary == false && ballsBool == true)
        {
            orbitScript.MoveToOrbitPoint();
            turnCount++;
            ChangeTurn();
        }
        ballsAreStationary = ballsBool;
    }

    public void CheckVictoryCondition(bool blackIsScored)
    {
        if (blackIsScored && players[currentTurn] == "stripes" && stripes > 0 ||
            blackIsScored && players[currentTurn] == "solids" && solids > 0 ||
            blackIsScored && players[currentTurn] == "")
        {
            winText.text = currentTurn.ToUpper() + " WON!";
            panel.SetActive(true);
            victoryConditionIsMet = true;
        }
        else
        {
            winText.text = nextTurn.ToUpper() + " WON!";
            panel.SetActive(true);
            victoryConditionIsMet = true;
        }
        BackToMainMenu(3);
    }

    public void BackToMainMenu(int waitingTime = 0)
    {
        StartCoroutine(WaitAndLoadMenu(waitingTime));
        Pause pauseScript = GetComponent<Pause>();
        if(pauseScript.paused)
            pauseScript.paused = pauseScript.togglePause();
    }

    public IEnumerator WaitAndLoadMenu(int waitingTime = 0)
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene("Menu");
    }

    public void ChangeTurn()
    {
        if (ballWasScored)
        {
            ballWasScored = false;
            return;
        }
        string tempString = currentTurn;
        currentTurn = nextTurn;
        nextTurn = tempString;

        Color tempColor = playerOneName.color;
        playerOneName.color = playerTwoName.color;
        playerTwoName.color = tempColor;
        playerOneBallType.color = playerOneName.color;
        playerTwoBallType.color = playerTwoName.color;
    }

    public void AssignBallTypes(string firstPlayerType, string secongPlayerType)
    {
        playerOneBallType.text = firstPlayerType;
        playerTwoBallType.text = secongPlayerType;
    }
}

public static class InfoPasser
{
    public static string firstPlayerName = "Player 1";
    public static string secondPlayerName = "Player 2";
    public static void SetPlayerNames(string name1, string name2)
    {
        firstPlayerName = name1;
        secondPlayerName = name2;
    }
}