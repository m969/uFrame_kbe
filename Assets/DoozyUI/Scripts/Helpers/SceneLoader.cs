// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using UnityEngine;

#if (UNITY_5_1 == false && UNITY_5_2 == false)
using UnityEngine.SceneManagement;
#endif

#if dUI_EnergyBarToolkit
using EnergyBarToolkit;
#endif


namespace DoozyUI
{
    public class SceneLoader : QuickEngine.Common.Singleton<SceneLoader>
    {
        #region Context Menu
#if UNITY_EDITOR
        [UnityEditor.MenuItem(DUI.TOOLS_MENU_SCENE_LOADER, false, DUI.MENU_PRIORITY_SCENE_LOADER)]
        [UnityEditor.MenuItem(DUI.GAMEOBJECT_MENU_SCENE_LOADER, false, DUI.MENU_PRIORITY_SCENE_LOADER)]
        static void CreateSceneLoader(UnityEditor.MenuCommand menuCommand)
        {
            if (FindObjectOfType<SceneLoader>() != null)
            {
                Debug.Log("[Scene Loader] Cannot add another Scene Loader to this Scene because you don't need more than one.");
                UnityEditor.Selection.activeObject = FindObjectOfType<SceneLoader>();
                return;
            }

            GameObject go = new GameObject("SceneLoader", typeof(SceneLoader));
            UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            UnityEditor.Selection.activeObject = go;
        }
#endif

        #endregion

        public const string DEFAULT_LOAD_SCENE_ASYNC_SCENE_NAME = "LoadSceneAsync_Name_";
        public const string DEFAULT_LOAD_SCENE_ASYNC_SCENE_BUILD_INDEX = "LoadSceneAsync_ID_";
        public const string DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_NAME = "LoadSceneAdditiveAsync_Name_";
        public const string DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_BUILD_INDEX = "LoadSceneAdditiveAsync_ID_";
        public const string DEFAULT_UNLOAD_SCENE_SCENE_NAME = "UnloadScene_Name_";
        public const string DEFAULT_UNLOAD_SCENE_SCENE_BUILD_INDEX = "UnloadScene_ID_";
        public const string DEFAULT_UNLOAD_LEVEL = "UnloadLevel_";
        public const string DEFAULT_LOAD_LEVEL = "LoadLevel_";
        public const string DEFAULT_LEVEL_SCENE_NAME = "Level_";
        public const string DEFAULT_LEVEL_LOADED = "LevelLoaded";

#if dUI_EnergyBarToolkit
        public List<EnergyBar> energyBars = new List<EnergyBar>();
#endif

        public string command_LoadSceneAsync_SceneName = DEFAULT_LOAD_SCENE_ASYNC_SCENE_NAME;
        public string command_LoadSceneAsync_SceneBuildIndex = DEFAULT_LOAD_SCENE_ASYNC_SCENE_BUILD_INDEX;
        public string command_LoadSceneAdditiveAsync_SceneName = DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_NAME;
        public string command_LoadSceneAdditiveAsync_SceneBuildIndex = DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_BUILD_INDEX;
        public string command_UnloadScene_SceneName = DEFAULT_UNLOAD_SCENE_SCENE_NAME;
        public string command_UnloadScene_SceneBuildIndex = DEFAULT_UNLOAD_SCENE_SCENE_BUILD_INDEX;
        public string command_LoadLevel = DEFAULT_LOAD_LEVEL;
        public string command_UnloadLevel = DEFAULT_UNLOAD_LEVEL;
        public string levelSceneName = DEFAULT_LEVEL_SCENE_NAME;
        public string levelLoadedGameEvent = DEFAULT_LEVEL_LOADED;

        private AsyncOperation async = null; // When assigned, load is in progress.
        private int sceneBuildIndex = -1;
        private string sceneName = "";

        void OnEnable()
        {
            RegisterSceneLoader();
        }

        void OnDisable()
        {
            UnregisterSceneLoader();
        }

        void Update()
        {
            CheckIfLevelLoaded();
            UpdateEnergyBars();
        }

        void CheckIfLevelLoaded()
        {
            if (async != null)
            {
                if (async.isDone)
                {
                    UIManager.SendGameEvent(levelLoadedGameEvent);
                    async = null;
                }
            }
        }

        #region UPdate EnergyBars
        void UpdateEnergyBars()
        {
#if dUI_EnergyBarToolkit
            // If if we are loading a level and we have energyBars linkd then we update their values
            if (async != null && energyBars != null && energyBars.Count > 0)
            {
                for (int i = 0; i < energyBars.Count; i++)
                {
                    if (energyBars[i] != null)
                        energyBars[i].SetValueF(async.progress);
                }
            }
#endif
        }
        #endregion

        #region Register and Unregister SceneLoader
        void RegisterSceneLoader()
        {
            if (UIManager.sceneLoader == null)
            {
                UIManager.sceneLoader = this;
            }
            else
            {
                gameObject.name = "SceneLoader_DUPLICATE";
                Debug.LogWarning("[DoozyUI] An instance of a SceneLoader is already registared to the UIManager. There should never be more than 1 SceneLoader in the Hierarcy! This gameObject has been renamed to 'SceneLoader_DUPLICATE'. Look for it in the Hierarchy, while 'Play Mode', then select it, exit 'Play Mode' and then delete it.");
            }
        }

        void UnregisterSceneLoader()
        {
            if (UIManager.sceneLoader != null)
            {
                UIManager.sceneLoader = null;
            }
        }
        #endregion

        #region SceneLoader for Unity 5.1 and Unity 5.2
#if (UNITY_5_1 || UNITY_5_2)
        #region Event Listeners - uses Application.LoadLevel...

        public void OnGameEvent(GameEventMessage m)
        {
            if (m.command.Contains(command_LoadSceneAsync_SceneName))
            {
                sceneName = m.command.Split('_')[2];
                LoadSceneAsync(sceneName);

            }
            else if (m.command.Contains(command_LoadSceneAsync_SceneBuildIndex))
            {
                sceneBuildIndex = int.Parse(m.command.Split('_')[2]);
                LoadSceneAsync(sceneBuildIndex);
            }
            else if (m.command.Contains(command_LoadSceneAdditiveAsync_SceneName))
            {
                sceneName = m.command.Split('_')[2];
                LoadLevelAdditiveAsync(sceneName);
            }
            else if (m.command.Contains(command_LoadSceneAdditiveAsync_SceneBuildIndex))
            {
                sceneBuildIndex = int.Parse(m.command.Split('_')[2]);
                LoadLevelAdditiveAsync(sceneBuildIndex);
            }
            else if (m.command.Contains(command_LoadLevel))   //SHORTCUT VARIANT - we just call LoadLevel_{LevelNumber} and we load additive async the level data
            {
                sceneName = levelSceneName + m.command.Split('_')[1];
                async = Application.LoadLevelAdditiveAsync(sceneName);
            }
        }

        public void LoadSceneAsync(string sceneName)
        {
            async = Application.LoadLevelAsync(sceneName);
        }

        public void LoadSceneAsync(int sceneBuildIndex)
        {
            async = Application.LoadLevelAsync(sceneBuildIndex);
        }

        public void LoadLevelAdditiveAsync(string sceneName)
        {
            async = Application.LoadLevelAdditiveAsync(sceneName);
        }

        public void LoadLevelAdditiveAsync(int sceneBuildIndex)
        {
            async = Application.LoadLevelAdditiveAsync(sceneBuildIndex);
        }

        public void LoadLevel(int levelNumber)
        {
            sceneName = levelSceneName + levelNumber;
            async = Application.LoadLevelAdditiveAsync(sceneName);
        }
        #endregion
#endif
        #endregion

        #region SceneLoader for Unity 5.3 and up
#if (UNITY_5_1 == false && UNITY_5_2 == false)
        #region Event Listeners - uses SceneManager.LoadScene...

        public void OnGameEvent(string gameEvent)
        {
            if (gameEvent.Contains(command_LoadSceneAsync_SceneName))
            {
                sceneName = gameEvent.Split('_')[2];
                LoadSceneAsync(sceneName);
            }
            else if (gameEvent.Contains(command_LoadSceneAsync_SceneBuildIndex))
            {
                sceneBuildIndex = int.Parse(gameEvent.Split('_')[2]);
                LoadSceneAsync(sceneBuildIndex);
            }
            else if (gameEvent.Contains(command_LoadSceneAdditiveAsync_SceneName))
            {
                sceneName = gameEvent.Split('_')[2];
                LoadLevelAdditiveAsync(sceneName);
            }
            else if (gameEvent.Contains(command_LoadSceneAdditiveAsync_SceneBuildIndex))
            {
                sceneBuildIndex = int.Parse(gameEvent.Split('_')[2]);
                LoadLevelAdditiveAsync(sceneBuildIndex);
            }
            else if (gameEvent.Contains(command_UnloadScene_SceneName))
            {
                sceneName = gameEvent.Split('_')[2];
                UnloadScene(sceneName);
            }
            else if (gameEvent.Contains(command_UnloadScene_SceneBuildIndex))
            {
                sceneBuildIndex = int.Parse(gameEvent.Split('_')[2]);
                UnloadScene(sceneBuildIndex);
            }
            else if (gameEvent.Contains(command_LoadLevel))  //SHORTCUT VARIANT - we just call LoadLevel_{LevelNumber} and we load additive async the level data
            {
                sceneName = levelSceneName + gameEvent.Split('_')[1];
                async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else if (gameEvent.Contains(command_UnloadLevel))    ///SHORTCUT VARIANT - we just call UnloadLevel_{LevelNumber} and we unload the level data
            {
                sceneName = levelSceneName + gameEvent.Split('_')[1];
#if UNITY_5_5_OR_NEWER
                SceneManager.UnloadSceneAsync(sceneName);
#else
                SceneManager.UnloadScene(sceneName);
#endif
            }
        }

        public void LoadSceneAsync(string sceneName)
        {
            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void LoadSceneAsync(int sceneBuildIndex)
        {
            async = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
        }

        public void LoadLevelAdditiveAsync(string sceneName)
        {
            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void LoadLevelAdditiveAsync(int sceneBuildIndex)
        {
            async = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
        }

        public void UnloadScene(string sceneName)
        {
#if UNITY_5_5_OR_NEWER
            SceneManager.UnloadSceneAsync(sceneName);
#else
            SceneManager.UnloadScene(sceneName);
#endif
        }

        public void UnloadScene(int sceneBuildIndex)
        {
#if UNITY_5_5_OR_NEWER
            SceneManager.UnloadSceneAsync(sceneBuildIndex);
#else
            SceneManager.UnloadScene(sceneBuildIndex);
#endif
        }

        public void LoadLevel(int levelNumber)
        {
            sceneName = levelSceneName + levelNumber;
            async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadLevel(int levelNumber)
        {
            sceneName = levelSceneName + levelNumber;
#if UNITY_5_5_OR_NEWER
            SceneManager.UnloadSceneAsync(sceneName);
#else
            SceneManager.UnloadScene(sceneName);
#endif
        }

        #endregion
#endif
        #endregion
    }
}

