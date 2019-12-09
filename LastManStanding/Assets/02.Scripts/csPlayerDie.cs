using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector.CharacterController
{
    public class csPlayerDie : MonoBehaviourPun
    {
        Animator pd;
        CapsuleCollider pc;


        bool Die;

        private void Start()
        {
            pd = GetComponent<Animator>();
            pc = GameObject.Find("VBOT_:RightToe_End_end").GetComponent<CapsuleCollider>();
            Die = false;
        }
        public void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.tag == "toe" && this.gameObject != other.gameObject && !Die)
                {
                    Die = true;
                    pd.SetTrigger("Die");
                    PhotonNetwork.Destroy(this.gameObject);
                    PhotonNetwork.LoadLevel("Die");
                }
            }
        }
    }
}