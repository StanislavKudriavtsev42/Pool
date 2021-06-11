using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public TMP_InputField player1;
    public TMP_InputField player2;
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
        string firstName = "Player 1";
        string secondName = "Player 2";
        if (player1.text.Trim() != "")
            firstName = player1.text;
        if (player2.text.Trim() != "")
            secondName = player2.text;
        if (secondName == firstName)
            secondName += " ";
        InfoPasser.SetPlayerNames(firstName, secondName);
    }
}
