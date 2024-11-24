using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MG_PatternController : MonoBehaviour
{
    private GameManager gameManager;
    private CountdownTimer countdownTimer;

    public TerminalManager terminalManager;

    [Header("Audio Settings")]
    public SoundPool soundPool;
    public AudioClip errorSound;
    public AudioClip deleteSound;

    public class Pattern
    {
        public string prompt;
        public string[] answers;
        public string correct;
    }

    public List<List<Pattern>> patternGroups = new List<List<Pattern>>()
    {
        new List<Pattern>
        {
            new Pattern { prompt = "Find the next value: Timmy, ..", answers = new string[] { "Told", "Heard" }, correct = "Told" },
            new Pattern { prompt = "Find the next value: Timmy, Told, ..", answers = new string[] { "Tall", "Wall" }, correct = "Tall" },
            new Pattern { prompt = "Find the next value: Timmy, Told, Tall, ..", answers = new string[] { "Time", "Space" }, correct = "Time" }
        },
        new List<Pattern>
        {
            new Pattern { prompt = "Find the next value: Fox, ..", answers = new string[] { "Box", "Barn" }, correct = "Box" },
            new Pattern { prompt = "Find the next value: Fox, Box, ..", answers = new string[] { "ox", "cow" }, correct = "ox" },
            new Pattern { prompt = "Find the next value: Fox, Box, ox, ..", answers = new string[] { "paradox", "mystery" }, correct = "paradox" }
        }
    };

    private List<List<Pattern>> allPatternGroups = new List<List<Pattern>>();
    private List<Pattern> selectedPatternGroup = new List<Pattern>();
    private List<string> messages = new List<string>();
    private int stage;
    private int correctAnswers;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        countdownTimer = GetComponent<CountdownTimer>();
        ResetPatternGroup();
    }

    public void InitializePatternGame()
    {
        stage = 0;
        correctAnswers = 0;
        GetRandomPatternGroup();
        messages.Clear();
        messages.Add(selectedPatternGroup[stage].prompt);
        messages.Add(string.Join(" or ", selectedPatternGroup[stage].answers));
        terminalManager.AddTerminalResponse(messages);
        terminalManager.ResetInputField();
    }

    public List<string> ManageTerminalInput(string inputMessage) {
        bool isCorrect = string.Equals(inputMessage, selectedPatternGroup[stage++].correct, StringComparison.OrdinalIgnoreCase);
        messages.Clear();
        // TODO: Add SFX
        if (isCorrect) {
            messages.Add("Correct!");
            correctAnswers++;
        } else {
            countdownTimer.SubtractTime(5f);
            messages.Add("Error!");
            soundPool.PlaySound(errorSound, Vector2.zero, 2f, false);
        }

        if (correctAnswers > 3) {
            EndGameSuccessfully(true);
            messages.Add("File Recovered!");
            return messages;
        }

        // Render next pattern
        if (stage < selectedPatternGroup.Count) {
            messages.Add(selectedPatternGroup[stage].prompt);
            messages.Add(string.Join(" or ", selectedPatternGroup[stage].answers));
        } else {
            if (isCorrect) {
                EndGameSuccessfully(true);
                messages.Add("File Recovered!");
            } else {
                EndGameSuccessfully(false);
                messages.Add("File Lost!");
                soundPool.PlaySound(deleteSound, Vector2.zero, 0.8f, false);
            }
        }

        return messages;
    }

    private void EndGameSuccessfully(bool isSuccessful) {
        if (isSuccessful) {
            gameManager.WonGame();
        } else gameManager.LostGame();
    }

    private List<Pattern> GetRandomPatternGroup()
    {
        if (allPatternGroups.Count == 0)
        {
            ResetPatternGroup();
        }

        int selectedIndex = UnityEngine.Random.Range(0, allPatternGroups.Count);
        selectedPatternGroup.Clear();
        selectedPatternGroup = allPatternGroups[selectedIndex];

        allPatternGroups.RemoveAt(selectedIndex);

        return selectedPatternGroup;
    }

    private void ResetPatternGroup()
    {
        allPatternGroups.Clear();
        foreach (var group in patternGroups)
        {
            allPatternGroups.Add(new List<Pattern>(group));
        }
    }
}
