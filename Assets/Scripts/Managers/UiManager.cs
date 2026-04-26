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
        Player.onPlayerHealed += UpdateHealthBar;

        AbilityUser.onShieldCollected += ShowShieldButton;
        AbilityUser.onHealthCollected += ShowHealthButton;
        AbilityUser.onDamageCollected += ShowDamageButton;
        AbilityUser.onAbilityUsed += DisableAbilityButton;
        AbilityUser.onAbilityUsable += EnableAbilityButton;
        
        Player.onShieldDamageTaken += UpdateShieldBar;
        Player.onHealthDamageTaken += UpdateHealthBar;

        GameManager.onGamePaused += ShowPauseMenu;
        GameManager.onGameResumed += HidePauseMenu;
    }

    private void OnDisable()
    {
        Player.onShieldActivated -= ShowShieldBar;
        Player.onShieldDeactivated -= HideShieldBar;
        Player.onPlayerHealed -= UpdateHealthBar;
        
        AbilityUser.onShieldCollected -= ShowShieldButton;
        AbilityUser.onHealthCollected -= ShowHealthButton;
        AbilityUser.onDamageCollected -= ShowDamageButton;
        AbilityUser.onAbilityUsed -= DisableAbilityButton;
        AbilityUser.onAbilityUsable -= EnableAbilityButton;
        
        Player.onShieldDamageTaken -= UpdateShieldBar;
        Player.onHealthDamageTaken -= UpdateHealthBar;
        
        GameManager.onGamePaused -= ShowPauseMenu;
        GameManager.onGameResumed -= HidePauseMenu;
    }

    private void Start()
    {
        //deactivates all ability buttons in ui except the first
        foreach (Button abi in abilities)
        {
            abi.gameObject.SetActive(false);
        }
        abilities[0].gameObject.SetActive(true);
    }

    private void ShowShieldBar()
    {
        UpdateShieldBar(1f);
        shield.SetActive(true);
    }

    private void ShowShieldButton()
    {
        if (abilities.Length >= 2)
            abilities[1].gameObject.SetActive(true);
    }

    private void ShowHealthButton()
    {
        if (abilities.Length >= 3)
            abilities[2].gameObject.SetActive(true);
    }

    private void ShowDamageButton()
    {
        if (abilities.Length >= 4)
            abilities[3].gameObject.SetActive(true);
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
}
