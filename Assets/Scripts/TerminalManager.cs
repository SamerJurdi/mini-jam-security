using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalManager : MonoBehaviour
{
    public GameObject terminalResponse;
    public GameObject userCommand;
    public GameObject userInput;
    public GameObject terminalContainer;
    public ScrollRect sr;
    public TerminalInterpreter terminalInterpreter;

    private TMP_InputField terminalInput;

    private void Start() {
        terminalInput = userInput.GetComponentsInChildren<TMP_InputField>()[0];
        terminalInput.ActivateInputField();
    }

    private void OnGUI() {
        if (terminalInput.isFocused &&  terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return)) {
            AddUserCommand(terminalInput.text);
            AddTerminalResponse(terminalInterpreter.Interpret(terminalInput.text));
            ResetInputField();
        }
    }

    private void AddUserCommand(string userInputMessage) {
        Vector2 terminalContainerSize = terminalContainer.GetComponent<RectTransform>().sizeDelta;
        terminalContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(terminalContainerSize.x, terminalContainerSize.y + 35.0f);

        GameObject oldCommand = Instantiate(userCommand, terminalContainer.transform);
        oldCommand.transform.SetSiblingIndex(terminalContainer.transform.childCount - 1);
        oldCommand.GetComponentsInChildren<TMP_Text>()[1].text = userInputMessage;
    }

    public void AddTerminalResponse(List<string> responses) {
        for (int i = 0; i < responses.Count; i++) {
            Vector2 terminalContainerSize = terminalContainer.GetComponent<RectTransform>().sizeDelta;
            terminalContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(terminalContainerSize.x, terminalContainerSize.y + 35.0f);

            GameObject response = Instantiate(terminalResponse, terminalContainer.transform);
            response.transform.SetAsLastSibling();
            response.GetComponentsInChildren<TMP_Text>()[0].text = responses[i];
        }
        sr.verticalNormalizedPosition = 0;
    }

    public void ResetInputField() {
        if (terminalInput != null) {
            terminalInput.text = "";
            terminalInput.ActivateInputField();
        }
        userInput.transform.SetAsLastSibling();
    }

    public void DisableInputField() {
        userInput.SetActive(false);
    }

    public void ActivateInputField() {
        userInput.SetActive(true);
        ResetInputField();
    }

    public void ClearTerminal() {
        foreach (Transform child in terminalContainer.transform)
        {
            if (child.gameObject.name != "UserInput")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
