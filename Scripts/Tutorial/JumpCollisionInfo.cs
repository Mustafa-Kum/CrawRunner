using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollisionInfo : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject jumpText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            JumpText();
        }
    }

    private void JumpText()
    {
        jumpText.gameObject.SetActive(true);
        Time.timeScale = 0.3f;
    }

    public void JumpButton()
    {
        jumpText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
