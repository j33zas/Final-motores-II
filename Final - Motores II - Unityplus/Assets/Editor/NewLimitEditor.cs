using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(NewLimitDefiner))]

public class NewLimitEditor : Editor
{
    List<Vector3> saveBezier = new List<Vector3>();

    List<GameObject> myNodes = new List<GameObject>();

    private NewLimitDefiner _nld;

    private void OnEnable()
    {
        _nld = (NewLimitDefiner)target;
    }

    private void OnSceneGUI()
    {
        var tgt = (NewLimitDefiner)target;

        Handles.BeginGUI();

        var v = EditorWindow.GetWindow<SceneView>().camera.pixelRect;

        var main = Camera.current.WorldToScreenPoint(_nld.transform.position);
        var newNode = Camera.current.WorldToScreenPoint(_nld.Prefab.transform.position);

        var mainRect = new Rect(main.x - 40, Screen.height - main.y - 100, 50, 40);
        var nodeRect = new Rect(newNode.x - 40, Screen.height - newNode.y - 100, 50, 20);

        if (GUI.Button(new Rect(v.width - 150, v.height - 70, 120, 50), "Add node"))
        {
            Debug.Log("nodo agregado");
            Instantiate(_nld.Prefab, new Vector3(0,0,0), Quaternion.identity, _nld.transform);
        }

        if(_nld.transform.childCount >= 2)
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

        if (_nld.newNodes.Count >= 2)
        {
            _nld.Prefab = _nld.newNodes[_nld.newNodes.Count - 1];
            _nld.oldPrefab = _nld.newNodes[_nld.newNodes.Count - 2];

            Handles.color = Color.blue;

            Handles.DrawLine(_nld.oldPrefab.transform.position, _nld.Prefab.transform.position);
        }

        GUI.TextArea(nodeRect, "NODO");

        Handles.EndGUI();

        tgt.Prefab.transform.position = Handles.PositionHandle(tgt.Prefab.transform.position, tgt.Prefab.transform.rotation);
    }
}
