using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Animator animator;
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
    }

}
