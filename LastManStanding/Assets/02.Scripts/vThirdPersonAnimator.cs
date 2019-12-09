using UnityEngine;
using System.Collections;

namespace Invector.CharacterController
{
    public abstract class vThirdPersonAnimator : vThirdPersonMotor
    {


        public virtual void UpdateAnimator()
        {
            if (animator == null || !animator.enabled) return;

            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("GroundDistance", groundDistance);


            if (!isGrounded)
                animator.SetFloat("VerticalVelocity", verticalVelocity);

            if (isStrafing)
            {
                // strafe movement get the input 1 or -1
                animator.SetFloat("InputHorizontal", direction, 0.1f, Time.deltaTime);
            }

            // fre movement get the input 0 to 1
            animator.SetFloat("InputVertical", speed, 0.1f, Time.deltaTime);
        }

        public void OnAnimatorMove()
        {
            //we implement this function to override the default root motion.
            //this allows us to modify the positional speed before it's applied.
            if(keepcrouch)
            {
                if (isGrounded)
                {
                    transform.rotation = animator.rootRotation;
                    if (speed > 0.5 && speed <= 1f)
                        ControlSpeed(freeWalkSpeed);
                    else
                        ControlSpeed(freeRunningSpeed);
                }
            }
            
        }
    }
}