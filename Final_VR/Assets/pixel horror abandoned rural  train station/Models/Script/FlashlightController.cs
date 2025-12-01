/*using Oculus.Interaction;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // ต้องมี XR Toolkit

public class FlashlightController : MonoBehaviour
{
    public Light spotlight; // ลากตัว Spotlight ในลูกไฟฉายมาใส่
    public AudioClip clickSound; // เสียง 'คลิก' (ถ้ามี)
    private AudioSource audioSource;
    private bool isOn = true;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnEnable()
    {
        // เชื่อมต่อปุ่ม Activate (ปุ่ม Trigger ปกติ)
        grabInteractable.activated.AddListener(ToggleLight);
    }

    void OnDisable()
    {
        grabInteractable.activated.RemoveListener(ToggleLight);
    }

    void ToggleLight(ActivateEventArgs args)
    {
        isOn = !isOn;
        if (spotlight != null) spotlight.enabled = isOn;

        if (clickSound != null) audioSource.PlayOneShot(clickSound);
    }
}*/