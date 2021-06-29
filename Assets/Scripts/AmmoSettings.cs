using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSettings : MonoBehaviour
{
    [Header("Урон")] public int Damage;
    [Header("Снижение скорости")] [Range(1,10)]public float Slow;
}
