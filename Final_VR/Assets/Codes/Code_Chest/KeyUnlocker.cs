using UnityEngine;

public class KeyUnlocker : MonoBehaviour
{
    [Header("ตั้งค่าการล็อค")]
    // ตรงนี้เราจะรับ "Script" อะไรก็ได้ที่ใช้จับ (เช่น OVRGrabbable, Grabbable, XRGrabInteractable)
    public MonoBehaviour grabScriptToLock;
    public string requiredKeyID = "Key_01";
    public bool destroyKeyAfterUse = true; // ไขแล้วกุญแจหายไหม?

    [Header("เสียงและเอฟเฟกต์")]
    public AudioSource audioPlayer;
    public AudioClip unlockSound;
    public AudioClip lockedSound; // (Optional) เสียงตอนไขผิด

    private bool isLocked = true;

    void Start()
    {
        // เริ่มเกมมา ถ้ามีสคริปต์จับอยู่ ให้สั่ง "ปิด" (Disable) มันซะ -> ทำให้ผู้เล่นจับไม่ได้
        if (grabScriptToLock != null)
        {
            grabScriptToLock.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ถ้าปลดล็อคไปแล้ว ก็ไม่ต้องทำอะไร
        if (!isLocked) return;

        // เช็คว่าเป็นกุญแจมั้ย
        KeyItem key = other.GetComponent<KeyItem>();

        if (key != null)
        {
            // เช็ครหัสตรงกันมั้ย
            if (key.keyID == requiredKeyID)
            {
                UnlockNow();
                if (destroyKeyAfterUse) Destroy(key.gameObject); // ลบกุญแจทิ้ง
            }
            else
            {
                // กุญแจผิดดอก
                if (audioPlayer && lockedSound) audioPlayer.PlayOneShot(lockedSound);
            }
        }
    }

    void UnlockNow()
    {
        isLocked = false;

        // หัวใจสำคัญ: สั่ง "เปิด" (Enable) สคริปต์จับ -> ตอนนี้ผู้เล่นจะจับฝาได้แล้ว!
        if (grabScriptToLock != null)
        {
            grabScriptToLock.enabled = true;
        }

        // เล่นเสียงปลดล็อค
        if (audioPlayer && unlockSound) audioPlayer.PlayOneShot(unlockSound);

        Debug.Log("Unlocked! You can now grab the lid.");
    }
}