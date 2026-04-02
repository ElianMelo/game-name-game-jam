using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [SerializeField] private float moveSpeed;
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
        GetInputsActions();
        SpeedControl();
        if(moveDirection != Vector3.zero)
            transformVisual.forward = moveDirection;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;
        moveDirection.y = 0f;
        playerRb.linearVelocity = moveDirection * moveSpeed;
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

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerRb.linearVelocity = new Vector3(limitedVel.x, playerRb.linearVelocity.y, limitedVel.z);
        }
    }
}
