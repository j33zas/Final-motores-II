using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCitizenScript : MonoBehaviour
{
    public float LevelOfAggression;
    public float LevelOfIntelligence;
    public float LevelOfStrength;
    public float LevelofAgility;
    public float LevelOfPerception;

    public GameObject head;
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject LeftEyebrow;
    public GameObject RightEyebrow;
    public GameObject Mouth;
    public GameObject Eyes;
    public GameObject EyesBase;
    public GameObject LeftLeg;
    public GameObject RightLeg;
    public List<GameObject> Hats = new List<GameObject>();
}