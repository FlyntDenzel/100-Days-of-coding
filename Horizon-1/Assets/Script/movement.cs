using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Animator animator;
    Vector2 curr_movement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool walk = Input.GetKey("w");
        bool run = Input.GetKey("left shift");
        if (walk)
        {
            animator.SetBool("IsWalking", true);
        }

        if (!walk)
        {
            animator.SetBool("IsWalking", false);
        }

        if (walk && run)
        {
            animator.SetBool("IsRunning", true);
        }

        if (!(walk && run))
        {
            animator.SetBool("IsRunning", false);
        }

        // HandleRotation();
    }

    // void HandleRotation(){
    //     Vector3 current_position = transform.position;
    //     Vector3 new_position = new Vector3(curr_movement.x,0,curr_movement.y);

    //     Vector3 lookatPosiiton = current_position + new_position; 
    //     transform.LookAt(lookatPosiiton);
    // }

}
