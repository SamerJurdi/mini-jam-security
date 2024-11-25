using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI lostTimeText;
    public float startTimeInSeconds = 80f;

    private float remainingTime;
    private float lostTimeDisplayDuration = 1f;
    private bool isDisplayingLostTime = false;
    private bool isPaused = false;
    private GameManager gameManager;

    void Start()
    {
        remainingTime = startTimeInSeconds;
        lostTimeText.gameObject.SetActive(false);
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (!isPaused) {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0f;
                gameManager.GameFailed();
            }
        }

        UpdateTimerUI();

        if (isDisplayingLostTime)
        {
            lostTimeDisplayDuration -= Time.deltaTime;
            if (lostTimeDisplayDuration <= 0f)
            {
                lostTimeText.gameObject.SetActive(false);
                isDisplayingLostTime = false;
                lostTimeDisplayDuration = 1f;
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Update the timer text with the formatted time (e.g., "01:01")
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (remainingTime < 30f) {
            if (Mathf.FloorToInt(remainingTime) % 2 == 0) {
                timerText.color = Color.red;
            } else timerText.color = Color.white;
        }
    }

    public void SubtractTime(float secondsToSubtract)
    {
        if (remainingTime > 0)
        {
            remainingTime = Mathf.Max(0f, remainingTime - secondsToSubtract);
            UpdateTimerUI();
            ShowLostTime(secondsToSubtract);
        }
        else
        {
            lostTimeText.gameObject.SetActive(false);
        }
    }

    public void PauseTimer() {
        isPaused = true;
    }

    public void ResumeTimer() {
        isPaused = false;
    }

    private void ShowLostTime(float secondsLost)
    {
        lostTimeText.gameObject.SetActive(true);
        lostTimeText.text = "-" + secondsLost.ToString("0") + "s";
        lostTimeText.color = Color.red;
        isDisplayingLostTime = true;
    }
}
