using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX;
    private float delay = 0.3f;
    private bool attackBlocked;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (dirX > 0f)
        {
            anim.SetBool("Running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("Running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (Input.GetButtonDown("BasicAttack"))
        {
            Attack();
        }

    }

    private void Attack()
    {
        if (attackBlocked)
            return;
        anim.SetTrigger("BasicAttack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
