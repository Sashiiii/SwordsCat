using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    const float MOVE_SPEED = 10f;

    private enum State { 
        Normal,
        Rolling,
    }

    [SerializeField] private LayerMask dashLayerMask;

    private Rigidbody2D rigidbody2D;
    Vector3 moveDir;
    Vector3 rollDir;
    float rollSpeed;
    bool isDashButtonDown;
    State state;


    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    
    void Update() {
        switch (state) {
            case State.Normal:
                float moveX = 0f;
                float moveY = 0f;

                // Movement Keys
                if (Input.GetKey(KeyCode.W))
                {
                    moveY = 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = 1f;
                }

                moveDir = new Vector3(moveX, moveY).normalized;

                // Dash Key
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isDashButtonDown = true;
                }

                // Roll Key
                if (Input.GetKeyDown(KeyCode.F))
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
            rigidbody2D.velocity = moveDir * MOVE_SPEED;

            if (isDashButtonDown) 
            {
                float dashAmount = 3f;
                Vector3 dashPosition = transform.position + moveDir * dashAmount;
    
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
                if (raycastHit2D.collider != null) 
                {
                    dashPosition = raycastHit2D.point;
                }

                rigidbody2D.MovePosition(dashPosition);
                isDashButtonDown = false;
            }
                break;
        case State.Rolling:
                rigidbody2D.velocity = rollDir * rollSpeed;
                break;
        }
        
    }
}
