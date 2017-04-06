/* Class Name: ToolTipSetting
 * Describe: Single Class, Show/Hide the tooltip plane of the inventory system
 * Author: Ghostyii
 * Create Time: 2016/4/14
 */

using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ToolTipSetting : MonoBehaviour
    {
        public GameObject toolTip;
        [Header("ToolTip Ref")]
        public Text title;        
        public Text effect;
        public Text describe;

        static public ToolTipSetting Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void Show(Vector2 screenPos)
        {
            (toolTip.transform as RectTransform).anchoredPosition = screenPos;
            toolTip.SetActive(true); 
        }

        public void Hide()
        { toolTip.SetActive(false); }       

        public void SetToolTip(TooltipData toolTipData)
        {
            string textColor = toolTipData.level.ToRichText();
            title.text = RichText.HexColor.GetColorText(toolTipData.name, textColor);
            effect.text = RichText.HexColor.GetColorText(toolTipData.effect, textColor);
            describe.text = RichText.HexColor.GetColorText(toolTipData.introduction, textColor);
        }

        public void SetToolTip(int id)
        { throw new System.NotImplementedException(); }
    }
}

