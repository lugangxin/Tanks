using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
public class LoadAseetBundleFromFile : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //相对路径    有依赖的话要加载出依赖
        //AssetBundle ab=AssetBundle.LoadFromFile("AssetBundles/tank.ab");
        //1.异步加载需要等待加载完成(比如整个场景比较大用异步加载)，可以采用协程   同步会直接返回AssetBundle
        //AssetBundleCreateRequest request=AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes("AssetBundles/tank.ab"));
        //yield return request;
        //AssetBundle ab = request.assetBundle;
        //2.WWW
        //while (!Caching.ready) {
        //    yield return null;
        //}
        ////@表示路径。    file:///从本地加载
        //WWW www = WWW.LoadFromCacheOrDownload(@"file://D:\unity\Tanks\AssetBundles\tank.ab", 1);
        //WWW www = WWW.LoadFromCacheOrDownload(@"http://localhost/AssetBundles/tank.ab", 1);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //    yield break;
        //}
        //AssetBundle ab = www.assetBundle;
        string uri = @"file:///D:\unity\Tanks\AssetBundles\tank.ab";
        //string uri = @"http://localhost/AssetBundles\tank.ab";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        yield return request.Send();//调用Send方法进行加载
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        //AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        //保存下载的内容
        //File.WriteAllBytes("/.",request.downloadHandler.data);
        GameObject tankPrefab = ab.LoadAsset<GameObject>("Tank");//要和包里的资源名字一致，区分大小写
        Instantiate(tankPrefab);

        //加载manifest文件
        AssetBundle manifestAB =AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
        AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");//文件名固定
        foreach (string name in manifest.GetAllAssetBundles()) {
            print(name);
        }
        //得到依赖
        string[] strs=manifest.GetDirectDependencies("tank.ab");
        foreach (string name in strs) {
            print(name);
            AssetBundle.LoadFromFile("AssetBundles/"+name);
        }
        //卸载资源
        AssetBundle.UnloadAllAssetBundles(true);//true卸载所有   false卸载没被使用的
        Resources.UnloadUnusedAssets();//个别资源卸载




        /*文件校验
         * CRC 多项式除法 16或32位  效率高   通信数据
         * 文件校验，数字签名
         * MD5  替换、轮转 16个字节（128位） 安全性高
         * SHA1  20个字节  安全性最高
         */


        //图片类型sprite  2D      如果不指定Packing Tag    所有图片会打包到一个图集，加载的时候会加载图集中的所有图片


        /*Unity Technologies 官方GitHub
         AssetBundles-Browser
         */

    }

}
