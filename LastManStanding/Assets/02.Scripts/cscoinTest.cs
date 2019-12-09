using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;


public class cscoinTest : MonoBehaviourPun
{
    int scorecoin;
    Text score;
    private void Start()
    {
      score = GameObject.Find("score").GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("코인먹음" + scorecoin);

            Destroy(other.gameObject);
            if (photonView.IsMine)
            {
                score = GameObject.Find("score").GetComponent<Text>();
                scorecoin++;
                score.text = "Score:" + scorecoin;
            }
        }
        if(scorecoin == 4)
        {
            PhotonNetwork.LoadLevel("Win");
        }
    }
}