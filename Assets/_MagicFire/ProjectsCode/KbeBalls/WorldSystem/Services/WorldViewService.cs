using uFrame.MVVM.Events;
using uFrame.MVVM.ViewModels;

namespace KbeBalls {
    using KbeBalls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using UniRx;
    using UnityEngine;
    using KBEngine;
    
    
    public class WorldViewService : WorldViewServiceBase {
        private UnityEngine.GameObject terrain = null;
        public UnityEngine.GameObject terrainPerfab;

        private UnityEngine.GameObject player = null;
        public UnityEngine.GameObject foodsPerfab;
        public UnityEngine.GameObject smashsPerfab;
        public UnityEngine.GameObject avatarPerfab;

        public Sprite[] avatarSprites = new Sprite[2];
        public Sprite[] foodsSprites = new Sprite[3];
        public Sprite[] smashsSprites = new Sprite[1];

        public static float GAME_MAP_SIZE = 0.0f;
        public static int ROOM_MAX_PLAYER = 0;
        public static int GAME_ROUND_TIME = 0;

        /// <summary>
        /// This method is invoked whenever the kernel is loading
        /// Since the kernel lives throughout the entire lifecycle  of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            // Use the line below to subscribe to events
            // this.OnEvent<MyEvent>().Subscribe(myEventInstance => { TODO });

            this.OnEvent<OnAvatarEnterWorldEvent>().ObserveOnMainThread().Subscribe(OnAvatarEnterWorld);

            this.OnEvent<onEnterWorldEvent>().ObserveOnMainThread().Subscribe(OnEnterWorld);
            this.OnEvent<onLeaveWorldEvent>().ObserveOnMainThread().Subscribe(OnLeaveWorld);
            this.OnEvent<setpositionEvent>().ObserveOnMainThread().Subscribe(SetPosition);
            this.OnEvent<setdirectionEvent>().ObserveOnMainThread().Subscribe(SetDirection);
            this.OnEvent<onControlledEvent>().ObserveOnMainThread().Subscribe(OnControlled);
            this.OnEvent<addSpaceGeometryMappingEvent>().ObserveOnMainThread().Subscribe(AddSpaceGeometryMapping);
            this.OnEvent<updatePositionEvent>().ObserveOnMainThread().Subscribe(UpdatePosition);
            this.OnEvent<onSetSpaceDataEvent>().ObserveOnMainThread().Subscribe(OnSetSpaceData);
            this.OnEvent<onDelSpaceDataEvent>().ObserveOnMainThread().Subscribe(OnDelSpaceData);
        }

        private void CreatePlayer()
        {
            if (player != null)
            {
                player.GetComponent<EntityCommonView>().entityEnable();
                return;
            }

            if (KBEngineApp.app.entity_type != "Avatar")
            {
                return;
            }

            KBEngine.Entity avatar = (KBEngine.Entity)KBEngineApp.app.player();
            if (avatar == null)
            {
                Debug.Log("wait create(palyer)!");
                return;
            }

            // 玩家默认在第0层，越小的应该越在下一层， 大的覆盖小的
            var layer = 0.0f;

            var cmd = new InstantiateViewCommand()
            {
                ViewModelObject = avatar as ViewModel,
                Prefab = avatarPerfab
            };
            this.Publish(cmd);
            player = cmd.Result.gameObject;

            var entity = player.GetComponent<EntityCommonView>();
            entity.entityDisable();
            avatar.renderObj = player;
            entity.isAvatar = true;
            entity.isPlayer = true;

            // 有必要设置一下，由于该接口由Update异步调用，有可能set_position等初始化信息已经先触发了
            // 那么如果不设置renderObj的位置和方向将为0
            SetPosition(new setpositionEvent() { Entity = avatar });
            SetDirection(new setdirectionEvent() { Entity = avatar });

            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
            Camera.main.transform.parent = player.transform;
        }

        private void OnAvatarEnterWorld(OnAvatarEnterWorldEvent evt)
        {
            var avatar = evt.Entity;
            if (!avatar.isPlayer())
                return;
            CreatePlayer();
        }

        private void OnEnterWorld(onEnterWorldEvent evt)
        {
            var entity = evt.Entity;
            if (entity.isPlayer())
            {
                CreatePlayer();
            }
            else
            {
                UnityEngine.GameObject entityPerfab = null;

                float layer = 0.0f;
                if (entity.className == "FoodViewModel")
                {
                    entityPerfab = foodsPerfab;
                    layer = 100.0f;
                }
                else if (entity.className == "SmashViewModel")
                {
                    layer = -9.0f;

                    // 粉碎永远都应该在所有avatar和粮食的上面一层
                    entityPerfab = smashsPerfab;
                }
                else
                {
                    entityPerfab = avatarPerfab;
                }

                var cmd = new InstantiateViewCommand()
                {
                    ViewModelObject = entity as ViewModel,
                    Prefab = entityPerfab
                };
                this.Publish(cmd);
                entity.renderObj = cmd.Result.gameObject;

                ((UnityEngine.GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
                SetPosition(new setpositionEvent() { Entity = entity });
                SetDirection(new setdirectionEvent() { Entity = entity });

                if (entity.className == "AvatarViewModel")
                {
                    ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>().isAvatar = true;
                }
            }
        }

        private void OnLeaveWorld(onLeaveWorldEvent evt)
        {
            var entity = evt.Entity;
            if (entity.renderObj == null)
                return;

            UnityEngine.GameObject.Destroy((UnityEngine.GameObject)entity.renderObj);
            entity.renderObj = null;
        }

        private void OnControlled(onControlledEvent evt)
        {
            var entity = evt.Entity;
            var isControlled = evt.isControlled;

            if (entity.renderObj == null)
                return;

            EntityCommonView EntityCommonView = ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>();
            EntityCommonView.isControlled = isControlled;
        }

        private void SetPosition(setpositionEvent evt)
        {
            var entity = evt.Entity;
            if (entity.renderObj == null)
                return;

            GameObject go = ((UnityEngine.GameObject)entity.renderObj);
            Vector3 currpos = new Vector3(entity.position.x, entity.position.z, go.transform.position.z);
            go.GetComponent<EntityCommonView>().destPosition = currpos;
            go.GetComponent<EntityCommonView>().position = currpos;
        }

        private void SetDirection(setdirectionEvent evt)
        {
            var entity = evt.Entity;
            if (entity.renderObj == null)
                return;

            ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>().destDirection =
                new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
        }

        private void AddSpaceGeometryMapping(addSpaceGeometryMappingEvent evt)
        {
            var respath = evt.Respath;

            if (player)
                player.GetComponent<EntityCommonView>().entityEnable();
        }

        private void OnSetSpaceData(onSetSpaceDataEvent evt)
        {
            if ("GAME_MAP_SIZE" == evt.Key)
                GAME_MAP_SIZE = float.Parse(evt.Value);
            else if ("ROOM_MAX_PLAYER" == evt.Key)
                ROOM_MAX_PLAYER = int.Parse(evt.Value);
            else if ("GAME_MAP_SIZE" == evt.Key)
                GAME_ROUND_TIME = int.Parse(evt.Value);
        }

        private void OnDelSpaceData(onDelSpaceDataEvent evt)
        {

        }

        private void UpdatePosition(updatePositionEvent evt)
        {
            var entity = evt.Entity;

            if (entity.renderObj == null)
                return;

            EntityCommonView EntityCommonView = ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>();
            GameObject go = ((UnityEngine.GameObject)entity.renderObj);
            EntityCommonView.destPosition = new Vector3(entity.position.x, entity.position.z, go.transform.position.z);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            KBEngine.Event.deregisterOut(this);
        }

    }
}
