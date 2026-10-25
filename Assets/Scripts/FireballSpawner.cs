using System.Collections;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float spawnInterval = 2.0f; // Kaç saniyede bir atılacak
    [SerializeField] private float launchForce = 8.0f;  // Fırlatma hızı/kuvveti

    [Header("Açı Ayarları")]
    [Range(0f, 90f)]
    [SerializeField] private float minAngle = -60f; // Sol sapma açısı (Örn: -60 veya -75)
    [Range(0f, 90f)]
    [SerializeField] private float maxAngle = 60f;  // Sağ sapma açısı (Örn: 60 veya 75)

    private Coroutine spawnCoroutine;

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnAndLaunchFireball();
        }
    }

    private void SpawnAndLaunchFireball()
    {
        if (fireballPrefab == null) return;

        // 1. Fireball'u kapı pozisyonunda oluştur
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // 2. Inspector'dan belirlediğiniz min/max değerleri arasında rastgele açı seç
        float randomAngle = Random.Range(-minAngle, maxAngle);

        // Açıya göre fırlatma yönünü hesapla (Aşağı yönü baz alarak)
        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        Vector2 launchDirection = rotation * Vector2.down;

        // 3. Rigidbody2D bileşenine kuvvet uygula
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        }
    }

    // Unity Editöründe fırlatma açısını görsel olarak görmek için:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 leftDir = Quaternion.Euler(0, 0, -minAngle) * Vector3.down;
        Vector3 rightDir = Quaternion.Euler(0, 0, maxAngle) * Vector3.down;

        Gizmos.DrawRay(transform.position, leftDir * 5f);
        Gizmos.DrawRay(transform.position, rightDir * 5f);
    }
}