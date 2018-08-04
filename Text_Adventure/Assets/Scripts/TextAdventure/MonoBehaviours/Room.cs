using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject
{
    public string displayName;
    [TextArea]
    public string description;
	public string roomName;
	public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;
}
