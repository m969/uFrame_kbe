// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(SceneLoader), true)]
    [DisallowMultipleComponent]
    public class SceneLoaderEditor : QEditor
    {
        SceneLoader sceneLoader { get { return (SceneLoader)target; } }

        SerializedProperty
            command_LoadSceneAsync_SceneName,
            command_LoadSceneAsync_SceneBuildIndex,
            command_LoadSceneAdditiveAsync_SceneName,
            command_LoadSceneAdditiveAsync_SceneBuildIndex,
            command_UnloadScene_SceneName,
            command_UnloadScene_SceneBuildIndex,
            command_UnloadLevel,
            command_LoadLevel,
            levelSceneName,
            levelLoadedGameEvent;

        void SerializedObjectFindProperties()
        {
            command_LoadSceneAsync_SceneName = serializedObject.FindProperty("command_LoadSceneAsync_SceneName");
            command_LoadSceneAsync_SceneBuildIndex = serializedObject.FindProperty("command_LoadSceneAsync_SceneBuildIndex");
            command_LoadSceneAdditiveAsync_SceneName = serializedObject.FindProperty("command_LoadSceneAdditiveAsync_SceneName");
            command_LoadSceneAdditiveAsync_SceneBuildIndex = serializedObject.FindProperty("command_LoadSceneAdditiveAsync_SceneBuildIndex");
            command_UnloadScene_SceneName = serializedObject.FindProperty("command_UnloadScene_SceneName");
            command_UnloadScene_SceneBuildIndex = serializedObject.FindProperty("command_UnloadScene_SceneBuildIndex");
            command_UnloadLevel = serializedObject.FindProperty("command_UnloadLevel");
            command_LoadLevel = serializedObject.FindProperty("command_LoadLevel");
            levelSceneName = serializedObject.FindProperty("levelSceneName");
            levelLoadedGameEvent = serializedObject.FindProperty("levelLoadedGameEvent");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>();
            infoMessage.Add("ComponentInfo", new InfoMessage() { message = "Here you can customize the game event commands that trigger different methods for scene loading.", type = InfoMessageType.Help, show = new AnimBool(true, Repaint) });
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerSceneLoader.texture, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            DrawEnergyBars();
            DrawCustomGameEvents();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawEnergyBars()
        {
#if dUI_EnergyBarToolkit
            QUI.DrawTexture(DUIResources.barEnergyBars.texture, WIDTH_420, 18);
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
            QUI.Space(SPACE_2);
            if (sceneLoader.energyBars == null || sceneLoader.energyBars.Count == 0)
            {
                QUI.BeginHorizontal(WIDTH_420);
                {
                    QUI.Label("No Energy Bars referenced... Click [+] to start...", WIDTH_420 - 27);
                    QUI.BeginVertical(18);
                    {
                        QUI.Space(-1);
                        if (QUI.ButtonPlus())
                        {
                            Undo.RecordObject(sceneLoader, "Added Energy Bar");
                            sceneLoader.energyBars = new List<EnergyBar> { null };
                        }
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
                QUI.Space(SPACE_8);
                return;
            }

            QUI.BeginVertical(WIDTH_420);
            {
                for (int i = 0; i < sceneLoader.energyBars.Count; i++)
                {
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Space(-4);
                        QUI.Label(" " + i.ToString(), 22);
                        sceneLoader.energyBars[i] = (EnergyBar) QUI.ObjectField(sceneLoader.energyBars[i], typeof(EnergyBar), true, WIDTH_420 - 18 - 18 - 13);
                        QUI.BeginVertical(18);
                        {
                            QUI.Space(-1);
                            if (QUI.ButtonMinus())
                            {
                                Undo.RecordObject(sceneLoader, "Removed Energy Bar");
                                sceneLoader.energyBars.RemoveAt(i);
                            }
                        }
                        QUI.EndVertical();
                    }
                    QUI.EndHorizontal();
                }

                QUI.BeginHorizontal(WIDTH_420);
                {
                    QUI.Space(WIDTH_420 - 23);
                    QUI.BeginVertical(18);
                    {
                        QUI.Space(-1);
                        if (QUI.ButtonPlus())
                        {
                            Undo.RecordObject(sceneLoader, "Added Energy Bar");
                            sceneLoader.energyBars.Add(null);
                        }
                    }
                    QUI.EndVertical();
                    QUI.Space(2);
                }
                QUI.EndHorizontal();
            }
            QUI.EndVertical();
            QUI.Space(SPACE_8);
            RestoreColors();
#endif
        }

        void DrawCustomGameEvents()
        {
            QUI.DrawTexture(DUIResources.barCustomGameEvents.texture, WIDTH_420, 18);
            //DrawInfoMessage("ComponentInfo", WIDTH_420);
            SaveColors();
            QUI.SetGUIBackgroundColor(DUIColors.PurpleLight.Color);
            QUI.Space(SPACE_2);
            QUI.Label("LoadLevel Async", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_LoadSceneAsync_SceneName, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_LoadSceneAsync_SceneName.stringValue = SceneLoader.DEFAULT_LOAD_SCENE_ASYNC_SCENE_NAME;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by build index", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_LoadSceneAsync_SceneBuildIndex, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_LoadSceneAsync_SceneBuildIndex.stringValue = SceneLoader.DEFAULT_LOAD_SCENE_ASYNC_SCENE_BUILD_INDEX;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_8);
            QUI.Label("LoadLevel Additive Async", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_LoadSceneAdditiveAsync_SceneName, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_LoadSceneAdditiveAsync_SceneName.stringValue = SceneLoader.DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_NAME;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by build index", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_LoadSceneAdditiveAsync_SceneBuildIndex, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_LoadSceneAdditiveAsync_SceneBuildIndex.stringValue = SceneLoader.DEFAULT_LOAD_SCENE_ADDITIVE_ASYNC_SCENE_BUILD_INDEX;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_8);
            QUI.Label("Unload Scene", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_UnloadScene_SceneName, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_UnloadScene_SceneName.stringValue = SceneLoader.DEFAULT_UNLOAD_SCENE_SCENE_NAME;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("by build index", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_UnloadScene_SceneBuildIndex, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_UnloadScene_SceneBuildIndex.stringValue = SceneLoader.DEFAULT_UNLOAD_SCENE_SCENE_BUILD_INDEX;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_8);
            QUI.Label("Load / Unload Level", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("load level", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_LoadLevel, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_LoadLevel.stringValue = SceneLoader.DEFAULT_LOAD_LEVEL;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("unload level", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(command_UnloadLevel, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        command_UnloadLevel.stringValue = SceneLoader.DEFAULT_UNLOAD_LEVEL;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
            QUI.Label("Level Scene Name (naming convention)", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("scene name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(levelSceneName, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        levelSceneName.stringValue = SceneLoader.DEFAULT_LEVEL_SCENE_NAME;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_8);
            QUI.Label("Game Event sent after a scene was loaded", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), WIDTH_420);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(SPACE_16);
                QUI.Label("game event", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 90);
                QUI.PropertyField(levelLoadedGameEvent, 280);
                QUI.BeginVertical(22);
                {
                    QUI.Space(-1);
                    if (QUI.ButtonReset())
                    {
                        levelLoadedGameEvent.stringValue = SceneLoader.DEFAULT_LEVEL_LOADED;
                    }
                }
                QUI.EndVertical();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }

    }
}
