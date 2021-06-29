using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [HideInInspector]public GameObject Target;
    private void FixedUpdate()
    {
        if (Target != null)
        {    
            //Debug.Log(Target.transform.position + " " + gameObject.transform.position);
            transform.LookAt(Target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position , 0.1f);
            if (Target.transform.position == gameObject.transform.position)
            {
                StartCoroutine(DestroyAmmo());
            }
        }

        if (Target == null)
        {
            Destroy(gameObject);
        }

        IEnumerator DestroyAmmo()
        {
            yield return new WaitForSeconds(1f);
            Target.GetComponent<EnemyLogic>().Slowly(gameObject.GetComponent<AmmoSettings>().Slow);
            Target.GetComponent<EnemyLogic>().TakeDamage(gameObject.GetComponent<AmmoSettings>().Damage);
            Destroy(gameObject);
        }
    }
}
