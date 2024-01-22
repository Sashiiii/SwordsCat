using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private enum State { 
        Idle,
        Moving,
        Rolling,
    }

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LayerMask dashLayerMask;
    [SerializeField] private int playerSpriteLayer;

    [SerializeField] private Dummy dummyShouldBeRemoved;

    private Rigidbody2D rb;
    private InputSystem input;
    private Animator animator;
    private WeaponParent weaponParent;

    private Vector3 moveDir;
    private Vector3 rollDir;
    private float rollSpeed;
    private bool isDashButtonDown;
    private State state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputSystem>();
        animator = GetComponentInChildren<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        state = State.Idle;

        SpriteRenderer spriteRenderer = GetComponentsInChildren<SpriteRenderer>().FirstOrDefault();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = playerSpriteLayer;
        }
    }
    
    private void Start()
    {
        weaponParent.Enable(playerSpriteLayer);
    }

    private void Update() {
        bool attack = input.GetAttack();
        if (attack)
        {
            IDamageable damageable = dummyShouldBeRemoved;
            damageable.Damage(10);
        }

        moveDir = input.GetMovementInput();
        if (moveDir.x != 0 || moveDir.y != 0)
        {
            state = State.Moving;
        }
        else
        {
            state = State.Idle;
        }

        Vector2 direction = (input.GetPointerPosition() - (Vector2)transform.position).normalized;
        weaponParent.UpdateDirection(direction);

        animator.SetFloat("Horizontal", Mathf.Clamp(direction.x, -1, 1));
        animator.SetFloat("Vertical", Mathf.Clamp(direction.y, -1, 1));

        switch (state) {
            case State.Idle:
                animator.SetFloat("Speed", 1);
                animator.SetBool("Moving", false);
                break;
            case State.Moving:
                if (direction.x > 0)
                {
                    animator.SetFloat("Speed", 1);
                }
                else if (direction.x < 0)
                {
                    animator.SetFloat("Speed", -1);
                }
                animator.SetBool("Moving", true);

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
                    state = State.Idle;
                }
                break;
        }
    }

    private void FixedUpdate() {
        switch (state) {
            case State.Idle:
                rb.velocity = Vector2.zero;
                break;
            case State.Moving:
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
