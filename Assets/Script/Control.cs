using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private ContactFilter2D _platform;
    private Rigidbody2D _rigidbody;

    private bool Get_isOnPlatform()
    {
        return _rigidbody.IsTouching(_platform);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump() 
    {
        if(Get_isOnPlatform() == true)
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
