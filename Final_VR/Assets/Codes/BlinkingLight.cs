using UnityEngine;
using System.Collections;

public class BlinkingLight : MonoBehaviour
{
    public Light targetLight; // ลากหลอดไฟมาใส่ตรงนี้
    public float blinkDuration = 0.5f; // ความเร็วในการกระพริบ (วินาที)

    // ถ้าอยากให้กระพริบแบบสุ่ม (เหมือนไฟเสีย) ให้ติ๊กถูกตรงนี้
    public bool isBrokenLight = false;

    void Start()
    {
        // ถ้าลืมลากหลอดไฟมาใส่ ให้มันหาเองในตัวมัน
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        // เริ่มทำงาน
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        while (true) // วนลูปตลอดไป
        {
            // สลับสถานะ (ถ้าเปิดก็ปิด, ถ้าปิดก็เปิด)
            targetLight.enabled = !targetLight.enabled;

            // ถ้าเป็นไฟเสีย ให้สุ่มเวลา
            if (isBrokenLight)
            {
                yield return new WaitForSeconds(Random.Range(0.05f, 0.8f));
            }
            else
            {
                // ถ้าไฟปกติ ให้รอตามเวลาที่ตั้งไว้
                yield return new WaitForSeconds(blinkDuration);
            }
        }
    }
}