using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Animator animator;
    public GameObject MapPanel;

    private bool mapActive = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = MapPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.GamePaused)
            return;

        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.M) && !mapActive)
            {
                MapPanel.SetActive(true);
                animator.SetBool("mapOpen", true);
                animator.SetBool("mapClose", false);
                mapActive = true;
            }
            else if(Input.GetKeyDown(KeyCode.M) && mapActive)
            {
                MapPanel.SetActive(false);
                animator.SetBool("mapClose", true);
                animator.SetBool("mapOpen", false);
                mapActive = false;
            }
        }        
    }
}
