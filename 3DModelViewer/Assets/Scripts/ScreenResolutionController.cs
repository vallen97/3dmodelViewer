using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionController : MonoBehaviour
{
    private Text txtInstruc;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current Screen Resolution: " + Screen.currentResolution);
        Screen.SetResolution(2736, 1824, true, 0);
        Debug.Log("Current Screen Resolution: " + Screen.currentResolution);
        string currRes = "Current Screen Resolution: " + Screen.currentResolution;
        txtInstruc = GameObject.Find("txtInstruc").GetComponent<Text>();
        txtInstruc.text = currRes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
