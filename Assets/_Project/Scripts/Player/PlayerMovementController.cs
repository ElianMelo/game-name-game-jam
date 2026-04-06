using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerController playerController;
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [SerializeField] private InputActionReference move;
    [SerializeField] private Transform transformVisual;
    [SerializeField] private float baseSmoothTime = 0.1f;

    private float currentSmoothTime;

    private Vector3 velocity = Vector3.zero;

    private const string MovingAnim = "Moving";

    void Start()
    {
        currentSmoothTime = baseSmoothTime;
        playerAnimator = GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl)
        {
            playerAnimator.SetBool(MovingAnim, false);
            return;
        }
        GetInputsActions();
        SpeedControl();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        Move();
    }

    private void Move()
    {
        if (playerController.CurrentState == PlayerState.Attacking || playerController.CurrentState == PlayerState.UsingSkill)
        {
            playerRb.linearVelocity = Vector3.zero;
            return;
        }
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        moveDirection.y = 0f;
        playerRb.linearVelocity = moveDirection * KaijuUpgradeManager.Instance.Speed;
    }

    private void GetInputsActions()
    {
        Vector2 movement = move.action.ReadValue<Vector2>();
        horizontalInput = movement.y * -1;
        verticalInput = movement.x;
        playerAnimator.SetBool(MovingAnim, movement.x != 0 || movement.y != 0);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);

        if (flatVel.magnitude > KaijuUpgradeManager.Instance.Speed)
        {
            Vector3 limitedVel = flatVel.normalized * KaijuUpgradeManager.Instance.Speed;
            playerRb.linearVelocity = new Vector3(limitedVel.x, playerRb.linearVelocity.y, limitedVel.z);
        }
    }
    private void RotatePlayer()
    {
        float angle = Vector3.Angle(transformVisual.forward, moveDirection);
        currentSmoothTime = angle > 130 ? baseSmoothTime * 5 : baseSmoothTime;
        if (moveDirection != Vector3.zero)
            transformVisual.forward = Vector3.Lerp(transformVisual.forward, moveDirection, currentSmoothTime * Time.deltaTime);
    }        
}
