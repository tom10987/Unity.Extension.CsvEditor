
//------------------------------------------------------------
// TIPS:
// Unity のみで扱える独自形式のファイルをインスペクターに表示します。
//
// CSV 同様、テキストとして保存しますが、ヘッダー行を必ず含みます。
//
//------------------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomAsset(".ucsv")]
public class CustomCsvEditor : CustomAssetEditor
{
  public override void OnInspectorGUI()
  {
    GUILayout.Label(assetImporter.userData);
  }
}
