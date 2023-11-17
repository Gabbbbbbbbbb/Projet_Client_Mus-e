using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveAction.ReadValue<Vector2>());
        Vector2 direction = moveAction.ReadValue<Vector2>().normalized;
        transform.position += new Vector3(direction.x, 0, direction.y)  * Time.deltaTime;
    }
}
