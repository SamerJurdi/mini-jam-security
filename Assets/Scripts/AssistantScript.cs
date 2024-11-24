using UnityEngine;

public class AssistantScript : MonoBehaviour
{
    [Header("Emotions")]
    public GameObject[] AssistantEmotions;

    [Header("Dialogues")]
    public GameObject[] Dialogues;

    [Header("Timing")]
    public float emotionChangeTime = 2f;
    static public bool stop = false;

    [Header("Emotion Order")]
    public int[] emotionSequence = { 0, 2, 1, 3, 4 }; // 0 neutral; 1 irritated; 2 happy; 3 sad; 4 angry
    private int currentEmotionIndex = 0;
    private int currentDialogueIndex = 0;
    private float timer;

    void Start()
    {
        timer = emotionChangeTime;

        if (AssistantEmotions == null || AssistantEmotions.Length == 0)
        {
            Debug.LogError("Empty emotions array.");
        }

        if (Dialogues != null && Dialogues.Length > 0)
        {
            DeactivateAll(Dialogues);
        }

        DeactivateAll(AssistantEmotions);
    }

    void Update()
    {
        if (stop) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            DisplayEmotion();

            if (Dialogues != null && Dialogues.Length > 0)
            {
                DisplayDialogue();
            }

            timer = emotionChangeTime;
        }

        if (currentEmotionIndex >= emotionSequence.Length && currentDialogueIndex >= Dialogues.Length)
        {
            stop = true; 
            DeactivateAll(AssistantEmotions);
            DeactivateAll(Dialogues);
        }
    }

    private void DisplayEmotion()
    {
        if (AssistantEmotions == null || AssistantEmotions.Length == 0)
            return;

        int emotionIndex = emotionSequence[currentEmotionIndex];

        if (emotionIndex >= 0 && emotionIndex < AssistantEmotions.Length)
        {
            ActivateOnly(AssistantEmotions[emotionIndex], AssistantEmotions);
        }

        currentEmotionIndex++;

        if (currentEmotionIndex >= emotionSequence.Length)
        {
            stop = true; 
        }
    }

    private void DisplayDialogue()
    {
        if (Dialogues == null || Dialogues.Length == 0)
            return;

        ActivateOnly(Dialogues[currentDialogueIndex], Dialogues);

        currentDialogueIndex++;

        if (currentDialogueIndex >= Dialogues.Length)
        {
            stop = true; 
        }
    }

    private void ActivateOnly(GameObject obj, GameObject[] objArray)
    {
        foreach (var item in objArray)
        {
            if (item != null)
            {
                item.SetActive(item == obj);
            }
        }
    }

    private void DeactivateAll(GameObject[] objArray)
    {
        foreach (var obj in objArray)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
