using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    // Start is called before the first frame update
   private void Awake(){
    inputManager = GetComponent<InputManager>();
    playerLocomotion = GetComponent<playerLocomotion>();
   } 
}
