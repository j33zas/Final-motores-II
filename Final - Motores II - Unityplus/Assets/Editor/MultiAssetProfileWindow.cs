using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class MultiAssetProfileWindow : EditorWindow
{
    MultiAssetSettings _currentSettings;
    
    string _currentsearch;

    string _searchQuery;

    List<Object> _searchResults = new List<Object>();

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

                Object _current = AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object));

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

                }
                
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
