using UnityEngine;

public class ShowUIOnTrigger : MonoBehaviour
{
    [Header("ตั้งค่า UI")]
    public GameObject uiObject; // ลากตัว Canvas หรือ Panel ที่อยากให้เปิดมาใส่ตรงนี้
    public bool closeOnExit = true; // ถ้าเดินออกจะให้ปิดไหม? (ติ๊กถูก = ปิด)

    [Header("ตั้งค่าตัวผู้เล่น")]
    public string playerTag = "Player"; // เช็ค Tag ว่าใครเป็นคนเดินชน

    void Start()
    {
        // เริ่มเกมมา สั่งปิด UI ไว้ก่อน (กันลืม)
        if (uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }

    // เมื่อเดินเข้าโซน
    void OnTriggerEnter(Collider other)
    {
        // เช็คว่าคนที่ชนใช่ผู้เล่นไหม (ป้องกันพวกกล่องหรือของหล่นมาชนแล้ว UI เด้ง)
        if (other.CompareTag(playerTag))
        {
            if (uiObject != null)
            {
                uiObject.SetActive(true); // สั่งเปิด (Show)
                Debug.Log("Open UI");
            }
        }
    }

    // เมื่อเดินออกจากโซน
    void OnTriggerExit(Collider other)
    {
        // ถ้าตั้งค่าให้ปิดตอนออก + คนที่เดินออกคือผู้เล่น
        if (closeOnExit && other.CompareTag(playerTag))
        {
            if (uiObject != null)
            {
                uiObject.SetActive(false); // สั่งปิด (Hide)
            }
        }
    }
}