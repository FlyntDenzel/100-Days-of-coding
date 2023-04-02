using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    void Awake(){
        input = new PlayerInput();//creating instance of playerinput class
        input.CharacterControls.Movement.performed += ctx => Debug.log(ctx.ReadValueAsObject());//callback funci=tion returning the current context of the player
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleMovement(){
        //getting parameter values from animator
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
    }

    void OnEnable(){
        input.CharacterControls.Enable();
    }

    void OnDisable(){
        input.CharacterControls.Disable();
    }
}
