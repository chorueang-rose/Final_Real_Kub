using UnityEngine;
using UnityEngine.UI; // จำเป็นสำหรับการคุมข้อความ

public class StoryDisplay : MonoBehaviour
{
    public Text uiText; // ลากตัว Text มาใส่ที่นี่
    public float showTime = 10f; // โชว์กี่วินาทีแล้วหายไป

    private void Start()
    {
        // เริ่มเกมมา ให้เคลียร์ข้อความก่อน
        if(uiText != null) uiText.text = "";
    }

    // ฟังก์ชันนี้จะถูกเรียกโดยกล่อง
    public void ShowStory(string message)
    {
        if (uiText != null)
        {
            uiText.text = message; // เปลี่ยนข้อความ
            
            // ยกเลิกการนับเวลาเก่า (ถ้ามี) แล้วเริ่มนับถอยหลังใหม่เพื่อลบข้อความ
            CancelInvoke("ClearText");
            Invoke("ClearText", showTime);
        }
    }

    void ClearText()
    {
        if(uiText != null) uiText.text = "";
    }
}