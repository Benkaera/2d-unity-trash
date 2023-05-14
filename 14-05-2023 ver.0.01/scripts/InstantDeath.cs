using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstantDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        var player = other.collider.GetComponent<PlayerMovement>();
        if (player != null) 
        {
            player.Die();
        }
    }
}
