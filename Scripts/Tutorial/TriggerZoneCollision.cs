using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneCollision : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameManager.instance.RespawnPlayer(playerGameObject);
            Time.timeScale = 1f;
        }
    }
}
