using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMainButton : MonoBehaviour
{
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void GotoMainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Main");
    }
}
