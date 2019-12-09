using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csQuit : MonoBehaviour
{
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
