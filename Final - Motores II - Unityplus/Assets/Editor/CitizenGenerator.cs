using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Citizen))]
public class CitizenGenerator : EditorWindow
{
    private GUIStyle _titleStyle;
    private GUIStyle _labelStyle;
    private GUIStyle _centeredMiniLabel;

    public Citizen prefabBase;
    private Citizen _baseClone;

    private GameObject _preCheck;

    private bool _isOnScreen;

    private float _genericStat;

    private string _civilizationName;

    public Citizen[] _typesList; //= GameObject.FindObjectsOfType<BaseCitizenScript>();

    bool _baseWasFound;

    [MenuItem("CustomTools/CitizenGenerator")]
    public static void OpenWindow()
    {
        CitizenGenerator myWindow = (CitizenGenerator)GetWindow(typeof(CitizenGenerator));
        myWindow.Show();
    }

    private void OnEnable()
    {
        _typesList = GameObject.FindObjectsOfType<Citizen>();

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

        //using (new EditorGUI.DisabledScope(prefabBase == true)) //
            //prefabBase = (pogger)EditorGUILayout.ObjectField("Citizen", prefabBase, typeof(pogger), true); //

        if (prefabBase == true)
        {
            EditorGUILayout.Space();

            EditorGUILayout.Space();

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
    }

    private void ActivationButton() //NOT FUNCIONANDO
    {
        //var funnyButton = GUILayout.Button("ayuda");

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

        prefabBase.LevelOfAggression = EditorGUILayout.Slider(prefabBase.LevelOfAggression, 0, 10);

        prefabBase.LevelOfAggression = Mathf.Round(prefabBase.LevelOfAggression * 10) * 0.1f;

        if (prefabBase.LevelOfAggression > 5)
        {
            prefabBase.Mouth.transform.rotation = new Quaternion(0, 0, -180, 0);
            prefabBase.RightEyebrow.transform.rotation = new Quaternion(0, 0, -19.804f, 80);
            prefabBase.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 25.196f, 80);
        }

        else if (prefabBase.LevelOfAggression <= 5)
        {
            prefabBase.Mouth.transform.rotation = new Quaternion(0, 0, -0, 0);
            prefabBase.RightEyebrow.transform.rotation = new Quaternion(0, 0, -5.507f, 160);
            prefabBase.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 10.196f, 160);
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void IntelligenceSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Intelligence", _labelStyle);

        prefabBase.LevelOfIntelligence = EditorGUILayout.Slider(prefabBase.LevelOfIntelligence, 0, 10);

        prefabBase.LevelOfIntelligence = Mathf.Round(prefabBase.LevelOfIntelligence * 10) * 0.1f;

        if (prefabBase.LevelOfIntelligence > 3)
            prefabBase.head.transform.localScale = new Vector3(prefabBase.LevelOfIntelligence / 3, prefabBase.LevelOfIntelligence / 3, prefabBase.LevelOfIntelligence / 3);
        else if (prefabBase.LevelOfIntelligence <= 3)
            prefabBase.head.transform.localScale = new Vector3(1, 1, 1);

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void StrengthSlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Streght", _labelStyle);

        prefabBase.LevelOfStrength = EditorGUILayout.Slider(prefabBase.LevelOfStrength, 0, 10);

        prefabBase.LevelOfStrength = Mathf.Round(prefabBase.LevelOfStrength * 10) * 0.1f;

        if (prefabBase.LevelOfStrength > 3)
        {
            prefabBase.RightHand.transform.localScale = new Vector3(prefabBase.LevelOfStrength / 3, prefabBase.LevelOfStrength / 3, prefabBase.LevelOfStrength / 3);
            prefabBase.LeftHand.transform.localScale = new Vector3(prefabBase.LevelOfStrength / 3, prefabBase.LevelOfStrength / 3, prefabBase.LevelOfStrength / 3);
        }
        else if (prefabBase.LevelOfStrength <= 3)
        {
            prefabBase.RightHand.transform.localScale = new Vector3(1, 1, 1);
            prefabBase.LeftHand.transform.localScale = new Vector3(1, 1, 1);
        }

        EditorGUILayout.Space();

        IntensityApplayer();

        EditorGUILayout.Space();
    }

    private void AgilitySlider()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Agility", _labelStyle);

        prefabBase.LevelofAgility = EditorGUILayout.Slider(prefabBase.LevelofAgility, 0, 10);

        prefabBase.LevelofAgility = Mathf.Round(prefabBase.LevelofAgility * 10) * 0.1f;

        if (prefabBase.LevelofAgility > 3)
        {
            prefabBase.RightLeg.transform.localScale = new Vector3(1, prefabBase.LevelofAgility / 6, 1);
            prefabBase.LeftLeg.transform.localScale = new Vector3(1, prefabBase.LevelofAgility / 6, 1);
        }
        else if (prefabBase.LevelofAgility <= 3)
        {
            prefabBase.RightLeg.transform.localScale = new Vector3(1, 1, 1);
            prefabBase.LeftLeg.transform.localScale = new Vector3(1, 1, 1);
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

        prefabBase.currentHat = EditorGUILayout.IntSlider(prefabBase.currentHat, 0, 9);

        switch (prefabBase.currentHat)
        {
            case 0:
                CleanHats();
                break;

            case 1:
                CleanHats();
                prefabBase.Hats[0].SetActive(true);
                break;

            case 2:
                CleanHats();
                prefabBase.Hats[1].SetActive(true);
                break;

            case 3:
                CleanHats();
                prefabBase.Hats[2].SetActive(true);
                break;

            case 4:
                CleanHats();
                prefabBase.Hats[3].SetActive(true);
                break;

            case 5:
                CleanHats();
                prefabBase.Hats[4].SetActive(true);
                break;

            case 6:
                CleanHats();
                prefabBase.Hats[5].SetActive(true);
                break;

            case 7:
                CleanHats();
                prefabBase.Hats[6].SetActive(true);
                break;

            case 8:
                CleanHats();
                prefabBase.Hats[7].SetActive(true);
                break;

            case 9:
                CleanHats();
                prefabBase.Hats[8].SetActive(true);
                break;
        }

        void CleanHats()
        {
            foreach (GameObject objet in prefabBase.Hats)
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
        var currentPath = AssetDatabase.GetAssetPath(prefabBase);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Civilization name", GUILayout.Width(115));
        name = EditorGUILayout.TextField(name);

        var saveButton = GUILayout.Button("Save edited citizen");

        if (saveButton && name != "" && name != " ")
        {
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(prefabBase), "Assets/" + name + ".prefab");
            Reseter();
            AssetDatabase.Refresh();
            AssetDatabase.OpenAsset(prefabBase);
            prefabBase.currentHat = 0;
        }
    }

    private void PrefabChecker()
    {
        if (!_isOnScreen)
        {
            AssetDatabase.OpenAsset(prefabBase);
            _isOnScreen = true;
        }
    }

    private void Reseter()
    {
        prefabBase.LevelOfAggression = 3;
        prefabBase.LevelofAgility = 3;
        prefabBase.LevelOfIntelligence = 3;
        prefabBase.LevelOfPerception = 3;
        prefabBase.LevelOfStrength = 3;
    }

    public void UpdateDatabase()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.OpenAsset(prefabBase);
    }

    public void IntensityApplayer()
    {
        var buttonPerception = GUILayout.Button("Apply");

        if (buttonPerception)
            UpdateDatabase();
    }
}
