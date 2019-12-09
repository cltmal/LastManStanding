using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    // 순찰 지점들을 저장하기 위한 List 타입 변수
    public List<Transform> wayPoints;
    Animator stop;
    Animator aiDie;
    NavMeshAgent stopNav;
    private NavMeshAgent agent;
    bool Die;
    // 다음 순찰 지점의 배열의 Index
    public int nextIdx = 0;

    // NavMeshAgent 컴포넌트를 저장할 변수
    [PunRPC]
    void Start()
    {
        stop = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        aiDie = GetComponent<Animator>();
        Die = false;
        // autobraking = 목적지에 가까워지면 속도가 느려지고 다시 출발하면 서서히 빨라짐, So, false;
        //agent.autoBraking = false;
        var group = GameObject.Find("WayPointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
            nextIdx = Random.Range(0, wayPoints.Count);
        }
        StartCoroutine(MoveWayPoint());
    }
    [PunRPC]
    IEnumerator MoveWayPoint()
    {
        // 최단 거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음
        if (agent.isPathStale)
            yield return null;

        while (true)
        {
            stop.SetFloat("InputVertical", 1f);
            // 다음 목적지를 wayPoints 배열에서 추출한 위치로 다음 목적지를 지정
            if (!Die)
            {
                agent.destination = wayPoints[nextIdx].position;
                agent.isStopped = false;                
            }
            yield return new WaitForSeconds(Random.Range(0f, 30f));
        }
    }
    [PunRPC]
    void Update()
    {
        // NayMeshAgent가 이동하고 있고 목적지에 도착했는지 여부를 계산
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            agent.isStopped = true;
            stop.SetFloat("InputVertical", 0f);
            nextIdx = Random.Range(0, wayPoints.Count);
        }
    }
    [PunRPC]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "toe" && !Die)
        {
            aiDie.SetTrigger("AiDie");
            Die = true;
            agent.isStopped = true;
        }
    }
}
