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
    [SerializeField] private GameObject gameEndMenu;
    
    private void OnEnable()
    {
        Player.onShieldActivated += ShowShieldBar;
        Player.onShieldDeactivated += HideShieldBar;
        Player.onPlayerHealed += UpdateHealthBar;

        ChangeStatusCommand.onAbilityChanged += ChangeAbilityButton;
        AbilityUser.onAbilityUsed += DisableAbilityButton;
        AbilityUser.onAbilityUsable += EnableAbilityButton;
        
        Player.onShieldDamageTaken += UpdateShieldBar;
        Player.onHealthDamageTaken += UpdateHealthBar;

        GameManager.onGamePaused += ShowPauseMenu;
        GameManager.onGameResumed += HidePauseMenu;
        
        GameManager.onGameEnded += ShowGameEndMenu;
    }

    private void OnDisable()
    {
        Player.onShieldActivated -= ShowShieldBar;
        Player.onShieldDeactivated -= HideShieldBar;
        Player.onPlayerHealed -= UpdateHealthBar;
        
        ChangeStatusCommand.onAbilityChanged -= ChangeAbilityButton;
        AbilityUser.onAbilityUsed -= DisableAbilityButton;
        AbilityUser.onAbilityUsable -= EnableAbilityButton;
        
        Player.onShieldDamageTaken -= UpdateShieldBar;
        Player.onHealthDamageTaken -= UpdateHealthBar;
        
        GameManager.onGamePaused -= ShowPauseMenu;
        GameManager.onGameResumed -= HidePauseMenu;

        GameManager.onGameEnded -= ShowGameEndMenu;
    }

    private void Start()
    {
        //deactivates all ability buttons in ui except the first
        foreach (Button abi in abilities)
        {
            abi.gameObject.SetActive(false);
        }
        abilities[0].gameObject.SetActive(true);
        
        //deactivates paase and game end menus
        pauseMenu.SetActive(false);
        gameEndMenu.SetActive(false);
    }

    private void ShowShieldBar()
    {
        UpdateShieldBar(1f);
        shield.SetActive(true);
    }

    private void ChangeAbilityButton(bool status, AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.Shield:
                if (abilities.Length >= 2)
                    abilities[1].gameObject.SetActive(status);
                break;
            
            case AbilityType.Health:
                if (abilities.Length >= 3)
                    abilities[2].gameObject.SetActive(status);
                break;
            
            case AbilityType.Damage:
                if (abilities.Length >= 4)
                    abilities[3].gameObject.SetActive(status);
                break;
        }
    }

    private void DisableAbilityButton(IAbility ability)
    {
        switch (ability)
        {
            case SpeedAbility:
                if (abilities.Length >= 1)
                    abilities[0].interactable = false;
                break;
            
            case ShieldAbility:
                if (abilities.Length >= 2)
                    abilities[1].interactable = false;
                break;
            
            case HealthAbility:
                if (abilities.Length >= 3)
                    abilities[2].interactable = false;
                break;
            
            case DamageAbility:
                if (abilities.Length >= 4)
                    abilities[3].interactable = false;
                break;
        }
    }

    private void EnableAbilityButton(IAbility ability)
    {
        switch (ability)
        {
            case SpeedAbility:
                if (abilities.Length >= 1)
                    abilities[0].interactable = true;
                break;
            
            case ShieldAbility:
                if (abilities.Length >= 2)
                    abilities[1].interactable = true;
                break;
            
            case HealthAbility:
                if (abilities.Length >= 3)
                    abilities[2].interactable = true;
                break;
            
            case DamageAbility:
                if (abilities.Length >= 4)
                    abilities[3].interactable = true;
                break;
        }
    }

    private void HideShieldBar() => shield.SetActive(false);

    //fill amount is calculated beforehand and passed in parameter of function
    private void UpdateShieldBar(float shieldFill) => shieldBar.fillAmount = shieldFill;
    
    private void UpdateHealthBar(float healthFill) => healthBar.fillAmount = healthFill;

    private void ShowPauseMenu() => pauseMenu.SetActive(true);
    
    private void HidePauseMenu() => pauseMenu.SetActive(false);

    private void ShowGameEndMenu() => gameEndMenu.SetActive(true);
}
