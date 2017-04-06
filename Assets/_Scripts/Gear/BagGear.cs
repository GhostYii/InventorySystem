/* Class Name: BagGear
 * Describe: Use this to control and set the bag gear
 * Author: Ghostyii
 * Create Time: 2016/4/14
 */

using UnityEngine;
using System.Collections;

namespace InventorySystem
{
    [AddComponentMenu("InventorySystem/Gear/Bag Gear")]
    public class BagGear : MonoBehaviour
    {
        public int count = 1;

        private int fieldCount = 0;
        public int FieldCount
        {
            get { return fieldCount; }
            set { fieldCount = value; }
        }

        public bool IsFull
        { get { return fieldCount >= count; } }

        public int RemainSlot
        { get { return count - fieldCount; } }
    }
}
