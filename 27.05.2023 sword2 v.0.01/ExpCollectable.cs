using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectable : MonoBehaviour
{
    public GameObject player;
    public float timer;
    public bool moveToPlayer;
    public float speed;
    public Rigidbody2D rb;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (moveToPlayer == false)
        {
            if (timer < 1)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                moveToPlayer = true;
                rb.gravityScale = 0;
            }
        }

        if (moveToPlayer == true)
        {
            Vector3 movementVector = player.transform.position - transform.position;
            rb.velocity = movementVector * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().currExp += 1;
            Destroy(gameObject);
        }
    }
}
