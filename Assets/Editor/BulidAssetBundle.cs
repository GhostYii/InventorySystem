using UnityEngine;
using UnityEditor;

public class BulidAssetBundle : Editor
{
    [MenuItem("Tool/Asset Bundle/Build Asset Bundle")]
    static void Bulid()
    {
        //BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/Assetbundles");
        //AssetDatabase.Refresh();

        //Debug.Log("Build Completed!");
    }
}
