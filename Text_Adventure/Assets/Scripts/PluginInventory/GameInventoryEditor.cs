using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameInventory))]   // Type of object targetted (so it shows in the Inspector)
public class GameInventoryEditor : Editor
{
    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;
    private bool[] showItemSlots = new bool[GameInventory.numItemSlots];

    // Both these string constants value MUST match item and item image variable names
    private const string gameInventoryPropItemImagesName = "itemImages";
    private const string gameInventoryPropItemsName = "items";

    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(gameInventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(gameInventoryPropItemsName);
    }

    public override void OnInspectorGUI()
    {
        // We need information up to date
        serializedObject.Update();

        for (int i = 0; i < GameInventory.numItemSlots; i++)
        {
            ItemSlotGUI(i);
        }

        // 
        serializedObject.ApplyModifiedProperties();
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);    // The following will be drawn in a box
        EditorGUI.indentLevel++;

        showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Item slot " + index);

        if (showItemSlots[index])
        {
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
