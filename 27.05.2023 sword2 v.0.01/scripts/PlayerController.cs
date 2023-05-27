using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    //public float speed;
    //private Vector2 direction;
    //private Rigidbody2D rb;


    public Animator animator;
    public GameObject swordHitbox;
    Collider2D swordCollider;


    void Start()
    {
        swordCollider = swordHitbox.GetComponent<Collider2D>();   
    }


    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime *5;


        

    }

    //void FixedUpdate()
    //{
    //    rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    //}

}
