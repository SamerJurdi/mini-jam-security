using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGame
{
    Pattern,
    Decipher // TODO: For the second terminal mini-game
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject terminal;
    public TerminalManager terminalManager;

    public MiniGame selectedMiniGame;
    private MG_PatternController mg_PatternController;

    // TODO: Create Countdown Timer

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        mg_PatternController = GetComponent<MG_PatternController>();
        InitializeMiniGame();
    }

    public void InitializeMiniGame()
    {
        selectedMiniGame = MiniGame.Pattern; // TODO: Randomize minigame selection

        if (selectedMiniGame == MiniGame.Pattern) {
            mg_PatternController.InitializePatternGame();
            OpenTerminal();
        }
    }

    public List<string> ManageTerminalInput(string inputMessage) {
        if (selectedMiniGame == MiniGame.Pattern) {
            return mg_PatternController.ManageTerminalInput(inputMessage);
        } else return new List<string> { "Your message: " + inputMessage };
    }

    public void WonGame() {
        Debug.Log("File Recovered!");
        StartCoroutine(CloseTerminal());
    }

    public void LostGame() {
        Debug.Log("File Lost!");
        StartCoroutine(CloseTerminal());
    }

    private IEnumerator CloseTerminal()
    {
        terminalManager.DisableInputField();
        yield return new WaitForSeconds(1);
        terminalManager.ClearTerminal();
        terminal.SetActive(false);
    }

    private void OpenTerminal()
    {
        terminal.SetActive(true);
        terminalManager.ActivateInputField();
    }
}
