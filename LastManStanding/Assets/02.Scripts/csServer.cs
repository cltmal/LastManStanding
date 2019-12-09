using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csServer : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";// 게임버젼
    public Button JoinButton;
    public Text text;



    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //접속에 필요한 정보(게임버젼)설정
        PhotonNetwork.GameVersion = gameVersion;
        text = GameObject.Find("Status Text").GetComponent<Text>();


        //설정 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
        JoinButton.interactable = false;
        //접속을 시도 중임을 알림
        text.text = "마스터서버에 접속 중...";
        Debug.Log("마스터서버에 접속 중...");
    }

    //마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //접속 정보 표시
        JoinButton.interactable = true;
        text.text = "온라인 : 마스터 서버와 연결 됨";
        Debug.Log("온라인 : 마스터 서버와 연결 됨");
    }
    //마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        JoinButton.interactable = false;

        //접속 정보 표시
        Debug.Log("온라인 :  마스터 서버와 연결 됨");
        text.text = "온라인 :  마스터 서버와 연결 됨";
        //마스터 서버로 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }
    //룸 접속 시도
    public void ConnectRoom()
    {
        JoinButton.interactable = false;
        //마스터 서버에 접속 중이라면
        if (PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            text.text = "룸에 접속...";
            Debug.Log("룸에 접속...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            text.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

            //마스터 서버에 접속중이 아니라면 마스터서버에 접속 시도
            Debug.Log("오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    //(빈 방이 없어) 랜덤 룸 참가에 실패한 경우 자동 실행0
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("빈 방이 없음 새로운 방 생성...");
        text.text = "빈 방이 없음 새로운 방 생성...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
    //룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        text.text = "방 참가 성공";
        Debug.Log("현재 접속 인원 : " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("LastManStanding");
    }
}
