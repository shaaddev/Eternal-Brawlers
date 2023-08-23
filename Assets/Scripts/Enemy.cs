using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health {
        set{
            health = value;
            if (health <= 0){
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    Animator anim;

    public float health = 5;

    private void Start(){
        anim = GetComponent<Animator>();
    }

    public void Defeated(){
        anim.SetTrigger("death");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }
}
