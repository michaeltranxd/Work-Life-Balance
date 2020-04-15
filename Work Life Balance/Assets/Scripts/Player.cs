using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    private float speed = 0f;            // Speed of player
    private float maxSpeed = 8f;         // Max speed of player
    private float gravity = -9.81f;      // Gravity constant for world

    bool isGrounded;                    // Boolean value describing the groundness of the player
    bool inEvent;                       // Boolean value describing if player is currently in event
    public bool playerInControl               // Boolean value describing if player can control character
    { get; set; }
    public Camera PlayerCamera;
    public GameObject bed;              // Bed at which player starts from

    Vector3 velocity = new Vector3(0, 0, 0);                   // Velocity of the player

    Animator playerAnimation;         // Animation of player
    public RectTransform recapPlane;
    public Recap recapManager;
    public TaskManager taskManager;

    public AudioSource miiSoundSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInControl = true;
        inEvent = false;
        playerAnimation = GetComponent<Animator>();
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (inEvent)
            speed = 0f;
        else
            speed = maxSpeed;

        playerGravity();
        playerControl();

        if (DayNightController.getDayNightController().isPastSleep())
        {
            teleportToSleep();
        }
    }

    /* 
     * Function handling gravity of player
     */
    private void playerGravity()
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
        // Apply gravity physics to player
        controller.Move(velocity * Time.deltaTime);
    }

    /* 
     * Player Controls
     */
    private void playerControl()
    {

        // Movement Handling
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Apply movement relative to camera
        Vector3 move = PlayerCamera.transform.right * x + PlayerCamera.transform.forward * z;

        // Remove rotation up/down direction
        move.y = 0f;
        if (playerInControl)
        {
            // Move controller
            controller.Move(move * speed * Time.deltaTime);

            // Only rotate when moving
            if (move != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.1f);
        }
        if (move == Vector3.zero || !playerInControl)
            playerAnimation.SetBool("IsWalking", false);
        else
            playerAnimation.SetBool("IsWalking", true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Bed") && DayNightController.CanSkipNighttime())
        {
            print("On Bed");
            // Add prompt for user to decide if to sleep

            // If "yes" to sleep, run the following code:
            handlePlayerSleep();
            // Remove above code when we add dialogue for the bed
        }
    }

    // Function to signify an event beginning, most likely dialogue so we stop player movement
    // To be used only by Event class
    public void eventBegin()
    {
        inEvent = true;
        playerInControl = false;
    }

    public void eventEnded()
    {
        inEvent = false;
        playerInControl = true;
    }

    public void playerSkipTime(float amount)
    {
        SkipTimeEvent skipTimeEvent = new SkipTimeEvent(this, amount);
        skipTimeEvent.run();
    }

    public void handlePlayerSleep()
    {
        taskManager.checkTasks();
        DayNightController.freezeTime();

        recapPlane.gameObject.SetActive(true);
        recapManager.startTyping();
        
        miiSoundSource.Play();
        
        playerInControl = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void teleportToSleep()
    {
        handlePlayerSleep();

        foreach (Transform child in bed.transform)
        { 
            if (child.name.Equals("NextToBed"))
            {
                transform.position = child.position;
            }
        }
    }

    public void startOfNewDay()
    {
        playerInControl = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void stopMiiMusic()
    {
        miiSoundSource.Stop();
    }
}
