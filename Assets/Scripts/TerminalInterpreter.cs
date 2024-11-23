using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInterpreter : MonoBehaviour
{
    List<string> response = new List<string>();

    public List<string> Interpret(string inputMessage) {
        response.Clear();
        response.Add("Hello World");
        response.Add("Your message: " + inputMessage);
        return response;
    }
}
