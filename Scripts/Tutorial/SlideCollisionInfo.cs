using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCollisionInfo : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject slideText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            SlideText();
        }
    }

    private void SlideText()
    {
        slideText.gameObject.SetActive(true);
        Time.timeScale = 0.3f;
    }

    public void SlideButton()
    {
        slideText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
