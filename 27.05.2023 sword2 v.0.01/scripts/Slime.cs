using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    public GameObject[] itemDrops;// exp orb massiv
    

    Animator animator;

    public float Health {
        set {
            if (value < _health) {
                animator.SetTrigger("hit");
            }

            _health = value;
            print(value);
                      

            if(_health <= 0){
                
               Destroy(gameObject);
               ItemDrop();
               

            }
        }

        get {
            return _health;
        }
    }

    public float _health = 10;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.SetBool("isAlive", isAlive);
    }

    void FixedUpdate() {
        if (detectionZone.detectedObjs.Count > 0) {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnHit(float damage) {
        Health -= damage;
    }


    private void ItemDrop() 
    {
        for (int i = 0; i < itemDrops.Length; i++) 
        {
            Instantiate(itemDrops[i], transform.position, Quaternion.identity);

        }
    }
}
