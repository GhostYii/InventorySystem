/* Class Name: EnumParse
 * Describe: Try parse the string to diffren
 * Author: Ghostyii
 * Create Time: 2016/4/18
 */

using UnityEngine;
using InventorySystem;

static public class EnumParse
{
    /// <summary>
    /// Convert string to itemType, doesn't distinguish between uppercase and lowercase.
    /// </summary>
    /// <param name="value">the string you want to convert</param>
    /// <returns>With the same string name of the ItemType</returns>
    static public ItemType ToItemType(string value)
    {
        value = value.ToLower();

        switch (value)
        {
            case "equipment":
                return ItemType.Equipment;
            case "item":
                return ItemType.Item;
            case "special":
                return ItemType.Special;
            case "task":
                return ItemType.Task;
            default:
                throw new System.Exception("Can't convert this string to ItemType!");
        }
    }

    /// <summary>
    /// Convert string to ItemLevel, doesn't distinguish between uppercase and lowercase.
    /// </summary>
    /// <param name="value">the string you want to convert</param>
    /// <returns>With the same string name of the ItemLevel</returns>
    static public ItemLevel ToItemLevel(string value)
    {
        value = value.ToLower();

        switch (value)
        {
            case "white":
                return ItemLevel.White;
            case "green":
                return ItemLevel.Green;
            case "blue":
                return ItemLevel.Blue;
            case "purple":
                return ItemLevel.Purple;
            case "orange":
                return ItemLevel.Orange;
            case "pink":
                return ItemLevel.Pink;
            case "none":
                return ItemLevel.None;
            default:
                //return ItemLevel.None;
                throw new System.Exception("Can't convert this string to ItemLevel!");
        }
    }

    /// <summary>
    /// Convert string to EquipmentType, doesn't distinguish between uppercase and lowercase.
    /// </summary>
    /// <param name="value">the string you want to convert</param>
    /// <returns>With the same string name of the EquipmentType</returns>
    static public EquipmentType ToEquipmentType(string value)
    {
        value = value.ToLower();

        switch (value)
        {
            case "head":
                return EquipmentType.Head;
            case "lefthand":
                return EquipmentType.LeftHand;
            case "righthand":
                return EquipmentType.RightHand;
            case "body":
                return EquipmentType.Body;
            case "leg":
                return EquipmentType.Leg;
            case "foot":
                return EquipmentType.Foot;
            case "none":
                return EquipmentType.None;
            default:
                //return EquipmentType.None;
                throw new System.Exception("Can't convert this string to EquipmenType!");
        }
    }
}
