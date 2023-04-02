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
        bool isWalking = Input.GetBool(isWalkingHash);
        bool isRunning = Input.GetBool(isRunningHash);
    }
}
