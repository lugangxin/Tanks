using System.IO;
using UnityEditor;
public class CreateAssetBundle
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles() {
        string dir = "AssetBundles";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        //Ĭ��ʹ��LZMA�㷨ѹ��������С��������ʱ�������ʹ��֮ǰ���������ѹ
        //LZ4����Ҫ�����ѹ�������ٶȿ죬��ѹ���İ���
        //BuildAssetBundleOptions.UncompressedAssetBundle;
        //BuildAssetBundleOptions.ChunkBasedCompression;  LZ4
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
