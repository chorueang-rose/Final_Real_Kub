using UnityEngine;

public class KeyDestroyer : MonoBehaviour
{
    [Header("สิ่งที่ต้องการทำลาย")]
    public GameObject objectToDestroy; // ลากกำแพง หรือสิ่งกีดขวางมาใส่ตรงนี้

    [Header("ตั้งค่ากุญแจ / อาวุธ")]
    public string requiredKeyID = "Hammer"; // รหัสของที่จะเอามาทำลาย (เช่น ค้อน, ระเบิด)
    public bool destroyKeyAfterUse = false; // ใช้แล้วของชิ้นนั้นหายไปไหม? (ถ้าเป็นระเบิด=หาย, ค้อน=ไม่หาย)

    [Header("เสียงและเอฟเฟกต์ (Optional)")]
    public GameObject destroyEffect; // เอฟเฟกต์ตอนพัง (เช่น เศษหิน, ควัน)
    public AudioSource audioSource;
    public AudioClip destroySound;   // เสียงตูม!

    private bool isDestroyed = false; // กันพลาด พังซ้ำซ้อน

    void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        // 1. มองหา KeyItem (หาทั้งตัวลูกและตัวแม่ เพื่อความชัวร์)
        KeyItem key = other.GetComponentInParent<KeyItem>();
        if (key == null) key = other.GetComponent<KeyItem>();

        // 2. ถ้าเจอของที่มีสคริปต์ KeyItem
        if (key != null)
        {
            // 3. เช็ครหัสตรงกันไหม?
            if (key.keyID == requiredKeyID)
            {
                SmashIt(key.gameObject);
                
            }
        }
    }

    void SmashIt(GameObject keyObj)
    {
        isDestroyed = true;

        // --- แก้ส่วนที่ 1: เปลี่ยนวิธีเล่นเสียง ---
        // เช็คว่ามีไฟล์เสียงมั้ย
        if (destroySound != null)
        {
            // คำสั่งนี้จะสร้างเสียงขึ้นมาเอง โดยไม่ง้อ AudioSource ของวัตถุ
            // (ไฟล์เสียง, ตำแหน่งที่จะให้ดัง, ความดัง 0-1)
            AudioSource.PlayClipAtPoint(destroySound, transform.position, 1.0f);
        }

        // --- ส่วนที่ 2: สร้าง Effect (เหมือนเดิม) ---
        if (destroyEffect != null)
            Instantiate(destroyEffect, objectToDestroy.transform.position, objectToDestroy.transform.rotation);

        // --- ส่วนที่ 3: ทำลายของ (เหมือนเดิม) ---
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        if (destroyKeyAfterUse)
        {
            Destroy(keyObj);
        }

        // ทำลาย Trigger ทิ้ง
        Destroy(gameObject);
    }
}