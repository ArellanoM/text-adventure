using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text displayText;
    public Text roomDisplay;    // NEW!
    public InputAction[] inputActions;

    [HideInInspector] public int numeroDeAccion = 86;

	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
	[HideInInspector] public InteractableItems interactableItems;

	List<string> actionLog = new List<string>();

	// Use this for initialization
	void Awake ()
	{
		interactableItems = GetComponent<InteractableItems>();
		roomNavigation = GetComponent<RoomNavigation>();
	}

	void Start()
	{
        //NEW!
        displayText.supportRichText = true;

        //LogStringWithReturn(Constants.TITLE);
        LogStringWithReturn(Constants.INTRO);   // Display introductory text
        DisplayRoomText();
		DisplayLoggedText();
	}

	public void DisplayHelp()
	{
		LogStringWithReturn(Constants.AYUDA);
	}

	public void DisplayLoggedText()
	{
		string logAsText = string.Join ("\n", actionLog.ToArray());				// List of strings (Log) -> Join into a single string

		displayText.text = logAsText;											// Update display of Log
	}

	public void DisplayRoomText()
	{
		ClearCollectionsForNewRoom();

		UnpackRoom();

		string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray ());

		string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

		LogStringWithReturn(combinedText);
	}

	void UnpackRoom()
	{
		roomNavigation.UnpackExitsInRoom();
		PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
	}

	void PrepareObjectsToTakeOrExamine(Room currentRoom)
	{
		for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
		{
			string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
			if (descriptionNotInInventory != null)
			{
				interactionDescriptionsInRoom.Add(descriptionNotInInventory);
			}

			InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

			for (int j = 0; j < interactableInRoom.interactions.Length; j++)
			{
				Interaction interaction = interactableInRoom.interactions[j];
				if (interaction.inputAction.keyWord == "examinar")
				{
					interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
				}

				if (interaction.inputAction.keyWord == "agarrar")
				{
					interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
				}
			}
		}
	}

	public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
	{
		if (verbDictionary.ContainsKey (noun))
		{
			return verbDictionary [noun];
		}

		return "No podés " + verb + " " + noun;
	}

	void ClearCollectionsForNewRoom()
	{
		interactableItems.ClearCollections();
		interactionDescriptionsInRoom.Clear();
		roomNavigation.ClearExits();
	}

	public void LogStringWithReturn(string stringToAdd)
	{
		actionLog.Add(stringToAdd + "\n");
	}
	
	// Update is called once per frame
	void Update ()
    {
        roomDisplay.text = roomNavigation.currentRoom.displayName;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
