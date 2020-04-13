using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Animator animator;

    private bool mapActive = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            if (Input.GetKeyDown(KeyCode.M) && !mapActive)
            {
                animator.SetBool("mapOpen", true);
                animator.SetBool("mapClose", false);
                mapActive = true;
            }
            else if(Input.GetKeyDown(KeyCode.M) && mapActive)
            {
                animator.SetBool("mapClose", true);
                animator.SetBool("mapOpen", false);
                mapActive = false;
            }
        }        
    }
}
