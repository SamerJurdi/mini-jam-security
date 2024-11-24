using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTiming : MonoBehaviour
{
    private SpriteRenderer sp;
    public float back;
    private bool damaged = false;
    private float bk = 0;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            bk += Time.deltaTime;
            if(bk >= back)
            {
                sp.color = Color.white;
                bk = 0;
                damaged = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        sp.color = Color.red;
        damaged = true;
    }
}
