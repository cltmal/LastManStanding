using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caPothonInit : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼
    //public Button readyButton;
    public Text PlayerNum;
    public int maxPlayers;
    private RoomOptions options;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; //  하나의 클라이언트가 룸내의 모든 클라이언트들에게 로드해야할 레벨을 정의
    }
    void Start()
    {
        OnLogin();
    }

    void OnLogin()
    {
        PhotonNetwork.GameVersion = this.gameVersion; //포톤게임버전을 설정(같은 게임버전끼리 공유)
        PhotonNetwork.ConnectUsingSettings();//포톤을 이용한 온라인 연결
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!!!");
        PhotonNetwork.JoinRandomRoom();// 생성되있는 룸에 랜덤하게 접속합니다.
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed Join Room!!");
        CreateRoom();
    }
    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
}
