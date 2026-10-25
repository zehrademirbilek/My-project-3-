using UnityEngine;

public class GoblinPatrol : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float patrolDistance = 5.0f; // Başlangıçtan itibaren sağa/sola gidilecek max mesafe

    private Vector3 startPosition;
    private float direction = 1.0f; // 1: Sağa, -1: Sola
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Başlangıç konumunu kaydet
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Şimdiki karedeki yer değiştirmeyi (Delta X) hesapla
        float deltaX = direction * moveSpeed * Time.deltaTime;

        // 2. Konumu güncelle
        transform.Translate(new Vector3(deltaX, 0, 0));

        // 3. Başlangıç noktasından ne kadar uzaklaştığını kontrol et
        float currentOffset = transform.position.x - startPosition.x;

        // Sağ sınıra ulaşıldıysa sola dön
        if (currentOffset >= patrolDistance && direction > 0)
        {
            Flip();
        }
        // Sol sınıra ulaşıldıysa sağa dön
        else if (currentOffset <= -patrolDistance && direction < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        direction *= -1.0f;

        // Sprite kullanıyorsanız görsel yönünü çevir
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction < 0;
        }
        // Alternatif olarak nesnenin ölçeğini (Scale) çevirmek isterseniz:
        // Vector3 localScale = transform.localScale;
        // localScale.x *= -1;
        // transform.localScale = localScale;
    }

    // Editörde devriye alanını görsel olarak görmek için (Opsiyonel)
    private void OnDrawGizmosSelected()
    {
        Vector3 origin = Application.isPlaying ? startPosition : transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(origin.x - patrolDistance, origin.y, origin.z),
                         new Vector3(origin.x + patrolDistance, origin.y, origin.z));
    }
}