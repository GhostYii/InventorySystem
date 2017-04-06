/* Class Name: ItemGrid
 * Describe: Single Class, Manage all item's grid
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */ 

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Xml;

namespace InventorySystem
{
    public class ItemGrid : MonoBehaviour
    {
        //Refresh the display of the maximum number
        //InventoryManager.Instance.MAXSHOWSUM should be equal to this value        
        public int maxShowSum = 35;
        //Equiped bag gear ref
        public BagGear equipBag;
        //Backgrounds Length must be greater than maxShowSum
        public ItemBackground[] itemPos;
        //Item scripts Length must be less than or equal to maxShowSum
        public Item[] itemList;

        [SerializeField]
        private ItemClassifyList loadedItems = new ItemClassifyList();
        public List<ItemData> EquipmentList
        { get { return loadedItems.equipments; } }
        public List<ItemData> ItemList
        { get { return loadedItems.items; } }
        public List<ItemData> SpecailList
        { get { return loadedItems.specials; } }
        public List<ItemData> TaskList
        { get { return loadedItems.tasks; } }

        private int page = 1;
        public int CurrntPage
        { get { return page; } }

        [SerializeField]
        private List<ItemData> allItemData = new List<ItemData>();
        public List<ItemData> AllItemData
        { get { return allItemData; } }
        public int CurrentItemDataSum
        { get { return allItemData.Count; } }

        [SerializeField]
        private ItemType currentType = ItemType.Equipment;      
        public int LoadedItemSum
        { get { return loadedItems.Count; } }
        
        static public ItemGrid Instance;
        
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        void Start()
        {
            //int loopTimes = Random.Range(1, equipBag.count * 2 + 1);
            //Debug.Log("Max sum :" + loopTimes * 2);
            //for (int i = 0; i < loopTimes; i++)
            //{
            //    //AddItem(ItemDataController.Instance.WeaponsData[Random.Range(0, 3)]);
            //    ItemData tmp = ItemDataController.Instance.ItemsData[Random.Range(0, 4)];
            //    if (tmp.repeatable)
            //        tmp.num = Random.Range(1, tmp.maxSum + 1);

            //    AddItem(tmp);
            //}

            ShowItems(ItemType.Equipment, page);
            //Save();
        }
    
        public ItemData AddItem(ItemData data)
        {
            if(CheckFull(data))
            {
                Debug.Log("Inventory is full!");
                loadedItems.Group(allItemData);
                return data;
            }

            List<ItemData> results = new List<ItemData>();
            List<int> indexs = new List<int>();
            ItemData result = data;
            int count = allItemData.Count;

            for (int i = 0; i < count; i++)
            {
                if (allItemData[i].id == result.id && allItemData[i].num < allItemData[i].maxSum)
                {
                    results.Add(allItemData[i]);
                    indexs.Add(i);
                }
            }

            int len = results.Count;
            for (int i = 0; i < len; i++)
            {
                ItemData tmp = results[i];
                if (tmp.num + result.num > tmp.maxSum)
                {
                    result.num -= (tmp.maxSum - tmp.num);
                    tmp.num = tmp.maxSum;
                    results[i] = tmp;
                    result.num = result.num > 0 ? result.num : 0;
                }
                else
                {
                    tmp.num = tmp.num + result.num;
                    results[i] = tmp;
                    result.num = 0;
                }
            }

            int length = indexs.Count;
            for (int i = 0; i < length; i++)
                allItemData[indexs[i]] = results[i];

            if(result.num > 0)
            {
                int loopTimes = result.num % result.maxSum == 0 ? (result.num / result.maxSum == 0 ? 1 : result.num / result.maxSum) : (result.num / result.maxSum) + 1;
                for (int i = 1; i < loopTimes; i++)
                {
                    ItemData full = result;
                    full.num = full.maxSum;
                    if (!CheckFull(full))
                    {
                        full.dataID = allItemData.Count;
                        allItemData.Add(full);
                        equipBag.FieldCount = allItemData.Count;
                        result.num -= result.maxSum;
                    }
                    else
                    {
                        loadedItems.Group(allItemData);
                        return result;
                    }
                }

                if (result.num > 0)
                {
                    if (!CheckFull(result))
                    {
                        result.dataID = allItemData.Count;
                        allItemData.Add(result);
                        result.num = 0;
                        equipBag.FieldCount = allItemData.Count;
                    }
                    else
                    {
                        loadedItems.Group(allItemData);
                        return result;
                    }
                }
            }

            loadedItems.Group(allItemData);
            return result;
        }
        public ItemData AddItem(int id, int num)
        {
            ItemData temp = ItemDataController.Instance.FindItemDataWithId(id);
            temp.num = num;
            return AddItem(temp);
        }
        /*protected*/public void AddItemWithoutCheck(ItemData data)
        {
            if (CheckFull())
            {
                Debug.Log("Inventory Full");
                return;
            }

            try
            {
                data.dataID = allItemData.Count;
                allItemData.Add(data);
            }
            catch (System.Exception e)
            { Debug.LogError(e.Message); }
            finally
            { loadedItems.Group(allItemData); }
        }

        public bool RemoveItem(ItemData data)
        {
            bool result =  allItemData.Remove(data);
            loadedItems.Group(allItemData);
            ShowItems(currentType, CurrntPage);
            return result;
        }
        public bool RemoveItem(int dataId)
        {
            bool result = allItemData.Remove(allItemData[dataId]);
            loadedItems.Group(allItemData);
            ShowItems(currentType, CurrntPage);
            return result;
        }  
        public bool RemoveItem(int dataId, int num)
        {
            if (num < allItemData[dataId].num)
            {
                ItemData tmp = allItemData[dataId];
                tmp.num -= num;
                allItemData[dataId] = tmp;
                loadedItems.Group(allItemData);
                ShowItems(currentType, CurrntPage);
                return true;
            }
            else
            {
                loadedItems.Group(allItemData);
                ShowItems(currentType, CurrntPage);
                return RemoveItem(dataId);
            }
        }
        public bool RemoveItem(ItemData data, int num)
        { throw new System.NotImplementedException(); }

        //Return true if inventory is full
        public bool CheckFull()
        { return equipBag.IsFull; }
        //Check inventory is full if add data
        //Return true if inventory is full
        public bool CheckFull(ItemData data)
        {
            if (CheckFull())
                return true;

            if (!data.repeatable)
                return CheckFull();
            else
            {                
                int num = 0, sum = 0;
                List<ItemData> list = loadedItems.FindListById(data.id);
                num = list.Count;
                
                if (num == 0)
                    return CheckFull();                

                for (int i = 0; i < num; i++)                
                    sum += list[i].num;

                return sum + data.num > (num + equipBag.RemainSlot) * data.maxSum;
                
            }
        }

        //Swap item data (dataID is unchanged)
        public void SwapItemDataByDataID(int id1,int id2)
        {
            ItemData temp1 = allItemData[id1];
            ItemData temp2 = allItemData[id2];
            int id = temp1.dataID;
            temp1.dataID = temp2.dataID;
            temp2.dataID = id;

            allItemData[id1] = temp2;
            allItemData[id2] = temp1;
        }
        public void ChangeItemSumByDataID(int id,int sum)
        {
            ItemData tmp = allItemData[id];
            tmp.num = sum;
            allItemData[id] = tmp; 
        }

        //Refresh the inventory
        public void Refresh()
        {
            loadedItems.Group(allItemData);
            ShowItems(currentType, 1);
        }
        public void Refresh(int page)
        {
            loadedItems.Group(allItemData);
            ShowItems(currentType, page);
        }

        //Display a page in the inventory of items
        public void ShowItems(ItemType type, int page = 1)
        {
            this.page = page;
            ClearPanle();
            currentType = type;
            List<ItemData> currentList = GetCurrentList();

            int start = maxShowSum * (page - 1), end = page * maxShowSum - 1;
            int index = 0;
            for (int i = start; i <= end; i++)
            {
                if (i < currentList.Count)
                {
                    itemList[index].SetItem(currentList[i]);
                    itemList[index].GridIndex = i;
                    itemList[index].sumText.enabled = currentList[i].repeatable;
                    index++;
                }
                else
                    itemList[i % maxShowSum].SetNull();
            }
        }

        //Clear the old item's message
        public void ClearPanle()
        {
            for (int i = 0; i < itemList.Length; i++)
                itemList[i].Clear();
        }

        //Find item by grid index
        public Item FindItemByGridIndex(int gridIndex)
        {
            if (gridIndex < 0)
                return null;

            gridIndex = gridIndex >= ItemGrid.Instance.maxShowSum ? gridIndex % ItemGrid.Instance.maxShowSum : gridIndex;

            if (itemList[gridIndex].FixedGridIndex == gridIndex)
                return itemList[gridIndex];
            else
            {
                for (int i = 0; i < itemList.Length; i++)
                    if (itemList[i].FixedGridIndex == gridIndex)
                        return itemList[i];

                return null;
            }
        }

        //Get loaded item list by ItemType
        private List<ItemData> GetListByItemType(ItemType type)
        {
            switch (type)
            {
                case ItemType.Equipment:
                    return EquipmentList;
                case ItemType.Item:
                    return ItemList;
                case ItemType.Special:
                    return SpecailList;
                case ItemType.Task:
                    return TaskList;
                default:
                    Debug.Log("type error!");
                    return null;
            }
        }
        private List<ItemData> GetCurrentList()
        { return GetListByItemType(currentType); }
        private List<ItemData> GetListByItemId(int id)
        { return GetListByItemType((ItemType)(id / 1000 + 1)); }

        //UI use those for params
        public void ShowItems(int typeIndex)
        {
            page = 1;
            ShowItems((ItemType)typeIndex, page);
        }
        public int GetPageSum()
        {
            int count = GetCurrentList().Count;
            return count % maxShowSum == 0 ? (count / maxShowSum == 0 ? 1 : count / maxShowSum) : (count / maxShowSum) + 1;
        }
        public void NextPage()
        {
            int tmpPage = page++;
            page = Mathf.Clamp(page, 1, GetPageSum());
            if (tmpPage == page)
                return;
            ShowItems(currentType, page);
        }
        public void PrevPage()
        {
            int tmpPage = page--;
            page = Mathf.Clamp(page, 1, GetPageSum());
            if (tmpPage == page)
                return;
            ShowItems(currentType, page);
        }

        //Save item mes to file
        public XmlElement Save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml("<root>root</root>");
            xmlDoc.Load(Application.dataPath + "/test.xml");
            XmlElement root = xmlDoc.DocumentElement;
            XmlElement result = xmlDoc.CreateElement("inventory_items"); ;

            int count = allItemData.Count;
            for (int i = 0; i < count; i++)
            {
                XmlElement node = xmlDoc.CreateElement("item");
                XmlElement itemType = xmlDoc.CreateElement("itemType");
                itemType.InnerText = allItemData[i].itemType.ToString();
                XmlElement picName = xmlDoc.CreateElement("picName");
                picName.InnerText = allItemData[i].picName;
                XmlElement repeatable = xmlDoc.CreateElement("repeatable");
                repeatable.InnerText = allItemData[i].repeatable.ToString();

                node.SetAttribute("dataID", allItemData[i].dataID.ToString("D4"));
                node.SetAttribute("id", allItemData[i].id.ToString("D4"));
                node.AppendChild(itemType);
                node.AppendChild(picName);
                node.AppendChild(repeatable);

                if (allItemData[i].repeatable)
                {
                    XmlElement num = xmlDoc.CreateElement("num");
                    num.InnerText = allItemData[i].num.ToString();
                    //XmlElement repeatable = xmlDoc.CreateElement("repeatable");
                    //repeatable.InnerText = allItemData[i].repeatable.ToString();
                    XmlElement maxSum = xmlDoc.CreateElement("maxSum");
                    maxSum.InnerText = allItemData[i].maxSum.ToString();

                    //node.AppendChild(repeatable);
                    node.AppendChild(num);
                    node.AppendChild(maxSum);
                }

                if (allItemData[i].itemType == ItemType.Equipment)
                {
                    XmlElement equipmentType = xmlDoc.CreateElement("equipmentType");
                    equipmentType.InnerText = allItemData[i].equipmentType.ToString();
                    XmlElement level = xmlDoc.CreateElement("level");
                    level.InnerText = allItemData[i].level.ToString();

                    node.AppendChild(level);
                    node.AppendChild(equipmentType);
                }

                result.AppendChild(node);
            }

            root.AppendChild(result);
            //root.InsertAfter(result, root.FirstChild);
            xmlDoc.Save(Application.dataPath + "/test.xml");
            print("Save completed!");
            return result;
        }

        public void Load(string path)
        { throw new System.NotImplementedException(); }
        public void Load(List<ItemData> datas)
        {
            allItemData.Clear();
            allItemData = datas;
            loadedItems.Group(allItemData);
        }
        public void Load(XmlElement items)
        {
            if (items.Name != "inventory_items")
            {
                print(items.Name);
                return;
            }

            List<ItemData> data = new List<ItemData>();
            ItemData temp = new ItemData();
            XmlNodeList nodes = items.ChildNodes;
            foreach (XmlNode item in nodes)
            {
                temp.dataID = XmlConvert.ToInt32(item.Attributes["dataID"].InnerText);
                print(temp.dataID);
                temp.id = XmlConvert.ToInt32(item.Attributes["id"].InnerText);
                print(temp.id);
                temp.itemType = EnumParse.ToItemType(item.ChildNodes[0].InnerText);
                print(temp.itemType);
                temp.picName = item.ChildNodes[1].InnerText;
                print(temp.picName);

                temp.repeatable = XmlConvert.ToBoolean(item.ChildNodes[2].InnerText.ToLower());
                
                if (temp.repeatable)
                {
                    temp.num = XmlConvert.ToInt32(item.ChildNodes[3].InnerText);
                    temp.maxSum = XmlConvert.ToInt32(item.ChildNodes[4].InnerText);
                }
                else
                {
                    if (temp.itemType == ItemType.Equipment)
                    {
                        temp.level = EnumParse.ToItemLevel(item.ChildNodes[3].InnerText);
                        temp.equipmentType = EnumParse.ToEquipmentType(item.ChildNodes[4].InnerText);

                    }
                }
                data.Add(temp);
            }

            Load(data);
        }

    }
}
