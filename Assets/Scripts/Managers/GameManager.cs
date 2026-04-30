using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum GameState
{
    Running,
    Paused
}

public class GameManager : MonoBehaviour
{
    private GameState gameState;
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemySpawnAmount;

    private int killCount = 0;
    
    private bool speedAbility;
    private bool shieldAbility;
    private bool healthAbility;
    private bool damageAbility;

    public static event Action onGamePaused, onGameResumed, onGameEnded;

    private void OnEnable()
    {
        InputManager.Instance.inputMap.Player.Pause.performed += PauseGameInput;
        InputManager.Instance.inputMap.Pause.Resume.performed += ResumeGameInput;

        InputManager.Instance.inputMap.GameEnd.Restart.performed += ReloadSceneInput;
        
        Enemy.onEnemyKilled += UpdateKillCount;
    }

    private void OnDisable()
    {
        InputManager.Instance.inputMap.Player.Pause.performed -= PauseGameInput;
        InputManager.Instance.inputMap.Pause.Resume.performed -= ResumeGameInput;
        
        InputManager.Instance.inputMap.GameEnd.Restart.performed -= ReloadSceneInput;
        
        Enemy.onEnemyKilled -= UpdateKillCount;
    }

    private void Start()
    {
        //sets game to running at start
        gameState = GameState.Running;
        Time.timeScale = 1f;
        
        //spawns enemies
        SpawnEnemies(enemyPrefab, enemySpawnAmount);
        
        //enables only player inputs and disables the rest
        InputManager.Instance.inputMap.Player.Enable();
        InputManager.Instance.inputMap.Pause.Disable();
        InputManager.Instance.inputMap.GameEnd.Disable();
    }

    private void SpawnEnemies(GameObject enemy, int spawnAmount)
    {
        //spawns enemies equal to the amount specified through inspector's Spawn Amount
        for (int i = 0; i < spawnAmount; i++)
        {
            //gets a random position
            Vector3 randomPos = GetRandomPos();
            //spawns enemy
            Instantiate(enemy, randomPos, Quaternion.identity);
        }
        //returns a random position within arena
        Vector3 GetRandomPos()
        {
            return new Vector3(Random.Range(-25f, 25f), 1.5f, Random.Range(-25f, 25f));
        }
    }

    //function for game pause input action
    private void PauseGameInput(InputAction.CallbackContext context) => PauseGame();

    //function for game pause button
    public void PauseGame()
    {
        //sets status to paused and freezes game
        gameState = GameState.Paused;
        Time.timeScale = 0f;

        //switches input map to Pause map
        InputManager.Instance.inputMap.Player.Disable();
        InputManager.Instance.inputMap.Pause.Enable();
        
        //shows pause game panel in ui manager
        onGamePaused?.Invoke();
    }

    //function for game unpause input action
    private void ResumeGameInput(InputAction.CallbackContext context) => ResumeGame();
    
    //function for game resume button
    public void ResumeGame()
    {
        //sets status to running and resumes game
        gameState = GameState.Running;
        Time.timeScale = 1f;
        
        //switches input map to Player map
        InputManager.Instance.inputMap.Player.Enable();
        InputManager.Instance.inputMap.Pause.Disable();
        
        //hides pause game panel in ui manager
        onGameResumed?.Invoke();
    }

    private void UpdateKillCount()
    {
        //updates kill tally amount
        killCount++;

        //checks if all enemies are defeated for game completion
        if (killCount >= enemySpawnAmount)
            GameCompleted();
    }

    private void GameCompleted()
    {
        //pauses game
        gameState = GameState.Paused;
        Time.timeScale = 0f;
        
        //enables only GameEnd input map
        InputManager.Instance.inputMap.Player.Disable();
        InputManager.Instance.inputMap.GameEnd.Enable();

        //activates game end panel from UiManager
        onGameEnded?.Invoke();
    }

    private void ReloadSceneInput(InputAction.CallbackContext context)
    {
        ReloadScene();
    }

    public void ReloadScene()
    {
        //reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
