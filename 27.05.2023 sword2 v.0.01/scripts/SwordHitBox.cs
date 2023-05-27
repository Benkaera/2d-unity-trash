using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 500f;

    public Collider2D swordCollider;

    void Start()
    {
        if (swordCollider == null) {
            Debug.LogWarning("sc not set");
        }
        //swordCollider.GetComponent<Collider2D>();  
    }

    void OnCollisionEnter2D(Collision2D col) {
        col.collider.SendMessage("OnHit", swordDamage);
    }

    void onTriggerEnter2D(Collider2D collider) 
    {
         
         Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

         Vector2 direction = (Vector2) (collider.gameObject.transform.position - parentPosition).normalized;
         Vector2 knockback = direction * knockbackForce;

         //collider.SendMessage("OnHit", swordDamage, knockback);
    }
    
}
