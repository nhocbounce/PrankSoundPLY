using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PanelOnOff
{
    public static void Single(GameObject go, bool on)
    {
        if (go)
            go.SetActive(on);
    }
    
    public static void Dual(DualObject dual)
    {
        Single(dual.verti, dual.curState);
        Single(dual.hori, dual.curState);
    }

    public static void Canvas(DualObject canvas, bool hori)
    {
        Single(canvas.verti, !hori);
        Single(canvas.hori, hori);
    }
}
