using UnityEngine;

public class MemoryItem : MonoBehaviour
{
    [TextArea]
    public string storyText; // ข้อความที่จะโชว์ เช่น "รูปถ่ายวันแต่งงาน... เขาไม่เคยมา"
    public AudioSource voiceOver; // เสียงพูดในหัว

    // ใช้ Event นี้เชื่อมกับ XR Grab Interactable Event "On Select Entered"
    public void OnInspect()
    {
        Debug.Log("Memory: " + storyText);
        
        // เล่นเสียงบรรยาย
        if(voiceOver != null) voiceOver.Play();
        
        // TODO: ถ้ามีระบบ UI ให้ส่งข้อความ storyText ไปขึ้นบนจอ
    }
}