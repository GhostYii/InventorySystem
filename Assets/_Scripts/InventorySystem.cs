/* Namespace Name: InventorySystem
 * Describe: The inventory system's control namespace
 * Author:　Ghostyii
 * Create Time: 2016/4/5
 */

using System;
using UnityEngine;

namespace InventorySystem
{
    //Item's level. Non-Weapen set to None
    public enum ItemLevel
    {
        None = 0,
        White,
        Green,
        Blue,
        Purple,
        Orange,
        Pink,
    }

    //Item's kind
    public enum ItemType
    {
        Equipment = 1,
        Item,
        Special,
        Task,
    }

    //Equipment's type, ItemType != Equipment set to None
    public enum EquipmentType
    {
        None = 0,
        Head,
        LeftHand,
        RightHand,
        Body,
        Leg,
        Foot,
    }

    //Click menu
    public enum ClickMenu
    {
        Use,
        Equip,
        Delete,
        Sell,
    }

    //Rect's four angle
    public enum RoughEdge
    {
        LeftTop,
        RightTop,
        LeftBottom,
        RightBottom,
    }

    //Item's right click menu actions.
    //Include an enum type var and an array that should be add to UnityEvent
    [Serializable]
    public struct RightClickAction
    {
        public ClickMenu menu;
        public UnityEngine.Events.UnityAction[] actions;
    }

    //The item's data node from the file
    [Serializable]
    public struct ItemData
    {
        //this value just use in save file and swap item
        public int dataID;

        public bool repeatable;
        public int id;
        public int num;
        public int maxSum;
        public ItemType itemType;
        public ItemLevel level;
        public EquipmentType equipmentType;        
        public string picName;

        public TooltipData tooltip;

        public void SetTooltip(ref TooltipData tooltip)
        {
            if (tooltip.id != id)
            {
                Debug.Log("Id error!");
                return;
            }

            tooltip.level = level;

            this.tooltip.id = tooltip.id;
            this.tooltip.level = tooltip.level;
            this.tooltip.name = tooltip.name;
            this.tooltip.introduction = tooltip.introduction;
            this.tooltip.effect = tooltip.effect;
        }
    }

    //The tooltip's data node from the file
    [Serializable]
    public struct TooltipData
    {
        [HideInInspector]
        public int id;
        [HideInInspector]
        public ItemLevel level;

        public string name;
        [Multiline(4)]
        public string introduction;
        [Multiline(8)]
        public string effect;

        public void Clear()
        {
            id = 0;
            level = ItemLevel.None;
            name = string.Empty;
            introduction = string.Empty;
            effect = string.Empty;
        }

        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(name) && string.IsNullOrEmpty(introduction) && string.IsNullOrEmpty(effect);
        }
    }

    [Serializable]
    public class ColorList
    {
        [SerializeField]
        private Color normal = Color.clear;
        public Color Normal
        { get { return normal; } }
        [SerializeField]
        private Color white = Color.white;
        public Color White
        { get { return white; } }
        [SerializeField]
        private Color green = Color.green;
        public Color Green
        { get { return green; } }
        [SerializeField]
        private Color blue = Color.blue;
        public Color Blue
        { get { return blue; } }
        [SerializeField]
        private Color purple = Color.white;
        public Color Purple
        { get { return purple; } }
        [SerializeField]
        private Color orange = Color.white;
        public Color Orange
        { get { return orange; } }
        [SerializeField]
        private Color pink = Color.white;
        public Color Pink
        { get { return pink; } }
    }
}
