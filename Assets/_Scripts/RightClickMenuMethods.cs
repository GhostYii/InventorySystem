/* Class Name: RightClickMenuMethods
 * Describe: Write all methods what should called when rightclickagent pressed
 * Author: Ghostyii
 * Create Time: 2016/5/22
 */ 
 
using UnityEngine;
using System.Collections;

namespace InventorySystem
{
    static public class RightClickMenuMethods
    {
        static public void Use()
        { Debug.Log("Is this work?"); }
        static public void Equip(int itemId, EquipmentType equiType)
        { throw new System.NotImplementedException(); }
        static public void Delete(int dataID, int num = 1)
        { ItemGrid.Instance.RemoveItem(dataID, num); }
        static public void Sell()
        { throw new System.NotImplementedException(); }

    }
}