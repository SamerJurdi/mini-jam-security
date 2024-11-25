using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public SoundPool soundPool;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        soundPool = GameObject.Find("AudioPool").GetComponent<SoundPool>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HitSomething();
        }
    }
    public void HitSomething()
    {
       
        soundPool.PlaySound(hit, Vector2.zero, 0.5f, false, 0.2f);

    }

}


