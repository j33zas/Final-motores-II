﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(NewLimitDefiner))]

public class NewLimitEditor : Editor
{
    List<GameObject> myNodes = new List<GameObject>();

    private NewLimitDefiner _nld;

    bool _finalNodeAdded;    
    bool _nodeExist;

    bool test;
    bool stop;

    LimitSaver scriptable;
    List<Vector3> newSaverOfLimits;

    GUIStyle style;
    
    
    private void OnEnable()
    {
        _nld = (NewLimitDefiner)target;
        style.alignment = TextAnchor.MiddleCenter;
    }

    public override void OnInspectorGUI()
    {
        if (!scriptable)
        {
            scriptable = (LimitSaver)EditorGUILayout.ObjectField("Load A limit", scriptable, typeof(LimitSaver), true);
            return;
        }

        if(newSaverOfLimits == null)
        {
            newSaverOfLimits = scriptable.saverOfLimits;
            myNodes.Clear();

            _nodeExist = true;
            test = true;

            foreach (var item in newSaverOfLimits)
            {
                var current = Instantiate(_nld.Prefab, item, Quaternion.identity, _nld.transform);                      
                myNodes.Add(current);            
            }
        }
        EditorGUIUtility.labelWidth = 15;
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Axis");
        EditorGUILayout.LabelField("X", style);
        EditorGUILayout.LabelField("Y", style);
        EditorGUILayout.LabelField("Z", style);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < newSaverOfLimits.Count; i++)
        {
            EditorGUIUtility.labelWidth = 15;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Nodo " + i);
            EditorGUILayout.LabelField(newSaverOfLimits[i].x.ToString(), style);
            EditorGUILayout.LabelField(newSaverOfLimits[i].y.ToString(), style);
            EditorGUILayout.LabelField(newSaverOfLimits[i].z.ToString(), style);
            if(GUILayout.Button("Edit"))
                Selection.activeGameObject = myNodes[i];

            EditorGUILayout.EndHorizontal();
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 1), Color.grey);
            EditorGUIUtility.labelWidth = 0;
        }
    }

    private void OnSceneGUI()
    {
        Selection.activeGameObject = _nld.gameObject;

        var tgt = (NewLimitDefiner)target;

        Handles.BeginGUI();

        var v = EditorWindow.GetWindow<SceneView>().camera.pixelRect;

        var main = Camera.current.WorldToScreenPoint(_nld.transform.position);
        var newNode = Camera.current.WorldToScreenPoint(_nld.Prefab.transform.position);

        var mainRect = new Rect(main.x - 40, Screen.height - main.y - 100, 50, 20);
        var nodeRect = new Rect(newNode.x - 40, Screen.height - newNode.y - 100, 50, 20);

        GUI.TextArea(nodeRect, "NODO");
        GUI.TextArea(mainRect, "BASE");

        if (!_finalNodeAdded)
        {
            if (GUI.Button(new Rect(v.width - 150, v.height - 70, 120, 50), "Add node"))
            {
                _nodeExist = true;
                Instantiate(_nld.Prefab, new Vector3(_nld.Prefab.transform.position.x + 10, _nld.Prefab.transform.position.y, _nld.Prefab.transform.position.z), Quaternion.identity, _nld.transform);
            }

            if (_nodeExist)
            {               
                if (_nld.newNodes.Count > 2)
                {
                    if (GUI.Button(new Rect(v.width - 300, v.height - 70, 120, 50), "Undo node"))
                    {
                        DestroyImmediate(_nld.newNodes[_nld.newNodes.Count - 1]);
                        _nld.newNodes.RemoveAt(myNodes.Count - 1);
                        myNodes.RemoveAt(_nld.newNodes.Count - 1);

                        Debug.Log("chau nodo");                      
                    }
                }
                else if (_nld.newNodes.Count <= 2)
                {
                    _nodeExist = false;
                }

                if (GUI.Button(new Rect(v.width - 450, v.height - 70, 120, 50), "Add final node"))
                {
                    Debug.Log("nodo final agregado");

                    _finalNodeAdded = true;

                    Instantiate(_nld.Prefab, new Vector3(_nld.Prefab.transform.position.x + 50, _nld.Prefab.transform.position.y, _nld.Prefab.transform.position.z), Quaternion.identity, _nld.transform);
                }
            }         
        }
        else if (_finalNodeAdded)
        {
            if (GUI.Button(new Rect(v.width - 450, v.height - 70, 120, 50), "Save"))
            {
                LimitSaver _ls = ScriptableObjectUtility.CreateAsset<LimitSaver>("Assets/Limits/Saved Limits", " new limit");
                foreach (var item in myNodes)
                {
                    _ls.saverOfLimits.Add(item.transform.position);
                }
                myNodes.Clear();
                EditorUtility.SetDirty(_ls);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                DestroyImmediate(_nld.gameObject);

                Debug.Log("SAVED");
            }

            if (GUI.Button(new Rect(v.width - 600, v.height - 70, 120, 50), "Destroy"))
            {
                if(_nld.gameObject != null)
                {
                    DestroyImmediate(_nld.gameObject);
                }
            }
        }

        if (GUI.Button(new Rect(v.width - 925, v.height - 70, 120, 50), "Flat all"))
        {
            for (int i = 0; i < _nld.transform.childCount; i++)
            {
                var objCurrent = _nld.transform.GetChild(i).gameObject;

                objCurrent.transform.position = new Vector3(objCurrent.transform.position.x, 1, objCurrent.transform.position.z);
            }
        }

        if (_nld)
        {
            if (_nld.transform.childCount >= 2)
            {
                for (int i = 0; i < _nld.transform.childCount; i++)
                {
                    var objCurrent = _nld.transform.GetChild(i).gameObject;

                    if (!myNodes.Contains(objCurrent) && !_nld.newNodes.Contains(objCurrent))
                    {
                        myNodes.Add(objCurrent);
                        _nld.newNodes.Add(objCurrent);
                    }
                }
            }
        }

        Handles.EndGUI();

        Handles.color = Color.blue;

        if (_nld)
        {
            if (_nld.newNodes.Count >= 2)
            {
                _nld.Prefab = _nld.newNodes[_nld.newNodes.Count - 1];
            }

            if (_finalNodeAdded)
            {
                Handles.DrawLine(_nld.newNodes[0].gameObject.transform.position, _nld.Prefab.transform.position);
            }

            for (var i = 1; i < _nld.newNodes.Count; i++)
            {
                Handles.DrawLine(_nld.newNodes[i - 1].transform.position, _nld.newNodes[i].transform.position);
            }

            tgt.Prefab.transform.position = Handles.PositionHandle(tgt.Prefab.transform.position, tgt.Prefab.transform.rotation);

            if (test)
            {
                foreach (var item in myNodes)
                {
                    item.transform.position = Handles.PositionHandle(item.transform.position, item.transform.rotation);
                }

                for (var i = 1; i < myNodes.Count; i++)
                {
                    Handles.DrawLine(myNodes[i - 1].transform.position, myNodes[i].transform.position);
                }

                _finalNodeAdded = true;
            }
        }
    }
}
