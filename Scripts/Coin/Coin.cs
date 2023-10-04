using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem goldFX;
    private GameObject goldFXInstance;
    private ParticleSystem coinParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySFX(1);
            GameManager.instance.coins++;

            if (goldFX != null)
            {
                goldFXInstance = Instantiate(goldFX, transform.position, Quaternion.identity).gameObject;
                coinParticleSystem = goldFXInstance.GetComponent<ParticleSystem>();
                coinParticleSystem.Play();
                Destroy(goldFXInstance, 1f);
            }

            gameObject.SetActive(false);
        }
    }
}
