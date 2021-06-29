using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    private GameObject[] Enemys;
    private GameObject Target;
    private TowerSettings TowerSettings;

    private void Awake()
    {
        TowerSettings = gameObject.GetComponent<TowerSettings>();
    }

    private void Start()
    {
        StartCoroutine(Shot());
    }

    private void FixedUpdate()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        if (Target == null)
        {
            for (int i = 0; i < Enemys.Length; i++)
            {
                if (Vector3.Distance(gameObject.transform.position, Enemys[i].transform.position) <=
                    TowerSettings.AttackRange)
                {
                    Target = Enemys[i];
                }
            }  
        }
        if (Target != null && Vector3.Distance(Target.transform.position, gameObject.transform.position) > TowerSettings.AttackRange)
        {
            Target = null;
        }
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(TowerSettings.AttackSpeed);
            if (Target!=null)
            {
                var Ammo = Instantiate(TowerSettings.Ammo);
                Ammo.transform.position = gameObject.transform.position;
                Ammo.GetComponent<AmmoScript>().Target = Target;
            } 
        }
    }
}
