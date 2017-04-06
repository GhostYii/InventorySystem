/* Class Name: ItemDataControler
 * Describe: Single Class, Control the item data
 * Author: Ghostyii
 * Create Time: 2016/4/18
 */

using System;
using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class ItemDataController : MonoBehaviour
    {
        [SerializeField]
        private AllItemData loadedItemData;
        public ItemData[] WeaponsData
        { get { return loadedItemData.weapons; } }
        public ItemData[] ItemsData
        { get { return loadedItemData.items; } }
        public ItemData[] SpecialsData
        { get { return loadedItemData.specials; } }
        public ItemData[] TasksData
        { get { return loadedItemData.tasks; } }

        static public ItemDataController Instance;

        private TooltipData[] tooltips;
        
        void Awake()
        {
            loadedItemData.weapons = DataLoader.LoadItemData("ItemMes_Weapons");
            tooltips = DataLoader.LoadToolTipData(PathController.CHN.CHS.EquipmentsTooltipSaveFile);
            for (int i = 0; i < tooltips.Length; i++)
                loadedItemData.weapons[i].SetTooltip(ref tooltips[i]);

            loadedItemData.items = DataLoader.LoadItemData("ItemMes_Items");
            tooltips = DataLoader.LoadToolTipData(PathController.CHN.CHS.ItemTooltipSaveFile);
            for (int i = 0; i < tooltips.Length; i++)
                loadedItemData.items[i].SetTooltip(ref tooltips[i]);

            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public ItemData FindItemDataWithId(int id)
        {
            ItemData[] range = GetDataRange(id);
            foreach (ItemData item in range)
            {
                if (item.id == id)
                    return item;
            }

            throw new Exception("Can't Find id = " + id + "'s item mes");
        }

        public ItemData[] GetDataRange(int id)
        {
            ItemType aimType = (ItemType)(id / 1000 + 1);
            switch (aimType)
            {
                case ItemType.Equipment:
                    return WeaponsData;
                case ItemType.Item:
                    return ItemsData;
                case ItemType.Special:
                    return SpecialsData;
                case ItemType.Task:
                    return TasksData;
                default:
                    Debug.Log("Aim Type Error!");
                    return null;
            }
        }

        [Serializable]
        protected class AllItemData
        {
            public ItemData[] weapons;
            public ItemData[] items;
            public ItemData[] specials;
            public ItemData[] tasks;
        }
    }
}
