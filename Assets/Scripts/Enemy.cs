using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage = 4f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){

        if (detectionZone.detectedObjs.Count > 0){
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            // move towards object
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        IDamageable damageable = col.collider.GetComponent<IDamageable>();

        if (damageable != null){
            damageable.OnHit(damage);
        }
    }

}
