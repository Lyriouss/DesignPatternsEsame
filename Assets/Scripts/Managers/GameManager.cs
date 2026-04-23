using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Running,
    Paused
}

public class GameManager : MonoBehaviour
{
    private GameState gameState;

    private bool speedAbility;
    private bool shieldAbility;
    private bool healthAbility;
    private bool damageAbility;

    public static event Action onGamePaused, onGameResumed;

    private void OnEnable()
    {
        InputManager.Instance.inputMap.Player.Pause.performed += PauseGameInput;
        InputManager.Instance.inputMap.Pause.Resume.performed += ResumeGameInput;
    }

    private void OnDisable()
    {
        InputManager.Instance.inputMap.Player.Pause.performed -= PauseGameInput;
        InputManager.Instance.inputMap.Pause.Resume.performed -= ResumeGameInput;
    }

    private void Start()
    {
        //sets game to running at start
        gameState = GameState.Running;
        Time.timeScale = 1f;
        
        //enables only player inputs and disables the rest
        InputManager.Instance.inputMap.Player.Enable();
        InputManager.Instance.inputMap.Pause.Disable();
        InputManager.Instance.inputMap.GameEnd.Disable();
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
}
