using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private enum State { 
        Normal,
        Rolling,
    }

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LayerMask dashLayerMask;
    [SerializeField] private int playerSpriteLayer;

    private Rigidbody2D rb;
    private InputSystem input;
    private WeaponParent weaponParent;

    private Vector3 moveDir;
    private Vector3 rollDir;
    private float rollSpeed;
    private bool isDashButtonDown;
    private State state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputSystem>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        state = State.Normal;

        SpriteRenderer spriteRenderer = GetComponentsInChildren<SpriteRenderer>().FirstOrDefault();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = playerSpriteLayer;
        }
    }
    
    private void Start()
    {
        weaponParent.Initialize(input, playerSpriteLayer);
    }

    private void Update() {
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

    private void FixedUpdate() {
        switch (state) { 
            case State.Normal:
                rb.velocity = moveDir * stats.MoveSpeed;

                if (isDashButtonDown) {
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
