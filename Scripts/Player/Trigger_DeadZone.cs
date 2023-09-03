using UnityEngine;

public class Trigger_DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameManager.instance.GameEnded();
            Time.timeScale = 0;
        }
    }
}
