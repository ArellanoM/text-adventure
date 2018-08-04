using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
	public InputField inputField;

	GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController>();
		inputField.onEndEdit.AddListener(AcceptStringInput);
	}

	void AcceptStringInput(string userInput)
	{
		userInput = userInput.ToLower();										// User input is lowered to avoid ambiguity
		controller.LogStringWithReturn("<color=grey>" + userInput + "</color>");	// "<color=red>" + userInput + "</color>"    or    userInput

		char[] delimiterCharacters = { ' ' };									// We create a delimiter with a space char
		string[] separatedInputWords = userInput.Split(delimiterCharacters);	// separatedInputWords[0]="verb" separatedInputWords[1]="noun"

		// We iterate through existing inputActions
		for (int i = 0; i < controller.inputActions.Length; i++)
		{
			// We store the 'i' inputAction
			InputAction inputAction = controller.inputActions[i];
			// If the verb slot (from the array) IS a valid inputAction
			if (inputAction.keyWord == separatedInputWords[0])					// ERROR: Go, Examine, Take, Use
			{
				if (ValidInputAction (inputAction, separatedInputWords))
				{
					// We let the actionInput take an appropiate response
					inputAction.RespondToInput(controller, separatedInputWords);
				}
			}
		}

		InputComplete();
	}

	bool ValidInputAction(InputAction inputAction, string[] separatedInputWords)
	{
		// If the InputAction is one word only 
		if (inputAction.isKeyWordSimple)
		{
			// If user input has only one word
			if (separatedInputWords.Length == 1)
				return true;
		} else {
			// If user input has exactly two words
			if (separatedInputWords.Length == 2)
			{
				return true;
			}
		}

		return false;

	}

	void InputComplete()
	{
		controller.DisplayLoggedText();						// Update Log Content
		inputField.ActivateInputField();					// Make cursor available (Regain focus of the UI)
		inputField.text = null;								// Previous input sent is discarded, input field ready to work
	}
}
