using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//serves to store persistent data passed to all concrete states
public class PlayerStateMachine : MonoBehaviour
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
    bool isJumping = false;

    float initialJumpVelocity;
    public float maxJumpHeight = 10.0f;
    public float maxJumpTime = 5.0f;
    float rotationFactorPerFrame = 8.0f;
    float runSpeed = 5.0f;
    float gravity = -9.8f;
    float groundedGravity = -0.5f;

    PlayerBaseState currentState;
    PlayerStateFactory1 states;
    
    public PlayerBaseState CurrentState{get {return currentState;} set {currentState = value;}}
    public bool IsjumpPressed { get {return isJumpPressed;}}

    //declaring a getter setter nethods
    void Awake() {
        playerInput = new PlayerInput();//instancing playerinput class   
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        states = new PlayerStateFactory1(this);
        currentState = states.Grounded();
        currentState.EnterState();


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

        SetupJumpAnimation();
    }

    void SetupJumpAnimation(){
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //getting all callback functions

    // Update is called once per frame
    void Update()
    {
         handleRotation();
         characterController.Move(currentRunMovement * Time.deltaTime);
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
    
      void OnEnable() {
        //enables the callback function
        playerInput.CharacterControls.Enable();    
    }

     void OnDisable() {
        //disable the callback function
        playerInput.CharacterControls.Disable();    
    }
}
