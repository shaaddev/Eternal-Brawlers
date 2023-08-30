using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private AudioSource bluntImpact;

    private void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        healthBar.SetMaxHealth(Health);
    }


    public void OnHit(float damage, Vector2 knockback){
        bluntImpact.Play();
        Health -= damage;

        // apply force
        rb.AddForce(knockback);
    }

    public void OnHit(float damage){
        Health -= damage;

        healthBar.SetHealth(Health);
    }

    public void OnObjectDestroyed(){
        ScoreSystem.scoreValue += 10;
        Destroy(gameObject);
    }
}
