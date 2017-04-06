using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class TEST_LoadAsset : MonoBehaviour
{
    //The main Manifest name
    public string manidestName = "Assetbundles";
    //The AssetBundle name which you want to load
    public string assetBundleName = "prefabs";
    //The Gameobject's name which you want instantiate
    public string assetName = "Cube";

    private AssetBundle assetBundle = new AssetBundle();

    IEnumerator Start()
    {
        //AssetBundle path
        string assetBundlePath = "file://" + Application.streamingAssetsPath + "/Assetbundles/";
        //Manifest file path
        string manifestPath = assetBundlePath + manidestName;

        //Load the Manifest file
        WWW wwwManifest = WWW.LoadFromCacheOrDownload(manifestPath, 0);
        yield return wwwManifest;

        if(wwwManifest.error == null)
        {
            AssetBundle manifestBundle = wwwManifest.assetBundle;
            AssetBundleManifest manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
            manifestBundle.Unload(false);

            //Get the list of depentdents files
            string[] dependentAssetBundles = manifest.GetAllDependencies(assetBundleName);

            AssetBundle[] abs = new AssetBundle[dependentAssetBundles.Length];

            for (int i = 0; i < dependentAssetBundles.Length; i++)
            {
                //Load all depentdents files
                WWW www = WWW.LoadFromCacheOrDownload(assetBundlePath + dependentAssetBundles[i], 0);
                yield return www;
                abs[i] = www.assetBundle;
            }

            //Load what you want
            WWW www2 = WWW.LoadFromCacheOrDownload(assetBundlePath + assetBundleName, 0);
            yield return www2;

            if (www2.error == null)
            {
                assetBundle = www2.assetBundle;
                foreach (string item in assetBundle.GetAllAssetNames())
                {
                    print(item);
                }
                Debug.Log("Completed!");
            }
            else
                Debug.Log(www2.error);
        }
        else
        { Debug.Log(wwwManifest.error); }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Sync load
            Instantiate<GameObject>(assetBundle.LoadAsset(assetName) as GameObject).name += "_Sync";

            //Async load
            StartCoroutine(LoadAsync());
        }
    }
#endif

    IEnumerator LoadAsync()
    {
        AssetBundleRequest request = assetBundle.LoadAssetAsync(assetName, typeof(GameObject));

        yield return request;

        GameObject obj = request.asset as GameObject;
        Instantiate(obj).name += "_Async";
        
        //if you unload the assetBundle, you can't load the obj until you reload the assetBundle
        //assetBundle.Unload(false);
    }

}
