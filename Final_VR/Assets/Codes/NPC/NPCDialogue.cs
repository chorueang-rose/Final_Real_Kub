using UnityEngine;
using TMPro; // เรียกใช้ TextMeshPro

public class NPCDialogue : MonoBehaviour
{
    [Header("UI setting")]
    public GameObject dialogueCanvas; // ลาก Canvas ที่เราสร้างไว้มาใส่
    public TMP_Text textComponent;    // ลากตัว TextMeshPro มาใส่

    [TextArea]
    public string message = "สวัสดี! ยินดีที่ได้รู้จัก"; // พิมพ์ข้อความตรงนี้ได้เลย

    [Header("player setting")]
    public string playerTag = "Player"; // เช็คว่า Tag ของตัวละครเราชื่ออะไร (ปกติคือ Player)

    private void Start()
    {
        // เริ่มเกมมา ปิดข้อความไว้ก่อน
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        // ใส่ข้อความลงไป
        if (textComponent != null)
            textComponent.text = message;
    }

    // เมื่อเดินเข้าวง
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            dialogueCanvas.SetActive(true); // เปิดข้อความ
        }
    }

    // เมื่อเดินออกจากวง
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            dialogueCanvas.SetActive(false); // ปิดข้อความ
        }
    }
}