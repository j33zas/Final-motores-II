using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;


[CustomEditor(typeof(Transform)), CanEditMultipleObjects]
public class MultiAssetTransform : Editor
{
    AnimBool _show;

    public int _objSelectedAmount;
    public bool _IndividualDeegrees;
    public bool _IndividualScale;

    public bool _xRotate;
    public bool _yRotate;
    public bool _zRotate;

    public bool _xScale;
    public bool _yScale;
    public bool _zScale;

    public float _deegreesRotationA;
    public float _deegreesRotationB;

    public float _UnitsScaleA;
    public float _UnitsScaleB;

    public bool _RotateOnWorldAxis;
    public string CurrentStateR;
    public Space RotationSpace = Space.Self;

    GUIStyle SeparatorStyle;

    Transform Trans;
    
    private void OnEnable()
    {
        SeparatorStyle = new GUIStyle();
        SeparatorStyle.fontSize = 15;
        SeparatorStyle.fontStyle = FontStyle.Bold;
        SeparatorStyle.alignment = TextAnchor.MiddleCenter;
        SeparatorStyle.normal.textColor = Color.grey;

        _show = new AnimBool(false);
        _show.valueChanged.AddListener(Repaint);

        Trans = (Transform)target;
        
        if(Resources.Load("MultiAT/LastSettings"))
        {
            MultiAssetSettings MAT = (MultiAssetSettings)Resources.Load("MultiAT/LastSettings");
            MultiAssetTransformUtility.SettingsToTransform(MAT,this);
        }
    }

    private void OnDisable()
    {
        if (Resources.Load("MultiAT/LastSettings"))
        {
            MultiAssetSettings MAT = (MultiAssetSettings)Resources.Load("MultiAT/LastSettings");
            MultiAssetTransformUtility.TransformToSettings(MAT,this);
            AssetDatabase.SaveAssets();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _show.target = EditorGUILayout.Toggle("Randomize Transform", _show.target);
        if (EditorGUILayout.BeginFadeGroup(_show.faded))
        {
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(300, 1), Color.grey);
            EditorGUILayout.LabelField("Rotation", SeparatorStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 15;
            _xRotate = EditorGUILayout.Toggle("X", _xRotate, GUILayout.ExpandWidth(false));
            _yRotate = EditorGUILayout.Toggle("Y", _yRotate, GUILayout.ExpandWidth(false));
            _zRotate = EditorGUILayout.Toggle("Z", _zRotate, GUILayout.ExpandWidth(false));
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();

            _deegreesRotationB = EditorGUILayout.FloatField("min Deegrees", _deegreesRotationB, GUILayout.ExpandWidth(false));
            _deegreesRotationA = EditorGUILayout.FloatField("max Deegrees", _deegreesRotationA, GUILayout.ExpandWidth(false));

            _RotateOnWorldAxis = EditorGUILayout.Toggle("Rotating on " + CurrentStateR + " axis", _RotateOnWorldAxis);
            if (_RotateOnWorldAxis)
            {
                CurrentStateR = "WORLD";
                RotationSpace = Space.World;
            }
            else
            {
                CurrentStateR = "LOCAL";
                RotationSpace = Space.Self;
            }
            //----------------------------------------------------------------------------------------------------------------------
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(300, 1), Color.grey);
            EditorGUILayout.LabelField("Scale", SeparatorStyle);
            //----------------------------------------------------------------------------------------------------------------------
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 15;
            _xScale = EditorGUILayout.Toggle("X", _xScale, GUILayout.ExpandWidth(false));
            _yScale = EditorGUILayout.Toggle("Y", _yScale, GUILayout.ExpandWidth(false));
            _zScale = EditorGUILayout.Toggle("Z", _zScale, GUILayout.ExpandWidth(false));
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();

            _UnitsScaleB = EditorGUILayout.FloatField("min Scale", _UnitsScaleB, GUILayout.ExpandWidth(false));
            _UnitsScaleA = EditorGUILayout.FloatField("max Scale", _UnitsScaleA, GUILayout.ExpandWidth(false));

            if(GUILayout.Button("Randomize"))
            {
                if(_xRotate)
                    foreach (var item in Selection.gameObjects)
                        item.transform.Rotate(new Vector3(Random.Range(_deegreesRotationA, _deegreesRotationB), 0, 0), RotationSpace);
                if(_yRotate)
                    foreach (var item in Selection.gameObjects)
                        item.transform.Rotate(new Vector3(0,Random.Range(_deegreesRotationA, _deegreesRotationB), 0), RotationSpace);
                if(_zRotate)
                    foreach (var item in Selection.gameObjects)
                        item.transform.Rotate(new Vector3(0,0,Random.Range(_deegreesRotationA, _deegreesRotationB)), RotationSpace);
                if (_xScale)
                    foreach (var item in Selection.gameObjects)
                        item.transform.localScale += new Vector3(Random.Range(_UnitsScaleA, _UnitsScaleB),0,0);
                if (_yScale)
                    foreach (var item in Selection.gameObjects)
                        item.transform.localScale += new Vector3(0,Random.Range(_UnitsScaleA, _UnitsScaleB),0);
                if(_zScale)
                    foreach (var item in Selection.gameObjects)
                        item.transform.localScale += new Vector3(0, 0,Random.Range(_UnitsScaleA, _UnitsScaleB));
            }

            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Save Profile"))
            {
                MultiAssetSettings MAT = (MultiAssetSettings)Resources.Load("MultiAT/LastSettings");
                MultiAssetTransformUtility.TransformToSettings(MAT, this);
                var window = new MultiAssetProfileSaveWindow(MAT);
                AssetDatabase.SaveAssets();
                window.Show();
            }
            if(GUILayout.Button("Open Profile"))
            {
                var window = new MultiAssetProfileWindow(this);
                window.Show();
                window.Focus();
                MultiAssetTransformUtility.TransformToSettings(window.currentSettings, this);
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.EndFadeGroup();
    }
}