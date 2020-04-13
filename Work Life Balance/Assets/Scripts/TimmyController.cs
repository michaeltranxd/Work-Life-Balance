using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimmyController : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator anim;
    public RectTransform messagePlane;
    public Text message;
    public Stats StatsManager;

    private int counter = 0;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.transform.position, this.transform.position));
        if(Vector3.Distance(player.transform.position,this.transform.position)< 1){
            audioSource.Play();
            StartCoroutine(Wait());
        }else{
            if(counter > 1000){
            transform.rotation *= Quaternion.Euler(0,90.0f, 0);
            counter = 0;
            }
            else{
                transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * 1.0f;
            }
            counter ++;
        }
    }
    IEnumerator Wait()
    {
        anim.SetBool("isWalking", false);
        message.text = "Timmy gave you a cookie.";
        messagePlane.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        messagePlane.gameObject.SetActive(false);
        Destroy(gameObject);
        StatsManager.AddNutri(10.0f);
    }
}
