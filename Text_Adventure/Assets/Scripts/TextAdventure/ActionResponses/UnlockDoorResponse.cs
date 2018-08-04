using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/UnlockDoor")]
public class UnlockDoorResponse : ActionResponse
{
    public string doorDirection;

    public override bool DoActionResponse(GameController controller)
    {
        // If the current room is correct for current action
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            for (int i = 0; i < controller.roomNavigation.currentRoom.exits.Length; i++)  // We search for current exit data
            {
                Exit currentExit = controller.roomNavigation.currentRoom.exits[i];  // We make an Exit variable for legibility's sake
                if (currentExit.keyString == doorDirection)  // If this is the exit we care about
                {
                    if (currentExit.locked) // Only IF door is locked
                    {
                        currentExit.locked = false;  // Unlock door
                        controller.LogStringWithReturn("La puerta fue desbloqueada.");
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
