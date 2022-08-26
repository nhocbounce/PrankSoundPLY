using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFace : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float dur, x, y, z, ran;

    [SerializeField]
    int vib;

    void Start()
    {
        Shake();
    }

    public void Shake()
    {
        transform.DOShakePosition(dur, new Vector3(x, y, z), vib, ran, false, true).SetLoops(-1);
    }
}
