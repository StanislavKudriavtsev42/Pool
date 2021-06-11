using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredBallMovement : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().material = null;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (gameObject.name.Contains("Striped"))
        {
            if (gameManager.turnCount > 0 && gameManager.players[gameManager.currentTurn] == "")
            {
                gameManager.players[gameManager.currentTurn] = "stripes";
                gameManager.players[gameManager.nextTurn] = "solids";
                if(gameManager.currentTurn == InfoPasser.firstPlayerName)
                    gameManager.AssignBallTypes("SOLIDS", "STRIPES");
                else
                    gameManager.AssignBallTypes("STRIPES", "SOLIDS");
            }
            gameManager.stripes--;
            if(gameManager.players[gameManager.currentTurn] == "stripes")
            {
                gameManager.ballWasScored = true;
            }
        }
        else if (gameObject.name.Contains("Solid"))
        {
            if (gameManager.turnCount > 0 && gameManager.players[gameManager.currentTurn] == "")
            {
                gameManager.players[gameManager.currentTurn] = "solids";
                gameManager.players[gameManager.nextTurn] = "stripes";
                if (gameManager.currentTurn == InfoPasser.firstPlayerName)
                    gameManager.AssignBallTypes("STRIPES", "SOLIDS");
                else
                    gameManager.AssignBallTypes("SOLIDS", "STRIPES");
            }
            gameManager.solids--;
            if (gameManager.players[gameManager.currentTurn] == "solids")
            {
                gameManager.ballWasScored = true;
            }
        }
        else if (gameObject.name == "Black")
            gameManager.CheckVictoryCondition(true);
    }

    void Update()
    {
        rb.AddForce(-1f, 0f, 0f, ForceMode.Acceleration);
    }
}
