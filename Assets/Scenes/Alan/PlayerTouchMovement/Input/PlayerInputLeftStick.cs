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

    // [Header("Always Show Left Joy Stick:")]
    // [SerializeField]
    // private bool m_ShowJoyStick = true;

    [Header("Reference to Input Action:")]
    [SerializeField]
    private InputActionReference ref_InputActionMove = null;

    [Header("JoyStick (IMG_LeftStick_BG):")]
    [SerializeField]
    private FloatingJoystick Joystick;

    // Joystick size
    [Header("Joy Stick Size:")]
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(300, 300);

    // Different events raised by touch input system
    private Finger m_MovementFinger;
    private Vector2 m_MovementAmount;

    private void OnEnable()
    {
        // Enhanced touch support provides automatic finger tracking and touch history recording.
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/api/UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.html
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    // To avoid extra callbacks
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        m_MovementFinger = TouchedFinger;
        m_MovementAmount = Vector2.zero;

        // Debug.Log("Movement Finger: " + m_MovementFinger);
        // Debug.Log("Movement Amount: " + m_MovementAmount);

        // ETouch.Touch currentTouch = TouchedFinger.currentTouch;
        // Debug.Log("Current Touch: " + currentTouch);

        // if (m_MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2.0f)
        // {
        //     m_MovementFinger = TouchedFinger;
        //     m_MovementAmount = Vector2.zero;

        //     if (!m_ShowJoyStick)
        //     {
        //         // Make the Joystick visible when player touches the screen
        //         Joystick.gameObject.SetActive(true);
        //         Joystick.RectTransform.sizeDelta = JoystickSize;
        //         Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        //     }
        // }
    }

    private void HandleFingerUp(Finger LostFinger)
    {
        if (LostFinger == m_MovementFinger)
        {
            m_MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;

            // Hides the Joystick when player's finger is not touching the screen
            // if (m_ShowJoyStick)
            // {
            //     Joystick.gameObject.SetActive(false);
            // }
            
            m_MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == m_MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2.0f;
            
            ETouch.Touch currentTouch = MovedFinger.currentTouch;
            // Debug.Log("Current Touch: " + currentTouch);

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;

                Debug.Log("Knob Position: " + knobPosition);
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            m_MovementAmount = knobPosition / maxMovement;
        }
    }

    // Set the joystick position to where player touches the screen
    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < JoystickSize.x / 2)
        {
            StartPosition.x = JoystickSize.x / 2;
        }

        if (StartPosition.y < JoystickSize.y / 2)
        {
            StartPosition.y = JoystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - JoystickSize.y / 2)
        {
            StartPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return StartPosition;
    }
}
