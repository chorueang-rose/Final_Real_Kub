using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueCanvas;
    public TMP_Text textComponent;

    [TextArea]
    public string message = "สวัสดี! ช่วยหาตั๋วให้ฉันหน่อย";
    [TextArea]
    public string thankYouMessage = "ขอบคุณมาก! ฉันไปล่ะนะ";

    [Header("Tag")]
    public string playerTag = "Player";
    public string ticketTag = "Ticket";

    [Header("Disappear")]
    public GameObject npcRootObject; // <--- 1. เพิ่มตัวนี้เข้ามา (ลากตัวแม่มาใส่ตรงนี้)
    public float destroyDelay = 3.0f;
    public GameObject effectObj;

    private bool isQuestCompleted = false;

    private void Start()
    {
        if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
        if (textComponent != null) textComponent.text = message;

        // กันลืม: ถ้าไม่ได้ลากตัวแม่มาใส่ ให้เดาว่าตัวแม่คือตัว Parent ของ Trigger นี้
        if (npcRootObject == null)
        {
            npcRootObject = transform.parent.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (dialogueCanvas != null) dialogueCanvas.SetActive(true);
        }
        else if (other.CompareTag(ticketTag) && !isQuestCompleted)
        {
            AcceptTicket(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (dialogueCanvas != null) dialogueCanvas.SetActive(false);
        }
    }

    void AcceptTicket(GameObject ticketObj)
    {
        isQuestCompleted = true;

        if (textComponent != null) textComponent.text = thankYouMessage;
        if (dialogueCanvas != null) dialogueCanvas.SetActive(true);

        Destroy(ticketObj); // ลบตั๋ว

        // --- ส่วนที่แก้: สั่งลบตัวแม่ (npcRootObject) แทนตัวสคริปต์เอง ---
        if (npcRootObject != null)
        {
            Destroy(npcRootObject, destroyDelay);
        }
        else
        {
            // ถ้าหาตัวแม่ไม่เจอจริงๆ ก็ลบตัวเองไปก่อน (กัน Error)
            Destroy(gameObject, destroyDelay);
        }

        if (effectObj != null)
        {
            StartCoroutine(SpawnEffectAfterDelay());
        }
    }

    System.Collections.IEnumerator SpawnEffectAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Instantiate(effectObj, transform.position, transform.rotation);
    }
}