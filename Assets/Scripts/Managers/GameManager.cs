using UnityEngine;

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

    private void Start()
    {
        //sets game to running at start
        gameState = GameState.Running;
        Time.timeScale = 1;
        
        //enables only player inputs and disables the rest
        InputManager.Instance.inputMap.Player.Enable();
        InputManager.Instance.inputMap.Pause.Disable();
        InputManager.Instance.inputMap.GameEnd.Disable();
    }
}
