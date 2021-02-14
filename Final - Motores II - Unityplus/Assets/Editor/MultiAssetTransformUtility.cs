using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAssetTransformUtility
{
    public static MultiAssetTransform TransformToSettings(MultiAssetSettings MultiAS, MultiAssetTransform MultiAT)
    {
        MultiAS._xRotate = MultiAT._xRotate;
        MultiAS._yRotate = MultiAT._yRotate;
        MultiAS._zRotate = MultiAT._zRotate;

        MultiAS._xScale = MultiAT._xScale;
        MultiAS._yScale = MultiAT._yScale;
        MultiAS._zScale = MultiAT._zScale;

        MultiAS._deegreesRotationA = MultiAT._deegreesRotationA;
        MultiAS._deegreesRotationB = MultiAT._deegreesRotationB;

        MultiAS._UnitsScaleA = MultiAT._UnitsScaleA;
        MultiAS._UnitsScaleB = MultiAT._UnitsScaleB;

        MultiAS._RotateOnWorldAxis = MultiAT._RotateOnWorldAxis;

        MultiAS.CurrentStateR = MultiAT.CurrentStateR;
        MultiAS.RotationSpace = MultiAT.RotationSpace;

        return MultiAT;
    }

    public static MultiAssetSettings SettingsToTransform(MultiAssetSettings MultiAS, MultiAssetTransform MultiAT)
    {
        MultiAT._xRotate = MultiAS._xRotate;
        MultiAT._yRotate = MultiAS._yRotate;
        MultiAT._zRotate = MultiAS._zRotate;

        MultiAT._xScale = MultiAS._xScale;
        MultiAT._yScale = MultiAS._yScale;
        MultiAT._zScale = MultiAS._zScale;

        MultiAT._deegreesRotationA = MultiAS._deegreesRotationA;
        MultiAT._deegreesRotationB = MultiAS._deegreesRotationB;

        MultiAT._UnitsScaleA = MultiAS._UnitsScaleA;
        MultiAT._UnitsScaleB = MultiAS._UnitsScaleB;

        MultiAT._RotateOnWorldAxis = MultiAS._RotateOnWorldAxis;

        MultiAT.CurrentStateR = MultiAS.CurrentStateR;
        MultiAT.RotationSpace = MultiAS.RotationSpace;

        return MultiAS;
    }

    public static void CopySettings(MultiAssetSettings From, MultiAssetSettings To)
    {
        To._xRotate = From._xRotate;
        To._yRotate = From._yRotate;
        To._zRotate = From._zRotate;

        To._xScale = From._xScale;
        To._yScale = From._yScale;
        To._zScale = From._zScale;

        To._deegreesRotationA = From._deegreesRotationA;
        To._deegreesRotationB = From._deegreesRotationB;

        To._UnitsScaleA = From._UnitsScaleA;
        To._UnitsScaleB = From._UnitsScaleB;

        To._RotateOnWorldAxis = From._RotateOnWorldAxis;

        To.CurrentStateR = From.CurrentStateR;
        To.RotationSpace = From.RotationSpace;
    }
}
