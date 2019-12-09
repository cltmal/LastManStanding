using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class EnemySpwan : MonoBehaviourPun
{
    //몬스터가 출현할 위치를 담을 배열
    public Transform[] points;
    //몬스터 프리팹을 할당할 변수
    public GameObject monsterPrefab;
    

    // Use this for initialization
    void Start()
    {
        //Hierarchy View의 Spawn Point를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();
        CreateMonster();
    }
    [PunRPC]
    public void CreateMonster()
    {
        for(int i = 0; i < sShareValue.aiNum; i++)
        { 
            int idx = Random.Range(1, points.Length);
            PhotonNetwork.Instantiate(monsterPrefab.name, points[idx].position, points[idx].rotation);
            //Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
        }
    }
    
}