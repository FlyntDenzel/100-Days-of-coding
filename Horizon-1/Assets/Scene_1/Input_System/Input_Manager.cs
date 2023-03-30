using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
  PlayerControls playercontrols;//generating a playercontrols class
  public Vector2 movementInput;

  void OnEnable(){
    if (playercontrols == null)
    {
        playercontrols = new PlayerControls();//creating an instance of a playercontrols
        playercontrols.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();//callback function
    }

    playercontrols.Enable();
  }

  void OnDisable(){
    playercontrols.Disable();
  }
}
