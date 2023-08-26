using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public  Collider2D swordCollider;
    public float swordDamage = 2f;
    public float knockbackForce = 500f;

    public Vector3 faceRight = new Vector3(0.8f, 0.2f, 0);
    public Vector3 faceLeft = new Vector3(-0.8f, 0.2f, 0);

    void Start(){
        // swordCollider.GetComponent<Collider2D>();
        if (swordCollider == null){
            Debug.LogWarning("Sword Collider not set");
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        col.collider.SendMessage("OnHit", swordDamage);
    }

    void OnTriggerEnter2D(Collider2D other){
    
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null){
            // calculate direction between player and enemy
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            //
            damageableObject.OnHit(swordDamage, knockback);
        } 
    }

    void IsFacingRight(bool isFacingRight){
        if (isFacingRight){
            gameObject.transform.localPosition = faceRight;
        } else {
            gameObject.transform.localPosition = faceLeft;
        }
    }

}
