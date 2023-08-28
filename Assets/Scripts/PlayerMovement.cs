using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    bool IsMoving {
        set{
            isMoving = value;
            anim.SetBool("isMoving", isMoving);
        }
    }
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    Vector2 movementInput = Vector2.zero;

    public float moveSpeed = 200f;
    public float maxSpeed = 7f;
    public float idleFriction = 0.9f;

    bool canMove = true;
    bool isMoving = false;

    public GameObject swordHitBox;
    Collider2D swordCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        swordCollider = swordHitBox.GetComponent<Collider2D>();
    }

    private void FixedUpdate() {

        if (canMove == true && movementInput != Vector2.zero){

            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

            if (movementInput.x > 0){
                sprite.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            } else if (movementInput.x < 0){
                sprite.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            IsMoving = true;

        } else {
            // rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);

            IsMoving = false;
        }
        

    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        anim.SetTrigger("sword_attack");
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }

    
}
