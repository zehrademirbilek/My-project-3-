using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Invincibility Settings")]
    [SerializeField] private float invincibilityDuration = 1.0f; // Hasar aldıktan sonraki dokunulmazlık süresi (saniye)
    private bool isInvincible = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // Dokunulmazlık sürecindeyse hasar alma
        if (isInvincible) return;

        currentHealth -= damageAmount;
        Debug.Log("Player Canı: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Hasar alınca kısa süreli dokunulmazlık başlat (üst üste anında ölmemek için)
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private System.Collections.IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Player Öldü!");
        // Şimdilik sahneyi yeniden başlatabilir veya karakteri gizleyebilirsiniz:
        gameObject.SetActive(false);
    }
}