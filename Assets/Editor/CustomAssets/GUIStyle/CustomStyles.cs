
using UnityEditor;
using UnityEngine;

public static class CustomStyles
{
  static GUIStyle header { get { return CustomSkins.shurikenModuleTitle; } }

  static readonly Vector2 styleOffset = new Vector2(16f, 22f);


  /// <summary> 標準のラベルを作成 </summary>
  public static void Label(string title)
  {
    DrawLabel(title, header);
  }

  /// <summary> 折りたたみつきラベルを作成 </summary>
  public static bool Foldout(string title, bool display)
  {
    var headerRect = DrawLabel(title, header);
    var toggleRect = ReplaceToggleRectOffset(headerRect);

    var current = Event.current;

    if (current.Is(EventType.Repaint))
    {
      FoldoutDraw(toggleRect, display);
    }

    if (current.Is(EventType.MouseDown)
        && headerRect.Contains(current.mousePosition))
    {
      display = !display;
      current.Use();
    }

    return display;
  }


  // 指定したタイトルのラベルを作成、ラベルのサイズを返す
  static Rect DrawLabel(string title, GUIStyle style)
  {
    var rect = GUILayoutUtility.GetRect(styleOffset.x, styleOffset.y, style);
    GUI.Box(rect, title, style);

    return rect;
  }

  // 折り畳みアイコンを表示
  static void FoldoutDraw(Rect rect, bool display)
  {
    EditorStyles.foldout.Draw(rect, false, false, display, false);
  }


  // イベントが条件に一致したら true を返す
  static bool Is(this Event current, EventType type)
  {
    return current.type == type;
  }


  // トグルアイコン向けに補正した矩形サイズを返す
  static Rect ReplaceToggleRectOffset(Rect rect)
  {
    var result = new Rect(rect)
    {
      x = rect.x + 4f,
      y = rect.y + 2f,
      width = 13f,
      height = 13f,
    };

    return result;
  }
}
