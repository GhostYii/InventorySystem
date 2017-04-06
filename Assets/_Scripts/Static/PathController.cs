/* Class Name: PathController
 * Describe: You can control and modify all paths in this class
 * Author: Ghostyii
 * Create Time: 2016/4/19
 */
 
using UnityEngine;
using System.Collections;
using System.IO;

static public class PathController
{
    static public string DataDirectory
    { get { return Application.dataPath + "/GhostyiiGame"; } }

    //All paths about chinese
    static public class CHN
    {
        static public string Directory
        { get { return DataDirectory + "/Localization/CHN"; } }

        //Chinese Simplified
        static public class CHS
        {
            static public string Directory
            { get { return CHN.Directory + "/CHS"; } }

            static public string TooltipSaveDirection
            { get { return Directory + "/Tooltip"; } }
            static public string EquipmentsTooltipSaveFile
            { get { return TooltipSaveDirection + "/Tooltip_Equipments_CHS.tooltip"; } }
            static public string ItemTooltipSaveFile
            { get { return TooltipSaveDirection + "/Tooltip_Items_CHS.tooltip"; } }
            static public string SpeacialTooltipSaveFile
            { get { return TooltipSaveDirection + "Tooltip_Speacials_CHS.tooltip"; } }
            static public string TaskTooltipSaveFile
            { get { return TooltipSaveDirection + "Tooltip_Tasks_CHS.tooltip"; } }
        }

        //Chinese Traditional
        static public class CHT
        {
            static public string Directory
            { get { return CHN.Directory + "/CHT"; } }

            static public string TooltipSaveDirection
            { get { return Directory + "/Tooltip"; } }
            static public string EquipmentsTooltipSaveFile
            { get { return TooltipSaveDirection + "/Tooltip_Equipments_CHT.tooltip"; } }
            static public string ItemTooltipSaveFile
            { get { return TooltipSaveDirection + "/Tooltip_Items_CHT.tooltip"; } }
            static public string SpeacialTooltipSaveFile
            { get { return TooltipSaveDirection + "Tooltip_Speacials_CHT.tooltip"; } }
            static public string TaskTooltipSaveFile
            { get { return TooltipSaveDirection + "Tooltip_Tasks_CHT.tooltip"; } }
        }
        
    }

    //English
    static public class ESN
    {
        static public string Directory
        { get { return DataDirectory + "/Localization/ESN"; } }

        static public string TooltipSaveDirection
        { get { return Directory + "/Tooltip"; } }
        static public string EquipmentsTooltipSaveFile
        { get { return TooltipSaveDirection + "/Tooltip_Equipments_ESN.tooltip"; } }
        static public string ItemTooltipSaveFile
        { get { return TooltipSaveDirection + "/Tooltip_Items_ESN.tooltip"; } }
        static public string SpeacialTooltipSaveFile
        { get { return TooltipSaveDirection + "Tooltip_Speacials_ESN.tooltip"; } }
        static public string TaskTooltipSaveFile
        { get { return TooltipSaveDirection + "Tooltip_Tasks_ESN.tooltip"; } }
    }

    //Check if the necessary files exists, create a new file if it doesn't exist
    static public void CheckFile()
    { throw new System.NotImplementedException(); }
}
