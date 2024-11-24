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
    public float holdTime;
    private float hold;
    public float rechargeTime;
    private float recharge;
    public Transform spawnPoint;
    public GameObject[] projectiles;
    public float Dificulty;
    [Range(0f, 2f)]
    [SerializeField] public float shootRate;
    private float shootTm;

    void Start()
    {
        hitboxRight.SetActive(false);
        hitboxUp.SetActive(false);
        chargeBar.maxValue = rechargeTime;
    }

    void Update()
    {
        recharge += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D) && recharge >= rechargeTime)
        {
            Slash(hitboxRight);
            recharge = 0f;
        }

        if (Input.GetKeyDown(KeyCode.W) && recharge >= rechargeTime)
        {
            Slash(hitboxUp);
            recharge = 0f;
        }

        chargeBar.value = recharge;

        shootTm -= Time.deltaTime;
        if (shootTm <= 0)
        {
            Shoot(projectiles[UnityEngine.Random.Range(0, projectiles.Length)]);
            shootTm = UnityEngine.Random.Range(0.3f, shootRate);
        }
    }

    void Slash(GameObject obj)
    {
        StartCoroutine(HitboxRoutine(obj));
    }

    IEnumerator HitboxRoutine(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(holdTime);
        obj.SetActive(false);
    }

    void Shoot(GameObject bulletType)
    {
        Instantiate(bulletType, spawnPoint.position, Quaternion.identity);
    }
}
