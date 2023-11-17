using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_CameraTransition : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform camPos1;
    [SerializeField] private Transform camPos2;
    [SerializeField] private Transform playerPos1;
    [SerializeField] private Transform playerPos2;

    [Header("Smooth Damp")]
    [SerializeField] private float smoothDampAccuracy = 0.2f;
    [SerializeField] private float movementSmoothTime = 0.2f;
    private Vector3 targetPosition;
    private Vector3 moveVelocity;

    private bool canMove = false;

    [Space]
    [TextArea]
    public string notes = " - Tag Player sur le joueur\n - Tag Main Camera sur la caméra\n - Le collider doit être trigger";


    private void Start()
    {
        canMove = false;
        targetPosition = camPos1.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (targetPosition == camPos1.position) {
                targetPosition = camPos2.position;
                canMove = true;
                other.transform.position = playerPos2.position;
            }
            else if (targetPosition == camPos2.position)
            {
                targetPosition = camPos1.position;
                canMove = true;
                other.transform.position = playerPos1.position;
            }
        }
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (canMove)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) <= smoothDampAccuracy) canMove = false;
        }
    }
}
