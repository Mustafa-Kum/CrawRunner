using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //RenderSettings.skybox = GameManager.instance.skyBoxMat[3];
        }
    }
}
