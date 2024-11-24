using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public float fireCooldown;
    public Slider fireCooldownSlider;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float cooldownTimer;
    private SpriteRenderer sp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireCooldownSlider.maxValue = fireCooldown;
        fireCooldownSlider.value = fireCooldown;
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        cooldownTimer -= Time.deltaTime;
        fireCooldownSlider.value = Mathf.Clamp(fireCooldown - cooldownTimer, 0, fireCooldown);

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            Shoot();
            cooldownTimer = fireCooldown;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Hit");
        StartCoroutine(TurnRed(0.5f));
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }
    private IEnumerator TurnRed(float time)
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(time);
        sp.color = Color.white;
    }
}
