using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]

public class LeftJoyStick : MonoBehaviour
{
    // [HideInInspector]
    public RectTransform RectTransform;
    public RectTransform Knob;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

        Debug.Log(RectTransform.anchoredPosition);
    }
}
