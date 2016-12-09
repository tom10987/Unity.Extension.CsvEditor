
//------------------------------------------------------------
// TIPS:
// CSV ファイルを文字列テーブルとして管理するクラスです。
//
// リソースの読み込み部分は、ゲームエンジンに依存しています。
//
// Resources.Load<TextAsset>(string path) を呼び出すため、
// メソッドの挙動に合わせて、ファイルのパスを指定してください。
//
// 読み取ったリソースを内部で管理しているテーブルに格納します。
//
// Load(), Convert() の呼び出しに、区切り文字を指定しない場合は、
// コンマを使用しているものとして振る舞います。
//
//------------------------------------------------------------
// USAGE:
//
// 1. Load():
// var csv = CsvAsset.Load("path");   // delimiter is comma.
//
// // *** The same behaviour.
// //var csv = CsvAsset.Load("path", CsvAsset.Delimiter.comma);
//
// 2. Convert():
// var textAsset = Resources.Load<TextAsset>("path");
// var csv = CsvAsset.Convert(textAsset, CsvAsset.Delimiter.comma);
//
// CAUTION:
// * Convert() は、CSV であるかを解釈しません。
// * Convert() method does not interpret whether the CSV file.
//
//------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Csv
{
  public sealed class CsvAsset
  {
    readonly string[] _table = null;

    private CsvAsset(string[] table) { _table = table; }


    /// <summary> 内部のテーブルに安全にアクセスする </summary>
    public string this[int index]
    {
      get { return _table[index < 0 ? 0 : index % _table.Length]; }
    }

    /// <summary>
    /// foreach などの繰り返し処理で動作するイテレーターを返す
    /// </summary>
    public IEnumerable<string> GetEnumerator()
    {
      foreach (var text in _table) { yield return text; }
    }


    /// <summary> 全体の要素数 </summary>
    public int length { get { return _table.Length; } }


    /// <summary> 区切り文字で接続した文字列を返す <para>
    /// 区切り文字は コンマ を使用する </para></summary>
    public string Serialize(int columnLength)
    {
      return Serialize(columnLength, Delimiter.comma);
    }

    /// <summary> 区切り文字で接続した文字列を返す </summary>
    public string Serialize(int columnLength, Delimiter option)
    {
      // 配列の範囲外アクセスを防止するため、長さを補正する
      columnLength = Mathf.Clamp(columnLength, 1, _table.Length - 1);

      // 行末になる要素
      var results = _table.Select((cell, id) => Parse(cell, id, columnLength));
      return string.Join(option, results.ToArray());
    }

    static string Parse(string cell, int id, int length)
    {
      return "";
    }

    /*

    /// <summary> 指定したファイルを CSV アセットとして読み込む <para>
    /// 区切り文字は コンマ "," として扱う </para></summary>
    public static CsvAsset Load(string filePath)
    {
      return Load(filePath, Delimiter.comma);
    }

    /// <summary> 指定したファイルを CSV アセットとして読み込む </summary>
    public static CsvAsset Load(string filePath, Delimiter delimiter)
    {
      var textAsset = Resources.Load<TextAsset>(filePath);
      return Convert(textAsset, delimiter);
    }

    /// <summary> 指定したリソースを CSV アセットに変換する <para>
    /// 区切り文字は コンマ "," として扱う </para></summary>
    public static CsvAsset Convert(TextAsset resource)
    {
      return Convert(resource, Delimiter.comma);
    }

    /// <summary> 指定したリソースを CSV アセットに変換する <para>
    public static CsvAsset Convert(TextAsset resource, Delimiter delimiter)
    {
      if (resource == null) { return null; }

      // セルごとに切り分ける
      var source = Regex.Split(resource.text, delimiter.SplitPattern());

      // セルのデータを整形する
      var select = source.Select(data => RegexExclude(data, Delimiter.exclude));

      // 余計な空データを取り除いたデータ列を取得
      var result = select.Where(data => !string.IsNullOrEmpty(data));

      foreach (var value in result) { Debug.Log(value); }

       */
    /*
    // 指定した区切り文字と改行コード以外の余計な文字を取り除く
    var stream = RegexExclude(resource.text, @"[^\w\r\n" + delimiter + @"]");

    // 改行コードの位置で配列を分ける
    var lines = RegexExcludeSplit(stream, _linefeed, string.Empty);

    // 区切り文字の位置で配列をさらに分ける
    var table = lines.Select(line => Regex.Split(line, delimiter));

    return new CsvAsset(table.ToArray());
    return null;
  }


  // 指定したパターンに合致する要素を文字列から取り除く
  static string RegexExclude(string stream, string pattern)
  {
    return Regex.Replace(stream, pattern, string.Empty);
  }

  // Regex.Split(string) で発生した余計な配列要素を取り除く
  static string[] RegexExcludeSplit(string input, Regex regex, string replacement)
  {
    var lines = regex.Split(input).Select(line => regex.Replace(line, replacement));
    return lines.Where(value => !string.IsNullOrEmpty(value)).ToArray();
  }
   */
  }
}
