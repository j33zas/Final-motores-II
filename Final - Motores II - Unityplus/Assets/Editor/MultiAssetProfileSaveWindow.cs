using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MultiAssetProfileSaveWindow : EditorWindow
{
    MultiAssetSettings _settings;

    string _name;

    string _route;

    public MultiAssetProfileSaveWindow(MultiAssetSettings multi)
    {
        _settings = multi;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Name: ");
        _name = EditorGUILayout.TextField(_name);

        EditorGUILayout.LabelField("Save Location: ");
        _route = EditorGUILayout.TextField(_route);

        var t = new GUIStyle().alignment = TextAnchor.MiddleCenter;
        if (GUILayout.Button("Save"))
        {
            var obj = ScriptableObjManager.CreateScriptable<MultiAssetSettings>("Assets/" + _route + "/", _name);
            MultiAssetTransformUtility.CopySettings(_settings, obj);
            Close();
        }
    }

    public static void OpenWindow()
    {
        var me = GetWindow<MultiAssetProfileSaveWindow>();
        me.Show();
    }
}
