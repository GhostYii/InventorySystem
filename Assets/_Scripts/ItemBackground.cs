/* Namespace Name: ItemBackground
 * Describe: Control the inventory-grid's background color
 * Author:　Ghostyii
 * Create Time: 2016/4/14
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InventorySystem
{
    [RequireComponent(typeof(Image))]
    public class ItemBackground : MonoBehaviour
    {
        public ItemLevel itemLevel = ItemLevel.None;
        [SerializeField]
        private Item item;
        public Item Item
        { set { item = value; } }

        private Image target;

        void Start()
        {
            item = GetComponentInChildren<Item>();
            target = GetComponent<Image>();
        }

        void Update()
        {
            if (item.itemType == ItemType.Equipment)
            {
                itemLevel = item.quality;
                SetColor(itemLevel);
            }
            else
                SetColor(ItemLevel.None);

        }

        void SetColor(ItemLevel level)
        {
            target.color = level.ToColor();
        }
    }
}
