
using UnityEditor;
using UnityEngine;

public static class CustomSkins
{
  public class BuiltinStyleName
  {
    public const string shurikenModuleTitle = "ShurikenModuleTitle";
  }

  public static GUIStyle shurikenModuleTitle
  {
    get
    {
      var style = new GUIStyle(BuiltinStyleName.shurikenModuleTitle);

      style.font = new GUIStyle(EditorStyles.label).font;
      style.border = new RectOffset(15, 7, 4, 4);
      style.fixedHeight = 22f;
      style.contentOffset = new Vector2(20f, -2f);

      return style;
    }
  }
}
