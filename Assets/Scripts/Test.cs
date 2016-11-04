
using UnityEngine;

public class Test : MonoBehaviour
{
  void Start()
  {
    // CSV ファイルの読み込みを実行
    var csv = CsvAsset.Load("test03", CsvAsset.Delimiter.comma);

    // CSV ファイルへのアクセス（読み取り専用）
    Debug.Log("first = " + csv[0]);

    Debug.Log("r: " + csv.row);
    Debug.Log("c: " + csv.column);
    Debug.Log("l: " + csv.length);

    // ファイルの内容をコンソールに出力
    Debug.Log("csv.text = " + csv.text);
    for (int i = 0; i < csv.length; ++i) { Debug.Log(csv[i]); }

    // foreach による操作
    foreach (var text in csv.GetEnumerator()) { Debug.Log(text); }
  }
}
