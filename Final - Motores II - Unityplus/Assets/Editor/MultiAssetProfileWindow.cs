using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class MultiAssetProfileWindow : EditorWindow
{
    MultiAssetSettings _currentSettings;

    bool _showSearches;

    string _currentsearch;

    string _searchQuery;

    List<MultiAssetSettings> _searchResults = new List<MultiAssetSettings>();

    [MenuItem("Unity+/MultiAssetProfile &M")]
    public static void OpenWindow()
    {
        var me = GetWindow<MultiAssetProfileWindow>();
        me.Show();
    }

    private void OnEnable()
    {

    }
    private void OnGUI()
    {
        EditorGUILayout.LabelField("search");
        EditorGUILayout.BeginHorizontal();
        _searchQuery = EditorGUILayout.TextField(_searchQuery);
        if (GUILayout.Button("Search", GUILayout.Width(50), GUILayout.Height(18)) || Input.GetKeyDown(KeyCode.Return))
            _showSearches = true;
        EditorGUILayout.EndHorizontal();
        if (_showSearches)
            SearchBar();
    }
    void SearchBar()
    {
        _searchResults.Clear();
        List<string> paths = new List<string>(AssetDatabase.FindAssets(_searchQuery)).Where(x => x.GetType() == typeof(MultiAssetSettings)).ToList();
        if(paths.Count > 0)
        {
            for (int i = 0; i < paths.Count - 1; i++)
            {

                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);

                MultiAssetSettings _current = (MultiAssetSettings)AssetDatabase.LoadAssetAtPath(paths[i], typeof(MultiAssetSettings));

                if (_current != null && !_searchResults.Contains(_current))
                    _searchResults.Add(_current);
            }
        }
        if(_searchResults.Count > 0)
        {
            foreach (var current in _searchResults)
            {
                EditorGUI.DrawRect(GUILayoutUtility.GetRect(1, position.height), Color.black);
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(current.name);

                if(GUILayout.Button("Load"))
                {
                    _currentSettings = current;
                    var me = GetWindow<MultiAssetProfileWindow>();
                    me.Close();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
