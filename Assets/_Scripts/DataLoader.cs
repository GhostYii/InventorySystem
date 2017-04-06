/* Class Name: DataLoader
 * Describe: Load Some Data...
 * Author: Ghostyii
 * Create Time: 2016/4/18
 */

using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.IO;

namespace InventorySystem
{
    static public class DataLoader
    {
        /// <summary>
        /// Load item's datas from Resources
        /// </summary>
        /// <param name="fileName">File's name</param>
        /// <returns>ItemData array</returns>
        static public ItemData[] LoadItemData(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(Resources.Load<TextAsset>(fileName).text);

            XmlNode itemMes = xmlDoc.SelectSingleNode("ItemMes");
            XmlNodeList elements = itemMes.ChildNodes;

            ItemData[] nodes = new ItemData[elements.Count];

            for (int i = 0; i < nodes.Length; i++)
            {
                XmlElement element = (XmlElement)elements[i];
                nodes[i].id = Convert.ToInt32(element.GetAttribute("ID"));
                nodes[i].itemType = EnumParse.ToItemType(element.GetAttribute("type"));

                XmlNodeList childList = element.ChildNodes;
                nodes[i].repeatable = Convert.ToBoolean(childList.Item(0).InnerText);
                nodes[i].level = EnumParse.ToItemLevel(childList.Item(1).InnerText);
                nodes[i].equipmentType = EnumParse.ToEquipmentType(childList.Item(2).InnerText);
                nodes[i].maxSum = Convert.ToInt32(childList.Item(3).InnerText);

                nodes[i].picName = childList.Item(4).InnerText;

                nodes[i].num = 1;
            }

            return nodes;
        }

        /// <summary>
        /// Load item's tooltips from file
        /// </summary>
        /// <param name="path">file path</param>
        /// <returns>TooltipData array</returns>
        static public TooltipData[] LoadToolTipData(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText(path));

            XmlNode root = xmlDoc.SelectSingleNode("TooltipData");
            XmlNodeList elements = root.ChildNodes;

            TooltipData[] nodes = new TooltipData[elements.Count];
            for (int i = 0; i < nodes.Length; i++)
            {
                XmlElement element = (XmlElement)elements[i];
                nodes[i].id = Convert.ToInt32(element.GetAttribute("ID"));
                //nodes[i].level = 

                XmlNodeList childList = element.ChildNodes;
                nodes[i].name = childList.Item(0).InnerText;
                nodes[i].introduction = childList.Item(1).InnerText;
                nodes[i].effect = childList.Item(2).InnerText;
            }

            return nodes;
        }        
    }
}