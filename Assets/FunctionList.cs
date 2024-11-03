using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunctionList : MonoBehaviour {

    public Font defaultFont;
    public GameObject canvas;
    private List<GameObject> lText;
    private List<Text> ltext;
    private int cnt;
    // Use this for initialization
    void Start () {
        lText = new List<GameObject>();
        ltext = new List<Text>();
        cnt = 0;
        //Init();
	}

    private void Init()
    {
        int yValue = 0;
        for (int i = 0; i < 10; i++)
        {
            string ItemName;
            ItemName = string.Format("Item{0}", i);
            GameObject textobj = new GameObject(ItemName);
            Text textcom = textobj.AddComponent<Text>();
            if (canvas != null)
            {
                textobj.transform.SetParent(canvas.transform, false);
            }
            textcom.font = defaultFont;
            textcom.text = "test";
            textobj.transform.SetParent(canvas.transform, false);
            //textobj.transform.location = new Vector3
        }
    }
    public void AddFunction(string fn, string d1, string d2, string d3, string d4, string d5, string d6) {
        string ItemName;
        ItemName = string.Format("Item{0}", cnt++);
        GameObject textobj = new GameObject(ItemName);
        Text textcom = textobj.AddComponent<Text>();
        
        textobj.transform.SetParent(canvas.transform, false);
        
        lText.Add(textobj);
        ltext.Add(textcom);
        RectTransform textRectTransform = textcom.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(300,50);
        textcom.font = defaultFont;
        
        textcom.text = fn + "\n\t" + d1 + "\t" + d2 + "\t" + d3 + "\n\t" + d4 + "\t" + d5 + "\t" + d6;
        textobj.transform.SetParent(canvas.transform, false);
        
    }
    public void AllDelete()
    {
        GameObject tx;
        Text txt;
        if (ltext.Count == 0) return;
        for (int i = 0; i < ltext.Count; i++)
        {
            txt = ltext[i];
            txt.gameObject.SetActive(false);

            Destroy(txt);

        }
        // EditorSceneManager.SaveOpenScenes();
        //EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path);
        
        lText.Clear();
        ltext.Clear();
        cnt = 0;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
