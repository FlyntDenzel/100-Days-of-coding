using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  PlayerControls playercontrols;//generating a playercontrols class
  public Vector2 movementInput;

  public float verticalInput;
  public float horizontalInput;
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

  private void HandleMovementInput(){
    verticalInput = movementInput.y;//takes the verticalinput and give it the value of the movementinput either 0 or -1
    horizontalInput = movementInput.x;
  }
}
