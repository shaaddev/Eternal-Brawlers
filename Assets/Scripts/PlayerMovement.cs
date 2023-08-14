using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    

    private float dirX;
    private float delay = 0.3f;
    //private bool attackBlocked;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask jumpableGround;

    private bool doubleJump;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    int currentLife = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            anim.SetBool("Jumping", true);
            doubleJump = false;
        } else
        {
            anim.SetBool("Jumping", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump)
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            } else
            {
                anim.SetBool("Jumping", false);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            anim.SetBool("Jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        } else
        {
            anim.SetBool("Jumping", false);
        }

        if (!PauseMenu.isPaused)
        {
            UpdateAnimationState();
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            currentLife++;
            Debug.Log("Player Collision:" + currentLife);
            if (currentLife >= 3)
            {
                Time.timeScale = 0f;
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateAnimationState()
    {

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
        } else if (Input.GetButtonDown("BasicAttack2"))
        {
            Attack2();
        }

    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Attack()
    {
        anim.SetTrigger("BasicAttack");
        //attackBlocked = true;
        StartCoroutine(DelayAttack());

        // detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

        // Damage Them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    private void Attack2()
    {
        anim.SetTrigger("BasicAttack2");
        //attackBlocked = true;
        StartCoroutine(DelayAttack());

        // detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

        // Damage Them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        //attackBlocked = false;
    }
}
