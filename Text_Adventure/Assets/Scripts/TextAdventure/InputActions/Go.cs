using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "TextAdventure/InputActions/Go")]
public class Go : InputAction
{
	public override void RespondToInput (GameController controller, string[] separatedInputWords)
	{
		// Attempt to change rooms with the Direction string passed as argument ([0=ActionGo][1=Direction])
		controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
	}
}
