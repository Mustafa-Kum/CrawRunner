using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySFX(1);
            GameManager.instance.coins++;
            Destroy(gameObject);
        }
    }
}
