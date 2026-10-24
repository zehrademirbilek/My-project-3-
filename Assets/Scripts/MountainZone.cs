using UnityEngine;

public class MountainZone : MonoBehaviour
{
    private OneWayCamera mainCam;

    void Start()
    {
        mainCam = Camera.main.GetComponent<OneWayCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCam.isMountainMode = true; // Dağa girince Dağ Modunu Aç (One-way İPTAL, Serbest Kamera AKTİF)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCam.isMountainMode = false; // Dağdan çıkınca Normal Moduna Dön (One-way AKTİF)
        }
    }
}