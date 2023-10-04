using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollision : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject tutorialFinishedText;
    public int finishedGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            TutorialFinishedText();
            finishedGame = 1;
            PlayerPrefs.SetInt("finishedGame", finishedGame);
        }
    }

    private void TutorialFinishedText()
    {
        tutorialFinishedText.gameObject.SetActive(true);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.ui.OpenEndGameUI();
    }
}
