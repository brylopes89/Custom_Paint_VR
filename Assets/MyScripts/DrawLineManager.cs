using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DrawLineManager : MonoBehaviour
{
    public static DrawLineManager Instance;
    public GameObject penPoint;
    public GameObject menu;
    public Hand leftHand;
    public SteamVR_Behaviour_Pose trackedObj;    
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Vibration hapticAction;
    public Material lMat;
    public Transform playerTrans;
    public float lineWidth = .1f;
    
    [HideInInspector] public List<GameObject> lineRendererList = new List<GameObject>();
    [HideInInspector] public MeshLineRenderer currLine;

    private int numClicks = 0;

    public void Awake()
    {
        Instance = this;      
        
    }

    private void Start()
    {        
        ShowBrushMenu();
    }
    void Update()// to do, smooth out currposition with last position to create ribbon effect
    {
        VR_Pointer laserPointer = FindObjectOfType<VR_Pointer>();
        if (laserPointer.PointAt)
        {
            currLine = null;
        }
        else
        {
            if (trackPadAction.GetStateDown(trackedObj.inputSource))
            {
                GameObject go = new GameObject();
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>();

                currLine = go.AddComponent<MeshLineRenderer>();
                currLine.lmat = new Material(lMat);
                currLine.SetWidth(lineWidth);
                currLine.transform.position = go.transform.position;               

                lineRendererList.Add(go);
            }
            else if (trackPadAction.GetState(trackedObj.inputSource))
            {              
                //Vector3 points = penPoint
                currLine.AddPoint(penPoint.transform.position, playerTrans.position); //trackedObj.transform.position

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

        penPoint.GetComponent<Renderer>().material.color = ColorManager.Instance.GetCurrentColor();
    }

    public void ShowBrushMenu()
    {
        SetMenuPosition(menu);
        menu.SetActive(true);
    }

    public void SetMenuPosition(GameObject menu)
    {        
        GameObject game = menu.transform.parent.gameObject;
        game.transform.parent = leftHand.transform;
        game.transform.localRotation = Quaternion.Euler(new Vector3(55f, leftHand.transform.rotation.eulerAngles.y, 0));
        game.transform.localPosition = new Vector3(0.2f, 0, 0);
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }

}   

