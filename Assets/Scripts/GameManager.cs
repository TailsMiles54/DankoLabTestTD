using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameSettings;
    private GameSettingsScript GameSettingsScript;
    [HideInInspector] public int CurrentWave;
    [HideInInspector] public int PlayerMoney;
    [HideInInspector] public int PlayerHealth;

    public Text _Health, _Money, _Wave;
    
    private void Awake()
    {
        GameSettingsScript = GameSettings.GetComponent<GameSettingsScript>();
    }

    private void Start()
    {
        CurrentWave = 0;
        PlayerMoney = GameSettingsScript.StartMoney;
        PlayerHealth = GameSettingsScript.StartHealth;
        UIupdate();
    }

    public bool Buy(int x)
    {
        if (PlayerMoney >= x)
        {
            PlayerMoney = PlayerMoney - x;
            UIupdate();
            return true;
        }
        return false;
    }

    public void Sell(int x)
    {
        PlayerMoney = PlayerMoney + (x/2);
        UIupdate();
    }
    
    public void UIupdate()
    {
        _Health.text = "Health: " + PlayerHealth;
        _Money.text = "Money: " + PlayerMoney;
        _Wave.text = "Wave: " + CurrentWave;
    }
}
