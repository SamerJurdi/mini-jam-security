using UnityEngine;

public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    IDLE
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float decelerationTime = 0.1f;
    private Rigidbody2D rb;
    private Vector2 targetMovement;

    public GameObject leftAnimObject;
    public GameObject upAnimObject;
    public GameObject downAnimObject;
    public GameObject idleAnimObject;
    private GameObject newAnimation;
    private DIRECTION selectedDirection;
    private DIRECTION newDirection;
    private SoundPool soundPool;
    public AudioClip walkSf;

    private bool isWalking;
    private bool isPlayingWalkSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        selectedDirection = DIRECTION.IDLE;
        newDirection = DIRECTION.IDLE;
        soundPool = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        isWalking = false;
        isPlayingWalkSound = false;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        targetMovement = new Vector2(moveX, moveY).normalized;

        UpdatePlayerAnimation(moveX, moveY);

        isWalking = targetMovement != Vector2.zero;

        if (isWalking && !isPlayingWalkSound)
        {
            soundPool.PlaySound(walkSf, Vector2.zero, 0.1f, true, 1.2f, false);
            isPlayingWalkSound = true;
        }
        else if (!isWalking && isPlayingWalkSound)
        {
            soundPool.StopSound(walkSf);
            isPlayingWalkSound = false;
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
        {
            rb.velocity = targetMovement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref targetMovement, decelerationTime);
        }
    }

    void UpdatePlayerAnimation(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            newAnimation = leftAnimObject;
            newDirection = DIRECTION.RIGHT;
        }
        else if (moveX < 0)
        {
            newAnimation = leftAnimObject;
            newDirection = DIRECTION.LEFT;
        }
        else if (moveY > 0)
        {
            newAnimation = upAnimObject;
            newDirection = DIRECTION.UP;
        }
        else if (moveY < 0)
        {
            newAnimation = downAnimObject;
            newDirection = DIRECTION.DOWN;
        }
        else
        {
            newAnimation = idleAnimObject;
            newDirection = DIRECTION.IDLE;
        }

        if (newDirection != selectedDirection)
        {
            ActivateAnimation(newAnimation);
            selectedDirection = newDirection;
        }
    }

    void ActivateAnimation(GameObject selectedAnimObject)
    {
        if (newDirection == DIRECTION.RIGHT && selectedDirection != DIRECTION.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector2(transform.position.x - 1.8999f, transform.position.y);
        }
        else if (newDirection != DIRECTION.RIGHT && selectedDirection == DIRECTION.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector2(transform.position.x + 1.8999f, transform.position.y);
        }

        leftAnimObject.SetActive(false);
        upAnimObject.SetActive(false);
        downAnimObject.SetActive(false);
        idleAnimObject.SetActive(false);

        selectedAnimObject.SetActive(true);
    }
}
