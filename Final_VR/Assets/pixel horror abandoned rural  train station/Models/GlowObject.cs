using UnityEngine;

public class GlowObject : MonoBehaviour
{
    [Header("Settings")]
    public Color glowColor = Color.yellow;
    public float glowIntensity = 2f; // ความแรงแสง

    private Material objectMat;
    private Color originalColor;
    private Color originalEmission;

    void Start()
    {
        // จำค่าสีเดิมไว้
        objectMat = GetComponent<Renderer>().material;
        originalColor = objectMat.color;

        // เปิดระบบ Emission ของ Material (ถ้ายังไม่เปิด)
        objectMat.EnableKeyword("_EMISSION");
        originalEmission = objectMat.GetColor("_EmissionColor");
    }

    // เรียกฟังก์ชันนี้เมื่อมือผู้เล่นเข้าใกล้ (Hover Enter)
    public void StartGlow()
    {
        // เปลี่ยนสี Emission ให้สว่างขึ้น
        objectMat.SetColor("_EmissionColor", glowColor * glowIntensity);
    }

    // เรียกฟังก์ชันนี้เมื่อมือผู้เล่นออกห่าง (Hover Exit)
    public void StopGlow()
    {
        // กลับเป็นสีเดิม
        objectMat.SetColor("_EmissionColor", originalEmission);
    }
}