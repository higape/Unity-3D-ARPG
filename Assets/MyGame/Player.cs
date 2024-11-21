using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class Player : Character, IHit
    {
        public float invincibilityTime;
        public float cameraRotationSpeed = 3.0f;
        public GameObject body;
        public GameObject cameraFocus;
        public Weapon weapon;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private new Rigidbody rigidbody;

        public float InvincibilityTimeCount { get; private set; }

        void Start()
        {
            Input.imeCompositionMode = IMECompositionMode.Off;
            if (weapon != null)
                weapon.TriggerEnterCallback = ProcessAttack;
        }

        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Hit(new AttackData() { strength = attack });
            }
            UpdateCamera();
            if (InvincibilityTimeCount > 0)
            {
                InvincibilityTimeCount -= Time.deltaTime;
                anyChanged.Invoke(this);
            }
        }

        // void OnTriggerEnter(Collider other)
        // {
        //     Debug.Log("Player收到消息");
        // }

        private void UpdateCamera()
        {
            //摄像机左右调整
            float mouseX = Input.GetAxis("Mouse X") * cameraRotationSpeed;
            Camera.main.transform.RotateAround(cameraFocus.transform.position, Vector3.up, mouseX);

            //摄像机上下调整
            float mouseY = Input.GetAxis("Mouse Y") * cameraRotationSpeed;
            if (Mathf.Abs(mouseY) > 0.2f)
            {
                Camera.main.transform.RotateAround(
                    cameraFocus.transform.position,
                    Camera.main.transform.right,
                    -mouseY
                );
            }

            //摄像机远近调整
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel") * cameraRotationSpeed * 10;
            Camera.main.fieldOfView = Mathf.Clamp(
                Camera.main.fieldOfView - mouseScrollWheel,
                30,
                50
            );
        }

        public void UpdateMove()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
            {
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 cameraRight = Camera.main.transform.right;

                // 将y轴设为0，保持在水平面上移动
                cameraForward.y = 0;
                cameraRight.y = 0;

                cameraForward.Normalize();
                cameraRight.Normalize();

                Vector3 moveDirection =
                    cameraForward * verticalInput + cameraRight * horizontalInput;
                moveDirection.Normalize();
                Quaternion r = Quaternion.LookRotation(moveDirection);
                body.transform.rotation = Quaternion.RotateTowards(
                    body.transform.rotation,
                    r,
                    TurnSpeed * Time.deltaTime
                );

                if (body.transform.rotation == r)
                {
                    Vector3 m = moveSpeed * moveDirection;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(AnimationHash.IsRunning, true);
                        m *= 2;
                    }
                    else
                    {
                        animator.SetBool(AnimationHash.IsRunning, false);
                    }

                    rigidbody.velocity = new Vector3(m.x, rigidbody.velocity.y, m.z);
                }

                animator.SetBool(AnimationHash.IsWalking, true);
            }
            else
            {
                animator.SetBool(AnimationHash.IsWalking, false);
                animator.SetBool(AnimationHash.IsRunning, false);
            }
        }

        public void UpdateAttack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (animator.GetBool(AnimationHash.IsAttacking))
                {
                    animator.SetBool(AnimationHash.IsAttackContinue, true);
                }
                else
                {
                    animator.SetBool(AnimationHash.IsAttacking, true);
                }
            }
        }

        public void UpdateDefend()
        {
            animator.SetBool(AnimationHash.IsDefending, Input.GetButton("Fire2"));
        }

        public void Hit(AttackData data)
        {
            if (IsAlive && InvincibilityTimeCount <= 0)
            {
                if (animator.GetBool(AnimationHash.IsDefending))
                {
                    Life -= Mathf.Max(data.strength - defence * 2, 0);
                }
                else
                {
                    Life -= Mathf.Max(data.strength - defence, 0);
                    if (IsAlive)
                    {
                        animator.SetTrigger(AnimationHash.HitTrigger);
                    }
                }

                if (!IsAlive)
                {
                    animator.SetBool(AnimationHash.IsDead, true);
                    StartCoroutine(Revive());
                }
            }
        }

        private IEnumerator Revive()
        {
            yield return new WaitForSeconds(3);
            Life = MaxLife;
            InvincibilityTimeCount = invincibilityTime;
            animator.SetBool(AnimationHash.IsDead, false);
        }

        public void ProcessAttack(Collider other)
        {
            if (other.TryGetComponent<IHit>(out var hit))
            {
                hit.Hit(new AttackData() { strength = attack });
            }
        }
    }
}
