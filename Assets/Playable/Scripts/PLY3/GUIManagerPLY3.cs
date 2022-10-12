using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Luna.Unity;
using System;
using UnityEngine.Video;

public class GUIManagerPLY3 : MonoBehaviour
{

    private static GUIManagerPLY3 instance;

    public static GUIManagerPLY3 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GUIManagerPLY3>();
            }

            return instance;
        }
    }

    [SerializeField]
    Camera mainCam;

    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    VideoClip[] videoClips;

    //Object Unchanged when rotate
    [SerializeField]
    GameObject endCard;

    //Object Changed when rotate
    [SerializeField]
    DualObject canVas, hand, whu, ta,zz, songPan1, songPan2;

    [SerializeField]
    Animator verti, hori;

    bool isHor, rotateScreen, second, third;

    [SerializeField]
    DualAnimator[] btns;

    bool once,clicked;

    public int clickCnt, clickNum;

    //Luna Required
    [SerializeField]
    float FixedXsCameraSize, verticalCameraSize, horizontalCameraSize;

    [LunaPlaygroundField("Auto Goto Store: On / Off ", 1, "Game Settings")]
    public bool autoStore = true;


    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Luna Required
        LifeCycle.OnMute += MuteAudio;
        LifeCycle.OnUnmute += UnmuteAudio;

        //Rotate Handle Setup
        InitState();
        isHor = Screen.width >= Screen.height;
        if (isHor)
        {
            mainCam.orthographicSize = horizontalCameraSize;
        }
        else
        {
            mainCam.orthographicSize = verticalCameraSize;
        }
        Adjust();
        VideoAdjust();

        //BGSound
        AudioManager.Instance.Play("BG");
    }

    //Luna Required
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

        //Rotate Handler
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
        Adjust();
        VideoAdjust();
    }

    public void SoundClicked(string soundName)
    {
        hori.enabled = false;
        verti.enabled = false;
        hand.curState = false;
        clickCnt++;
        AudioManager.Instance.StopAll();
        AudioManager.Instance.AfterPlay(soundName);
        FireEvent(soundName + " clicked");
        once = true;

        //PLY5
        if(clickCnt == 1)
        {
            ta.curState = false;
            Invoke(nameof(WakeUp), 1.5f);
            //ShowEndCard1();
        }
        if (clickCnt == 2)
        {
            CancelInvoke(nameof(WakeUp));
            clicked = true;
            SecondStage();
        }
        //if (clickCnt == 3)
        //{
        //    clicked = true;
        //    ShowEndCard1();
        //}
        whu.curState = false;
        
        //PLY4
        //if(clickCnt == 2)
        //{
        //    ta.curState = false;
        //    Invoke(nameof(WakeUp), 1.5f);
        //    //ShowEndCard1();
        //}
        //if (clickCnt == 3)
        //{
        //    clicked = true;
        //    ShowEndCard1();
        //}
        //whu.curState = false;

        //PLY3
        //if (clickCnt == 1)
        //{
        //    ta.curState = false;
        //    Invoke(nameof(WakeUp), 1.5f);
        //    //ShowEndCard1();
        //}
        //if (clickCnt == 2)
        //{
        //    clicked = true;
        //    ShowEndCard1();
        //}
        //whu.curState = false;
    }

    void WakeUp()
    {
        //videoPlayer.isLooping = false;
        second = true;
        zz.curState = false;
        //videoPlayer.playbackSpeed = 0.5f;
    }


    public void SecondStage()
    {
        ShowEndCard1();
        for (int i = 0; i < btns.Length; i++)
            if (i == clickNum - 1)
            {
                btns[i].curState = false;
            }

        hori.enabled = true;
        verti.enabled = true;
        hand.curState = true;

        songPan2.curState = true;
        songPan1.curState = false;

        second = false;
        ta.curState = true;
        third = true;
        //videoPlayer.playbackSpeed = 0.25f;
        //videoPlayer.isLooping = false;
    }


    public void SecondClick()
    {
        AudioManager.Instance.Play("BG");

        Invoke(nameof(NewTxt), 0.5f);
        for (int i = 0; i < btns.Length; i++)
            if (i == clickNum - 1)
            {
                btns[i].curState = false;
            }

    }

    void NewTxt()
    {
        ta.curState = true;
    }

    public void FireEvent(string eventName)
    {
        Analytics.LogEvent(eventName, 0);
    }

    public void ShowEndCard()
    {
        AudioManager.Instance.Play("BG");
        //Prevent this script run 2 time because of AudioManager.ShowEndcard
        if (!clicked)
        {
            AudioManager.Instance.StopAll();
            Analytics.LogEvent("EndCard Shown", 0);
            PanelOnOff.Single(endCard, true);
            AutoStore();

        }
    }
    public void ShowEndCard1()
    {
        autoStore = false;
        AudioManager.Instance.StopAll();
        Analytics.LogEvent("EndCard Shown", 0);
        PanelOnOff.Single(endCard, true);
        AutoStore();
    }

    public void BtnClicked(int button)
    {
        clickNum = button;
        for (int i = 0; i < btns.Length; i++)
            if (i == button - 1)
            {
                btns[i].curState = true;

            }
            else
            {
                btns[i].verti.gameObject.transform.localScale = Vector3.one;
                btns[i].hori.gameObject.transform.localScale = Vector3.one * 3.8f;
                btns[i].curState = false;
            }
    }




    public void AutoStore()
    {
        if (autoStore)
            Invoke(nameof(GotoStore), 4);
    }

    public void GotoStore()
    {
        CancelInvoke(nameof(GotoStore));
        LifeCycle.GameEnded();
        Playable.InstallFullGame();

    }


    //Rotate Handler Function
    void Adjust()
    {
        //Invincible
        PanelOnOff.Canvas(canVas, isHor);

        //DualObjects Rotate Handler
        PanelOnOff.Dual(hand);
        PanelOnOff.Dual(whu);
        PanelOnOff.Dual(ta);
        PanelOnOff.Dual(zz);
        PanelOnOff.Dual(songPan2);
        PanelOnOff.Dual(songPan1);

        //DualAnimator
        for (int i = 0; i < btns.Length; i++)
            btns[i].Play();
    }


    void VideoAdjust()
    {
        if (isHor)
        {
            if (second)
                videoPlayer.clip = videoClips[3];
            else if (third)
                videoPlayer.clip = videoClips[5];
            else
                videoPlayer.clip = videoClips[1];
        }
        else
        {
            if (second)
                videoPlayer.clip = videoClips[2];
            else if (third)
                videoPlayer.clip = videoClips[4];
            else
                videoPlayer.clip = videoClips[0];
        }
    }

    void InitState()
    {
        //Init State of DualObjects
        hand.InitCurState();
        whu.InitCurState();
        ta.InitCurState();
        zz.InitCurState();
        songPan2.InitCurState();
        songPan1.InitCurState();
    }

}

[Serializable]
public class DualObject
{
    public GameObject verti, hori;
    [HideInInspector]
    public bool curState;

    public void InitCurState()
    {
        curState = verti.activeSelf;
    }
}
[Serializable]
public class DualAnimator
{
    public Animator verti, hori;
    [HideInInspector]
    public bool curState;

    public void Play()
    {
        hori.SetBool("ClickedHori", curState);
        verti.SetBool("Clicked", curState);
    }
}