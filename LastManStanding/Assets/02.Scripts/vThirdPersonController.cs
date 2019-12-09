using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        
        Transform tr;
        Animator dieAni;
        protected virtual void Start()
        {

#if !UNITY_EDITOR
                Cursor.visible = false;
#endif
            tr = GetComponent<Transform>();
            dieAni = GetComponent<Animator>();
        }

        public virtual void Sprint(bool value)
        {
                isSprinting = value;
        }


        public virtual void Jump()
        {
             // conditions to do this action
                bool jumpConditions = isGrounded && !isJumping;
                // return if jumpCondigions is false
                if (!jumpConditions) return;
                // trigger jump behaviour
                jumpCounter = jumpTimer;
                isJumping = true;
                // trigger jump animations            
                if (_rigidbody.velocity.magnitude < 1)
                    animator.CrossFadeInFixedTime("Jump", 0.1f);
                else
                    animator.CrossFadeInFixedTime("JumpMove", 0.2f);
         
        }
        //플레이어 따라오는 함수
        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
                var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
                targetRotation = transform.rotation;
        }
    }
}