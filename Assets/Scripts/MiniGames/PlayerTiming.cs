using System.Collections;
using UnityEngine;

public class PlayerTiming : MonoBehaviour
{
    public SpriteRenderer sp;
    public float back;
    private bool damaged = false;
    private float bk = 0;
    public GameObject[] healthPoints;
    private int hp = 3;
    public SoundPool soundPool;
    public AudioClip hit;
    public GameObject endScreen;

    private GameObject gameStateObject;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        soundPool = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        gameStateObject = GameObject.FindWithTag("GameState");

        if (gameStateObject != null)
        {
            gameManager = gameStateObject.GetComponent<GameManager>();
        }
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
        if(hp <= 0)
        {
            Destroy(gameObject, 3f);
            endScreen.SetActive(true);
            StartCoroutine(delay());
            
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.3f);
        gameManager.EndGame();
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
        soundPool.PlaySound(hit, Vector2.zero, 1f, false, 0.2f);

    }
    
}
