namespace KBEngine
{
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using uFrame.MVVM.Views;
    using UniRx;

    public class AvatarView : AvatarViewBase
    {
        private bool isMouseDown = false;

        public bool isPlayer = false;
        public bool isAvatar = false;

        private Vector3 _position = Vector3.zero;
        private Vector3 _eulerAngles = Vector3.zero;
        private Vector3 _scale = Vector3.zero;

        public Vector3 destPosition = Vector3.zero;
        public Vector3 destDirection = Vector3.zero;

        private float _speed = 1.5f;

        public string entity_name = "";

        public bool isOnGround = true;

        public bool isControlled = false;

        public bool entityEnabled = true;

        private Vector3 lastMoveDir = Vector3.zero;

        private static GameObject directionObj = null;
        private static GameObject directionObj_sprite = null;


        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model)
        {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as FoodViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }

        public override void Bind()
        {
            base.Bind();
            // Use this.Food to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void Update()
        {
            base.Update();
            float deltaSpeed = (_speed * Time.deltaTime);
            if (isPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isMouseDown = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isMouseDown = false;
                }

                if (isMouseDown)
                {
                    Vector3 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
                    movement.z = 0.0f;

                    if (movement.magnitude <= 1.0)
                    {
                        lastMoveDir = Vector3.zero;
                        return;
                    }

                    movement.Normalize();
                    lastMoveDir = movement;
                    updatePos(this.transform.position + (movement * deltaSpeed));
                }
                else
                {
                    if (lastMoveDir != Vector3.zero)
                        updatePos(this.transform.position + (lastMoveDir * deltaSpeed));
                }
            }
            else
            {
                // 如果是其他玩家移动
                float dist = Vector3.Distance(new Vector3(destPosition.x, destPosition.y, 0f),
                        new Vector3(transform.position.x, transform.position.y, 0f));

                if (dist > 0.01f)
                {
                    Vector3 pos = transform.position;

                    Vector3 movement = destPosition - pos;
                    movement.z = 0f;
                    movement.Normalize();

                    movement *= deltaSpeed;

                    if (dist > deltaSpeed || movement.magnitude > deltaSpeed)
                        pos += movement;
                    else
                        pos = destPosition;

                    transform.position = pos;
                }
                else
                {
                    transform.position = destPosition;
                }
            }
        }

        void FixedUpdate()
        {
            KBEngine.Event.fireIn("updatePlayer", gameObject.transform.position.x, gameObject.transform.position.z, gameObject.transform.position.y, gameObject.transform.rotation.eulerAngles.y);
        }

        void updatePos(Vector3 targetPos)
        {
            // 球身不允许超出边界
            Vector3 size = GetComponent<SpriteRenderer>().sprite.bounds.size;
            size *= this.transform.localScale.x;

            float x_half = (size.x / 2);
            float y_half = (size.y / 2);

            if (targetPos.x < x_half)
                targetPos.x = x_half;

            if (targetPos.x > World.GAME_MAP_SIZE - x_half)
                targetPos.x = World.GAME_MAP_SIZE - x_half;

            if (targetPos.y < y_half)
                targetPos.y = y_half;

            if (targetPos.y > World.GAME_MAP_SIZE - y_half)
                targetPos.y = World.GAME_MAP_SIZE - y_half;

            UpdateDirection(targetPos);
            this.transform.position = targetPos;
        }

        void UpdateDirection(Vector3 targetPos)
        {
            if (directionObj == null)
            {
                directionObj = GameObject.Find("direction");
                if (directionObj)
                {
                    directionObj.transform.position = transform.position;
                    directionObj.transform.parent = transform;
                    directionObj_sprite = GameObject.Find("direction/sprite");
                }
            }

            // 更新距离
            if (targetPos == Vector3.zero)
            {
                if (directionObj_sprite)
                {
                    // 球身边界
                    Vector3 size = GetComponent<SpriteRenderer>().sprite.bounds.size;
                    size *= this.transform.localScale.x;

                    float x_half = (size.x / 2);

                    directionObj_sprite.transform.localPosition = new Vector3(-(x_half + 1.2f), directionObj_sprite.transform.localPosition.y,
                        directionObj_sprite.transform.localPosition.z);
                }

                return;
            }

            Vector3 dir = targetPos - transform.position;
            dir.z = 0f;
            dir = dir.normalized;

            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            directionObj.transform.eulerAngles = Quaternion.Slerp(Quaternion.Euler(directionObj.transform.eulerAngles), Quaternion.Euler(0, 0, targetAngle - 180.0f), 8f * Time.deltaTime).eulerAngles;
        }

        public override void levelChanged(byte arg1)
        {
            base.levelChanged(arg1);
        }

        public override void massChanged(int arg1)
        {
            base.massChanged(arg1);
        }

        public override void modelIDChanged(byte arg1)
        {
            base.modelIDChanged(arg1);
        }

        public override void modelScaleChanged(float arg1)
        {
            base.modelScaleChanged(arg1);
        }

        public override void moveSpeedChanged(float arg1)
        {
            Debug.Log("AvatarView:moveSpeedChanged " + arg1);
            base.moveSpeedChanged(arg1);
            _speed = arg1;
        }

        public override void nameChanged(string arg1)
        {
            base.nameChanged(arg1);
        }

        public override void stateChanged(sbyte arg1)
        {
            base.stateChanged(arg1);
        }
    }
}