using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAssetSettings : ScriptableObject
{
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
    public Space RotationSpace;
}
