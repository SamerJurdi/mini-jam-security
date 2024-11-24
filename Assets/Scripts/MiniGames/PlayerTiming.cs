using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTiming : MonoBehaviour
{
    public SpriteRenderer sp;
    public float back;
    private bool damaged = false;
    private float bk = 0;
    public GameObject[] healthPoints;
    private int hp = 3;
  
    // Start is called before the first frame update
    void Start()
    {
        
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
        hp--;
        sp.color = Color.red;
        damaged = true;
        healthPoints[hp].SetActive(false);
        
    }
    
}
