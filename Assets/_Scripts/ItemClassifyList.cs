/* Class Name: ItemClassifyList
 * Describe: ItemGrid script need this class to manager the grid
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */

using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class ItemClassifyList
    {
        public List<ItemData> equipments;
        public List<ItemData> items;
        public List<ItemData> specials;
        public List<ItemData> tasks;

        public ItemClassifyList()
        {
            equipments = new List<ItemData>();
            items = new List<ItemData>();
            specials = new List<ItemData>();
            tasks = new List<ItemData>();
        }

        public int Count
        { get { return equipments.Count + items.Count + specials.Count + tasks.Count; } }

        public List<ItemData> FindListById(int id)
        {
            List<ItemData> result = new List<ItemData>();

            List<ItemData> list = new List<ItemData>();
            switch ((ItemType)(id / 1000 + 1))
            {
                case ItemType.Equipment:
                    list = equipments;
                    break;
                case ItemType.Item:
                    list = items;
                    break;
                case ItemType.Special:
                    list = specials;
                    break;
                case ItemType.Task:
                    list = tasks;
                    break;
                default:
                    Debug.Log("ItemType Error!");
                    break;
            }

            int count = list.Count;
            for (int i = 0; i < count; i++)
                if (list[i].id == id)
                    result.Add(list[i]);

            return result;
        }

        public List<ItemData> FindItemsListById(int id)
        {
            List<ItemData> result = new List<ItemData>();
            List<ItemData> list = FindListById(id);
            int count = list.Count;

            for (int i = 0; i < count; i++)
                if (list[i].id == id)
                    result.Add(list[i]);

            return result;
        }

        public ItemData[] FindItemsArrayById(int id)
        { return FindItemsListById(id).ToArray(); }

        //get value form datas or allItems
        public void Group(List<ItemData> datas)
        {
            Clear();
            int count = datas.Count;
            for (int i = 0;i < count;i++)
            {
                ItemData tmp = datas[i];
                tmp.dataID = i;
                datas[i] = tmp;

                switch (datas[i].itemType)
                {
                    case ItemType.Equipment:
                        equipments.Add(datas[i]);
                        break;
                    case ItemType.Item:
                        items.Add(datas[i]);
                        break;
                    case ItemType.Special:
                        specials.Add(datas[i]);
                        break;
                    case ItemType.Task:
                        tasks.Add(datas[i]);
                        break;
                    default:
                        Debug.Log(datas[i].tooltip.name + "'s Type Error!\n Id:" + datas[i].id);
                        break;
                }
            }
        }
        public void Group(ItemData[] allItems)
        {
            Clear();
            int count = allItems.Length;
            for (int i = 0; i < count; i++)
            {
                ItemData tmp = allItems[i];
                tmp.dataID = i;
                allItems[i] = tmp;
                switch (allItems[i].itemType)
                {
                    case ItemType.Equipment:
                        equipments.Add(allItems[i]);
                        break;
                    case ItemType.Item:
                        items.Add(allItems[i]);
                        break;
                    case ItemType.Special:
                        specials.Add(allItems[i]);
                        break;
                    case ItemType.Task:
                        tasks.Add(allItems[i]);
                        break;
                    default:
                        Debug.Log(allItems[i].tooltip.name + "'s Type Error!\n Id:" + allItems[i].id);
                        break;
                }
            }
        }

        public void Clear()
        {
            equipments.Clear();
            items.Clear();
            specials.Clear();
            tasks.Clear();
        }
    }
}