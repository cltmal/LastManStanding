using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csInputInt : MonoBehaviour
{

    InputField ipf;
    int aiNum;

    void Start()
    {
        ipf = this.GetComponent<InputField>();
    }
    private void Update()
    {
        aiNum = int.Parse(ipf.text);
        sShareValue.aiNum = aiNum;
    }
}
