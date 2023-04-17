using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{
    int isWalkingHash;
    int isRunningHash;

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;

    void Awake(){
        playerInput = new PlayerInput();//creating instance of playerinput class
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //callback funci=tion returning the current context of the player
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput; 
             
    }

    void HandleRotation(){
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;//keeps a value of zero for y axis to prevent character from going upwards
        positionToLookAt.z = currentMovement.z; 

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);//creates a rotation based on where the player is pressing
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    void OnMovementInput(InputAction.CallbackContext context){
        currentMovementInput = context.ReadValue<Vector2>(); 
             currentMovement.x = currentMovementInput.x;
             currentMovement.z = currentMovementInput.y;//player controls movement x and z axis
             isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0; 
    }

    //have to find a way to make the rotation possible
     
    void HandleAnimation(){
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleAnimation();
        characterController.Move(currentMovement * Time.deltaTime);//passing constantly updated char movement
    }


    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }

    void OnDisable(){
        playerInput.CharacterControls.Disable();
    }
}


