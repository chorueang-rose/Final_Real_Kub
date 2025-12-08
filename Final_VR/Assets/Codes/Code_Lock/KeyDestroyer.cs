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

        // --- ส่วนที่ 1: เล่น Effect ---
        if (audioSource && destroySound)
            audioSource.PlayOneShot(destroySound);

        if (destroyEffect != null)
            Instantiate(destroyEffect, objectToDestroy.transform.position, objectToDestroy.transform.rotation);

        // --- ส่วนที่ 2: ทำลายเป้าหมาย ---
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("ทำลายสิ่งกีดขวางแล้ว!");
        }

        // --- ส่วนที่ 3: จัดการกุญแจ/อาวุธ ---
        if (destroyKeyAfterUse)
        {
            Destroy(keyObj); // เช่น ถ้าระเบิดทำงานแล้ว ตัวระเบิดต้องหายไป
        }

        // (แถม) ทำลายตัว Trigger นี้ทิ้งด้วยเลย จะได้ไม่เกะกะ
        Destroy(gameObject, 0.5f);
    }
}