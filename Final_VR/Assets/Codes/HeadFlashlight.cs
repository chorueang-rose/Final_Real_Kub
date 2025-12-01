using UnityEngine;

public class HeadFlashlight : MonoBehaviour
{
    public Light myLight; // ลากตัว Spotlight มาใส่ตรงนี้
    public AudioSource clickSound; // (Optional) ใส่เสียงคลิก

    void Start()
    {
        // เริ่มเกมมา สั่งปิดไฟก่อน (หรือจะเปิดไว้เลยก็ได้)
        myLight.enabled = false;
    }

    void Update()
    {
        // เช็คว่ากดปุ่ม "A" หรือ "X" บนจอยไหม (OVRInput.Button.One คือปุ่ม A หรือ X แล้วแต่ข้าง)
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        myLight.enabled = !myLight.enabled; // สลับสถานะ (เปิด->ปิด, ปิด->เปิด)

        // ถ้ามีเสียง ให้เล่นเสียงด้วย
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}