/* Class Name: InventoryView
 * Describe: Control the InventorySystem's View part
 * Author: Ghostyii
 * Create Time: 2016/4/21
 */
 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [Header("Up Part")]
        public Text goldSum;
        public Text soulSum;

        [Header("Left Part")]
        public Image playerPreview;

        [Header("Right Part")]
        public Text inventoryVol;
        public Text currentPage;
        public Text pageSum;

        void OnGUI()
        {
            inventoryVol.text = string.Format("{0}/{1}", ItemGrid.Instance.LoadedItemSum, ItemGrid.Instance.equipBag.count);

            currentPage.text = ItemGrid.Instance.CurrntPage.ToString();
            pageSum.text = ItemGrid.Instance.GetPageSum().ToString();
        }
        
    }
}