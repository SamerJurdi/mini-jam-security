using UnityEngine;

public enum MiniGame
{
    Pattern,
    Decipher
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MiniGame selectedMiniGame;

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
        InitializeMiniGame();
    }

    // TODO: Randomize minigame selection
    public void InitializeMiniGame()
    {
        selectedMiniGame = MiniGame.Pattern;
    }

    public MiniGame GetSelectedMiniGame()
    {
        return selectedMiniGame;
    }
}
