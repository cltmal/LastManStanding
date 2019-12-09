using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Coinspawn : MonoBehaviour
{
    public GameObject goldCoins;
    public GameObject goldCoinsInstant;
    bool isCoSpawn = false;
    bool istime = false;
    PhotonView photonView;



    Text _text;
    public float timer = 0;

    [PunRPC]
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            goldCoinsInstant = PhotonNetwork.Instantiate(goldCoins.name, transform.position, Quaternion.identity);
            Debug.Log("최초 코인 생성");
        }
        _text = GameObject.Find("TimeText").GetComponent<Text>();

    }
    [PunRPC]
    private void Update()
    {
        if (goldCoinsInstant == null)
        {
            //timer = 60;
            if (isCoSpawn == false)
            {
                isCoSpawn = true;
                Debug.Log("코인 코루틴 시작");
                StartCoroutine(coRespawnCoin(60f));
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            timer = 60;
        }
        //Debug.Log(_text.text);
        if (goldCoinsInstant == null)
        {
            _text.text = "코인생성까지 남은시간  : " + string.Format("{0:D2}", (int)timer);
        }
        else
            _text.text = "코인전쟁을 시작하지!!";
    }
    [PunRPC]
    IEnumerator coRespawnCoin(float sec)
    {
        timer = 60;
        yield return new WaitForSeconds(sec);
        //PhotonNetwork.Instantiate(goldCoins.name, transform.position, transform.rotation);
        if (PhotonNetwork.IsMasterClient)
            goldCoinsInstant = PhotonNetwork.Instantiate(goldCoins.name, transform.position, transform.rotation);
        isCoSpawn = false;
    }
}