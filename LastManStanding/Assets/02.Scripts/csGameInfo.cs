using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csGameInfo : MonoBehaviour
{
    GameObject obj;
    Text txt;
    
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("ServerInfo");
        txt = obj.GetComponent<Text>();
    }
    public void ChangeText()
    {

        txt.text = "New Level\nYou can now select from multiple(2)\nlevels in the Game Config menu.\nTry out the new Map2 level!\n\nControls\nWASD + Mouse to Move and Lock\nLeft Click to attack\nRight Click to duck\nLeft Shift to Sprint\nP to open Pause Menu";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
