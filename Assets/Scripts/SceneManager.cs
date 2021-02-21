using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Player[] playerArr = new Player[4];
    public Dictionary<string, Player> playerList = new Dictionary<string, Player>();

    int score = 0;
    float scoreTimer = 1.0f;
    float timer = 0.0f;

    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverText;

    [DllImport("__Internal")]
    private static extern string[,] GetInput();

    bool idChecked = false;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            timer += Time.deltaTime;
            if (timer >= scoreTimer)
            {
                timer = 0.0f;
                score += 10;
                UpdateScore();
            }

            GetPlayerInputs();

            CheckGameOver();
        }
    }

    void GetPlayerInputs()
    {
        string[,] inputs = GetInput();
        for (int i = 0; i < inputs.GetLength(0); i++)
        {
            if (!idChecked)
            {
                CreatePlayer(inputs[i, 0]);
            }

            // Input comes in as [{id=123, forward=1, horizontal=-1, action=0},...]
            ApplyMovement(inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3]);
        }
        idChecked = true;
    }

    void ApplyMovement(string id, string forward, string right, string action)
    {
        GameObject player = playerList[id].gameObject;
        //player.GetComponent<Player>().MovePlayer();
        // Call a movement function from Player script to move that specific player.
        playerList[id].GetComponent<Player>().PlayerMovement(forward, right, action);
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    void CreatePlayer(string id)
    {
        for (int i = 0; i < playerArr.Length; i++)
        {
            if(playerArr[i] == null)
            {
                playerArr[i].GetComponent<Player>().idNumber = id;
            }
        }
    }

    /// <summary>
    /// If all players die, end game and display score
    /// </summary>
    void CheckGameOver()
    {
        // Only two players
        if (playerArr[0] == null && playerArr[1] == null)
        {
            gameOver = true;
        }
    }

    void GameOverScreen()
    {
        gameOverText.text = $"Game Over!\nScore: {score}";
        gameOverText.enabled = true;
    }
}
