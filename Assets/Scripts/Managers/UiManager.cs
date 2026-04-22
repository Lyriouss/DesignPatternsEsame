using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject shield;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Button[] abilities;

    private void OnEnable()
    {
        Player.onShieldActivated += ShowShieldBar;
        Player.onShieldDeactivated += HideShieldBar;
    }

    private void OnDisable()
    {
        Player.onShieldActivated -= ShowShieldBar;
        Player.onShieldDeactivated -= HideShieldBar;
    }

    private void Start()
    {
        //deactivates all abilities except the first
        foreach (Button abi in abilities)
        {
            abi.gameObject.SetActive(false);
        }
        abilities[0].gameObject.SetActive(true);
    }

    private void ShowShieldBar()
    {
        shield.SetActive(true);
    }

    private void HideShieldBar()
    {
        shield.SetActive(false);
    }
}
