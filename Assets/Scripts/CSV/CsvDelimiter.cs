
namespace Csv
{
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

    // 区切り文字の種類は決まっているため、コンストラクタを使わせたくない
    // struct にすると、public コンストラクタが使えてしまうため、class にしている
    private Delimiter(string value) { _value = value; }


    // string.Format() で区切り文字を組み込むためのフォーマット
    static readonly string splitFormat =
      "(?:^|{0})((?:\"|\')(?:[^\"\']+|(?:\"\"|\'\'))*(?:\"|\')|[^{0}\r\n]*)";

    /// <summary>
    /// <see cref="System.Text.RegularExpressions.Regex.Split(string)"/>
    /// 用のパターンを出力
    /// </summary>
    public string split { get { return string.Format(splitFormat, _value); } }


    // Regex の各種メソッドが char 型に対応していないため、string 型で管理する
    string _value = string.Empty;

    public override string ToString() { return _value; }

    public static explicit operator char(Delimiter d) { return d._value[0]; }
    public static implicit operator string(Delimiter d) { return d._value; }
  }
}
