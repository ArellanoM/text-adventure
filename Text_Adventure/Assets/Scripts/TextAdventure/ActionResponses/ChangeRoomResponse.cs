using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse
{
	public Room roomToChangeTo;

	public override bool DoActionResponse (GameController controller)
	{
		// If the current room is correct for current action
		if (controller.roomNavigation.currentRoom.roomName == requiredString)
		{
			controller.roomNavigation.currentRoom = roomToChangeTo;				// Change the current room
			controller.DisplayRoomText();										// Notify change through log
			return true;
		}

		return false;
	}
}
