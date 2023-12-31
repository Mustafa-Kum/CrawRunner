using UnityEngine;

public class Trigger_DeadZone : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            StartCoroutine(player.Die());
        }
    }
}
