using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private float lineSize;
    private GameObject penPointSize;
   
    List<GameObject> lineList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        lineSize = DrawLineManager.Instance.lineWidth;
        penPointSize = DrawLineManager.Instance.penPoint;        
    }

    public void UndoPressed()
    {
        lineList = DrawLineManager.Instance.lineRendererList;
        if (lineList.Count > 0)
        {
            GameObject.Destroy(lineList[lineList.Count - 1]);
            lineList.Remove(lineList[lineList.Count - 1]);
            Debug.Log("Undo Pressed");
        }
    }
    public void ClearPressed()
    {
        lineList = DrawLineManager.Instance.lineRendererList;
        if (lineList.Count > 0)
        {
            foreach (GameObject item in lineList)
            {
                GameObject.Destroy(item);
            }
            lineList.Clear();
        }
    }
    public void PlusSizePressed()
    {       
        lineSize += .02f;
        
        if (lineSize > .22f)
        {
            lineSize = .22f;
            
        }
        else if (lineSize >= .02f && lineSize <= .22f)
        {
            DrawLineManager.Instance.lineWidth = lineSize;
            penPointSize.transform.localScale *= 1.25f;
            Debug.Log(lineSize);
        }        
    }
    public void MinusSizePressed()
    {
        lineSize -= .02f;
        if (lineSize < 0.02f)
        {
            lineSize = 0.02f;
        }        
        else if(lineSize >= .02f && lineSize <= .22f)
        {
            penPointSize.transform.localScale /= 1.25f;
            DrawLineManager.Instance.lineWidth = lineSize;
        }

        Debug.Log(lineSize);
        
    }
}
