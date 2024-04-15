using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class BuildTool : MonoBehaviour
{
    /// <summary>
    /// Encrypt asset bundle file
    /// </summary>
    /// <param name="assetBundleFileDir"></param>
    private static bool EncryptAssetBundleServerFile(string assetBundleFileDir)
    {
        if (!File.Exists(assetBundleFileDir))
        {
            Debug.LogError("Config.unity3d does not exist!!!");
            return false;
        }
        KTResourceCrypto.SetKey("eabb22bdc77d9d8fc85f52a572ae2f52eabb22bdc77d8d9fc85f52a572ae2f52eabb22bdc97d8d8fc85f52a572ae2f52eabb22bdc79d8d8fc85f52a572ae2f52eabb22bdc79d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc97d8d8fc85f52a572ae2f52eabb22bdc97d8d8fc85f52a572ae2f59");
        byte[] byteData = File.ReadAllBytes(assetBundleFileDir);

        byte[] encryptedBytes = KTResourceCrypto.Encrypt(byteData.ToArray());

        File.Delete(assetBundleFileDir);
        File.WriteAllBytes(assetBundleFileDir, encryptedBytes);

        return true;
    }
    /// <summary>
    /// Encrypt asset bundle file
    /// </summary>
    /// <param name="assetBundleFileDir"></param>
    private static bool EncryptAssetBundleFile(string assetBundleFileDir)
    {
        //if (!File.Exists(assetBundleFileDir))
        //{
        //    Debug.LogError("Config.unity3d does not exist!!!");
        //    return false;
        //}
        KTResourceCrypto.SetKey2("eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52eabb22bdc77d8d8fc85f52a572ae2f52");
        byte[] byteData = File.ReadAllBytes(assetBundleFileDir);

        byte[] encryptedBytes = KTResourceCrypto.Encrypt(byteData.ToArray(), false);

        File.Delete(assetBundleFileDir);
        File.WriteAllBytes(assetBundleFileDir, encryptedBytes);

        return true;
    }

    /// <summary>
    /// Build
    /// </summary>
    [MenuItem("Build Server Config/lua")]
    public static void AndroidBuildServerConfig()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "lua", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
        }
        bool ret = BuildTool.EncryptAssetBundleServerFile(path);
        if (ret)
        {
            Debug.Log("Build Config.unity3d successfully!");
        }
        else
        {
            Debug.Log("Build Config.unity3d failed!");
        }
    }

    [MenuItem("Build Server Config/ioslua")]
    public static void IOSBuildServerConfig()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "lua", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.iOS);
        }
        bool ret = BuildTool.EncryptAssetBundleServerFile(path);
        if (ret)
        {
            Debug.Log("Build Config.unity3d successfully!");
        }
        else
        {
            Debug.Log("Build Config.unity3d failed!");
        }
    }

    /// <summary>
    /// Build
    /// </summary>
    [MenuItem("Build Config/Android")]
    public static void AndroidBuildConfig()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "Config", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
        }
        bool ret = BuildTool.EncryptAssetBundleFile(path);
        if (ret)
        {
            Debug.Log("Build Config.unity3d successfully!");
        }
        else
        {
            Debug.Log("Build Config.unity3d failed!");
        }
    }
	    [MenuItem("Build Config/Ios")]
    public static void IosBuildConfig()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "Config", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.iOS);
        }
        bool ret = BuildTool.EncryptAssetBundleFile(path);
        if (ret)
        {
            Debug.Log("Build Config.unity3d successfully!");
        }
        else
        {
            Debug.Log("Build Config.unity3d failed!");
        }
    }
	
	
	
	
	
	
    /// <summary>
    /// Adds newly (if not already in the list) found assets.
    /// Returns how many found (not how many added)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="assetsFound">Adds to this list if it is not already there</param>
    /// <returns></returns>
    public static T[] GetAssetsFromPath<T>(string path) where T : Object
    {
        string[] filePaths = Directory.GetFiles(path);

        List<T> list = new List<T>();

        if (filePaths != null && filePaths.Length > 0)
        {
            for (int i = 0; i < filePaths.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(filePaths[i], typeof(T));
                if (obj is T asset)
                {
                    if (!list.Contains(asset))
                    {
                        list.Add(asset);
                    }
                }
            }
        }

        return list.ToArray();
    }

    [MenuItem("Build Res/Android - Build AssetBundle from selected")]
    private static void AndroidBuildResource()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);

        }
        bool ret = BuildTool.EncryptAssetBundleFile(path);
        if (ret)
        {
            Debug.Log("Build Config.unity3d successfully!");
        }
        else
        {
            Debug.Log("Build Config.unity3d failed!");
        }
    }

    [MenuItem("Build Res/Android - Build All AssetBundles inside selected folders")]
    private static void AndroidBuildAllResources()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                BuildPipeline.BuildAssetBundle(asset, subAssets, outputFolder + "/" + asset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            }
        }
    }

    [MenuItem("Build Res/Android - Build All Assets inside selected folders individually (For build MAP)")]
    private static void AndroidBuildAllResourcesInsideFolderIndividually()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                foreach (Object subAsset in subAssets)
                {
                    BuildPipeline.BuildAssetBundle(subAsset, null, outputFolder + "/" + subAsset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
                }
            }
        }
    }

    [MenuItem("Build Res/IOS - Build AssetBundle from selected")]
    private static void IOSBuildResource()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.iOS);
        }
    }

    [MenuItem("Build Res/IOS - Build All AssetBundles inside selected folders")]
    private static void IOSBuildAllResources()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                BuildPipeline.BuildAssetBundle(asset, subAssets, outputFolder + "/" + asset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.iOS);
            }
        }
    }

    [MenuItem("Build Res/IOS - Build All Assets inside selected folders individually (For build MAP)")]
    private static void IOSBuildAllResourcesInsideFolderIndividually()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                foreach (Object subAsset in subAssets)
                {
                    BuildPipeline.BuildAssetBundle(subAsset, null, outputFolder + "/" + subAsset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.iOS);
                }
            }
        }
    }

    [MenuItem("Build Res/Windows - Build AssetBundle from selected")]
    [System.Obsolete]
    private static void WindowsBuildResource()
    {
        // Bring up save panel
        var path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            var selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
        }
    }

    [MenuItem("Build Res/Windows - Build All AssetBundles inside selected folders")]
    private static void WindowsBuildAllResources()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                BuildPipeline.BuildAssetBundle(asset, subAssets, outputFolder + "/" + asset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
            }
        }
    }

    [MenuItem("Build Res/Windows - Build All Assets inside selected folders individually (For build MAP)")]
    private static void WindowsBuildAllResourcesInsideFolderIndividually()
    {
        string outputFolder = EditorUtility.SaveFolderPanel("Save Resources", "Build - Android", "");
        if (!string.IsNullOrEmpty(outputFolder))
        {
            Object[] selectedAssets = Selection.objects;
            foreach (Object asset in selectedAssets)
            {
                Object[] subAssets = BuildTool.GetAssetsFromPath<Object>(AssetDatabase.GetAssetPath(asset));
                foreach (Object subAsset in subAssets)
                {
                    BuildPipeline.BuildAssetBundle(subAsset, null, outputFolder + "/" + subAsset.name + ".unity3d", BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows64);
                }
            }
        }
    }
}