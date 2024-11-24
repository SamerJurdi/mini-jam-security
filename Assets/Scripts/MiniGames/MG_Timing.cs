using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        hitboxRight.SetActive(false);
        hitboxUp.SetActive(false);
        chargeBar.maxValue = rechargeTime;
        typeOfShot = UnityEngine.Random.Range(0, projectiles.Length);
        BossHealthBar.maxValue = BossHealth;
    }

    void Update()
    {
        recharge += Time.deltaTime;
        BossHealthBar.value = BossHealth;
        if (Input.GetMouseButtonDown(0) && !mouseHeld)
        {
            playerStates[0].SetActive(false);
            playerStates[1].SetActive(true);
           
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
            mouseHeld = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && recharge >= rechargeTime)
        {
            up = false;
            hitboxRight.SetActive(true);
            hitboxUp.SetActive(false);
            playerShield.SetBool("up", false);
            recharge = 0f;
        }

        if (Input.GetKeyDown(KeyCode.W) && recharge >= rechargeTime)
        {
            hitboxRight.SetActive(false);
            hitboxUp.SetActive(true);
            up = true;
            playerShield.SetBool("up", true);
            recharge = 0f;
        }
        if(BossHealth <= 0f)
        {
            gamewon = true;    
        }
        chargeBar.value = recharge;

        shootTm -= Time.deltaTime;
        if (shootTm <= 0)
        {
            ProjectilesCs[typeOfShot].SetActive(true);
            Shoot(projectiles[typeOfShot]);
            shootTm = UnityEngine.Random.Range(0.5f, shootRate);
            typeOfShot = UnityEngine.Random.Range(0, projectiles.Length);
            ProjectilesCs[typeOfShot].SetActive(false);
        }
    }

    void Shoot(GameObject bulletType)
    {
        Instantiate(bulletType, spawnPoint.position, Quaternion.identity);
        animator.SetTrigger("shoot");
    }
}
