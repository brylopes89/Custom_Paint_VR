using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text sizetext;

    private int size;
    private float lineWidth;
   
    List<GameObject> lineList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        lineWidth = .1f;
    }

    // Update is called once per frame
    private void Update()
    {
        
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
}
