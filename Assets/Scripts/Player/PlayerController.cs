using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onDeath;

    [SerializeField]
    private UnityEvent<int> onLivesChange;

    [SerializeField]
    private int playerLives = 3;

    [SerializeField]
    private float playerSpeed = 10.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float smootRotation = 5f;
    [SerializeField]
    private GunfireController gunfireController;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private Transform cameraTransform;
    private Animator animator;
    int moveXAnimationParameterId;
    int moveZAnimationParameterId;
    int jumpAnimation;

    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;

    [SerializeField]
    private float animationSmoothTime = 0.05f;

    [SerializeField]
    private float animatonPlayTransition = 0.15f;

    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private float aimDistance = 10f;

    private void Awake()
    {
        transform.position = GameManager.Instance.levelDifficulty.InitPlayerPosition;
        
        gunfireController = GetComponentInChildren<GunfireController>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        moveXAnimationParameterId = Animator.StringToHash("MoveX");
        moveZAnimationParameterId = Animator.StringToHash("MoveZ");
        jumpAnimation = Animator.StringToHash("Jump");
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void ShootGun()
    {
        gunfireController.FireWeapon();
    }

    void Update()
    {
        
        setAimDistance();
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
        animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
           // playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
           // animator.CrossFade(jumpAnimation, animatonPlayTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smootRotation);

    }


    void setAimDistance()
    {
        aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;
    }

    public void addPlayerLives()
    {
        playerLives += 1;
    }
    public void subtractPlayerLives()
    {
        playerLives -= 1;
    }
    public int getPlayerLives()
    {
        return playerLives;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            subtractPlayerLives();
            onLivesChange?.Invoke(playerLives);

            if (getPlayerLives() < 1)
            {
                onDeath?.Invoke();
                Destroy(gameObject);
                
            }
        }
    }
}