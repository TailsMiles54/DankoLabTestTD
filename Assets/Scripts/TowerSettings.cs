using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSettings : MonoBehaviour
{
    [Header("Цена покупки")] public int Price;
    [Header("Скорость атаки")] public float AttackSpeed;
    [Header("Урон")] public int Damage;
    [Header("Дальность атаки")] public int AttackRange;
    [Header("Стоимость улучшения радиуса")] public int RangeCost;
    [Header("Стоимость улучшения скорости")] public int SpeedCost;
    [Header("Префаб снаряда")] public GameObject Ammo;
}
