using System;
using UnityEngine;
using UnityEngine.InputSystem;
using QFramework;

namespace ShootingDemo
{
    public class PlayerController : Shooting2DController
    {
        public float mSpeed;
        public float mJumpForce;
        private Rigidbody2D mRig;
        private PlayerControl mPlayerControl;
        private Trigger2DCheck mGroundCheck;
        private Gun mGun;

        private void Awake()
        {
            mRig = GetComponent<Rigidbody2D>();
            mPlayerControl = new PlayerControl();
            mGroundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
            mGun = transform.Find("Gun").GetComponent<Gun>();
        }

        private void OnEnable()
        {
            mPlayerControl.Player.Enable();
            mPlayerControl.Player.Jump.performed += OnJump;
            mPlayerControl.Player.Fire.performed += OnFire;
            mPlayerControl.Player.Reload.performed += OnReload;
            mPlayerControl.Player.ShiftGun.performed += OnShiftGun;
        }

        private void FixedUpdate()
        {
            float moveSignal = mPlayerControl.Player.Move.ReadValue<float>();
            mRig.velocity = new Vector2(moveSignal * mSpeed, mRig.velocity.y);

            if (moveSignal * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }

        private void OnDisable()
        {
            mPlayerControl.Player.Disable();
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (mGroundCheck.Triggered)
            {
                mRig.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
            }
        }

        private void OnFire(InputAction.CallbackContext ctx)
        {
            mGun.Shoot();
        }

        private void OnReload(InputAction.CallbackContext obj)
        {
            mGun.Reload();
        }

        private void OnShiftGun(InputAction.CallbackContext obj)
        {
            this.SendCommand<ShiftGunCommand>();
        }
    }
}
