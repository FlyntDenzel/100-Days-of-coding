using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
  PlayerControls playercontrols;//generating a playercontrols class
  Vector2 movementInput;

  void OnEnable(){
    if (playercontrols == null)
    {
        playercontrols = new PlayerControls();//creating an instance of a playercontrold
    }
  }
}
