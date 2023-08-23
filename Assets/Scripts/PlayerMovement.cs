using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    Vector2 movementInput;

    public float moveSpeed = 7f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public SwordAttack swordAttack;
    bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (canMove){
            if (movementInput != Vector2.zero) {
                bool success = TryMove(movementInput);

                if (!success){
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success){
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                anim.SetBool("isMoving", success);
            } else {
                anim.SetBool("isMoving", false);
            }

            if (movementInput.x < 0){
                sprite.flipX = true;
            } else if (movementInput.x > 0){
                sprite.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction){
        // check for potential collisions
        int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0){
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        anim.SetTrigger("sword_attack");
    }

    public void SwordAttack(){
        LockMovement();

        if (sprite.flipX == true){
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }   
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }
}
