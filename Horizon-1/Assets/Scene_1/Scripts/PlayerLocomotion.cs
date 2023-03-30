using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Vector3 moveDirection;//direction player moves
    Transform cameraObject;
    InputManager inputManager;
    Rigidbody playerRigidbody;

    public float movementSpeed = 10f;
    public float rotationSpeed = 15f;

    public static void Awake(){
        inputManager = GetComponent<InputManager>();//calls the input manager created in the player component
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void HandleMovement(){

        moveDirection = cameraObject.forward * inputManager.verticalInput;//movement input
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;//movement input along the horizontal input based on our horizontal inputs value
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;//move our player based on previous calculation
    }

    public void HandleRotation(){
        Vector3 targetDirection = Vector3.zero;//initializes the rotation to zero when started
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);//looks towards target direction and rotate in that direction
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);//rotation between players current rotation and new rotation from pressing movement keys
        transform.rotation = playerRotation;//assign player rotation to current rotation
    }
}
