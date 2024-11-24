using System.Collections;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] TextMeshPro tmpro;
    [SerializeField] string text;
    [SerializeField] float textDisplayTime = 0.07f;
    private float originalTime;
    [SerializeField] public bool printText;
    [SerializeField] public SoundPool soundThing;
    [SerializeField] public AudioClip audioClip;
    [Range(0f, 1f)]
    [SerializeField] public float volumeControl;
    [Range(0f, 1f)]
    [SerializeField] public float pitchRange;

    void Start()
    {
        tmpro = GetComponent<TextMeshPro>();
        soundThing = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        originalTime = textDisplayTime;
    }

    void Update()
    {
        if (printText)
        {
            StartCoroutine(WriteText(text, originalTime));
            printText = false;
        }
    }

    IEnumerator WriteText(string text, float displayTimePerCharacter)
    {
        tmpro.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            tmpro.text += text[i];
            soundThing.PlaySound(audioClip, Vector2.zero, volumeControl, false, pitchRange);
            yield return new WaitForSeconds(displayTimePerCharacter);
        }
    }
}
