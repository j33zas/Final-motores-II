using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(pogger))]
public class CitizenGenerator : EditorWindow
{
    private GUIStyle _titleStyle;
    private GUIStyle _labelStyle;
    private GUIStyle _centeredMiniLabel;

    private pogger _prefabBase;
    private GameObject _preCheck;

    private bool _isOnScreen;

    private float _genericStat;

    private string _civilizationName;

    public pogger[] _typesList; //= GameObject.FindObjectsOfType<BaseCitizenScript>();

    bool _baseWasFound;

    [MenuItem("CustomTools/CitizenGenerator")]
    public static void OpenWindow()
    {
        CitizenGenerator myWindow = (CitizenGenerator)GetWindow(typeof(CitizenGenerator));
        myWindow.Show();
    }

    private void OnEnable()
    {
        _typesList = GameObject.FindObjectsOfType<pogger>();

        _labelStyle = new GUIStyle();
        _labelStyle.fontStyle = FontStyle.Bold;
        _labelStyle.alignment = TextAnchor.MiddleCenter;
        _labelStyle.fontSize = 15;

        _titleStyle = new GUIStyle();
        _titleStyle.fontStyle = FontStyle.Bold;
        _titleStyle.alignment = TextAnchor.MiddleCenter;
        _titleStyle.fontSize = 18;

        _centeredMiniLabel = new GUIStyle();
        _centeredMiniLabel.fontStyle = FontStyle.Normal;
        _centeredMiniLabel.alignment = TextAnchor.MiddleCenter;

        maxSize = new Vector2(345, 530);
        minSize = new Vector2(345, 530);

        //var prefab = AssetDatabase.LoadAssetAtPath("Assets/Assets/Prefabs/BASEcharacterMedium.prefab", typeof(GameObject));
    }

    private void OnGUI()
    {
        ShowOptions();
    }

    /*private void OnSceneGui()
    {
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            //UpdateDatabase();
            Debug.Log("pog");
            _fakeMouse = true;
        }
    }*/

    private void ShowOptions()
    {

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("CITIZEN GENERATOR 3000", _titleStyle);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Please press the button to start", _centeredMiniLabel);

        EditorGUILayout.Space();

        //ActivationButton();

        using (new EditorGUI.DisabledScope(_prefabBase == true)) //
            _prefabBase = (pogger)EditorGUILayout.ObjectField("Citizen", _prefabBase, typeof(pogger), true); //

        if (_prefabBase == true)
        {
            EditorGUILayout.Space();

            EditorGUILayout.Space();

            //EditorGUILayout.Space();

            //EditorGUILayout.Space();

            EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.grey);

            PrefabChecker();

            AggresionSlider();

            IntelligenceSlider();

            StrengthSlider();

            AgilitySlider();

            //PerceptionSlider();

            HatSelector();

            EditorGUILayout.Space();

            PrefabSaver();
        }

        //var byttonToCreate = GUILayout.Button("Create");
    }

    private void ActivationButton() //NOT FUNCIONANDO
    {
        string _nameOfTheBase = "MAINBASE";
        var funnyButton = GUILayout.Button("ayuda");

        if (funnyButton)
        {
            for (var i = 0; i < _typesList.Length; i++)
            {
                if (_typesList[i].gameObject.name == _nameOfTheBase)
                {
                    _prefabBase = _typesList[i];
                }
            }
        }


        /*

        if (funnyButton)
        {
            AssetDatabase.CopyAsset("Assets/Assets/Prefabs/BACKUPPORLASDUDAS2.prefab", "Assets/" + "Test" + ".prefab");
            //_prefabCitizen = GameObject.FindObjectOfType<>("Assets/Assets/Prefabs/Test.prefab");

            //_prefabCitizen = GameObject.FindObjectOfType<BaseCitizenScript>();

            //var newSaver = Resources.Load("Assets/Assets/Prefab/Test");
            _preCheck = PrefabUtility.LoadPrefabContents("Assets/Assets/Prefabs/Test.prefab");
        }
        //AssetDatabase.CopyAsset(AssetDatabase.get)*/
    }

    private void AggresionSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Aggression", _labelStyle);

        _prefabBase.LevelOfAggression = EditorGUILayout.Slider(_prefabBase.LevelOfAggression, 0, 10);

        _prefabBase.LevelOfAggression = Mathf.Round(_prefabBase.LevelOfAggression * 10) * 0.1f;

        if (_prefabBase.LevelOfAggression > 5)
        {
            _prefabBase.Mouth.transform.rotation = new Quaternion(0, 0, -180, 0);
            _prefabBase.RightEyebrow.transform.rotation = new Quaternion(0, 0, -19.804f, 80);
            _prefabBase.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 25.196f, 80);
        }

        else if (_prefabBase.LevelOfAggression <= 5)
        {
            _prefabBase.Mouth.transform.rotation = new Quaternion(0, 0, -0, 0);
            _prefabBase.RightEyebrow.transform.rotation = new Quaternion(0, 0, -5.507f, 160);
            _prefabBase.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 10.196f, 160);
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void IntelligenceSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Intelligence", _labelStyle);

        _prefabBase.LevelOfIntelligence = EditorGUILayout.Slider(_prefabBase.LevelOfIntelligence, 0, 10);

        _prefabBase.LevelOfIntelligence = Mathf.Round(_prefabBase.LevelOfIntelligence * 10) * 0.1f;

        if (_prefabBase.LevelOfIntelligence > 3)
            _prefabBase.head.transform.localScale = new Vector3(_prefabBase.LevelOfIntelligence / 3, _prefabBase.LevelOfIntelligence / 3, _prefabBase.LevelOfIntelligence / 3);
        else if (_prefabBase.LevelOfIntelligence <= 3)
            _prefabBase.head.transform.localScale = new Vector3(1, 1, 1);

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void StrengthSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Streght", _labelStyle);

        _prefabBase.LevelOfStrength = EditorGUILayout.Slider(_prefabBase.LevelOfStrength, 0, 10);

        _prefabBase.LevelOfStrength = Mathf.Round(_prefabBase.LevelOfStrength * 10) * 0.1f;

        if (_prefabBase.LevelOfStrength > 3)
        {
            _prefabBase.RightHand.transform.localScale = new Vector3(_prefabBase.LevelOfStrength / 3, _prefabBase.LevelOfStrength / 3, _prefabBase.LevelOfStrength / 3);
            _prefabBase.LeftHand.transform.localScale = new Vector3(_prefabBase.LevelOfStrength / 3, _prefabBase.LevelOfStrength / 3, _prefabBase.LevelOfStrength / 3);
        }
        else if (_prefabBase.LevelOfStrength <= 3)
        {
            _prefabBase.RightHand.transform.localScale = new Vector3(1, 1, 1);
            _prefabBase.LeftHand.transform.localScale = new Vector3(1, 1, 1);
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void AgilitySlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Agility", _labelStyle);

        _prefabBase.LevelofAgility = EditorGUILayout.Slider(_prefabBase.LevelofAgility, 0, 10);

        _prefabBase.LevelofAgility = Mathf.Round(_prefabBase.LevelofAgility * 10) * 0.1f;

        if (_prefabBase.LevelofAgility > 3)
        {
            _prefabBase.RightLeg.transform.localScale = new Vector3(1, _prefabBase.LevelofAgility / 6, 1);
            _prefabBase.LeftLeg.transform.localScale = new Vector3(1, _prefabBase.LevelofAgility / 6, 1);
        }
        else if (_prefabBase.LevelofAgility <= 3)
        {
            _prefabBase.RightLeg.transform.localScale = new Vector3(1, 1, 1);
            _prefabBase.LeftLeg.transform.localScale = new Vector3(1, 1, 1);
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    /*private void PerceptionSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Perception", _labelStyle);

        _prefabCitizen.LevelOfPerception = EditorGUILayout.Slider(_prefabCitizen.LevelOfPerception, 0, 10);

        _prefabCitizen.LevelOfPerception = Mathf.Round(_prefabCitizen.LevelOfPerception * 10) * 0.1f;

        if (_prefabCitizen.LevelOfPerception > 3)
        {
            //_prefabCitizen.Eyes.transform.localScale = new Vector3(_prefabCitizen.LevelOfPerception / 450, _prefabCitizen.LevelOfPerception / 450, _prefabCitizen.LevelOfPerception / 450);
            _prefabCitizen.Eyes.transform.localScale = new Vector3(_prefabCitizen.LevelOfPerception / 450, _prefabCitizen.LevelOfPerception / 450, 0.00939989f);
            //_prefabCitizen.Eyes.transform.position = _prefabCitizen.EyesBase.transform.position;

        }
            
        else if (_prefabCitizen.LevelOfPerception <= 3)
        {
            _prefabCitizen.Eyes.transform.localScale = new Vector3(0.00939989f, 0.00939989f, 0.00939989f);
            //_prefabCitizen.Eyes.transform.position = new Vector3 (1, -0.00054f, 0.00068f);
        }
            
        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }*/

    void HatSelector()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Hats", _labelStyle);

        _prefabBase.numbe = EditorGUILayout.IntSlider(_prefabBase.numbe, 0, 9);

        switch (_prefabBase.numbe)
        {
            case 0:
                CleanHats();
                break;

            case 1:
                CleanHats();
                _prefabBase.Hats[0].SetActive(true);
                break;

            case 2:
                CleanHats();
                _prefabBase.Hats[1].SetActive(true);
                break;

            case 3:
                CleanHats();
                _prefabBase.Hats[2].SetActive(true);
                break;

            case 4:
                CleanHats();
                _prefabBase.Hats[3].SetActive(true);
                break;

            case 5:
                CleanHats();
                _prefabBase.Hats[4].SetActive(true);
                break;

            case 6:
                CleanHats();
                _prefabBase.Hats[5].SetActive(true);
                break;

            case 7:
                CleanHats();
                _prefabBase.Hats[6].SetActive(true);
                break;

            case 8:
                CleanHats();
                _prefabBase.Hats[7].SetActive(true);
                break;

            case 9:
                CleanHats();
                _prefabBase.Hats[8].SetActive(true);
                break;
        }

        void CleanHats()
        {
            foreach (GameObject objet in _prefabBase.Hats)
            {
                objet.SetActive(false);
            }
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void PrefabSaver()
    {
        var currentPath = AssetDatabase.GetAssetPath(_prefabBase);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Civilization name", GUILayout.Width(115));
        name = EditorGUILayout.TextField(name);

        var saveButton = GUILayout.Button("Save edited citizen");

        if (saveButton && name != "" && name != " ")
        {
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(_prefabBase), "Assets/" + name + ".prefab");
            Reseter();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.OpenAsset(_prefabBase);
        }
    }

    private void PrefabChecker()
    {
        if (!_isOnScreen)
        {
            AssetDatabase.OpenAsset(_prefabBase);
            _isOnScreen = true;
        }
    }

    private void Reseter()
    {
        _prefabBase.LevelOfAggression = 3;
        _prefabBase.LevelofAgility = 3;
        _prefabBase.LevelOfIntelligence = 3;
        _prefabBase.LevelOfPerception = 3;
        _prefabBase.LevelOfStrength = 3;
    }

    public void UpdateDatabase()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.OpenAsset(_prefabBase);
    }

    public void IntensityApplayer()
    {
        var buttonPerception = GUILayout.Button("Apply");

        if (buttonPerception)
            UpdateDatabase();
    }
}
