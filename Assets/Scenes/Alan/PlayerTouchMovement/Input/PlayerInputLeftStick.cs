using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

// To avoid conflicts with the class Touch
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputLeftStick : MonoBehaviour
{
    // Accessible for getting values, but private for setting values
    // Initialy has value of zero
    public Vector2 MoveInput {get; private set;} = Vector2.zero;

    [Header("Show Left Joy Stick:")]
    [SerializeField]
    private bool m_ActivateJoyStick = true;

    [Header("Reference to Input Action:")]
    [SerializeField]
    private InputActionReference ref_InputActionMove = null;

    // Different events raised by touch input system
    private Finger m_MovementFinger;
    private Vector2 m_MovementAmount;

    private void OnEnable()
    {
        // Enhanced touch support provides automatic finger tracking and touch history recording.
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/api/UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.html
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        // ETouch.Touch.onFingerUp += HandleFingerUp;
        // ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    // To avoid extra callbacks
    private void OnDisable()
    {
        // ETouch.Touch.onFingerDown -= HandleFingerDown;
        // ETouch.Touch.onFingerUp -= HandleFingerUp;
        // ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (m_MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            m_MovementFinger = TouchedFinger;
            m_MovementAmount = Vector2.zero;

            // Make the Joystick visible when player touches the screen
            if (m_ActivateJoyStick)
            {
                // Joystick.gameObject.SetActive(true);
                // Joystick.RectTransform.sizeDelta = JoystickSize;
                // Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            }            
        }
    }
}
