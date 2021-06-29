using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject StartPos, FinishPos, GameManager, GameSettings;
    private GameSettingsScript GameSettingsScript;
    private GameManager GameManagerScript;
    public GameObject[] EnemyPrefab,Road;

    private void Awake()
    {
        GameSettingsScript = GameSettings.GetComponent<GameSettingsScript>();
        GameManagerScript = GameManager.GetComponent<GameManager>();
    }

    public void Start()
    {
        StartCoroutine(FirstWaveStart());
    }

    IEnumerator FirstWaveStart()
    {
        yield return new WaitForSeconds(GameSettingsScript.WavePause);
        StartCoroutine(WaveStarter());
    }
    
    IEnumerator WaveStarter()
    {
        for (int i = 0; i < GameSettingsScript.Waves; i++)
        {
            StartCoroutine(SpawnEnemy());
            GameManagerScript.CurrentWave++;
            GameManagerScript.UIupdate();
            yield return new WaitForSeconds(GameSettingsScript.EnemyCount * GameSettingsScript.SpawnSpeed+GameSettingsScript.WavePause);
        }
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < GameSettingsScript.EnemyCount; i++)
        {
            yield return new WaitForSeconds(GameSettingsScript.SpawnSpeed);
            var Enemy = Instantiate(EnemyPrefab[GameManagerScript.CurrentWave-1]);
            Enemy.transform.position = StartPos.transform.position;
            Enemy.GetComponent<EnemyLogic>().FinishPos = FinishPos;
            Enemy.GetComponent<EnemyLogic>().GameManagerScript = GameManagerScript;
            Enemy.GetComponent<EnemyLogic>().Road = Road;
        }
    }
}
