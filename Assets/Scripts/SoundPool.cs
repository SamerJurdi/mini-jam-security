using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private int poolSize = 10;
    private Queue<AudioSource> audioPool;
    private List<AudioSource> activeSources;

    void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        audioPool = new Queue<AudioSource>();
        activeSources = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            AudioSource audioSource = Instantiate(audioSourcePrefab, transform);
            audioSource.gameObject.SetActive(false);
            audioPool.Enqueue(audioSource);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f, bool loop = false, float pitchDiff = 1,bool RandomizePitch = true)
    {
        if (audioPool.Count > 0)
        {
            AudioSource audioSource = audioPool.Dequeue();
            audioSource.transform.position = position;
            audioSource.clip = clip;
            if (RandomizePitch)
            {
                audioSource.pitch = Random.Range(1f - pitchDiff, 1f + pitchDiff);
            }
            else
            {
                audioSource.pitch = pitchDiff;
            }
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            activeSources.Add(audioSource);

            if (!loop)
            {
                StartCoroutine(ReturnToPoolAfterPlayback(audioSource));
            }
        }
    }

    public void StopSound(AudioClip clip)
    {
        for (int i = activeSources.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = activeSources[i];
            if (audioSource.clip == clip)
            {
                audioSource.Stop();
                audioSource.clip = null;
                audioSource.gameObject.SetActive(false);
                activeSources.RemoveAt(i);
                audioPool.Enqueue(audioSource);
            }
        }
    }

    private IEnumerator ReturnToPoolAfterPlayback(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        if (!audioSource.loop)
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.gameObject.SetActive(false);
            activeSources.Remove(audioSource);
            audioPool.Enqueue(audioSource);
        }
    }
}
