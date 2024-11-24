using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float power; 
    public Vector2 dir; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        dir = dir.normalized;


        rb.velocity = dir * power;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       
            Destroy(gameObject);
        
    }
}
