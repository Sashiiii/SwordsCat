using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private Rigidbody2D rigidbody2D;
    Vector3 moveDir;
    const float speed = 10f;

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update() {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W)) {
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
    }

    void FixedUpdate() {
        rigidbody2D.velocity = moveDir * speed;
    }
}
