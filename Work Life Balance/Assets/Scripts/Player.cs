using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 8f;            // Speed of player
    public float gravity = -9.81f;      // Gravity constant for world

    bool isGrounded;                    // Boolean value describing the groundness of the player

    Vector3 velocity = new Vector3(0, 0, 0);                   // Velocity of the player
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (!isGrounded)
        {
            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
        }
        if (isGrounded && velocity.y < 0)
        {
            // Character is grounded so we do not have velocity downwards (we are not moving!)
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        // Movement Handling
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Apply movement relative to camera
        Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;

        // Remove rotation up/down direction
        move.y = 0f;

        // Move controller
        controller.Move(move * speed * Time.deltaTime);
        // Apply gravity physics to player
        controller.Move(velocity * Time.deltaTime);

        // Only rotate when moving
        if(move != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.1f);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit);
    }

    private void OnTriggerEnter(Collider other)
    {
        speed = 0f;
    }

}
