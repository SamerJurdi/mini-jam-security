using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    private CountdownTimer countdownTimer;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(sceneName);
        if (countdownTimer != null) {
            countdownTimer.ResumeTimer();
        }
    }

    void Start() {
        GameObject gameStateObject = GameObject.FindWithTag("GameState");
        if (gameStateObject != null) {
            GameManager gameManager = gameStateObject.GetComponent<GameManager>();
            gameManager.ResetState();
            countdownTimer = gameStateObject.GetComponent<CountdownTimer>();
            countdownTimer.PauseTimer();
        }
    }
}
    

