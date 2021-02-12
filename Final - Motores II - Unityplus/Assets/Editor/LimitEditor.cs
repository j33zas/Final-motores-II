using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(LimitDefiner))]

public class LimitEditor : Editor
{
    bool start = false;
    int amountOfPrefab;
    int counter;
    List<Vector3> saveBezier = new List<Vector3>();
    List<GameObject> myWaypoints = new List<GameObject>();

    private LimitDefiner _ld;

    private void OnEnable()
    {
        _ld = (LimitDefiner)target;
    }

    private void OnSceneGUI()
    {
        var tgt = (LimitDefiner)target;

        Handles.BeginGUI();
        var v = EditorWindow.GetWindow<SceneView>().camera.pixelRect;

        var main = Camera.current.WorldToScreenPoint(_ld.transform.position);
        var tanA = Camera.current.WorldToScreenPoint(_ld.tangentA.transform.position);
        var tanB = Camera.current.WorldToScreenPoint(_ld.tangentB.transform.position);
        var baseA = Camera.current.WorldToScreenPoint(_ld.bezierA.transform.position);
        var baseB = Camera.current.WorldToScreenPoint(_ld.bezierB.transform.position);

        var mainRect = new Rect(main.x - 40, Screen.height - main.y - 100, 90, 40);
        var tangARect = new Rect(tanA.x - 40, Screen.height - tanA.y - 150, 75, 20);
        var tangBRect = new Rect(tanB.x - 40, Screen.height - tanB.y - 150, 75, 20);
        var baseARect = new Rect(baseA.x - 40, Screen.height - baseA.y - 150, 75, 20);
        var baseBRect = new Rect(baseB.x - 40, Screen.height - baseB.y - 150, 75, 20);

        if (GUI.Button(new Rect(v.width - 150, v.height - 70, 120, 50), "Return Tangent A"))
        {
            _ld.tangentA.transform.position = _ld.bezierA.transform.position + new Vector3(0, 0, 2);
        }

        if (GUI.Button(new Rect(v.width - 290, v.height - 70, 120, 50), "Return Tangent B"))
        {
            _ld.tangentB.transform.position = _ld.bezierB.transform.position + new Vector3(0, 0, -2);
        }

        if (GUI.Button(new Rect(v.width - 430, v.height - 70, 120, 50), "Return Base A"))
        {
            _ld.bezierA.transform.position = _ld.transform.position + new Vector3(0, 0, 2);
        }

        if (GUI.Button(new Rect(v.width - 570, v.height - 70, 120, 50), "Return Base B"))
        {
            _ld.bezierB.transform.position = _ld.transform.position + new Vector3(0, 0, -2);
        }

        if (saveBezier.Count >= 1)
        {
            if (GUI.Button(new Rect(v.width - 1130, v.height - 70, 120, 50), "Save"))
            {
                LimitSaver _ls = ScriptableObjectUtility.CreateAsset<LimitSaver>("Assets/TrackSaves", "path" + counter.ToString());
                foreach (var item in saveBezier)
                {
                    _ls.saverOfLimits.Add(item);
                }
                saveBezier.Clear();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                counter++;
            }
            if (GUI.Button(new Rect(v.width - 1000, v.height - 70, 120, 50), "Delete"))
            {
                for (int i = 0; i < myWaypoints.Count; i++)
                {
                    var tempWaypoins = myWaypoints[i];
                    DestroyImmediate(tempWaypoins);
                }
                saveBezier.Clear();
            }
        }

        /*if (GUI.Button(mainRect, "Generate"))
        {
            start = true;
        }*/

        GUI.TextArea(tangARect, "Tangent A");
        GUI.TextArea(tangBRect, "Tangent B");
        GUI.TextArea(baseARect, "Base A");
        GUI.TextArea(baseBRect, "Base B");


        //AmountPanel();

        Handles.EndGUI();

        var verts = new Vector3[] { new Vector3(tgt.transform.position.x -5f, tgt.transform.position.y, tgt.transform.position.z-5f),
                    new Vector3(tgt.transform.position.x -5f, tgt.transform.position.y, tgt.transform.position.z+5f),
                    new Vector3(tgt.transform.position.x +5f, tgt.transform.position.y, tgt.transform.position.z+5f),
                    new Vector3(tgt.transform.position.x +5f, tgt.transform.position.y, tgt.transform.position.z- 5f),
        };

        if (tgt.tangentA && tgt.tangentB && tgt.bezierA && tgt.bezierB)
        {
            if (amountOfPrefab < 1)
            {
                amountOfPrefab = 1;
            }

            Handles.color = Color.yellow;

            tgt.tangentA.position = Handles.PositionHandle(tgt.tangentA.position, tgt.tangentA.rotation);
            tgt.tangentB.position = Handles.PositionHandle(tgt.tangentB.position, tgt.tangentB.rotation);
            tgt.bezierA.position = Handles.PositionHandle(tgt.bezierA.position, tgt.bezierA.rotation);
            tgt.bezierB.position = Handles.PositionHandle(tgt.bezierB.position, tgt.bezierB.rotation);

            Handles.DrawBezier(tgt.bezierA.position, tgt.bezierB.position,
                               tgt.tangentA.position, tgt.tangentB.position,
                               Color.blue, EditorGUIUtility.whiteTexture, 2);

            var myPoints = Handles.MakeBezierPoints(tgt.bezierA.position, tgt.bezierB.position,
            tgt.tangentA.position, tgt.tangentB.position, amountOfPrefab);

            for (int i = 0; i < myPoints.Length; i++)
            {
                Handles.SphereHandleCap(i, myPoints[i], Quaternion.identity, 0.12f, EventType.Repaint);
            }

            if (start && _ld.Prefab != null)
            {
                for (int i = 0; i < myPoints.Length; i++)
                {
                    start = false;
                    var tempPrefab = Instantiate(_ld.Prefab, myPoints[i], Quaternion.identity);
                    saveBezier.Add(tempPrefab.transform.position);
                    myWaypoints.Add(tempPrefab);
                }
            }
            else if (start && _ld.Prefab == null)
            {
                start = false;
                Debug.LogError("Please drop an object in the tracks definer prefab space");
            }

            Handles.DrawDottedLine(tgt.bezierA.position, tgt.tangentA.position, 10);
            Handles.DrawDottedLine(tgt.bezierB.position, tgt.tangentB.position, 10);
        }
    }
}

