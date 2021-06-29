using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [HideInInspector]public GameObject[] Road;
    [HideInInspector]public GameObject FinishPos;
    public GameObject EnemySettings;
    private int Target = 0;
    [HideInInspector]public int Health; 
    private EnemySettings EnemySettingsScript;
    [HideInInspector] public GameManager GameManagerScript;
    public GameObject HealthBar;
    private bool SlowCheck = false;
    private float Speed,DifSpeed;

    private void Awake()
    {
        EnemySettingsScript = EnemySettings.GetComponent<EnemySettings>();
        Health = EnemySettingsScript.EnemyHealth;
        Speed = EnemySettingsScript.EnemySpeed/20;
        DifSpeed = Speed;

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (Target < Road.Length)
        {
            transform.LookAt(Road[Target].transform.position + new Vector3(0, 0.7f,0));
            transform.position = Vector3.MoveTowards(transform.position, (Road[Target].transform.position + new Vector3(0, 0.7f ,0)), Speed);
            if (gameObject.transform.position == (Road[Target].transform.position) + new Vector3(0, 0.7f ,0))
            {
                Target++;
            }
        }

        if (Target>= Road.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, FinishPos.transform.position, Speed);
        }

        if (FinishPos.transform.position == gameObject.transform.position)
        {
            GameManagerScript.PlayerHealth--;
            GameManagerScript.UIupdate();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        var perc = Convert.ToDouble(100) / Convert.ToDouble(EnemySettingsScript.EnemyHealth) *
                   Convert.ToDouble(Health);
        float percf = (float)damage;
        HealthBar.transform.localScale = HealthBar.transform.localScale - new Vector3(percf*0.01f, 0 ,0 );
        if (Health <= 0)
        {
            GameManagerScript.PlayerMoney = GameManagerScript.PlayerMoney + EnemySettingsScript.MoneyForKill;
            GameManagerScript.UIupdate();
            Destroy(gameObject);
        }
    }

    public void Slowly(float x)
    {
        if (!SlowCheck)
        {
            StartCoroutine(SlowEnemy(x));
        }
    }

    IEnumerator SlowEnemy(float x)
    {
        SlowCheck = true;
        Speed = Speed / x;
        Debug.Log(Speed);
        yield return new WaitForSeconds(3f);
        Debug.Log(Speed);
        SlowCheck = false;
        Speed = DifSpeed;
    }
}
