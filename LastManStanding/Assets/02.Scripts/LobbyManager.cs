using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static Photon.Realtime.Player;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼
    //public Button readyButton;
    public Text PlayerNum;
    public int maxPlayers;
    private RoomOptions options;
    private TypedLobbyInfo playercount;

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false; // 비활성화 
        connectionInfoText.text = "마스터 서버에 접속중...";
        options = new RoomOptions();
        playercount = new TypedLobbyInfo();
        options.MaxPlayers = (byte)maxPlayers; //options.MaxPlayers가 byte 형 이기 떄문에 변수는 int형으로 되어있어서  byte로 형변환 해주었다.
    }
    // void Update()
    //{
    //    PlayerNum.text = "현재인원:" + PhotonNetwork.CurrentRoom.PlayerCount;
    //}

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";

        PlayerNum.text = "현재인원:";
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = "마스터 서버와 연결되지 않음\n재접속 시도중...";

        PhotonNetwork.ConnectUsingSettings();
    }


    // 룸 접속 시도
    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "룸 접속 시도중";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "빈 방이 없음. 새로운 방 생성...";
        PhotonNetwork.CreateRoom(null, options);
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {

        connectionInfoText.text = "방 참가 성공";
        //Debug.Log("현재 참가인원 : " + PhotonNetwork.CurrentRoom.PlayerCount); //Current = 현재의 룸에 PlayerCount를 Log에 띄운다
        PlayerNum.text = "현재인원:" + PhotonNetwork.CurrentRoom.PlayerCount;
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && !PhotonNetwork.IsMasterClient)
        //{
        //    Debug.Log(PhotonNetwork.IsMasterClient);
        //    //Debug.Log(PhotonNetwork.NickName);
        //    PhotonNetwork.LoadLevel("LastManStanding");
        //}
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2&& PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("LastManStanding");
        }
        else if (!PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel("LastManStanding");
        }
    }


    //override public void OnPlayerEnteredRoom(Player player)
    //{
    //    Debug.Log("엔터 현재 인원 : " + PhotonNetwork.CurrentRoom.PlayerCount);
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
    //    {
    //        Debug.Log(PhotonNetwork.IsMasterClient);
    //        Debug.Log("마스터임 : " + PhotonNetwork.CurrentRoom.PlayerCount);
    //        PhotonNetwork.LoadLevel("LastManStanding");
    //    }
    //    else if (!PhotonNetwork.IsMasterClient)
    //        PhotonNetwork.LoadLevel("LastManStanding");
    //}
}