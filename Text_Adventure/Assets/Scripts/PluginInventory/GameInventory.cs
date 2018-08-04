using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInventory : MonoBehaviour
{
    public Image[] itemImages = new Image[numItemSlots];
    public InteractableObject[] items = new InteractableObject[numItemSlots];

    public const int numItemSlots = 4;

    public void AddItem(InteractableObject itemToAdd)
    {
        // Search for an empty item slot, then populate it
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)                           // If Item Slot is empty
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;               // We re-enable item image
                return;                                     // As soon as we find an empty item slot, stop looping
            }
        }
    }

    public void RemoveItem(InteractableObject itemToRemove)
    {
        // Search for item to remove, then get rid of it
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)                           // If we found item to remove
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;               // We disable item image
                return;                                     // As soon as we find the appropiate item slot, stop looping
            }
        }
    }
}
