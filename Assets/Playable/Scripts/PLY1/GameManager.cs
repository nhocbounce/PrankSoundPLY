using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Luna.Unity;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject fartBtn, endCard, handTap, fartPanel, fartTap, volAnim;

    [SerializeField]
    Camera mainCam;

    [SerializeField]
    Animator LIGanim,handAnim;

    bool isHor, rotateScreen = false;

    public static GameManager Instance;
    public float FixedXsCameraSize = 18f;
    public float horizontalCameraSize = 18f;
    public float verticalCameraSize = 24f;

    [LunaPlaygroundField("Auto Goto Store: On / Off ", 1, "Game Settings")]
    public bool autoStore  = true;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LifeCycle.OnMute += MuteAudio;
        LifeCycle.OnUnmute += UnmuteAudio;
        isHor = Screen.width >= Screen.height;
        //vertiCanvas.SetActive(!isHor);
        //horiCanvas.SetActive(isHor);
    }


    void MuteAudio()
    {
        AudioListener.volume = 0;
    }

    void UnmuteAudio()
    {
        AudioListener.volume = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Screen.width < Screen.height && isHor)
        {
            if (Mathf.Abs(Screen.width / (float)Screen.height - 1125f / 2436f) < 0.1)
            {
                mainCam.orthographicSize = FixedXsCameraSize;
            }
            else
            {
                mainCam.orthographicSize = verticalCameraSize;
            }
            isHor = false;

            if (rotateScreen)
            {
                rotateScreen = false;
            }

        }
        else if (Screen.width > Screen.height && !isHor)
        {
            mainCam.orthographicSize = horizontalCameraSize;
            isHor = true;
            if (!rotateScreen)
            {
                rotateScreen = true;
            }
        }
        //AdjustBGCanvas(isHor);
    }


    public void ShowFartIcon()
    {
        PanelOnOff(handTap, false);
        PanelOnOff(fartBtn, true);
        LIGanim.Play("Clicked");
        PanelOnOff(fartTap, true);
    }

    public void FartIconClick()
    {
        AudioManager.Instance.Play("LIGfart");
        PanelOnOff(fartTap, false);
        PanelOnOff(fartBtn, false);
        PanelOnOff(volAnim, true);
        PanelOnOff(fartPanel, true);
        Invoke(nameof(ShowEndCard), 5);
    }

    public void ShowEndCard()
    {
        PanelOnOff(endCard, true);
        AutoStore();
    }

    public void AutoStore()
    {
        if (autoStore)
            Invoke(nameof(GotoStore), 4);
    }

    public void GotoStore()
    {
        Analytics.LogEvent("CTA clicked", 0);
        LifeCycle.GameEnded();
        Playable.InstallFullGame();

    }

    //public void AdjustBGCanvas(bool hori)
    //{
    //    vertiCanvas.SetActive(!hori);
    //    horiCanvas.SetActive(hori);
    //}

    void PanelOnOff(GameObject go, bool on)
    {
        if (go)
            go.SetActive(on);
    }

}
