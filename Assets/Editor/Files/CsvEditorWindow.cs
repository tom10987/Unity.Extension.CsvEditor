
using UnityEditor;
using UnityEngine;

public class CsvEditorWindow : EditorWindow
{
  static EditorWindow _instance = null;

  [MenuItem("Custom Window/Create CSV Edit Window")]
  static void CreateWindow()
  {
    _instance = GetWindow(typeof(CsvEditorWindow), true, "CSV Editor");
    //GetWindow<DataSheetEditorWindow>(true, "Data Sheet Editor");
    //_window.instance = GetWindow(typeof(DataSheetWindow), true, "Data Sheet Editor");
  }

  /*
  [MenuItem("Custom Window/Create Data Edit Window/Open XLSX File")]
  static void CreateWindowXLSX()
  {
    Debug.Log("実装中");
  }

  string _test = string.Empty;
  FileExtension _extension = FileExtension.CSV;

  bool _enabled = false;

  bool _toggle = false;
  float _slider = 0f;

  string _label = string.Empty;
   */


  /*
void OnGUI()
{
  GUILayout.Label("Test", EditorStyles.boldLabel);
  _test = EditorGUILayout.TextField("Input Test", _test);
   */

  //GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1f));

  /*
  CustomStyles.Label("File Status");
  _test = EditorGUILayout.TextField("Name", _test);
  _extension = (FileExtension)EditorGUILayout.EnumPopup("Type", _extension);

  _enabled = CustomStyles.Foldout("Test", _enabled);
  if (_enabled)
  {
    _toggle = EditorGUILayout.Toggle("Toggle", _toggle);
    _slider = EditorGUILayout.Slider("Slider", _slider, 0f, 10f);
  }
   */

  /*
  _enabled = EditorGUILayout.BeginToggleGroup("Optional Settings", _enabled);

  _toggle = EditorGUILayout.Toggle("Toggle", _toggle);
  _slider = EditorGUILayout.Slider("Slider", _slider, 0f, 10f);

  EditorGUILayout.EndToggleGroup();

  _label = _test;
  if (_toggle) { _label += _slider.ToString(); }

  GUILayout.Label(_label);
}
   */
}
