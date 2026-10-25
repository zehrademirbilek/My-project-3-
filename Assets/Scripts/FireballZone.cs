using UnityEngine;

public class FireballZone : MonoBehaviour
{
    [SerializeField] private FireballSpawner spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Alana giren nesne "Player" ise fırlatmayı başlat
        if (other.CompareTag("Player"))
        {
            if (spawner != null)
            {
                spawner.StartSpawning();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Alandan çıkan nesne "Player" ise fırlatmayı durdur
        if (other.CompareTag("Player"))
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
            }
        }
    }
}