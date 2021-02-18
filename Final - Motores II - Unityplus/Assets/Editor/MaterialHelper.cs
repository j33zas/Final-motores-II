using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshRenderer))]
public class MaterialHelper : Editor
{
    MeshRenderer MR;
    int currentIndex = 0;
    List<Material> materialSnapshots = new List<Material>();

    private void OnEnable()
    {
        MR = (MeshRenderer)target;
        materialSnapshots.Add(MR.sharedMaterial);    
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (MR.material != materialSnapshots[materialSnapshots.Count-1])
        {
            materialSnapshots.Add(MR.material);
            currentIndex++;
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("undo"))
            if(materialSnapshots.Count > 2)
                MR.sharedMaterial = materialSnapshots[currentIndex];

        if (GUILayout.Button("Return To original"))
            MR.sharedMaterial = materialSnapshots[0];

        GUILayout.EndHorizontal();
    }
}
