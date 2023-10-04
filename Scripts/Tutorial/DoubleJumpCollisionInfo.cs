using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpCollisionInfo : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject doubleJumpText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            DoubleJumpText();
        }
    }

    private void DoubleJumpText()
    {
        doubleJumpText.gameObject.SetActive(true);
        Time.timeScale = 0.3f;
    }

    public void DoubleJumpButton()
    {
        doubleJumpText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
