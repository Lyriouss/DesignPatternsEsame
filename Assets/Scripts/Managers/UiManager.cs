using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject shield;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Button[] abilities;

    [SerializeField] private GameObject pauseMenu;
    
    private void OnEnable()
    {
        Player.onShieldActivated += ShowShieldBar;
        Player.onShieldDeactivated += HideShieldBar;
        Player.onShieldDamageTaken += UpdateShieldBar;
        Player.onHealthDamageTaken += UpdateHealthBar;

        GameManager.onGamePaused += ShowPauseMenu;
        GameManager.onGameResumed += HidePauseMenu;
    }

    private void OnDisable()
    {
        Player.onShieldActivated -= ShowShieldBar;
        Player.onShieldDeactivated -= HideShieldBar;
        Player.onShieldDamageTaken -= UpdateShieldBar;
        Player.onHealthDamageTaken -= UpdateHealthBar;
        
        GameManager.onGamePaused -= ShowPauseMenu;
        GameManager.onGameResumed -= HidePauseMenu;
    }

    private void Start()
    {
        // //deactivates all abilities except the first
        // foreach (Button abi in abilities)
        // {
        //     abi.gameObject.SetActive(false);
        // }
        // abilities[0].gameObject.SetActive(true);
    }

    private void ShowShieldBar()
    {
        UpdateShieldBar(1f);
        shield.SetActive(true);
    }

    private void HideShieldBar() => shield.SetActive(false);

    private void UpdateShieldBar(float shieldFill) => shieldBar.fillAmount = shieldFill;
    
    private void UpdateHealthBar(float healthFill) => healthBar.fillAmount = healthFill;

    private void ShowPauseMenu()
    {
        
    }

    private void HidePauseMenu()
    {
        
    }
}
