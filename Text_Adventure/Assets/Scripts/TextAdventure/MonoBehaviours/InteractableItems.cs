using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();


	[HideInInspector] public List<string> nounsInRoom = new List<string>();

	Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
	List<string> nounsInInventory = new List<string>();
	GameController controller;

    // Handles all sounds
    AudioController audioController;

    //OnScreen Inventory handling
    GameInventory gameInventory;

	void Awake()
	{
        controller = GetComponent<GameController>();

        audioController = GetComponent<AudioController>();

        gameInventory = GetComponent<GameInventory>();
    }

	public string GetObjectsNotInInventory(Room currentRoom, int i)
	{
		InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

		if (!nounsInInventory.Contains(interactableInRoom.noun))
		{
			nounsInRoom.Add(interactableInRoom.noun);
			return interactableInRoom.description;
		}

		return null;
	}

	public void AddActionResponsesToUseDictionary()
	{
		for (int i = 0; i < nounsInInventory.Count; i++)
		{
			string noun = nounsInInventory[i];

			InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
			if (interactableObjectInInventory == null)
				continue;

			for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
			{
				Interaction interaction = interactableObjectInInventory.interactions[j];

				if (interaction.actionResponse == null)
					continue;

				if (!useDictionary.ContainsKey(noun))
				{
					useDictionary.Add(noun, interaction.actionResponse);
				}
			}
		}
	}

    // Returns an object that can be interacted with (must be in Usable Item List)
    InteractableObject GetInteractableObjectFromUsableList(string noun)
	{
		for (int i = 0; i < usableItemList.Count; i++)
		{
			if (usableItemList[i].noun == noun)
			{
				return usableItemList[i];
			}
		}

		return null;
	}

	public void DisplayInventory()
	{
		if (nounsInInventory.Count != 0)
		{
			controller.LogStringWithReturn("Revisás tu <color=green>mochila</color>, contiene: ");

			for (int i = 0; i < nounsInInventory.Count; i++)
			{
				controller.LogStringWithReturn ("<color=yellow>"+ nounsInInventory [i]+"</color>");
			}

		} else
			{
				controller.LogStringWithReturn ("La mochila está vacía.");
			}
	}

	public void ClearCollections()
	{
		examineDictionary.Clear();
		takeDictionary.Clear();
		nounsInRoom.Clear();
	}

	public Dictionary<string, string> Take(string[] separatedInputWords)
	{
		string noun = separatedInputWords[1];

		if (nounsInRoom.Contains (noun)) {
			nounsInInventory.Add (noun);
			// Custom
			audioController.audioTakeItem.Play();
            // OnScreen Inventory
            //InteractableObject itemToBeAdded = usableItemList.Find(x => x.noun == noun);
            InteractableObject itemToBeAdded = GetInteractableObjectFromUsableList(noun);
            gameInventory.AddItem(itemToBeAdded);

            AddActionResponsesToUseDictionary();
			nounsInRoom.Remove (noun);
			return takeDictionary;
		}
		else
		{
			controller.LogStringWithReturn("No se encuentra " + noun + " que pueda agarrarse");
			return null;
		}
	}

	public void UseItem(string[] separatedInputWords)
	{
        // For example "key"
        string nounToUse = separatedInputWords[1];

		if (nounsInInventory.Contains (nounToUse))
		{
			if (useDictionary.ContainsKey (nounToUse)) {
                bool actionResult = useDictionary [nounToUse].DoActionResponse (controller);
                if (actionResult)   //Was action succesful?
                {
                    // Custom
                    InteractableObject itemToBeRemoved = GetInteractableObjectFromUsableList(nounToUse);
                    gameInventory.RemoveItem(itemToBeRemoved);  // Remove object from OnScreen Inventory
                    nounsInInventory.Remove(nounToUse); // Remove object after using
                    audioController.audioSuccess.Play();
                }
                else
                    controller.LogStringWithReturn("Hmm. No ocurre nada.");
            } else {
				controller.LogStringWithReturn ("<color=yellow>" + nounToUse + "</color> no puede usarse.");
			}
		} else {
			controller.LogStringWithReturn("No hay " + nounToUse + " en tu mochila que pueda usarse.");
		}
	}

}
