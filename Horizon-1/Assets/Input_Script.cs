using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Script : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController charactercontroller;//setting a variable of type charactercontroller
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currRunMovement;

    bool IsMovementPressed;
    bool IsRunPressed;
    float rotationframe = 15.0f;
    public float runvalue = 3.0f;
    
    void Awake(){
        playerInput = new PlayerInput();//creating an instance of the playerinput class
        charactercontroller = GetComponent<CharacterController>();//getting access by passing the charactercontroller as component
        animator = GetComponent<Animator>();
        //setting callback function when key is being pressed
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        
    }

    void OnRun(InputAction.CallbackContext context){
        IsRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context){
         currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            currRunMovement.x = currentMovementInput.x * runvalue;
            currRunMovement.z = currentMovementInput.y * runvalue;
            IsMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void Gravity(){
        if (charactercontroller.isGrounded)
        {
            float grounded = -0.05f;
            currentMovement.y = grounded;
            currRunMovement.y = grounded;
        }
        else {
            float gravity =  -9.8f;
            currentMovement.y += gravity;
            currRunMovement.y += gravity; 
        }
    }

    void Animation(){
        bool IsWalking = animator.GetBool("IsWalking");
        bool IsRunning = animator.GetBool("IsRunning");

        if (IsMovementPressed && !IsWalking)
        {
            animator.SetBool("IsWalking", true);
        }

        else if (!IsMovementPressed && IsWalking)
        {
            animator.SetBool("IsWalking", false);
            
        }

        if ((IsMovementPressed && IsRunPressed) && !IsRunning)
        {
            animator.SetBool("IsRunning", true);
        }

        else if (!(IsMovementPressed || IsRunPressed) && IsRunning)
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void Rotation(){
        Vector3 LookAt;
        LookAt.x = currentMovement.x;
        LookAt.y = 0f;
        LookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (IsMovementPressed)
        {
        Quaternion targetrotation = Quaternion.LookRotation(LookAt);
        transform.rotation = Quaternion.Slerp(currentRotation, targetrotation, rotationframe * Time.deltaTime);
            
        }
    }

    void OnEnable(){
        playerInput.CharacterControls.Enable();
    }

    void OnDisable(){
        playerInput.CharacterControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Rotation();
        Animation();
        charactercontroller.Move(currentMovement * Time.deltaTime);
        if (IsRunPressed)
        {
            charactercontroller.Move(currRunMovement * Time.deltaTime);
        }
        else{
            charactercontroller.Move(currentMovement * Time.deltaTime);
        }
    }
}
