using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu, Running, GameOver, Instructions, Credits

}
public class GameManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject instructionCanvas;
    public GameObject CreditsCanvas;
    public GameObject HUD;
    public GameObject GameOverCanvas;
    public GameObject spawnManager;
    public GameObject player;
    public GameObject lasers;
    public event Action GameStateChange;
    public event Action ScoreChange;
    public event Action PowerChange;
    public float enemySpeed = -5.0f;
    public float enemySpeedChange = 0.5f;
    public static GameManager instance;
    public Action BatteryPowerChange;
    public float spawnDelay = 10.0f;
    public float SpawnDelayChangeOverTime = 0.03f;
    public float minSpawenDelay = 0.1f;
    private int numEnemiesToSpawn = 2;
    private float timeSinceLastSpawnIncrease = 0.0f;
    private int score;
    private float playerPower;
    public GameState gameState;



    public GameState GameState
    {
        get { return gameState; }
        set {
            gameState = value;
            GameStateChange();
        }
    }



    public float timeSinceLastSpawn = 0.0f;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            ScoreChange();
        }
    }
    [SerializeField]
    private float batteryPower;



    public float PlayerPower
    {
        get
        {
            return playerPower;
        }

        set
        {
            playerPower = value;
            PowerChange();
        }
    }

    public float BatteryPower
    {
        get
        {
            return batteryPower;
        }

        set
        {
            batteryPower = value;
            BatteryPowerChange();

        }
    }

    public Vector3[] defaulPowerBarPositions;
    public float powerBarPositionXoffset;

    private void Awake()
    {
        //Some ugly stuff here
        defaulPowerBarPositions = new Vector3[11];
        for (int i = 0; i < 11; i++)
        {
            defaulPowerBarPositions[i] = new Vector3(i * powerBarPositionXoffset, 0.0f, 0.0f);

        }
        gameState = GameState.Menu;
        timeSinceLastSpawn = 0.0f;
        instance = this;
        Debug.Log("Instance created");
    }

    public void Restart()
    {
        numEnemiesToSpawn = 1;
        HUD.SetActive(true);
        timeSinceLastSpawn = 0.0f;
        instance.PlayerPower = player.GetComponent<PlayerController>().maximumPower;
        instance.BatteryPower = 200f;
        instance.Score = 0;
        spawnManager.SetActive(true);
        SpawnManager.instance.Restart();
        player.SetActive(true);
        lasers.SetActive(true);
        instance.enemySpeed = -1.0f;
        instance.spawnDelay = 4.0f;
        instance.enemySpeedChange = 0.5f;
        instance.SpawnDelayChangeOverTime = 0.03f;
        player.transform.position = new Vector3(0.0f, player.transform.position.y, player.transform.position.z);


    }

    private void GameOver()
    {
        GameOverCanvas.SetActive(true);
        instance.BatteryPower = 200.0f;

    }
    public void OnPowerChange()
    {
        if (PowerChange == null) return;
        if (playerPower + batteryPower <= 0.0f && instance.GameState == GameState.Running)
        {
            instance.GameState = GameState.GameOver;
        }
    }

    public void DisplayMenu()
    {


        menuCanvas.SetActive(true);
        spawnManager.SetActive(false);
        
    }

    public void DisplayInstructions()
    {

        instructionCanvas.SetActive(true);
        
    }

    public void DisplayCredits()
    {
//        GameObject instructionCanvas = GameObject.Find("InstructionCanvas");
        CreditsCanvas.SetActive(true);
    }
    public void OnGameStateChanged()
    {
        //Debug.Log("Calling onGameStateChanged" + instance.GameState);
        spawnManager.SetActive(false);
        player.SetActive(false);
        lasers.SetActive(false);
        GameObject[] allUI = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject uiObject in allUI)
        {
            uiObject.SetActive(false);
        }
        switch (gameState)
        {
            case GameState.Menu:
                DisplayMenu();
                break;

            case GameState.Credits:
                DisplayCredits();
                break;

            case GameState.Instructions:
                DisplayInstructions();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.Running:
                Restart();
                break;
            

        }
    }
    private void Start()
    {
        instance.GameStateChange += OnGameStateChanged;
        instance.BatteryPowerChange += OnBatteryPowerChanged;
        instance.PowerChange += OnPowerChange;
        instance.GameState = GameState.Menu;
    }
    private void Update()
    {
        if (instance.GameState != GameState.Running) return;
        timeSinceLastSpawnIncrease += Time.deltaTime;
        //Debug.Log("Running update");
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnDelay)
        {
            enemySpeed -= enemySpeedChange;
            int i = 0;
            while (i < numEnemiesToSpawn) {
                SpawnManager.instance.SpawnEnemy(enemySpeed);
                i++;
            }
            if (timeSinceLastSpawnIncrease > 10.0f)
            {
                numEnemiesToSpawn++;
                timeSinceLastSpawnIncrease = 0.0f;
            }
            if (spawnDelay > minSpawenDelay) spawnDelay -= SpawnDelayChangeOverTime;
            timeSinceLastSpawn = 0.0f;


        }
        //if (instance.totalPower < 0.0f) instance.GameState = GameState.GameOver;
    }

    public void ChangePlayerPower(float addPower)
    {
        PlayerPower += addPower;
    }
    public void ChangePower(float addPower)
    {
        BatteryPower += addPower;


    }

    public void AddScore(int addScore)
    {
        score += addScore;
        ScoreChange();

    }

    public void OnStartGameClick()
    {
        instance.GameState = GameState.Running;

    }
    public void OnInstructionsClick()
    {
        instance.GameState = GameState.Instructions;

    }
    public void OnCreditsClick()
    {
        instance.GameState = GameState.Credits;

    }
    public void OnQuitClick()
    {
        Application.Quit();

    }

    public void OnBatteryPowerChanged()
    {
        if (BatteryPowerChange == null) return;
        if (playerPower + batteryPower <= 0.0f && instance.GameState == GameState.Running)
        {
            instance.GameState = GameState.GameOver;
        }
    }
    public void OnGenericButtonCLick(GameState gameState)
    {
        instance.GameState = gameState;
    }

    public void OnBackButtonClick()
    {
        instance.GameState = GameState.Menu;
    }
}
