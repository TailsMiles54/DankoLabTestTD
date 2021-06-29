using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    private Color StandartColor;
    
    private void Awake()
    {
        StandartColor = MainRenderer.material.color;
    }

    public void SetTtransparent(bool availible)
    {
        if (availible)
        {
            MainRenderer.material.color = Color.green;
        }
        else
        {
            MainRenderer.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        MainRenderer.material.color = StandartColor;
    }

    
    // Сетка под башней, мне кажется она не очень красиво смотрится, пока убрал.
    /*private void OnDrawGizmos()
    {
        if (Selected)
        {
            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        Gizmos.color = new Color(1f, 0.89f, 0.01f, 0.79f);
                    }
                    else
                    {
                        Gizmos.color = new Color(0.97f, 1f, 0f, 0.91f);
                    }

                    Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
                }
            }
        }
    }*/
}
