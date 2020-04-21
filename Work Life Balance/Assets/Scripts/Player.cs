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
    public GameObject minimapPanel;

    public static bool cursorShown = false;
    private static bool isFatigued = false;

    public StatManager statManager;

    public LevelLoader levelLoader;

    public GameObject bedPanel;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInControl = true;
        inEvent = false;
        playerAnimation = GetComponent<Animator>();
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;
        isFatigued = false;
        cursorShown = false;
        if (LevelLoader.LoadingSavedFile)
            LoadPlayer();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (StatManager.GameOver || DayNightController.GameWon)
        {
            showMouse();
            playerInControl = false;
        }

        if (inEvent)
            speed = 0f;
        else if (isFatigued)
            speed = 4f;
        else
            speed = maxSpeed;

        // Delete when release for beta TODO
        if (Input.GetKey(KeyCode.Alpha8))
        {
            speed = 30f;
            print("hello");
        }
        else
        {
            speed = 8f;
        }
        // End of delete


        playerGravity(); 
        playerControl();

        if (DayNightController.getDayNightController().isSleep() && playerInControl)
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

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.transform.CompareTag("Bed") && !bedPanel.activeSelf && !recapPlane.gameObject.activeSelf)
        {
            bedPanel.SetActive(true);
            showMouse();
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.transform.CompareTag("Bed") && bedPanel.activeSelf && !recapPlane.gameObject.activeSelf)
        {
            bedPanel.SetActive(false);
            hideMouse();
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

    public bool playerSkipTime(float amount)
    {
        SkipTimeEvent skipTimeEvent = new SkipTimeEvent(this, amount);
        if (skipTimeEvent.run())
            return true;
        return false;
    }

    public void handlePlayerSleep()
    {
        taskManager.checkTasks();
        
        DayNightController.freezeTime();

        recapManager.setInitialConfiguartion();

        recapPlane.gameObject.SetActive(true);
        minimapPanel.gameObject.SetActive(false);
        recapManager.startTyping();
        
        miiSoundSource.Play();
        
        playerInControl = false;

        showMouse();

        foreach (Transform child in bed.transform)
        {
            if (child.name.Equals("NextToBed"))
            {
                transform.position = child.position;
            }
        }
    }

    public void onBedSleep()
    {
        handlePlayerSleep();
        statManager.AddHygiene(-20);
        statManager.AddNutri(-20);
        statManager.AddEnergy(40);
        statManager.AddAbility(40);
    }

    public void onBedNo()
    {
        hideMouse();
    }

    public void teleportToSleep()
    {
        handlePlayerSleep();

        statManager.AddEnergy(20);
        statManager.AddAbility(20);
        statManager.AddHygiene(-30);
        statManager.AddNutri(-30);

    }

    public void startOfNewDay()
    {
        SavePlayer();
        playerInControl = true;
        hideMouse();
    }

    public void stopMiiMusic()
    {
        miiSoundSource.Stop();
        minimapPanel.gameObject.SetActive(true);
    }

    public static void showMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorShown = true;
    }

    public static void hideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorShown = false;
    }

    public static void fatigued()
    {
        isFatigued = true;
    }
    public static void noFatigued()
    {
        isFatigued = false;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this, PlayerCamera, statManager, DayNightController.getDayNightController());
    }

    public void SavePlayerAndMainMenu()
    {
        SavePlayer();
        levelLoader.LoadLevel(0); // Main menu
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }
}
