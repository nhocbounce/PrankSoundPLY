using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    float curHeight;

    bool once;

    void Start()
    {
        if (CheckScaler())
            UpdateScale();
        if (Screen.height < Screen.width)
            UpdateRotate();
        curHeight = Screen.height;
    }

    private void Update()
    {
        if (Screen.height != curHeight)
        {
            once = true;
            curHeight = Screen.height;
        }
        if (once)
        {
            UpdateRotate();
            if (CheckScaler())
                UpdateScale();
            once = false;
        }
    }

    bool CheckScaler()
    {
        float currHeight = Mathf.Max(Screen.height, Screen.width);
        float curWidth = Mathf.Min(Screen.height, Screen.width);
        float curRat = currHeight / curWidth;
        return curRat != ScreenSize.iniRat;
    }

    void UpdateScale()
    {
        if (Screen.height > Screen.width)
        {
            transform.localScale = new Vector3(transform.localScale.x / ScreenSize.ScaleRatio, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x / ScreenSize.ScaleRatio, transform.localScale.y, transform.localScale.z);
        }

    }
    void UpdateRotate()
    {
        if (Screen.height > Screen.width)
        {
            float ratio1 = (float)(Screen.height) / Screen.width;
            float ratio = Mathf.Pow(ratio1, 2);
            transform.localScale = new Vector3(transform.localScale.x / ratio, transform.localScale.y, transform.localScale.z);

        }
        else
        {
            float ratio1 = (float)(Screen.width) / Screen.height;
            float ratio = Mathf.Pow(ratio1, 2);
            transform.localScale = new Vector3(transform.localScale.x * ratio, transform.localScale.y, transform.localScale.z);
        }

    }
}
