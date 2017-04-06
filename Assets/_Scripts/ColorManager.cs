/* Class Name: ColorManage
 * Describe: Single Class, choose and manager your favorite color in the game
 * Author: Ghostyii
 * Create Time: 2016/4/14
 */

using System;
using UnityEngine;

namespace InventorySystem
{
    public class ColorManager : MonoBehaviour
    {
        public ColorList colors;

        static public ColorManager Instance;

        void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}

