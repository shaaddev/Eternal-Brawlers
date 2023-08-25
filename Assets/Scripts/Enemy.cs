using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
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
                // Destroy(gameObject);
            }
        }
        get {
            return health;
        }
    }

    Animator anim;

    public float health = 5;
    public bool Targetable{get { return _targetable; }
    set{
        _targetable = value;

        // rb.simulated = value;
        physicsCollider.enabled = value;
    } }
    public bool _targetable = true;

    private void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }


    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;

        // apply force
        rb.AddForce(knockback);
    }

    public void OnHit(float damage){
        Health -= damage;
    }

    public void OnObjectDestroyed(){
        Destroy(gameObject);
    }


    // public void Defeated(){
    //     anim.SetTrigger("death");
    // }

    // public void RemoveEnemy(){
    //     Destroy(gameObject);
    // }
}
