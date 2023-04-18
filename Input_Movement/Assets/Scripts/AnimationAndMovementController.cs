using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;

    Vector2 currentMovementInput;
    CharacterController characterController;

    //creates a 3d vector for movement
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isRunPressed;
    bool isMovementPressed;
    //since character doesnt jump when game is started
    bool isJumpPressed = false;

    float initialJumpVelocity;
    float maxJumpHeight;
    float maxJumpTime;
    float rotationFactorPerFrame = 8.0f;
    float runSpeed = 5.0f;
    float gravity = -9.8f;

    void Awake() {
        playerInput = new PlayerInput();//instancing playerinput class   
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //enables the input action
        playerInput.CharacterControls.Move.started += OnMovementInput;
        //stops the input action
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        //provides values between 0 and 1
        playerInput.CharacterControls.Move.performed += OnMovementInput;

        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        
    }

    void OnRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();//stores button value of run action
    }

    void OnJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed);
    }

    void OnMovementInput(InputAction.CallbackContext context){
        //stores the wasd movement values 
            currentMovementInput = context.ReadValue<Vector2>();
            //assign the movement values to the various axis being used
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            currentRunMovement.x = currentMovementInput.x * runSpeed;
            currentRunMovement.z = currentMovementInput.y * runSpeed;

            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y !=0 ;
    }

    void handleAnimation(){
        //gets parameter from value animator
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

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }

        if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void HandleGravity(){
        //character controller considers itself "floating" or "jumping" when a value of zero is applied to the y axis 
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }

        else {
            currentMovement.y += gravity;
            currentRunMovement.y += gravity; 
        }
    }

    void handleRotation(){
        Vector3 positionToLookAt;
        //change in position in which our character should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0;//zero because we dont need the character to face upwards
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //creates a new rotation based on where the character is pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
         
    } 

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        handleRotation();

        if (isRunPressed)
        {
            //enables running animation through run action
            characterController.Move(currentRunMovement * Time.deltaTime);
        }

        else {
        //enables the characterontroller through the currentmovement
        characterController.Move(currentMovement * Time.deltaTime);   
        }

        HandleGravity();
    }

    void OnEnable() {
        //enables the callback function
        playerInput.CharacterControls.Enable();    
    }

     void OnDisable() {
        //disable the callback function
        playerInput.CharacterControls.Disable();    
    }
}
