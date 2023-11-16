using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

// To avoid conflicts with the class Touch
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    // Input Action reference
    [SerializeField]
    private InputActionReference moveAction;

    // Joystick size
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(200, 200);

    // Reference to Floating Joystick object
    [SerializeField]
    private FloatingJoystick Joystick;

    // Reference to player
    [SerializeField]
    private GameObject Player;

    [SerializeField] private float smoothTime;

    // For different events raised by touch input system
    private Finger MovementFinger;
    private Vector2 MovementAmount;
    private Rigidbody playerRB;
    private Vector2 smoothInputSmoothVelocity;
    private Vector2 currentMoveInput;
    private Vector2 moveDirection;
    public float moveSpeed;
    
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/api/UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.html
    private void OnEnable()
    {
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

     private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2.0f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }
    
    private void HandleFingerUp(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;

            // Hides the Joystick when player's finger is not touching the screen
            // Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;

            // Make the Joystick visible where player touches the screen
            // Joystick.gameObject.SetActive(true);
            // Joystick.RectTransform.sizeDelta = JoystickSize;
            // Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    // TODO: Add smooth movement feature with rigidbody velocity
    private void Update()
    {
        Vector2 joystickMove = moveAction.action.ReadValue<Vector2>().normalized;

        // Debug.Log(joystickMove);

        Vector3 playerMovement = moveSpeed * Time.deltaTime * new Vector3(
            joystickMove.x,
            0,
            joystickMove.y    
        );

        // Debug.Log(playerMovement);

        Player.transform.LookAt(Player.transform.position + playerMovement, Vector3.up);
        Player.transform.position += playerMovement.magnitude * moveSpeed * Time.deltaTime * transform.forward;

        // currentMoveInput = Vector2.SmoothDamp(currentMoveInput, moveDirection, ref smoothInputSmoothVelocity, smoothTime);
        // playerRB.velocity = new Vector2(playerMovement.x * moveSpeed, playerMovement.y * moveSpeed);
        
        // Player.transform.position += transform.forward * speed * Time.deltaTime * playerMovement.magnitude;
        // Re-order operands for better performance
        // Player.transform.position += playerMovement.magnitude * moveSpeed * Time.deltaTime * transform.forward;
    }

    private void FixedUpdate()
    {
        // currentMoveInput = Vector2.SmoothDamp(currentMoveInput, moveDirection, ref smoothInputSmoothVelocity, smoothTime);
        // playerRB.velocity = new Vector2(currentMoveInput.x * moveSpeed, currentMoveInput.y * moveSpeed);
    }
}
