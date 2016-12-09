
//------------------------------------------------------------
// TIPS:
// Unity が認識できない拡張子のファイルを、
// Unity 上で扱えるようにするための属性クラスです。
//
// ファイルのデータ構造を表現するクラスに対して指定してください。
//
//------------------------------------------------------------

using System;

[AttributeUsage(targets, AllowMultiple = false, Inherited = false)]
public sealed class CustomAssetAttribute : Attribute
{
  const AttributeTargets targets = AttributeTargets.Class;

  public CustomAssetAttribute(params string[] extensions)
  {
    this.extensions = extensions;
  }

  public readonly string[] extensions = null;
}
