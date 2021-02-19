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
    Texture swordTex;
    Texture staffTex;
    Texture monacleTex;
    private void OnEnable()
    {
        CProxy = new Citizen();
        C = (Citizen)target;
        CProxy.Equals(C);
        cloverTex = (Texture)Resources.Load("Clover", typeof(Texture));
        swordTex = (Texture)Resources.Load("Sword", typeof(Texture));
        staffTex = (Texture)Resources.Load("Staff", typeof(Texture));
        monacleTex = (Texture)Resources.Load("Monacle", typeof(Texture));
    }
    public override void OnInspectorGUI()
    {
        C.LevelOfAggression = EditorGUILayout.Slider("Aggression",C.LevelOfAggression, 0, 10);

        C.LevelOfIntelligence = EditorGUILayout.Slider("Intelligence",C.LevelOfIntelligence, 0, 10);

        C.LevelOfStrength = EditorGUILayout.Slider("Strength", C.LevelOfStrength, 0, 10);

        C.LevelofAgility = EditorGUILayout.Slider("Agility", C.LevelofAgility, 0, 10);

        C.currentHat = EditorGUILayout.IntSlider("Hat", C.currentHat, 0, 7);

        if(C.currentHat != CProxy.currentHat)
        {
            CleanHats();
            C.Hats[C.currentHat].SetActive(true);
        }
        if(C.currentHat == 0)
            CleanHats();
        GUILayout.BeginHorizontal();

        if(GUILayout.Button(cloverTex))
        {

        }
        if (GUILayout.Button(swordTex))
        {

        }
        if (GUILayout.Button(staffTex))
        {

        }
        if (GUILayout.Button(monacleTex))
        {

        }
        GUILayout.EndHorizontal();
    }
    void CleanHats()
    {
        foreach (GameObject objet in C.Hats)
        {
            objet.SetActive(false);
        }
    }
}
