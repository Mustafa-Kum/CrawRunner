using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollisionInfo : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject heartText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            HeartText();
        }
    }

    private void HeartText()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        heartText.gameObject.SetActive(true);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        heartText.gameObject.SetActive(false);
    }
}
