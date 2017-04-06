using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using InventorySystem;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    private Item itemSetup;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        itemSetup = (Item)target;
        
        EditorGUILayout.LabelField("Gird Index", itemSetup.GridIndex.ToString());
        EditorGUILayout.LabelField("Data ID", itemSetup.DataID.ToString());
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("ID", itemSetup.id.ToString("D4"));
        EditorGUILayout.LabelField("Name", string.IsNullOrEmpty(itemSetup.name) ? "Unload" : itemSetup.name);
        itemSetup.sprite = (Sprite)EditorGUILayout.ObjectField("Item Image", itemSetup.sprite, typeof(Sprite), true);
        
        itemSetup.repeatable = EditorGUILayout.Toggle("Repeatable", itemSetup.repeatable);
        if (itemSetup.repeatable)
        {
            itemSetup.sum = EditorGUILayout.IntField("Sum", itemSetup.sum);
            EditorGUILayout.LabelField("Max Num", itemSetup.maxSum.ToString());
        
        }
        itemSetup.tooltip = (ToolTip)EditorGUILayout.ObjectField("Tooltip", itemSetup.tooltip, typeof(ToolTip), true);



        itemSetup.sumText = (Text)EditorGUILayout.ObjectField("Sum Text", itemSetup.sumText, typeof(Text), true);

        itemSetup.itemType = (ItemType)EditorGUILayout.EnumPopup("Type",itemSetup.itemType);
        if (itemSetup.itemType == ItemType.Equipment)
        {
            itemSetup.quality = (ItemLevel)EditorGUILayout.EnumPopup("Level", itemSetup.quality);
            itemSetup.equipmentType = (EquipmentType)EditorGUILayout.EnumPopup("Equipment Type", itemSetup.equipmentType);            
        }
        else
        {
            itemSetup.quality = ItemLevel.None;
            itemSetup.equipmentType = EquipmentType.None;
        }
    }
}
