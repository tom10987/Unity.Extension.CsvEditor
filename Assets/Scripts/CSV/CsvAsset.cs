
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

public class CsvAsset
{
  readonly string[] _table = null;

  readonly int _row = 0;
  readonly int _column = 0;

  /// <summary>
  /// <see cref="Load(string)"/> または <see cref="Convert(TextAsset)"/>
  /// を使用してください。
  /// </summary>
  protected CsvAsset(string[][] table)
  {
    _row = table.Length;
    _column = table[0].Length;

    _table = table.SelectMany(value => value).ToArray();
  }


  /// <summary> 内部のテーブルに安全にアクセスする </summary>
  public string this[int index]
  {
    get
    {
      int clamp = Mathf.Clamp(index, 0, _table.Length - 1);
      return _table[clamp];
    }
  }

  /// <summary> 内部のテーブルに安全にアクセスする </summary>
  public string this[int row, int column]
  {
    get
    {
      int clampR = Mathf.Clamp(row, 0, _row - 1) * _column;
      int clampC = Mathf.Clamp(column, 0, _column - 1);
      return _table[Mathf.Min(clampR + clampC, _table.Length - 1)];
    }
  }

  /// <summary> 読み取ったデータを連結して、１つの文字列として返す
  /// <para> 注意：区切り文字は含まない </para></summary>
  public string text
  {
    get { return _table.Aggregate((now, next) => string.Concat(now, next)); }
  }

  /// <summary> foreach などの繰り返し処理で動作するイテレーターを返す
  /// <para> 注意：区切り文字は含まない </para></summary>
  public IEnumerable<string> GetEnumerator()
  {
    foreach (var text in _table) { yield return text; }
  }


  /// <summary> 全体の要素数 </summary>
  public int length { get { return _table.Length; } }

  /// <summary> 行の要素数 </summary>
  public int row { get { return _row; } }

  /// <summary> 列の要素数 </summary>
  public int column { get { return _column; } }


  /// <summary> 区切り文字の種類 </summary>
  public class Delimiter
  {
    static readonly Delimiter[] _delimiters = null;

    public static Delimiter comma { get { return _delimiters[0]; } }
    public static Delimiter space { get { return _delimiters[1]; } }
    public static Delimiter tab { get { return _delimiters[2]; } }

    static Delimiter()
    {
      _delimiters = new Delimiter[]
      {
        new Delimiter(","),
        new Delimiter(" "),
        new Delimiter("\t"),
      };
    }

    // struct にすると、デフォルトコンストラクタが使えてしまうため、class にしている
    private Delimiter(string value) { _value = value; }

    // Regex の各種メソッドが char 型に対応していないため、string 型で管理する
    string _value = string.Empty;

    public static explicit operator char(Delimiter d) { return d._value[0]; }
    public static implicit operator string(Delimiter d) { return d._value; }

    public override string ToString() { return _value; }
  }

  // テキストから除外する文字の種類
  static readonly Regex _linefeed = new Regex(@"[\r\n]");


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

    // 指定した区切り文字と改行コード以外の余計な文字を取り除く
    var stream = RegexExclude(resource.text, @"[^\w\r\n" + delimiter + @"]");

    // 改行コードの位置で配列を分ける
    var lines = RegexExcludeSplit(stream, _linefeed, string.Empty);

    // 区切り文字の位置で配列をさらに分ける
    var table = lines.Select(line => Regex.Split(line, delimiter));

    return new CsvAsset(table.ToArray());
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
}
