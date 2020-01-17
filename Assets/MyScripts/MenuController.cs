using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text sizetext;

    private float size;    
   
    List<GameObject> lineList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        size = DrawLineManager.Instance.lineWidth;             
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
        size += .02f;
        if (size > .5f)
        {
            size = .5f;
        }
        else if (size > .02f && size < .5f)
        {
            DrawLineManager.Instance.lineWidth = size;
            Debug.Log(size);
        }        
    }
    public void BtnMinusBrushSize()
    {
        size -= .02f;
        if (size < 0.02f)
        {
            size = 0.02f;
        }
        DrawLineManager.Instance.lineWidth = size;
        Debug.Log(size);
        
    }
}
