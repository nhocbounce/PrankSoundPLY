using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenSize : MonoBehaviour
{

    [SerializeField]
    Vector2 initialScreenSize;

    static float  curHeight, curWidth;
    public static float curRat, iniRat;

    static Vector2 curScreenSize;

    private void Awake()
    {
        curScreenSize = initialScreenSize;
        iniRat = curScreenSize.x / curScreenSize.y;
    }

    static float CalScaleRatio()
    {
        curHeight = Mathf.Max(Screen.height, Screen.width);
        curWidth = Mathf.Min(Screen.height, Screen.width);
        curRat = curHeight / curWidth;

        if (curRat == iniRat)
            return 1;
        else
        {
            var ratio = curRat / iniRat;
            iniRat = curRat;
            return ratio;
        }
    }

    public static float ScaleRatio
    {
        get
        {
            var scale = CalScaleRatio();
            return scale;
        }
    }
}
