using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;

    [Header("Movement")]
    private float moveSpeed;
    private float regularMoveSpeed;

    private Vector3 moveDirection;

    private PlayerControls controls;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputsActions();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = new Vector3(verticalInput, 0f, horizontalInput);
        playerRb.linearVelocity = moveDirection;
    }

    private void GetInputsActions()
    {
        Vector2 movement = controls.Player.Move.ReadValue<Vector2>();
        horizontalInput = movement.x;
        verticalInput = movement.y;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerRb.linearVelocity = new Vector3(limitedVel.x, playerRb.linearVelocity.y, limitedVel.z);
        }
    }
}
