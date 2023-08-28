using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    // public GameObject healthText;
    public bool disableSimulation = false;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    public float Health {
        set{
            if (value < health){
                anim.SetTrigger("Hit");
                // RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                // textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                // Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                // textTransform.SetParent(canvas.transform);
            }

            health = value;

            
            if (health <= 0){
                anim.SetBool("isAlive", false);
                Targetable = false;
            
            }
        }
        get {
            return health;
        }
    }

    Animator anim;

    public float health;
    public bool Targetable{get { return _targetable; }
    set{
        _targetable = value;

        if (disableSimulation){
            rb.simulated = false;
        }
        physicsCollider.enabled = value;
    } }
    public bool _targetable = true;

    public HealthBar healthBar;

    private void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        healthBar.SetMaxHealth(Health);
    }


    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;

        // apply force
        rb.AddForce(knockback);
    }

    public void OnHit(float damage){
        Health -= damage;

        healthBar.SetHealth(Health);
    }

    public void OnObjectDestroyed(){
        Destroy(gameObject);
    }
}
