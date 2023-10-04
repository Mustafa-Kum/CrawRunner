using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAddCollision : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            player.moveSpeed = 15;
        }
    }
}
