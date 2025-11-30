using UnityEngine;
using UnityEngine.Events;

public class StoryBoxLock : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("ใส่ Tag ของกุญแจที่นี่ (เช่น Key)")]
    public string keyTag = "Key"; 
    
    [Tooltip("กล่องล็อกอยู่ใช่ไหม?")]
    public bool isLocked = true;

    [Header("Animation")]
    public Animator boxAnimator; // ลาก Animator ของกล่องมาใส่ (ถ้ามี)
    public string openTriggerName = "Open"; // ชื่อ Parameter ใน Animator

    [Header("Events (ลากวางคำสั่งที่นี่)")]
    // ตรงนี้คือจุดเด่น: คุณสามารถลาก Sound, Particle, หรือ Script อื่นๆ มาใส่ใน Inspector ได้เลย
    public UnityEvent onUnlock; 

    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าของที่มาชนคือ "กุญแจ" และ "กล่องยังล็อกอยู่"
        if (isLocked && other.CompareTag(keyTag))
        {
            UnlockBox(other.gameObject);
        }
    }

    void UnlockBox(GameObject keyObj)
    {
        isLocked = false;
        Debug.Log("Box Unlocked!");

        // 1. เล่น Animation เปิดกล่อง (ถ้ามี)
        if (boxAnimator != null)
        {
            boxAnimator.SetTrigger(openTriggerName);
        }

        // 2. เรียก Events ที่ตั้งค่าไว้ (เช่น เล่นเสียง, แสดงของข้างใน)
        onUnlock.Invoke();

        // 3. (Optional) ซ่อนกุญแจ หรือ ทำลายกุญแจทิ้งหลังจากไขเสร็จ
        // Destroy(keyObj); // เอา Comment ออกถ้าอยากให้กุญแจหายไปเลย
        
        // ทำให้กุญแจหลุดจากมือ (ใน VR บางระบบอาจต้องใช้คำสั่งเฉพาะ แต่เบื้องต้น Disable ไปก่อนได้)
        // keyObj.SetActive(false); 
    }
}
