
//------------------------------------------------------------
// TIPS:
// 独自アセットを対象にした CustomAsset 属性を持つクラスを使って、
// インスペクターに表示するエディター拡張です。
//
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultAsset))]
public class DefaultAssetInspector : Editor
{
  static Type[] _customAssetTypes = null;

  [InitializeOnLoadMethod]
  [MenuItem("Tools/Custom Assets Serialization", priority = 81)]
  static void CustomAssetsSerialization()
  {
    _customAssetTypes = GetCustomAssetTypes();
  }

  // CustomAsset 属性を持つ Editor クラスを全て取得
  static Type[] GetCustomAssetTypes()
  {
    var assemblyPaths = Directory.GetFiles("Library/ScriptAssemblies", "*.dll");
    var types = new List<Type>();

    // スクリプトとして作成されたクラスを取得
    foreach (var assembly in assemblyPaths.Select(path => Assembly.LoadFile(path)))
    {
      types.AddRange(assembly.GetTypes());
    }

    var results = new List<Type>();

    // CustomAsset 属性を持つクラスのみ抽出
    foreach (var type in types)
    {
      var attributes = type.GetCustomAttributes<CustomAssetAttribute>();
      if (0 < attributes.Length) { results.Add(type); }
    }

    return results.ToArray();
  }


  Editor _customAssetEditor = null;

  // 拡張子に対応する CustomAsset 属性付きクラスを返す
  Type GetCustomAssetEditorType(string extension)
  {
    // CustomAsset 属性が持つ拡張子と一致したら true を返す
    Func<Type, bool> predicate = type =>
    {
      var attributes = type.GetCustomAttributes<CustomAssetAttribute>();
      return attributes.Any(attribute => attribute.extensions.Contains(extension));
    };

    return _customAssetTypes.FirstOrDefault(predicate);
  }


  void OnEnable()
  {
    var extension = Path.GetExtension(AssetDatabase.GetAssetPath(target));

    var customAssetEditorType = GetCustomAssetEditorType(extension);
    if (customAssetEditorType == null) { return; }

    _customAssetEditor = CreateEditor(target, customAssetEditorType);
  }


  public override void OnInspectorGUI()
  {
    if (_customAssetEditor != null)
    {
      GUI.enabled = true;
      _customAssetEditor.OnInspectorGUI();
    }
  }

  public override bool HasPreviewGUI()
  {
    return _customAssetEditor != null ?
      _customAssetEditor.HasPreviewGUI() : base.HasPreviewGUI();
  }

  public override void OnPreviewGUI(Rect r, GUIStyle background)
  {
    if (!_customAssetEditor) { _customAssetEditor.OnPreviewGUI(r, background); }
  }

  public override void OnPreviewSettings()
  {
    if (!_customAssetEditor) { _customAssetEditor.OnPreviewSettings(); }
  }

  public override string GetInfoString()
  {
    return _customAssetEditor != null ?
      _customAssetEditor.GetInfoString() : base.GetInfoString();
  }
}

public static class TypeExtension
{
  public static T[] GetCustomAttributes<T>(this Type type)
  {
    return type.GetCustomAttributes<T>(typeof(T), false);
  }

  static T[] GetCustomAttributes<T>(this Type type, Type target, bool inherit)
  {
    var attributes = type.GetCustomAttributes(target, inherit);
    return attributes as T[];
  }
}
