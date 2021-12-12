using System.IO;
using UnityEditor;
public class CreateAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles() {
        string dir = "AssetBundles";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        //默认使用LZMA算法压缩，包更小，但加载时间更长，使用之前进行整体解压
        //LZ4不需要整体解压，加载速度快，但压缩的包大
        //BuildAssetBundleOptions.UncompressedAssetBundle;
        //BuildAssetBundleOptions.ChunkBasedCompression;  LZ4
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
