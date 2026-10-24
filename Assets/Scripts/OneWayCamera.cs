using UnityEngine;

public class OneWayCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothSpeed = 5f;

    [Header("Dağ Modu Ayarları")]
    public bool isMountainMode = false; // Dağda mıyız?
    public float defaultY = 0f;          // Normal seviyedeki sabit Y yüksekliği

    private float maxXPosition;

    void Start()
    {
        if (target != null)
        {
            maxXPosition = transform.position.x;
        }
        defaultY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetX;
        float targetY;

        if (isMountainMode)
        {
            // DAĞ MODU:
            // 1. One-way iptal: Karakter nereye giderse (sağa veya sola) X orada güncellenir.
            targetX = target.position.x + offset.x;
            maxXPosition = targetX; // Sağa kilitlenmeyi devreden çıkarır

            // 2. Yükselme aktif: Y ekseninde karakteri dikey olarak takip eder.
            targetY = target.position.y + offset.y;
        }
        else
        {
            // NORMAL MOD (Düz Yollar):
            // 1. One-way aktif: X sadece sağa doğru ilerler, sola dönmez.
            float currentTargetX = target.position.x + offset.x;
            if (currentTargetX > maxXPosition)
            {
                maxXPosition = currentTargetX;
            }
            targetX = maxXPosition;

            // 2. Yükselme kapalı: Sabit Y yüksekliğinde kalır.
            targetY = defaultY;
        }

        Vector3 desiredPosition = new Vector3(targetX, targetY, offset.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}