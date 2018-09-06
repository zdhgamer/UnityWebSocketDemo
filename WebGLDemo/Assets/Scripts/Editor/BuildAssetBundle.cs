using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundle : Editor {

    /// <summary>
    /// 需要生成的文件列表
    /// </summary>
    private static List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

    /// <summary>
    /// 需要生成md5的文件路径
    /// </summary>
    private static List<string> filePaths = new List<string>();

    /// <summary>
    /// 需要生成md5的文件夹路径
    /// </summary>
    private static List<string> dirPaths = new List<string>();

    /// <summary>
    /// 生成WebGL平台的assetbundle
    /// </summary>
    [MenuItem("WebGLBuild/Build WebGL AssetBundle")]
    public static void BuildWebGlResource() {
        string streamPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamPath)) {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();
        assetBundleBuilds.Clear();

        FindBuildAsset();

        BuildPipeline.BuildAssetBundles(streamPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.WebGL);

        BuildMD5File();

        AssetDatabase.Refresh();

    }

    /// <summary>
    /// 查找打包文件
    /// </summary>
    public static void FindBuildAsset() {

        AddBiildAssets("test" + AppConst.ABExtName, "*.prefab", "Assets/BuildResource/Prefabs/Test");
        AddBiildAssets("test_asset" + AppConst.ABExtName, "*.png", "Assets/BuildResource/Textures/Test");
    }

    /// <summary>
    /// 添加打包文件
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="pattern"></param>
    /// <param name="path"></param>
    public static void AddBiildAssets(string bundleName,string pattern,string path) {
        string[] files = Directory.GetFiles(path, pattern);
        if (files.Length <=0) {
            return;
        }
        for (int i=0;i<files.Length;i++) {
            files[i] = files[i].Replace("\\", "/");
        }

        AssetBundleBuild build = new AssetBundleBuild
        {
            assetBundleName = bundleName,
            assetNames = files
        };

        assetBundleBuilds.Add(build);
    }

    /// <summary>
    /// 生成md5版本文件
    /// </summary>
    public static void BuildMD5File() {
        string resPath = Application.streamingAssetsPath+"/";
        string newFilePath = resPath + "/file.txt";
        if (File.Exists(newFilePath)) {
            File.Delete(newFilePath);
        }
        dirPaths.Clear();
        filePaths.Clear();
        FindAllFiles(resPath);

        using (FileStream fs = new FileStream(newFilePath, FileMode.CreateNew)) {
            using (StreamWriter sw = new StreamWriter(fs)) {
                for (int i=0;i<filePaths.Count;i++) {
                    if (filePaths[i].EndsWith(".meta") || filePaths[i].Contains(".DS_Store")) {
                        continue;
                    }
                    string md5 = CommonUtil.GetMD5ByFile(filePaths[i]);
                    string value = filePaths[i].Replace(resPath, string.Empty);
                    sw.WriteLine(value + "|" + md5);
                }
            }
        }
    }

    /// <summary>
    /// 查找该路径下面所有的文件
    /// </summary>
    /// <param name="path"></param>
    public static void FindAllFiles(string path) {
        string[] files = Directory.GetFiles(path);
        string[] subDirs = Directory.GetDirectories(path);
        for (int i = 0; i < files.Length; i++) {
            string extName = Path.GetExtension(files[i]);
            if (extName.EndsWith(".meta")) {
                continue;
            }
            filePaths.Add(files[i].Replace("\\", "/"));
        }
        for (int i=0;i< subDirs.Length;i++) {
            dirPaths.Add(subDirs[i].Replace("\\", "/"));
            FindAllFiles(subDirs[i]);
        }
    }

}
