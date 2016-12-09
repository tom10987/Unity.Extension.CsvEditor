
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Csv
{
  public static class CsvImporter
  {
    static readonly ConstructorInfo _csvAsset = null;

    static CsvImporter()
    {
      var flags = BindingFlags.NonPublic | BindingFlags.Instance;
      var args = new Type[] { typeof(string[]), typeof(int), typeof(int), };

      _csvAsset = typeof(CsvAsset).GetConstructor(flags, null, args, null);
    }


    /// <summary> Resources フォルダから読み込みを試みる <para>
    /// 区切り文字は コンマ を使っているものとして扱う </para></summary>
    public static CsvAsset Load(string path)
    {
      return Load(path, Delimiter.comma, ImportOption.Resources);
    }

    /// <summary> Resources フォルダから読み込みを試みる </summary>
    /// <param name="delimiter"> 区切り文字の種類を指定 </param>
    public static CsvAsset Load(string path, Delimiter delimiter)
    {
      return Load(path, delimiter, ImportOption.Resources);
    }

    /// <summary> 指定した方法による読み込みを試みる <para>
    /// 区切り文字は コンマ を使っているものとして扱う </para></summary>
    public static CsvAsset Load(string path, ImportOption option)
    {
      return Load(path, Delimiter.comma, option);
    }

    /// <summary> 指定した方法による読み込みを試みる </summary>
    /// <param name="delimiter"> 区切り文字の種類を指定 </param>
    public static CsvAsset Load(string path, Delimiter delimiter, ImportOption option)
    {
      string[] table = option.LoadAsset(path, delimiter);
      return _csvAsset.Invoke(new object[] { table, 0, 0, }) as CsvAsset;
    }


    // 余計な文字列を取り除くためのパターン
    static readonly Regex _exclude = new Regex(@"[^\w]");

    // 指定したオプションに基づく方法でファイル読み込みを試みる
    static string[] LoadAsset(this ImportOption option,
                              string path, Delimiter delimiter)
    {
      // リソースをオプションに対応する方法で読み込む
      var resource = Import(option, path);

      // セルごとに切り分ける
      var split = Regex.Split(resource, delimiter.split);

      // セルのデータを整形する
      var table = split.Select(cell => _exclude.Replace(cell, string.Empty));

      // 余計な空データを取り除いたデータ列を返す
      return table.Where(cell => !string.IsNullOrEmpty(cell)).ToArray();
    }

    // オプションに基づいた方法でファイル読み込みを行う
    static string Import(ImportOption option, string path)
    {
      return "";
    }

    // 各オプションに対応したパスに変換
    static string ConvertPath(ImportOption option, string path)
    {
      return option != ImportOption.StreamingAssets
        ? path
        : "file://" + Application.streamingAssetsPath + "/" + path;
    }
  }
}
