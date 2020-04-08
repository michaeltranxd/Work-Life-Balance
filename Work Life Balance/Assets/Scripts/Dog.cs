using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    private bool walkState;
    private bool waitState;
    private GameObject attention;
    private GameObject lastAttention;
    private float Speed;
    private Animator dogAnimator;

    public GameObject[] patrolPoints;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        dogAnimator = GetComponent<Animator>();
        walkState = false;
        waitState = true;
        Speed = 5;
        attention = patrolPoints[0];
        lastAttention = patrolPoints[0];
        
    }

    // Update is called once per frame
    void Update()
    {
   
        transform.LookAt(attention.transform.position);
        transform.rotation = Quaternion.Euler(0,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        //Debug.Log(Vector3.Distance(transform.position, Player.transform.position));

        //Debug.Log(attention.name);


        if (Vector3.Distance(transform.position, Player.transform.position) < 5)
        {
            found();
        }
        else if (Vector3.Distance(transform.position, Player.transform.position) >= 5
            && attention.Equals(Player))
        {
            attention = patrolPoints[0];
            Speed = 5;
            dogAnimator.SetInteger("Walk", 0);
        }

    }

    private void found()
    {

        Speed = 0;
        attention = Player;
        dogAnimator.SetInteger("Walk", 1);

    }

    private void OnTriggerEnter(Collider other)
    {
  
        if (other.gameObject.name.Equals("Point A"))
            attention = patrolPoints[1];
        else
            attention = patrolPoints[0];
             
    }

    IEnumerator Wait() //implement later
    {
        yield return new WaitForSecondsRealtime(3);


    }
}
