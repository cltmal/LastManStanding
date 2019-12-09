using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] playerPoint;
    public GameObject player;
 
    // Start is called before the first frame update
    void Start()
    {
        playerPoint = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, playerPoint.Length);
        Debug.Log("플레이어 생성전");
        PhotonNetwork.Instantiate("Player", playerPoint[idx].position, playerPoint[idx].rotation);
        Debug.Log("플레이어 생성후");
       
    }
}
