using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damageAmount = 1; // Kaç can götüreceği
    [SerializeField] private bool destroyOnImpact = false; // Temas edince nesne yok olsun mu? (Fireball için true, Trap/Goblin için false)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Temas edilen nesne Player ise hasar ver
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer Trap veya Fireball "Is Trigger" olarak ayarlandıysa tetiklenir
        if (other.CompareTag("Player"))
        {
            ApplyDamage(other.gameObject);
        }
    }

    private void ApplyDamage(GameObject playerObj)
    {
        // Player üzerindeki PlayerHealth bileşenini bul ve hasar ver
        PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);

            // Eğer çarpan nesne fırlatılan bir şeyse (Fireball gibi) kendini yok et
            if (destroyOnImpact)
            {
                Destroy(gameObject);
            }
        }
    }
}