using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start() {
        GameObject gameStateObject = GameObject.FindWithTag("GameState");
        if (gameStateObject != null) {
            Destroy(gameStateObject);
        }
    }
}
    

