using UnityEngine;
using UnityEngine.UI; // ถ้าจะใช้ Text UI ให้เปิดบรรทัดนี้

public class GhostHinter : MonoBehaviour
{
    [Header("Hint Settings")]
    [TextArea]
    public string hintMessage = "...กุญแจ... อยู่ใต้ต้นไม้ใหญ่..."; // ข้อความคำใบ้
    public AudioSource ghostVoice; // เสียงพากย์คำใบ้

    [Header("Behavior")]
    public bool playOnlyOnce = false; // ให้พูดแค่ครั้งเดียวแล้วเงียบไปเลยไหม?
    public float cooldownTime = 10f; // ถ้าพูดซ้ำ ให้เว้นระยะกี่วินาที

    private bool hasPlayed = false;
    private float lastPlayTime = -99f;

    // ฟังก์ชันนี้จะทำงานเมื่อผู้เล่นเดินเข้าใกล้
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าเป็นผู้เล่นเดินมาชนไหม (เช็คจาก Tag "Player" หรือ "MainCamera")
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            TryGiveHint();
        }
    }

    void TryGiveHint()
    {
        // เงื่อนไขการพูด: ยังไม่เคยพูด หรือ เวลาผ่านไปนานพอแล้ว
        if (playOnlyOnce && hasPlayed) return;
        if (Time.time - lastPlayTime < cooldownTime) return;

        // 1. เล่นเสียง
        if (ghostVoice != null)
        {
            ghostVoice.Play();
        }

        // 2. แสดงข้อความใน Console (หรือส่งไปขึ้น UI ถ้าคุณมีระบบ UI)
        Debug.Log("Ghost whispers: " + hintMessage);

        hasPlayed = true;
        lastPlayTime = Time.time;
    }
}