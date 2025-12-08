using UnityEngine;

public class MultiKeyUnlocker : MonoBehaviour
{
    [Header("ตั้งค่าสิ่งที่ต้องการล็อค (ใส่ได้หลายอัน)")]
    // เปลี่ยนจากตัวเดียว เป็น Array [] เพื่อให้ใส่ได้หลายช่อง
    public MonoBehaviour[] grabScriptsToLock;

    [Header("ตั้งค่ากุญแจ")]
    public string requiredKeyID = "Key_01";
    public bool destroyKeyAfterUse = true;

    [Header("เสียงและเอฟเฟกต์")]
    public AudioSource audioPlayer;
    public AudioClip unlockSound;
    public AudioClip lockedSound;

    private bool isLocked = true;

    void Start()
    {
        // วนลูปสั่งปิดสคริปต์จับของทุกชิ้นที่ใส่มาในรายการ
        foreach (var script in grabScriptsToLock)
        {
            if (script != null)
            {
                script.enabled = false; // สั่งล็อค (จับไม่ได้)
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLocked) return;

        // มองหา KeyItem (รองรับทั้งอยู่ที่ลูกหรือแม่)
        KeyItem key = other.GetComponentInParent<KeyItem>();
        if (key == null) key = other.GetComponent<KeyItem>();

        if (key != null)
        {
            if (key.keyID == requiredKeyID)
            {
                UnlockAll(); // สั่งปลดล็อคทุกชิ้น
                if (destroyKeyAfterUse) Destroy(key.gameObject);
            }
            else
            {
                if (audioPlayer && lockedSound) audioPlayer.PlayOneShot(lockedSound);
            }
        }
    }

    void UnlockAll()
    {
        isLocked = false;

        // วนลูปสั่งเปิดสคริปต์จับของทุกชิ้น
        foreach (var script in grabScriptsToLock)
        {
            if (script != null)
            {
                script.enabled = true; // ปลดล็อค! (จับได้แล้ว)
            }
        }

        if (audioPlayer && unlockSound) audioPlayer.PlayOneShot(unlockSound);
        Debug.Log("Unlocked everything!");
    }
}