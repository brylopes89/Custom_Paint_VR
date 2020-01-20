using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text sizetext;

    private float lineSize;
    private GameObject penPointSize;
   
    List<GameObject> lineList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        lineSize = DrawLineManager.Instance.lineWidth;
        penPointSize = DrawLineManager.Instance.penPoint;
    }

    public void BtnUndoClicked()
    {
        lineList = DrawLineManager.Instance.lineRendererList;
        if (lineList.Count > 0)
        {
            GameObject.Destroy(lineList[lineList.Count - 1]);
            lineList.Remove(lineList[lineList.Count - 1]);
            Debug.Log("Undo Pressed");
        }
    }
    public void BtnClearClicked()
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
    public void BtnPlusBrushSize()
    {       
        lineSize += .02f;
        
        if (lineSize > .2f)
        {
            lineSize = .2f;
            
        }
        else if (lineSize > .02f && lineSize < .2f)
        {
            DrawLineManager.Instance.lineWidth = lineSize;
            penPointSize.transform.localScale *= 1.3f;
            Debug.Log(lineSize);
        }        
    }
    public void BtnMinusBrushSize()
    {
        lineSize -= .02f;
        if (lineSize < 0.02f)
        {
            lineSize = 0.02f;
        }        
        else if(lineSize > .02f && lineSize < .2f)
        {
            penPointSize.transform.localScale /= 1.3f;
            DrawLineManager.Instance.lineWidth = lineSize;
        }

        Debug.Log(lineSize);
        
    }
}
