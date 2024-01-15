using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    const float MOVE_SPEED = 10f;

    private enum State { 
        Normal,
        Rolling,
    }

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LayerMask dashLayerMask;

    private Rigidbody2D rb;
    private InputSystem input;

    Vector3 moveDir;
    Vector3 rollDir;
    float rollSpeed;
    bool isDashButtonDown;
    State state;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputSystem>();
        state = State.Normal;
    }
    
    void Update() {
        switch (state) {
            case State.Normal:
                moveDir = input.GetMovementInput();

                // Dash Key
                if (stats.CanDash && input.GetDash())
                {
                    isDashButtonDown = true;
                }

                // Roll Key
                if (stats.CanRoll && input.GetRoll())
                {
                    rollDir = moveDir;
                    rollSpeed = 15f;
                    state = State.Rolling;
                }
                break;
            case State.Rolling:
                float rollSpeedDropMultiplier = 4f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float rollSpeedMinimum = 1f;
                if (rollSpeed < rollSpeedMinimum) {
                    state = State.Normal;
                }
                break;
        }
        
    }

    void FixedUpdate() {
        switch (state) { 
        case State.Normal:
                rb.velocity = moveDir * MOVE_SPEED;

            if (isDashButtonDown) 
            {
                float dashAmount = 3f;
                Vector3 dashPosition = transform.position + moveDir * dashAmount;
    
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
                if (raycastHit2D.collider != null) 
                {
                    dashPosition = raycastHit2D.point;
                }

                rb.MovePosition(dashPosition);
                isDashButtonDown = false;
            }
                break;
        case State.Rolling:
                rb.velocity = rollDir * rollSpeed;
                break;
        }
    }
}
