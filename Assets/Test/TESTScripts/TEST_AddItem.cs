/* Class Name: TEST_AddItem
 * Describe: 
 * Author: Ghostyii
 * Create Time: 
 */
using System;
using UnityEngine;
using System.Collections;
using InventorySystem;
using UnityEngine.UI;

public class TEST_AddItem : MonoBehaviour 
{
    public Text text;
    public InputField fieldNum;
    public InputField fieldId;

    public void SearchItem()
    {
        var id = Convert.ToInt32(fieldId.text);
        try
        { text.text = ItemDataController.Instance.FindItemDataWithId(id).tooltip.name; }
        catch (Exception)
        { text.text = "not found"; }
        
    }

    public void AddItem()
    {
        //print("check full have a bug. YOU MUST FIX IT RIGHT NOW!!!!");
        int id = Convert.ToInt32(fieldId.text);
        int num = Convert.ToInt32(fieldNum.text);

        ItemData data = ItemDataController.Instance.FindItemDataWithId(id);
        data.num = num;

        var tmp = ItemGrid.Instance.AddItem(data);
        
        ItemGrid.Instance.Refresh();
    }

    public void AddItemWithoutCheck()
    {
        print("Just add an item");

        int id = Convert.ToInt32(fieldId.text);
        int num = Convert.ToInt32(fieldNum.text);

        ItemData data = ItemDataController.Instance.FindItemDataWithId(id);
        data.num = num;

        ItemGrid.Instance.AddItemWithoutCheck(data);
        ItemGrid.Instance.Refresh();
    }
}
