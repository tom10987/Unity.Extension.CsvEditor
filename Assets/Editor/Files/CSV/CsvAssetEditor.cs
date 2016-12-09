
using System;
using UnityEditor;
using UnityEngine;

/*
[CustomAsset(".csv")]
[CustomEditor(typeof(TextAsset))]
 */
public class CsvAssetEditor : CustomAssetEditor
{
  TextAsset _asset = null;

  void OnEnable()
  {
    _asset = AssetDatabase.LoadAssetAtPath<TextAsset>(targetPath);
  }


  public override void OnInspectorGUI()
  {
  }
}
