/* Class Name: ItemBase
 * Describe: The base of all Inventory Item,this is an abstract class
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    [RequireComponent(typeof(Image))]
    abstract public class ItemBase : MonoBehaviour
    {
        public int id = 0;        
        public string name = "";
        public ItemType itemType = ItemType.Item;
        public EquipmentType equipmentType = EquipmentType.None;
        public ItemLevel quality = ItemLevel.None;

        public bool repeatable = false;
        public int maxSum = 1;

        void Start()
        {
            OnStart();
        }

        //Use this instead of the Start method in a derived class
        virtual protected void OnStart() { }

        virtual public void SetItem(ItemData itemData)
        {
            id = itemData.id;
            repeatable = itemData.repeatable;
            maxSum = itemData.maxSum;
            name = itemData.tooltip.name;
            itemType = itemData.itemType;
            equipmentType = itemData.equipmentType;
            quality = itemData.level;
        }

        virtual public void Clear()
        {
            id = 0;
            repeatable = false;
            maxSum = 1;
            name = string.Empty;
            itemType = ItemType.Item;
            equipmentType = EquipmentType.None;
            quality = ItemLevel.None;
        }

        virtual public ItemData ToItemData()
        {
            ItemData result = new ItemData();
            result.id = id;
            result.itemType = itemType;
            result.equipmentType = equipmentType;
            result.level = quality;
            result.repeatable = repeatable;
            result.maxSum = maxSum;

            return result;
        }
    }
}
