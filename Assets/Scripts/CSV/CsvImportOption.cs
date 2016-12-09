
namespace Csv
{
  public enum ImportOption
  {
    /// <summary>
    /// <see cref="UnityEngine.Resources.Load(string)"/> による読み込み
    /// </summary>
    Resources,

    /// <summary>
    /// StreamingAssets フォルダからの読み込み
    /// </summary>
    StreamingAssets,

    /// <summary>
    /// <see cref="UnityEngine.WWW"/> クラスによる読み込み 
    /// </summary>
    WWW,
  }
}
