using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Citizen))]
public class CitizenEditor : Editor
{
    Citizen C;
    Citizen CProxy;

    Texture cloverTex;
    Texture gunTex;
    Texture staffTex;
    Texture monacleTex;

    GUIStyle _titleStyle;
    private void OnEnable()
    {
        CProxy = new Citizen();
        C = (Citizen)target;
        CProxy.Equals(C);
        cloverTex = (Texture)Resources.Load("Clover", typeof(Texture));
        gunTex = (Texture)Resources.Load("Gun", typeof(Texture));
        staffTex = (Texture)Resources.Load("Staff", typeof(Texture));
        monacleTex = (Texture)Resources.Load("Monacle", typeof(Texture));

        _titleStyle = new GUIStyle();
        _titleStyle.fontStyle = FontStyle.Bold;
        _titleStyle.alignment = TextAnchor.MiddleCenter;
        _titleStyle.fontSize = 18;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Modifiy Stats", _titleStyle);

        C.LevelOfAggression = EditorGUILayout.Slider("Aggression", C.LevelOfAggression, 0, 10);

        C.LevelOfIntelligence = EditorGUILayout.Slider("Intelligence", C.LevelOfIntelligence, 0, 10);

        C.LevelOfStrength = EditorGUILayout.Slider("Strength", C.LevelOfStrength, 0, 10);

        C.LevelofAgility = EditorGUILayout.Slider("Agility", C.LevelofAgility, 0, 10);

        C.currentHat = EditorGUILayout.IntSlider("Hat", C.currentHat, 0, 7);

        if (C.currentHat != CProxy.currentHat)
        {
            CleanHats();
            C.Hats[C.currentHat].SetActive(true);
            CProxy.Equals(C);
        }
        if (C.LevelOfAggression != CProxy.LevelOfAggression)
        {
            if (C.LevelOfAggression > 5)
            {
                C.Mouth.transform.rotation = new Quaternion(0, 0, -180, 0);
                C.RightEyebrow.transform.rotation = new Quaternion(0, 0, -19.804f, 80);
                C.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 25.196f, 80);
            }

            else if (C.LevelOfAggression <= 5)
            {
                C.Mouth.transform.rotation = new Quaternion(0, 0, -0, 0);
                C.RightEyebrow.transform.rotation = new Quaternion(0, 0, -5.507f, 160);
                C.LeftEyebrow.transform.rotation = new Quaternion(0, 0, 10.196f, 160);
            }
            CProxy.Equals(C);
        }
        if (C.LevelofAgility != CProxy.LevelofAgility)
        {
            if (C.LevelofAgility > 2)
            {
                C.RightLeg.transform.localScale = new Vector3(1, C.LevelofAgility / 6, 1);
                C.LeftLeg.transform.localScale = new Vector3(1, C.LevelofAgility / 6, 1);
            }
            else if (C.LevelofAgility <= 2)
            {
                C.RightLeg.transform.localScale = new Vector3(1, 0.25f, 1);
                C.LeftLeg.transform.localScale = new Vector3(1, 0.25f, 1);
            }
        }
        if (C.LevelOfIntelligence != CProxy.LevelOfIntelligence)
        {
            if (C.LevelOfIntelligence > 3)
                C.head.transform.localScale = new Vector3(C.LevelOfIntelligence / 3, C.LevelOfIntelligence / 3, C.LevelOfIntelligence / 3);
            else if (C.LevelOfIntelligence <= 3)
                C.head.transform.localScale = new Vector3(1, 1, 1);
        }
        if (C.LevelOfStrength != CProxy.LevelOfStrength)
        {
            if (C.LevelOfStrength > 3)
            {
                C.RightHand.transform.localScale = new Vector3(C.LevelOfStrength / 3, C.LevelOfStrength / 3, C.LevelOfStrength / 3);
                C.LeftHand.transform.localScale = new Vector3(C.LevelOfStrength / 3, C.LevelOfStrength / 3, C.LevelOfStrength / 3);
            }
            else if (C.LevelOfStrength <= 3)
            {
                C.RightHand.transform.localScale = new Vector3(1, 1, 1);
                C.LeftHand.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (C.currentHat == 0)
            CleanHats();

        GUILayout.Label("Pick a Preset", _titleStyle);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(cloverTex))
        {
            C.currentHat = 3;
            C.LevelOfAggression = 8;
            C.LevelOfIntelligence = 3;
            C.LevelOfStrength = 3;
            C.LevelofAgility = 1;
        }
        if (GUILayout.Button(gunTex))
        {
            C.currentHat = 5;
            C.LevelOfAggression = 10;
            C.LevelOfIntelligence = 1;
            C.LevelOfStrength = 8;
            C.LevelofAgility = 4;
        }
        if (GUILayout.Button(staffTex))
        {
            C.currentHat = 6;
            C.LevelOfAggression = 3;
            C.LevelOfIntelligence = 10;
            C.LevelOfStrength = 1;
            C.LevelofAgility = 4;
        }
        if (GUILayout.Button(monacleTex))
        {
            C.currentHat = 1;
            C.LevelOfAggression = 8;
            C.LevelOfIntelligence = 8;
            C.LevelOfStrength = 3;
            C.LevelofAgility = 1;
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Rename Civilization", _titleStyle);

        C.name = EditorGUILayout.TextField(C.name);

        if (GUILayout.Button("save"))
        {
            EditorUtility.SetDirty(C);
            PrefabUtility.SaveAsPrefabAsset(C.gameObject, "Assets/" + C.name + ".prefab");
        }
    }
    void CleanHats()
    {
        foreach (GameObject objet in C.Hats)
        {
            objet.SetActive(false);
        }
    }
}
