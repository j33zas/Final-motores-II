using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class MultiAssetProfileWindow : EditorWindow
{
    public MultiAssetSettings currentSettings;

    bool _showSearches;

    string _currentsearch;

    string _searchQuery;

    List<MultiAssetSettings> _searchResults = new List<MultiAssetSettings>();

    GUIStyle SeparatorStyle;

    MultiAssetTransform multiAT;

    public MultiAssetProfileWindow(MultiAssetTransform preWindow)
    {
        multiAT = preWindow;
    }

    [MenuItem("Unity+/MultiAssetProfile &M")]
    public static void OpenWindow()
    {
        var me = new MultiAssetProfileWindow(null);
        me.Show();
    }

    private void OnEnable()
    {
        SeparatorStyle = new GUIStyle();
        SeparatorStyle.fontSize = 15;
        SeparatorStyle.fontStyle = FontStyle.Bold;
        SeparatorStyle.alignment = TextAnchor.MiddleCenter;
        SeparatorStyle.normal.textColor = Color.grey;

        _showSearches = true;

        if (Resources.Load("MultiAT/LastSettings"))
            currentSettings = (MultiAssetSettings)Resources.Load("MultiAT/LastSettings");
        else
            ScriptableObjectUtility.CreateAsset<MultiAssetSettings>("Assets/Resources/MultiAT/", "LastSettings");
    }

    private void OnGUI()
    {
        if (_showSearches)
        {
            SearchBar();
        }
        else
        {
            if(currentSettings)
            {
                EditorGUI.DrawRect(GUILayoutUtility.GetRect(300, 1), Color.grey);
                EditorGUILayout.LabelField("Rotation", SeparatorStyle);

                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 15;
                currentSettings._xRotate = EditorGUILayout.Toggle("X", currentSettings._xRotate, GUILayout.ExpandWidth(false));
                currentSettings._yRotate = EditorGUILayout.Toggle("Y", currentSettings._yRotate, GUILayout.ExpandWidth(false));
                currentSettings._zRotate = EditorGUILayout.Toggle("Z", currentSettings._zRotate, GUILayout.ExpandWidth(false));
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.EndHorizontal();

                currentSettings._deegreesRotationB = EditorGUILayout.FloatField("min Deegrees", currentSettings._deegreesRotationB, GUILayout.ExpandWidth(false));
                currentSettings._deegreesRotationA = EditorGUILayout.FloatField("max Deegrees", currentSettings._deegreesRotationA, GUILayout.ExpandWidth(false));

                currentSettings._RotateOnWorldAxis = EditorGUILayout.Toggle("Rotating on " + currentSettings.CurrentStateR + " axis", currentSettings._RotateOnWorldAxis);
                if (currentSettings._RotateOnWorldAxis)
                {
                    currentSettings.CurrentStateR = "WORLD";
                    currentSettings.RotationSpace = Space.World;
                }
                else
                {
                    currentSettings.CurrentStateR = "LOCAL";
                    currentSettings.RotationSpace = Space.Self;
                }
                //----------------------------------------------------------------------------------------------------------------------
                EditorGUI.DrawRect(GUILayoutUtility.GetRect(300, 1), Color.grey);
                EditorGUILayout.LabelField("Scale", SeparatorStyle);
                //----------------------------------------------------------------------------------------------------------------------
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 15;
                currentSettings._xScale = EditorGUILayout.Toggle("X", currentSettings._xScale, GUILayout.ExpandWidth(false));
                currentSettings._yScale = EditorGUILayout.Toggle("Y", currentSettings._yScale, GUILayout.ExpandWidth(false));
                currentSettings._zScale = EditorGUILayout.Toggle("Z", currentSettings._zScale, GUILayout.ExpandWidth(false));
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.EndHorizontal();

                currentSettings._UnitsScaleB = EditorGUILayout.FloatField("min Scale", currentSettings._UnitsScaleB, GUILayout.ExpandWidth(false));
                currentSettings._UnitsScaleA = EditorGUILayout.FloatField("max Scale", currentSettings._UnitsScaleA, GUILayout.ExpandWidth(false));
            }
            GUILayout.BeginHorizontal();

            if(GUILayout.Button("Load"))
            {
                MultiAssetTransformUtility.SettingsToTransform(currentSettings, multiAT);
                AssetDatabase.SaveAssets();
                Close();
            }
            if(GUILayout.Button("Cancel"))
            {
                _showSearches = true;
            }

            GUILayout.EndHorizontal();
        }
    }
    void SearchBar()
    {
        EditorGUILayout.LabelField("Search");
        var _queryAux = _searchQuery;
        _searchQuery = EditorGUILayout.TextField(_queryAux);
        if(_queryAux != _searchQuery && _searchQuery != "")
        {
            _searchResults.Clear();
            string[] paths = AssetDatabase.FindAssets(_searchQuery);

            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
                var load = AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object));
                if(load.GetType() == typeof(MultiAssetSettings))
                    _searchResults.Add((MultiAssetSettings)load);
            }
        }
        else if (_searchQuery == "")
            _searchResults.Clear();

        if(_searchResults.Count > 0)
        {
            for (int i = 0; i < _searchResults.Count; i++)
            {
                if (_searchResults[i].GetType() == typeof(MultiAssetSettings))
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(_searchResults[i].ToString());
                    if(GUILayout.Button("Load"))
                    {
                        _searchQuery = "";
                        currentSettings = _searchResults[i];
                        _showSearches = false;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}
