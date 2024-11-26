using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MG_Timing : MonoBehaviour
{
    public GameObject hitboxRight;
    public GameObject hitboxUp;
    public Slider chargeBar;
    public Slider BossHealthBar;
    public float BossHealth = 20f;
    public float holdTime;
    private float hold;
    public float rechargeTime;
    private float recharge;
    public Transform spawnPoint;
    public GameObject[] projectiles;
    public float Dificulty;
    public Animator animator;
    public Animator playerShield;
    private bool up = false;
    public int typeOfShot;
    public GameObject[] ProjectilesCs;
    [Range(0f, 2f)]
    [SerializeField] public float shootRate;
    private float shootTm;
    public GameObject[] playerStates;
    private bool mouseHeld;
    private bool gamewon = false;
    public GameObject Boss;
    public GameObject Explosion;
    public SoundPool soundPool;
    public AudioClip light;
    public AudioClip BulletSHotSound;
    public AudioClip woosh;
    public AudioClip BossExplode;

    private GameObject gameStateObject;
    private GameManager gameManager;

    void Start()
    {
        hitboxRight.SetActive(false);
        hitboxUp.SetActive(false);
        chargeBar.maxValue = rechargeTime;
        typeOfShot = UnityEngine.Random.Range(0, projectiles.Length);
        BossHealthBar.maxValue = BossHealth;
        soundPool = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        gameStateObject = GameObject.FindWithTag("GameState");

        if (gameStateObject != null)
        {
            gameManager = gameStateObject.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        recharge += Time.deltaTime;
        BossHealthBar.value = BossHealth;
        if (Input.GetMouseButtonDown(0) && !mouseHeld)
        {
            playerStates[0].SetActive(false);
            playerStates[1].SetActive(true);
            soundPool.PlaySound(light, Vector2.zero, 0.1f, false, 1.2f, false);
            mouseHeld = true;
        }
        if (mouseHeld)
        {
            BossHealth -= Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerStates[1].SetActive(false);
            playerStates[0].SetActive(true);
            soundPool.StopSound(light);
            mouseHeld = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && recharge >= rechargeTime)
        {
            up = false;
            hitboxRight.SetActive(true);
            hitboxUp.SetActive(false);
            playerShield.SetBool("up", false);
            recharge = 0f;
            soundPool.PlaySound(woosh, Vector2.zero, 0.3f, false, 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.W) && recharge >= rechargeTime)
        {
            hitboxRight.SetActive(false);
            hitboxUp.SetActive(true);
            up = true;
            playerShield.SetBool("up", true);
            recharge = 0f;
            soundPool.PlaySound(woosh, Vector2.zero, 0.3f, false, 0.2f);
        }
        if(BossHealth <= 0f && !gamewon)
        {
            gamewon = true; 
            Boss.SetActive(false);
            Explosion.SetActive(true);
            soundPool.PlaySound(BossExplode, Vector2.zero, 0.6f, false, 0.2f);
            StartCoroutine(delay());

        }
        chargeBar.value = recharge;

        shootTm -= Time.deltaTime;
        if (shootTm <= 0 && ! gamewon)
        {
            ProjectilesCs[typeOfShot].SetActive(true);
            Shoot(projectiles[typeOfShot]);
            shootTm = UnityEngine.Random.Range(0.5f, shootRate);
            typeOfShot = UnityEngine.Random.Range(0, projectiles.Length);
            ProjectilesCs[typeOfShot].SetActive(false);
            
        }
    }
    
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
        gameManager.EndGame();
    }

    void Shoot(GameObject bulletType)
    {
        soundPool.PlaySound(BulletSHotSound, Vector2.zero, 0.2f, false, 0.2f);
        Instantiate(bulletType, spawnPoint.position, Quaternion.identity);
        animator.SetTrigger("shoot");
    }
    
}
