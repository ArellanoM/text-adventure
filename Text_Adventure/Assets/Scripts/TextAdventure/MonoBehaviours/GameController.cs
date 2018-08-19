using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    /// <summary> The Text field where all events are displayed </summary>
	public Text displayText;
    /// <summary> The Text field where the name of the current room is displayed </summary>
    public Text roomDisplay;    // NEW!
    /// <summary> The array of available Input Actions </summary>
    /// <remarks> Input Actions are added through the Inspector </remarks>
    public InputAction[] inputActions;

	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
	[HideInInspector] public InteractableItems interactableItems;

    /// <summary> The "Raw" form of the Log of events </summary>
    /// <remarks> The elements of this List are combined into a single String and then get Displayed </remarks>
    List<string> actionLog = new List<string>();

	//
    // METHODS
    //
    
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


    /// <summary>
    /// Update Game's text Log from actionLog (an array)
    /// </summary>
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

    /// <summary>
    /// Loop through interactableObjectsInRoom and their interactions
    /// </summary>
    /// <param name="currentRoom"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="verbDictionary"></param>
    /// <param name="verb"></param>
    /// <param name="noun"></param>
    /// <returns></returns>
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
        // Display Room name in Event Bar (Header)
        roomDisplay.text = roomNavigation.currentRoom.displayName;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
