using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [SerializeField] private InputActionReference move;
    [SerializeField] private Transform transformVisual;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        GetInputsActions();
        SpeedControl();
        if(moveDirection != Vector3.zero)
            transformVisual.forward = moveDirection;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        Move();
    }

    private void Move()
    {
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        moveDirection.y = 0f;
        playerRb.linearVelocity = moveDirection * KaijuUpgradeManager.Instance.Speed;
    }

    private void GetInputsActions()
    {
        Vector2 movement = move.action.ReadValue<Vector2>();
        horizontalInput = movement.y * -1;
        verticalInput = movement.x;
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
}
