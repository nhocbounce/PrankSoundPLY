using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpDownWord : MonoBehaviour
{

    [SerializeField]
    float far, duration;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        UpDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpDown()
    {
        if (i > transform.childCount - 1)
        {
            i = 0;
        }
        transform.GetChild(i).DOPunchPosition(Vector2.up * far, duration, 0);
        i++;
        Invoke(nameof(UpDown), 0.2f);
    }
}
