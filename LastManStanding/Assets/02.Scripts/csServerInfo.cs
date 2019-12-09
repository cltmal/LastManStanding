using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csServerInfo : MonoBehaviour
{
    GameObject obj;
    Text txt;

    void Start()
    {
        obj = GameObject.Find("ServerInfo");
        txt = obj.GetComponent<Text>();
    }
    public void ChangeText()
    {
        txt.text = "SERVER INSTRUCTIONS\n\nHow to host\nPrees Play and Host to begin\nhosting a server. You'll need to share \nyour IP Adress with your friends to\nlet them join, as there's no Server \nBrowser yet\n\nHow to host\n\nEnter the IP your friend has given you\nin the field below Join a Game. Click Join when done.";
    }
    // Update is called once per frame
    void Update()
    {

    }
}
