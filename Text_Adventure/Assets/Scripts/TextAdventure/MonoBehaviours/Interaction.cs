using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]								//Adds a foldable "Interactions" option for Items
public class Interaction
{
	public InputAction inputAction;
	[TextArea]
	public string textResponse;
	public ActionResponse actionResponse;
}
