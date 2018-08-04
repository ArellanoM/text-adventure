using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
	GameController controller;
    AudioController audioController;

	void Awake()
	{
		controller = GetComponent<GameController>();
        audioController = GetComponent<AudioController>();
	}

	public void UnpackExitsInRoom()
	{
		for (int i = 0; i < currentRoom.exits.Length; i++)													// For every exit in the current room:
		{
			exitDictionary.Add (currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);			// Store (keyString, valueRoom) into
			controller.interactionDescriptionsInRoom.Add (currentRoom.exits [i].exitDescription);
		}
	}

	public void AttemptToChangeRooms(string directionNoun)	// i.e. north
	{
		if (exitDictionary.ContainsKey (directionNoun))     // If direction chosen exists for currentRoom
        {

            for (int i = 0; i < currentRoom.exits.Length; i++)  // We search for current exit data
            {
                if (currentRoom.exits[i].keyString == directionNoun)  // If this is the exit we care about
                {
                    if (!currentRoom.exits[i].locked)  // If exit is unlocked
                    {
                        audioController.audioWalking.Play();
                        currentRoom = exitDictionary[directionNoun];
                        controller.LogStringWithReturn("Te encaminas hacia el <color=#33ccffff>" + directionNoun + "</color>");
                        controller.DisplayRoomText();                                              // We are in a new room, so we display its description
                    }
                    else
                    {
                        audioController.audioLockedDoor.Play();
                        controller.LogStringWithReturn("La puerta al <color=#33ccffff>" + directionNoun + "</color> está cerrada.");
                    }
                    break; // We don't need to search for the next exit
                }

            }
        }
		else
		{
			controller.LogStringWithReturn("No hay camino posible hacia el <color=#33ccffff>" + directionNoun + "</color>");
		}
	}

	// On new room entrance, clear current exit dictionary state
	public void ClearExits()
	{
		exitDictionary.Clear();
	}
}
