using UnityEngine;
using System.Collections;
using InventorySystem;

static public class ExtensionMethod
{
    static public Color ToColor(this ItemLevel itemLevel)
    {
        switch (itemLevel)
        {
            case ItemLevel.None:
                return ColorManager.Instance.colors.Normal;
            case ItemLevel.White:
                return ColorManager.Instance.colors.White;
            case ItemLevel.Green:
                return ColorManager.Instance.colors.Green;
            case ItemLevel.Blue:
                return ColorManager.Instance.colors.Blue;
            case ItemLevel.Purple:
                return ColorManager.Instance.colors.Purple;
            case ItemLevel.Orange:
                return ColorManager.Instance.colors.Orange;
            case ItemLevel.Pink:
                return ColorManager.Instance.colors.Pink;
            default:
                return ColorManager.Instance.colors.Normal;
        }
    }

    static public string ToRichText(this ItemLevel itemLevel)
    {
        switch (itemLevel)
        {
            case ItemLevel.White:
                return RichText.HexColor.White;
            case ItemLevel.Green:
                return RichText.HexColor.Green;
            case ItemLevel.Blue:
                return RichText.HexColor.Blue;
            case ItemLevel.Purple:
                return RichText.HexColor.Purple;
            case ItemLevel.Orange:
                return RichText.HexColor.Honeydew;
            case ItemLevel.Pink:
                return RichText.HexColor.Rosa;
            default:
                return RichText.HexColor.Black;;
        }
    }

    static public string ToTextString(this ClickMenu cm)
    {
        switch (cm)
        {
            case ClickMenu.Use:
                return "使用";
            case ClickMenu.Equip:
                return "装备";
            case ClickMenu.Delete:
                return "删除";
            case ClickMenu.Sell:
                return "出售";
            default:
                return "无法解析";               
        }
    }

}
