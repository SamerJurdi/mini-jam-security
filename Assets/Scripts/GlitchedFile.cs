using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomIntervalCaller : MonoBehaviour
{
    public float minInterval = 3f;
    public float maxInterval = 10f;
    public TextMeshProUGUI displayText;

    [Header("Animation Settings")]
    public Animator animator;
    public AnimationClip normalAnimation;
    public AnimationClip glitchAnimation;
    private int animGlitchFile;

    [Header("Audio Settings")]
    public SoundPool soundPool;
    public AudioClip[] glitchSounds;
    private AudioClip glitchSound;


    void Start()
    {
        animGlitchFile = Animator.StringToHash(glitchAnimation.name);
        StartCoroutine(CallGlitchFileAtRandomIntervals());
        displayText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(true);
            Vector3 textPosition = transform.position + new Vector3(0, 1.5f, 0);
            displayText.transform.position = Camera.main.WorldToScreenPoint(textPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 textPosition = transform.position + new Vector3(0, 1.5f, 0);
            displayText.transform.position = Camera.main.WorldToScreenPoint(textPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(false);
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
