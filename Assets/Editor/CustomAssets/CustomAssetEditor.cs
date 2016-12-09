
//------------------------------------------------------------
// TIPS:
// 独自のアセットに共通する処理をまとめた Editor の派生クラスです。
//
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

public abstract class CustomAssetEditor : Editor
{
  protected string targetPath
  {
    get { return AssetDatabase.GetAssetPath(target); }
  }

  protected AssetImporter assetImporter
  {
    get { return AssetImporter.GetAtPath(targetPath); }
  }

  protected static void CreateAsset(ScriptableObject @object, string fileName)
  {
    ProjectWindowUtil.CreateAsset(@object, fileName + ".asset");
  }
}
