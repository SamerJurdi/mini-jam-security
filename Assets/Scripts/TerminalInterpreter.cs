using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInterpreter : MonoBehaviour
{
    private GameManager gameManager;
    private List<string> response = new List<string>();

    public List<string> Interpret(string inputMessage) {
        response.Clear();
        response = gameManager.ManageTerminalInput(inputMessage);
        return response;
    }

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
}
