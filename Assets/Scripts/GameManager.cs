using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MiniGame
{
    Pattern,
    //Decipher // TODO: For the second terminal mini-game
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject terminal;
    public TerminalManager terminalManager;

    public MiniGame selectedMiniGame;
    private MG_PatternController mg_PatternController;
    private bool miniGameInProgress = false;
    private List<GameObject> allObjectsToToggle = new List<GameObject>();
    private GameObject interactionUI;
    private Canvas interactionCanvas;
    private CountdownTimer countdownTimer;

    private int numberOfInfectedFiles = 2;
    private int infectedFilesTested = 0;

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
        countdownTimer = GetComponent<CountdownTimer>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
            allObjectsToToggle.Add(playerObject);

        interactionUI = GameObject.FindWithTag("InteractionUI");
        if (interactionUI != null)
            interactionCanvas = interactionUI.GetComponent<Canvas>();
    }

    public void InitializeBossFight() {
        ToggleCustomGameObjects(false);
        SceneManager.LoadScene("DenisScene");
    }

    public bool InitializeMiniGame()
    {
        if (!miniGameInProgress) {
            ToggleCustomGameObjects(false);
            miniGameInProgress = true;
            selectedMiniGame = MiniGame.Pattern; // TODO: Randomize minigame selection

            if (selectedMiniGame == MiniGame.Pattern) {
                mg_PatternController.InitializePatternGame();
                OpenTerminal();
            }
            return true;
        } else return false;
    }

    public List<string> ManageTerminalInput(string inputMessage) {
        if (selectedMiniGame == MiniGame.Pattern) {
            return mg_PatternController.ManageTerminalInput(inputMessage);
        } else return new List<string> { "Your message: " + inputMessage };
    }

    public void WonGame() {
        infectedFilesTested++;
        StartCoroutine(CloseTerminal());
    }

    public void LostGame() {
        infectedFilesTested++;
        StartCoroutine(CloseTerminal());
    }

    public void EndGame() {
        // TODO: Disable all UIs and enable death canvas (semi transparent panel with Play again button and main picture)
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }

    private IEnumerator CloseTerminal()
    {
        terminalManager.DisableInputField();
        yield return new WaitForSeconds(1);
        terminalManager.ClearTerminal();
        terminal.SetActive(false);

        miniGameInProgress = false;
        ToggleCustomGameObjects(true);
        if (infectedFilesTested == numberOfInfectedFiles)
            InitializeBossFight();
    }

    private void OpenTerminal()
    {
        terminal.SetActive(true);
        terminalManager.ActivateInputField();
    }

    private void ToggleCustomGameObjects(bool isActive) {
        foreach (GameObject obj in allObjectsToToggle)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
        if (interactionUI != null)
            interactionCanvas.enabled = false;
    }
}
