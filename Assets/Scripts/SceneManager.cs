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

    [DllImport("__Internal")]
    private static extern string[,] GetInput();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= scoreTimer)
        {
            timer = 0.0f;
            score += 10;
            UpdateScore();
        }

        GetPlayerInputs();
    }

    void GetPlayerInputs()
    {
        string[,] inputs = GetInput();
        for (int i = 0; i < inputs.Length; i++)
        {
            // Input comes in as [{id=123, forward=1, horizontal=-1, action=0},...]
            ApplyMovement(inputs[i, 0], inputs[i, 1], inputs[i, 2], inputs[i, 3]);
        }
    }

    void ApplyMovement(string id, string forward, string right, string action)
    {
        GameObject player = playerList[id].gameObject;
        //player.GetComponent<Player>().MovePlayer();
        // Call a movement function from Player script to move that specific player.
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    void createPlayer(string id)
    {
        for (int i = 0; i < playerArr.Length; i++)
        {
            if(playerArr[i] == null)
            {
                playerArr[i].GetComponent<Player>().idNumber = id;
            }
        }
    }
}
