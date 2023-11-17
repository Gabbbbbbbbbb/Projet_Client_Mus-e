using UnityEngine;

public class PlayerControllerLeftStick : MonoBehaviour
{
    Rigidbody m_RigidBody = null;

    [SerializeField]
    PlayerInputLeftStick m_PlayerInput;

    Vector3 m_PlayerMoveInput = Vector3.zero;

    [Header("Movement")]
    [SerializeField] float m_MovementMultiplier = 30.0f;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_PlayerMoveInput = GetMoveInput();

        MovePlayer();

        m_RigidBody.AddRelativeForce(m_PlayerMoveInput, ForceMode.Force);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(m_PlayerInput.MoveInput.x, 0.0f, m_PlayerInput.MoveInput.y);
    }

    private void MovePlayer()
    {
        m_PlayerMoveInput = new Vector3(m_PlayerMoveInput.x * m_MovementMultiplier * m_RigidBody.mass, 
                                        m_PlayerMoveInput.y, 
                                        m_PlayerMoveInput.z * m_MovementMultiplier * m_RigidBody.mass);
    }
   
}
