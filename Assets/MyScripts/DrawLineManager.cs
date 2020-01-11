using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System.IO;
using System.Text;
using LitJson;
using JimmyGao;

public class DrawLineManager : MonoBehaviour
{
    public SteamVR_Behaviour_Pose trackedObj;   
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Vibration hapticAction;
    public Material lMat;
    public Transform playerTrans;

    private MeshLineRenderer currLine;
    private int numClicks = 0;

    //public SteamVR_TrackedObject TrackedObj;
    /*public static BrushManager Instance;
    public Hand BrushHand;
    public Hand SwitcherHand;
    public GameObject BrushObj;
    public GameObject BrushMenu;
    //public Hand hand2;
    public GameObject InitLineObj;
    public List<Color> BrushColor;
    public GameObject ColorPalette;

    public GameObject LaserBeam;

    private Vector3 lastPos;
    private int segNum = 0;
    // Use this for initialization
    //0 idle  1 draw
    int CurrentState;
    int colorIdx;
    public List<GameObject> GoList = new List<GameObject>();
    string txtname = "brushsavejson.txt";*/
    

    void Update()// to do, smooth out currposition with last position to create ribbon effect
    {
        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();

            currLine = go.AddComponent<MeshLineRenderer>();
            currLine.lmat = new Material(lMat);
            currLine.SetWidth(.1f);                  
        }
        else if (trackPadAction.GetState(trackedObj.inputSource))
        {
            //currLine.positionCount = numClicks + 1;
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position, playerTrans.position);
            numClicks++;
        }
        else if (trackPadAction.GetStateUp(trackedObj.inputSource))
        {
            numClicks = 0;
            currLine = null;
        }

        if (currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
        }
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }
}   

