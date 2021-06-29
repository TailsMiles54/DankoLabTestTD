using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsScript : MonoBehaviour
{
    [Header("Количество волн")] public int Waves;
    [Header("Количество врагов в волне")] public int EnemyCount;
    [Header("Здоровье игрока")] public int StartHealth;
    [Header("Стартовый капитал")] public int StartMoney;
    [Header("Пауза между волнами")] public int WavePause;
    [Header("Частота появления врагов")] public int SpawnSpeed;
}
