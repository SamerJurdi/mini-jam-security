using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomIntervalCaller : MonoBehaviour
{
    public Canvas interactionUI;
    public TextMeshProUGUI displayText;
    public float textHeight = 1f;
    public float minInterval = 3f;
    public float maxInterval = 10f;

    [Header("Animation Settings")]
    public Animator animator;
    public AnimationClip normalAnimation;
    public AnimationClip glitchAnimation;
    private int animGlitchFile;

    [Header("Audio Settings")]
    public SoundPool soundPool;
    public AudioClip[] glitchSounds;
    private AudioClip glitchSound;

    private GameObject gameStateObject;
    private GameManager gameManager;
    private bool isPlayerNearby = false;

    void Start()
    {
        animGlitchFile = Animator.StringToHash(glitchAnimation.name);
        StartCoroutine(CallGlitchFileAtRandomIntervals());
        interactionUI.enabled = false;

        gameStateObject = GameObject.FindWithTag("GameState");

        if (gameStateObject != null)
        {
            gameManager = gameStateObject.GetComponent<GameManager>();
        }
    }

    void Update() {
        
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && gameManager.InitializeMiniGame())
        {
            interactionUI.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionUI.enabled = true;
            Vector3 textPosition = transform.position + new Vector3(0, textHeight, 0);
            displayText.transform.position = Camera.main.WorldToScreenPoint(textPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Vector3 textPosition = transform.position + new Vector3(0, textHeight, 0);
            displayText.transform.position = Camera.main.WorldToScreenPoint(textPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionUI.enabled = false;
        }
    }

    private IEnumerator CallGlitchFileAtRandomIntervals()
    {
        while (true)
        {
            float randomInterval = Random.Range(minInterval, maxInterval);
            
            yield return new WaitForSeconds(randomInterval);
            
            GlitchFile();
        }
    }

    void GlitchFile()
    {
        glitchSound = glitchSounds[Random.Range(0, glitchSounds.Length)];
        animator.CrossFade(animGlitchFile, 0);
        soundPool.PlaySound(glitchSound, Vector2.zero, 0.5f, false);
    }
}
