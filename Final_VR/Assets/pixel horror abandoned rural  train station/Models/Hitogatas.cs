using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerHead; // ลาก Camera (Main Camera) มาใส่ที่นี่
    public bool lookAtPlayer = true; // อยากให้หันตามไหม
    public float fadeSpeed = 0.5f; // ความเร็วในการจางหาย

    private Renderer ghostRenderer;

    void Start()
    {
        ghostRenderer = GetComponent<Renderer>();
        // ถ้าหาคนเล่นไม่เจอ ให้หาอัตโนมัติจาก Tag "MainCamera"
        if (playerHead == null && Camera.main != null)
        {
            playerHead = Camera.main.transform;
        }
    }

    void Update()
    {
        // ให้ผีหันหน้ามองผู้เล่นตลอดเวลา (เฉพาะแกน Y ไม่ก้มเงย)
        if (lookAtPlayer && playerHead != null)
        {
            Vector3 targetPosition = new Vector3(playerHead.position.x, transform.position.y, playerHead.position.z);
            transform.LookAt(targetPosition);
        }
    }

    // ฟังก์ชันนี้เอาไว้เรียกใช้ตอน "ไขกล่องสำเร็จ"
    public void FadeAway()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        // ดึงค่าสีปัจจุบันมา
        Color originalColor = ghostRenderer.material.color;
        float alpha = originalColor.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            // ปรับค่า Alpha (ความโปร่งใส) ลดลงเรื่อยๆ
            ghostRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // พอจางสุดแล้ว ให้ปิดการทำงานไปเลย
        gameObject.SetActive(false);
    }
}