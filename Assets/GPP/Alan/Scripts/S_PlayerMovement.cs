using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    public float _moveSpeed = 10.0f;
    public Rigidbody _rigidbody = null;
    public Vector3 _movement = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        MovePlayer(_movement);
    }

    private void MovePlayer(Vector3 direction)
    {
        _rigidbody.velocity = direction * _moveSpeed * Time.fixedDeltaTime;
    }
}
