using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    [Header("สิ่งที่ต้องการเสก")]
    public GameObject prefabToSpawn; // ของที่จะเสก
    public Transform spawnPoint;     // จุดที่จะเสก

    [Header("ตั้งค่ากุญแจ")]
    public string requiredKeyID = "Key_01"; // รหัสกุญแจที่ต้องใช้
    public bool destroyKeyAfterUse = true;  // ไขแล้วกุญแจหายไหม?

    [Header("เงื่อนไขเพิ่มเติม")]
    public bool spawnOnce = true;    // เสกแค่ครั้งเดียวพอไหม?
    private bool hasSpawned = false; // ตัวจำว่าเสกไปหรือยัง

    [Header("เสียง (Optional)")]
    public AudioSource audioSource;
    public AudioClip spawnSound;     // เสียงตอนเสกของ (เช่น เสียงวิ้งๆ)

    void OnTriggerEnter(Collider other)
    {
        // 1. ถ้าเสกไปแล้ว และตั้งให้เสกครั้งเดียว -> จบการทำงาน
        if (spawnOnce && hasSpawned) return;

        // 2. พยายามหากุญแจจากคนที่เดินเข้ามา (หาทั้งในตัวมันเอง และตัวแม่ของมัน)
        KeyItem key = other.GetComponentInParent<KeyItem>();
        if (key == null) key = other.GetComponent<KeyItem>();

        // 3. ถ้าเจอกุญแจ
        if (key != null)
        {
            // 4. เช็คว่ารหัสตรงกันไหม?
            if (key.keyID == requiredKeyID)
            {
                SpawnObject();
                

                // 5. ถ้าตั้งให้ลบกุญแจทิ้ง -> ลบเลย
                if (destroyKeyAfterUse)
                {
                    Destroy(key.gameObject);
                }
            }
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // เสกของออกมา
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // เล่นเสียง (ถ้ามี)
            //if (audioSource && spawnSound) audioSource.PlayOneShot(spawnSound);
            if (spawnSound != null)
            {
                // คำสั่งนี้จะสร้างเสียงขึ้นมาเอง โดยไม่ง้อ AudioSource ของวัตถุ
                // (ไฟล์เสียง, ตำแหน่งที่จะให้ดัง, ความดัง 0-1)
                AudioSource.PlayClipAtPoint(spawnSound, transform.position, 1.0f);
            }

            hasSpawned = true; // มาร์คว่าเสกแล้วนะ
            Debug.Log("ไขกุญแจผ่าน! เสกของเรียบร้อย");
        }
    }
}