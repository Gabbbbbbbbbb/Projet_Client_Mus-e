using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class S_PlayerControllerAnimation : MonoBehaviour
{
    private Rigidbody m_rigidbody = null;
    private Animator m_animator = null;
    private Vector3 m_playerMoveInput  = Vector3.zero;

    [SerializeField] private S_LeftStick m_leftStick;
    [SerializeField] private float m_moveSpeed;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = new Vector3(m_leftStick.Horizontal * m_moveSpeed, m_rigidbody.velocity.y, m_leftStick.Vertical * m_moveSpeed);

        if (m_leftStick.Horizontal != 0 || m_leftStick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(m_rigidbody.velocity);
        }

        m_animator.SetFloat("Horizontal", m_leftStick.Horizontal);
        m_animator.SetFloat("Vertical", m_leftStick.Vertical);
    }

    private void MovePlayer()
    {
        m_playerMoveInput = new Vector3(m_playerMoveInput.x * m_moveSpeed * m_rigidbody.mass, 
                                        m_playerMoveInput.y, 
                                        m_playerMoveInput.z * m_moveSpeed * m_rigidbody.mass);
    }
}
