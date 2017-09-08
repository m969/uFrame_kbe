using uFrame.IOC;

namespace KbeBalls {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using KBEngine;
    
    
    public class EntityCommonView : EntityCommonViewBase
    {
        public override void modelIDChanged(Int32 arg1) {
            var worldViewService = uFrameKernel.Instance.GetComponentInChildren<WorldViewService>();
            Sprite sp = null;

            if (this.transform.gameObject.name.ToLower().Contains("avatar"))
            {
                sp = worldViewService.avatarSprites[arg1];
            }
            else if (this.transform.gameObject.name.ToLower().Contains("food"))
            {
                sp = worldViewService.foodsSprites[arg1];
            }
            else if (this.transform.gameObject.name.ToLower().Contains("smash"))
            {
                sp = worldViewService.smashsSprites[arg1];
            }

            GetComponent<SpriteRenderer>().sprite = sp;
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as EntityCommonViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.EntityCommon to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        private bool isMouseDown = false;

        public bool isPlayer = false;
        public bool isAvatar = false;

        private Vector3 _position = Vector3.zero;
        private Vector3 _eulerAngles = Vector3.zero;

        public Vector3 destPosition = Vector3.zero;
        public Vector3 destDirection = Vector3.zero;

        private float _speed = 0f;

        public bool isControlled = false;

        public bool entityEnabled = true;

        private Vector3 lastMoveDir = Vector3.zero;

        private static GameObject directionObj = null;
        private static GameObject directionObj_sprite = null;

        public Vector3 position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;

                if (gameObject != null)
                    gameObject.transform.position = _position;
            }
        }

        public Vector3 eulerAngles
        {
            get
            {
                return _eulerAngles;
            }

            set
            {
                _eulerAngles = value;

                if (directionObj != null)
                {
                    directionObj.transform.eulerAngles = _eulerAngles;
                }
            }
        }

        public Quaternion rotation
        {
            get
            {
                return Quaternion.Euler(_eulerAngles);
            }

            set
            {
                eulerAngles = value.eulerAngles;
            }
        }

        public float speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        public void entityEnable()
        {
            entityEnabled = true;
        }

        public void entityDisable()
        {
            entityEnabled = false;
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

            if (targetPos.x > WorldViewService.GAME_MAP_SIZE - x_half)
                targetPos.x = WorldViewService.GAME_MAP_SIZE - x_half;

            if (targetPos.y < y_half)
                targetPos.y = y_half;

            if (targetPos.y > WorldViewService.GAME_MAP_SIZE - y_half)
                targetPos.y = WorldViewService.GAME_MAP_SIZE - y_half;

            UpdateDirection(targetPos);
            this.transform.position = targetPos;
        }

        void FixedUpdate()
        {
            if (!isAvatar)
                return;

            if (!entityEnabled || KBEngineApp.app == null)
                return;

            if (isPlayer == isControlled)
                return;

            KBEngine.Event.fireIn("updatePlayer", gameObject.transform.position.x,
            gameObject.transform.position.z, gameObject.transform.position.y, gameObject.transform.rotation.eulerAngles.y);
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
            rotation = Quaternion.Slerp(rotation, Quaternion.Euler(0, 0, targetAngle - 180.0f), 8f * Time.deltaTime);
        }

        public override void Update()
        {
            //base.Update();

            if (!isAvatar)
                return;

            if (!entityEnabled)
            {
                position = destPosition;
                return;
            }

            float deltaSpeed = (speed * Time.deltaTime);

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
                        new Vector3(position.x, position.y, 0f));

                if (dist > 0.01f)
                {
                    Vector3 pos = position;

                    Vector3 movement = destPosition - pos;
                    movement.z = 0f;
                    movement.Normalize();

                    movement *= deltaSpeed;

                    if (dist > deltaSpeed || movement.magnitude > deltaSpeed)
                        pos += movement;
                    else
                        pos = destPosition;

                    position = pos;
                }
                else
                {
                    position = destPosition;
                }
            }
        }
    }
}
