using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [HideInInspector]public GameObject  _TowerPanel, GManager;
    [HideInInspector]public int UpCost, SpCost, SlMoney;
    [HideInInspector] public Button _UpgradeRange, _UpgradeSpeed, _Sell;
    [HideInInspector] public Text _RangeText, _RangeCost, _RangeLevel,_SpeedText, _SpeedCost, _SpeedLevel;
    private GameManager GameManager;
    private GameObject Selected;
    [HideInInspector]public GameObject Plane;

    public void Start()
    {
        Plane = GameObject.Find("Plane");
    }

    private void Perenos()
    {
        UpCost = Selected.GetComponent<TowerSettings>().RangeCost;
        SpCost = Selected.GetComponent<TowerSettings>().SpeedCost;
        SlMoney = Selected.GetComponent<TowerSettings>().Price/2;
    }

    public void TextParam(Text RangeText, Text RangeCost, Text RangeLevel, Text SpeedText, Text SpeedCost,
        Text SpeedLevel)
    {
        _RangeText = RangeText ;
        _RangeCost = RangeCost ;
        _RangeLevel = RangeLevel;
        _SpeedText = SpeedText ;
        _SpeedCost =  SpeedCost ;
        _SpeedLevel = SpeedLevel;
    }

    public void Param(Button Up,Button Sp,Button Sl,GameObject Pn, GameManager GM)
    {
        _UpgradeRange = Up;
        _UpgradeSpeed = Sp;
        _Sell = Sl;
        _TowerPanel = Pn;
        GameManager = GM;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.tag == "Tower" && Plane.GetComponent<BuildingsGrid>().flyingTower == null)
                {
                    Selected = hit.collider.gameObject;
                    _TowerPanel.SetActive(true);
                    Perenos();
                    TextUpdate();
                    Buttons();
                }
                else
                {
                    _TowerPanel.SetActive(false);
                }
            }
        }
    }

    void TextUpdate()
    {
        _RangeText.text = "RangeUpgrade";
        _RangeCost.text = "Cost: " + UpCost;
        _RangeLevel.text = "Current: " + Selected.GetComponent<TowerSettings>().AttackRange;
        _SpeedText.text = "RangeUpgrade";
        _SpeedCost.text = "Cost: " + SpCost;
        _SpeedLevel.text = "Current: " + Math.Round(Selected.GetComponent<TowerSettings>().AttackSpeed , 1);
    }
    void Buttons()
    {
        _UpgradeRange.onClick.AddListener(RangeUpgrade);
        _UpgradeSpeed.onClick.AddListener(SpeedUpgrade);
        _Sell.onClick.AddListener(Sell);
    }

    public void RangeUpgrade()
    {
        if (GameManager.Buy(UpCost))
        {
            Selected.GetComponent<TowerSettings>().AttackRange = Selected.GetComponent<TowerSettings>().AttackRange + 1;
        }
        TextUpdate();
        _TowerPanel.SetActive(false);
    }
    public void SpeedUpgrade()
    {
        if (GameManager.Buy(SpCost))
        {
            Selected.GetComponent<TowerSettings>().AttackSpeed = Selected.GetComponent<TowerSettings>().AttackSpeed - 0.1f;
        }
        TextUpdate();
        _TowerPanel.SetActive(false);
    }
    public void Sell()
    {
        GameManager.Sell(Selected.GetComponent<TowerSettings>().Price);
        _TowerPanel.SetActive(false);
        Destroy(Selected);
    }
}
