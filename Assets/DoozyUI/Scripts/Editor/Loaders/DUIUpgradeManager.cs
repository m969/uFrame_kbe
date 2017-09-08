// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DoozyUI
{
    [InitializeOnLoad]
    public class DUIUpgradeManager
    {
        static List<string> oldEditorFiles = new List<string>
        {
            "UpdateSortingLayerNameInspector.cs",
            "DoozyUIControlPanel.cs",
            "DoozyUIHelper.cs",
            "DoozyUIRedundancyCheck.cs",
            "DoozyUIResources.cs",
            "SceneLoaderInspector.cs",
            "UIButtonInspector.cs",
            "UIEffectInspector.cs",
            "UIElementInspector.cs",
            "UIManagerInspector.cs",
            "UINotificationInspector.cs",
            "UITriggerInspector.cs"
        };
        static List<string> oldScriptHelpers = new List<string>
        {
            "Singleton.cs",
            "UpdateSortingLayerName.cs",
            "FileHelper.cs"
        };
        static List<string> oldImages = new List<string>
        {
            "window_warning_play_mode.png",
            "doozyUI_background_1920x1080.png",
            "inspector_bar_button_disable.png",
            "inspector_bar_button_enable.png",
            "inspector_bar_button_landscape_disabled.png",
            "inspector_bar_button_landscape_enabled.png",
            "inspector_bar_button_portrait_disabled.png",
            "inspector_bar_button_portrait_enabled.png",
            "inspector_bar_disabled.png",
            "inspector_bar_ebt_disabled.png",
            "inspector_bar_ebt_enabled.png",
            "inspector_bar_enabled.png",
            "inspector_bar_enter_button_name.png",
            "inspector_bar_enter_game_event.png",
            "inspector_bar_general_info.png",
            "inspector_bar_hide_elements.png",
            "inspector_bar_ma_disabled.png",
            "inspector_bar_ma_enabled.png",
            "inspector_bar_nav_disabled.png",
            "inspector_bar_nav_enabled.png",
            "inspector_bar_navigation_disabled.png",
            "inspector_bar_om_disabled.png",
            "inspector_bar_om_enabled.png",
            "inspector_bar_pm_disabled.png",
            "inspector_bar_pm_enabled.png",
            "inspector_bar_pm_event_dispatcher.png",
            "inspector_bar_scene_loader.png",
            "inspector_bar_select_listener.png",
            "inspector_bar_send_game_events.png",
            "inspector_bar_show_elements.png",
            "inspector_bar_tmp_disabled.png",
            "inspector_bar_tmp_enabled.png",
            "inspector_bar_ui_button.png",
            "inspector_bar_ui_effect.png",
            "inspector_bar_ui_element.png",
            "inspector_bar_ui_manager.png",
            "inspector_bar_ui_notification.png",
            "inspector_bar_ui_trigger.png",
            "inspector_button_back.png",
            "inspector_button_checkbox_disabled.png",
            "inspector_button_checkbox_enabled.png",
            "inspector_button_unlink_from_notification.png",
            "inspector_icon_fade_disabled.png",
            "inspector_icon_fade_enabled.png",
            "inspector_icon_in_disabled.png",
            "inspector_icon_in_enabled.png",
            "inspector_icon_loop_disabled.png",
            "inspector_icon_loop_enabled.png",
            "inspector_icon_move_disabled.png",
            "inspector_icon_move_enabled.png",
            "inspector_icon_out_disabled.png",
            "inspector_icon_out_enabled.png",
            "inspector_icon_rotation_disabled.png",
            "inspector_icon_rotation_enabled.png",
            "inspector_icon_scale_disabled.png",
            "inspector_icon_scale_enabled.png",
            "inspector_label_fadeIn_disabled.png",
            "inspector_label_fadeIn_enabled.png",
            "inspector_label_fadeLoop_disabled.png",
            "inspector_label_fadeLoop_enabled.png",
            "inspector_label_fadeOut_disabled.png",
            "inspector_label_fadeOut_enabled.png",
            "inspector_label_highlighted_animations.png",
            "inspector_label_highlighted_animations_disabled.png",
            "inspector_label_in_animations.png",
            "inspector_label_in_animations_disabled.png",
            "inspector_label_loop_animations.png",
            "inspector_label_loop_animations_disabled.png",
            "inspector_label_moveIn_disabled.png",
            "inspector_label_moveIn_enabled.png",
            "inspector_label_moveLoop_disabled.png",
            "inspector_label_moveLoop_enabled.png",
            "inspector_label_moveOut_disabled.png",
            "inspector_label_moveOut_enabled.png",
            "inspector_label_movePunch_disabled.png",
            "inspector_label_movePunch_enabled.png",
            "inspector_label_normal_animations.png",
            "inspector_label_normal_animations_disabled.png",
            "inspector_label_on_click_animations.png",
            "inspector_label_on_click_animations_disabled.png",
            "inspector_label_out_animations.png",
            "inspector_label_out_animations_disabled.png",
            "inspector_label_rotateIn_disabled.png",
            "inspector_label_rotateIn_enabled.png",
            "inspector_label_rotateLoop_disabled.png",
            "inspector_label_rotateLoop_enabled.png",
            "inspector_label_rotateOut_disabled.png",
            "inspector_label_rotateOut_enabled.png",
            "inspector_label_rotatePunch_disabled.png",
            "inspector_label_rotatePunch_enabled.png",
            "inspector_label_scaleIn_disabled.png",
            "inspector_label_scaleIn_enabled.png",
            "inspector_label_scaleLoop_disabled.png",
            "inspector_label_scaleLoop_enabled.png",
            "inspector_label_scaleOut_disabled.png",
            "inspector_label_scaleOut_enabled.png",
            "inspector_label_scalePunch_disabled.png",
            "inspector_label_scalePunch_enabled.png",
            "inspector_logo_doozyUI.png",
            "inspector_logo_ebt.png",
            "inspector_message_linked_to_notification.png",
            "inspector_message_wait_compile.png",
            "inspector_minibar_green.png",
            "inspector_minibar_light_blue.png",
            "inspector_minibar_light_grey.png",
            "inspector_minibar_orange.png",
            "inspector_minibar_purple.png",
            "inspector_minibar_red.png",
            "NavigationSystem_icon.png",
            "PlaymakerEventDispatcher-icon.png",
            "SceneLoader-icon.png",
            "UiButton-icon.png",
            "UiEffect-icon.png",
            "UiElement-icon.png",
            "UiManager-icon.png",
            "UiNotification-icon.png",
            "UiTrigger-icon.png",
            "window_background_light_grey.png",
            "window_button_button_names.png",
            "window_button_button_sounds.png",
            "window_button_cancel_green.png",
            "window_button_ebt_disabled.png",
            "window_button_ebt_enabled.png",
            "window_button_element_names.png",
            "window_button_element_sounds.png",
            "window_button_ma_disabled.png",
            "window_button_ma_enabled.png",
            "window_button_nav_disabled.png",
            "window_button_nav_enabled.png",
            "window_button_om_disabled.png",
            "window_button_om_enabled.png",
            "window_button_online_documentation.png",
            "window_button_play.png",
            "window_button_pm_disabled.png",
            "window_button_pm_enabled.png",
            "window_button_quick_help.png",
            "window_button_reset_button_names.png",
            "window_button_reset_button_sounds.png",
            "window_button_reset_element_names.png",
            "window_button_reset_element_sounds.png",
            "window_button_reset_red.png",
            "window_button_tmp_disabled.png",
            "window_button_tmp_enabled.png",
            "window_button_upgrade_scene.png",
            "window_button_video_tutorials.png",
            "window_button_youtube.png",
            "window_header_doozy.png",
            "window_message_add_doozy.png",
            "window_message_wait_compile.png",
            "window_miniheader_edit_database.png",
            "window_miniheader_help_and_tutorials.png",
            "window_subheader_button_names.png",
            "window_subheader_button_sounds.png",
            "window_subheader_control_panel.png",
            "window_subheader_element_names.png",
            "window_subheader_element_sounds.png",
            "window_subheader_quick_help.png",
            "window_subheader_video_tutorials.png"
        };

        static DUIUpgradeManager()
        {
            EditorApplication.update += RunOnce;
        }

        static void RunOnce()
        {
            EditorApplication.update -= RunOnce;
            ExecuteGeneralUpgrade();
        }

        static void ExecuteGeneralUpgrade()
        {
            EditorUtility.ClearProgressBar();
#if dUI_SOURCE
            return;
#endif
            if (DUI.DUISettings.InternalSettings_ExecutedUpgrade) { return; }
            Debug.Log("[DoozyUI] [UpgradeManager] Starting automated upgrade...");
            bool databaseUpgraded = UpgradeDatabase();
            CleanFiles();
            Debug.Log("[DoozyUI] [UpgradeManager] Finished automated upgrade...");
            DUI.DUISettings.InternalSettings_ExecutedUpgrade = true;
            EditorUtility.ClearProgressBar();
            if (databaseUpgraded)
            {
                NotificationWindow.Ok("Automated Upgrade Manager",
                                      "DoozyUI's Automated Upgrade Manager has finished and your old database has been converted to the new format. " +
                                      "You need to open each scene that contains DoozyUI components and upgrade it manually. " +
                                      "You do that by opening the Upgrade Manager from the top bar" +
                                      "\n\n" +
                                      "Tools -> DoozyUI -> Upgrade Manager -> [Upgrade Current Scene]" +
                                      "\n\n" +
                                      "This will update all the relevant components. The process is not failproof and you may need to check and tweak some settings for each component in order to work as intended.");
            }
        }

        public static bool UpgradeDatabase()
        {
            EditorSceneManager.MarkAllScenesDirty();
            string oldDatabasePath = DUI.DOOZYUI_PATH + "/Data/";
            if (QuickEngine.IO.File.Exists(oldDatabasePath + "DoozyUI_Data.asset"))
            {
                Debug.Log("[DoozyUI] [UpgradeManager] Found old database system.");
                DoozyUI_Data oldDatabase = AssetDatabase.LoadAssetAtPath<DoozyUI_Data>(oldDatabasePath + "DoozyUI_Data.asset");
                if (oldDatabase == null)
                {
                    Debug.Log("[DoozyUI] [UpgradeManager] Something went wrong while retrieving the old database file from the AssetDatabase. Skipping database upgrade.");
                    return false;
                }
                Debug.Log("[DoozyUI] Started database conversion...");
                // Upgrading - Element Names Database
                if (oldDatabase.elementNames != null && oldDatabase.elementNames.Count > 0)
                {
                    if (!DUI.UIElementCategoryExists(DUI.DEFAULT_CATEGORY_NAME)) { DUI.CreateUIElementsCategory(DUI.DEFAULT_CATEGORY_NAME); }
                    for (int i = 0; i < oldDatabase.elementNames.Count; i++)
                    {
                        EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading - Element Names Database / " + oldDatabase.elementNames[i].elementName, (i + 1) / (oldDatabase.elementNames.Count));
                        if (DUI.UIElementNameExists(DUI.DEFAULT_CATEGORY_NAME, oldDatabase.elementNames[i].elementName)) { continue; }
                        DUI.AddUIElementName(DUI.DEFAULT_CATEGORY_NAME, oldDatabase.elementNames[i].elementName);
                    }
                }

                // Upgrading - Button Names Database
                if (oldDatabase.buttonNames != null && oldDatabase.buttonNames.Count > 0)
                {
                    if (!DUI.UIButtonCategoryExists(DUI.DEFAULT_CATEGORY_NAME)) { DUI.CreateUIButtonsCategory(DUI.DEFAULT_CATEGORY_NAME); }
                    for (int i = 0; i < oldDatabase.buttonNames.Count; i++)
                    {
                        EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading - Button Names Database / " + oldDatabase.buttonNames[i].buttonName, (i + 1) / (oldDatabase.buttonNames.Count));
                        if (DUI.UIButtonNameExists(DUI.DEFAULT_CATEGORY_NAME, oldDatabase.buttonNames[i].buttonName)) { continue; }
                        DUI.AddUIButtonName(DUI.DEFAULT_CATEGORY_NAME, oldDatabase.buttonNames[i].buttonName);
                    }
                }

                // Upgrading - UISounds Database with element sounds
                if (oldDatabase.elementSounds != null && oldDatabase.elementSounds.Count > 0)
                {
                    for (int i = 0; i < oldDatabase.elementSounds.Count; i++)
                    {
                        EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading - UISounds Database / " + oldDatabase.elementSounds[i].soundName, (i + 1) / (oldDatabase.elementSounds.Count));
                        if (DUI.UISoundNameExists(oldDatabase.elementSounds[i].soundName, SoundType.UIElements) || oldDatabase.elementSounds[i].soundName.Equals(DUI.DEFAULT_SOUND_NAME)) { continue; }
                        DUI.CreateUISound(oldDatabase.elementSounds[i].soundName, SoundType.UIElements, null);
                    }
                }

                // Upgrading - UISounds Database with button sounds
                if (oldDatabase.buttonSounds != null && oldDatabase.buttonSounds.Count > 0)
                {
                    for (int i = 0; i < oldDatabase.buttonSounds.Count; i++)
                    {
                        EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading - UISounds Database / " + oldDatabase.buttonSounds[i].onClickSound, (i + 1) / (oldDatabase.buttonSounds.Count));
                        if (DUI.UISoundNameExists(oldDatabase.buttonSounds[i].onClickSound, SoundType.UIButtons) || oldDatabase.buttonSounds[i].onClickSound.Equals(DUI.DEFAULT_SOUND_NAME)) { continue; }
                        DUI.CreateUISound(oldDatabase.buttonSounds[i].onClickSound, SoundType.UIButtons, null);
                    }
                }

                // Deleting the old database
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Moving old database asset file to trash.", 0.9f);
                if (AssetDatabase.MoveAssetToTrash(oldDatabasePath + "DoozyUI_Data.asset"))
                {
                    Debug.Log("[DoozyUI] [UpgradeManager] Moved the old database asset file to trash.");
                    FileUtil.DeleteFileOrDirectory(oldDatabasePath);
                }

                EditorUtility.ClearProgressBar();
                Debug.Log("[DoozyUI] Finished database conversion...");
                return true;
            }
            return false;
        }
        public static void CleanFiles()
        {
            Debug.Log("[DoozyUI] Started Clean Files...");
            //Deleting obsolete classes /Scripts/Editor/
            string presetsFolderPath = DUI.DOOZYUI_PATH + "/Presets/";
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... old presets", 0.9f);
            FileUtil.DeleteFileOrDirectory(presetsFolderPath);
            //Deleting obsolete classes /Scripts/Editor/
            string editorFilesPath = DUI.DOOZYUI_PATH + "/Scripts/Editor/";
            for (int i = 0; i < oldEditorFiles.Count; i++)
            {
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... " + oldEditorFiles[i], (i + 1) / (oldEditorFiles.Count));
                FileUtil.DeleteFileOrDirectory(editorFilesPath + oldEditorFiles[i]);
            }
            //Delete the old PlayMaker Inspector
            string playmakerInspectorFilePath = DUI.DOOZYUI_PATH + "/PlayMaker/Scripts/Editor/PlaymakerEventDispatcherInspector.cs";
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... " + "PlaymakerEventDispatcherInspector.cs", 0.9f);
            FileUtil.DeleteFileOrDirectory(playmakerInspectorFilePath);
            //Deleting obsolete classes /Scripts/Helpers/
            string helpersFilesPath = DUI.DOOZYUI_PATH + "/Scripts/Helpers/";
            for (int i = 0; i < oldScriptHelpers.Count; i++)
            {
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... " + oldScriptHelpers[i], (i + 1) / (oldScriptHelpers.Count));
                FileUtil.DeleteFileOrDirectory(helpersFilesPath + oldScriptHelpers[i]);
            }
            //Deleting old images
            string imagesFilePath = DUI.DOOZYUI_PATH + "/Images/";
            for (int i = 0; i < oldImages.Count; i++)
            {
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... " + oldImages[i], (i + 1) / (oldImages.Count));
                FileUtil.DeleteFileOrDirectory(imagesFilePath + oldImages[i]);
            }
            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
            Debug.Log("[DoozyUI] Finished Clean Files...");
        }
        public static void DeleteExamples()
        {
            Debug.Log("[DoozyUI] Started deleting the old examples...");
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Deleting... the old '_EXAMPLES' folder", 0.5f);
            FileUtil.DeleteFileOrDirectory(DUI.DOOZYUI_PATH + "/_EXAMPLES");
            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
            Debug.Log("[DoozyUI] Finished deleting the old examples...");
        }

#pragma warning disable 0612

        public static void UpgradeScene()
        {
            Debug.Log("[DoozyUI] Started Scene Upgrade...");
            FixEventSystem();
            UpgradeUIManager();
            UpgradeUICamera();
            UpgradeUIContainer();
            UpgradeUIElements();
            UpgradeUIButtons();
            EditorUtility.ClearProgressBar();
            Debug.Log("[DoozyUI] Finished Scene Upgrade...");
            EditorSceneManager.MarkAllScenesDirty();
        }
        static void FixEventSystem()
        {
            EventSystem es = GameObject.FindObjectOfType<EventSystem>();
            if (es != null)
            {
                es.transform.SetParent(null);
            }
        }
        static void UpgradeUIManager()
        {
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading... UIManager", 0.9f);
            UIManager manager = GameObject.FindObjectOfType<UIManager>();
            if (manager != null)
            {
                manager.transform.SetParent(null, true);
                if (manager.GetComponent<Soundy>() == null) { manager.gameObject.AddComponent<Soundy>(); }
                if (manager.GetComponent<UINotificationManager>() == null) { manager.gameObject.AddComponent<UINotificationManager>(); }
                FindAndRemoveMissingScripts(manager.gameObject);
            }
            EditorUtility.ClearProgressBar();
        }
        static void UpgradeUICamera()
        {
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading... UI Camera", 0.9f);
            GameObject camera = GameObject.Find("UI Camera");
            if (camera != null)
            {
                camera.transform.SetParent(null, true);
                FindAndRemoveMissingScripts(camera);
            }
            EditorUtility.ClearProgressBar();
        }
        static void UpgradeUIContainer()
        {
            EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading... UI Container", 0.9f);
            GameObject container = GameObject.Find("UI Container");
            if (container != null)
            {
                container.transform.SetParent(null, true);
                container.name = DUI.DEFAULT_CANVAS_NAME;
                UICanvas canvas = container.GetComponent<UICanvas>();
                if (canvas == null) { canvas = container.AddComponent<UICanvas>(); }
                UIManager.UpdateCanvasSortingLayerName(canvas.gameObject, canvas.Canvas.sortingLayerName);
                UIManager.UpdateRendererSortingLayerName(canvas.gameObject, canvas.Canvas.sortingLayerName);
                FindAndRemoveMissingScripts(container);
            }
            EditorUtility.ClearProgressBar();
        }
        static void UpgradeUIElements()
        {
            UIElement[] elements = GameObject.FindObjectsOfType<UIElement>();
            if (elements == null || elements.Length == 0) { return; }
            for (int elementIndex = 0; elementIndex < elements.Length; elementIndex++)
            {
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading... UIElement " + elements[elementIndex].gameObject.name, (elementIndex + 1) / elements.Length);

                if (elements[elementIndex].GetComponent<UIAnimationManager>() != null) { GameObject.DestroyImmediate(elements[elementIndex].GetComponent<UIAnimationManager>()); }

                if (elements[elementIndex].GetComponent<CanvasGroup>() == null) { elements[elementIndex].gameObject.AddComponent<CanvasGroup>(); }

                elements[elementIndex].Canvas.sortingLayerName = UIManager.GetMasterCanvas().Canvas.sortingLayerName;

                if (string.IsNullOrEmpty(elements[elementIndex].elementCategory)) { elements[elementIndex].elementCategory = DUI.DEFAULT_CATEGORY_NAME; }
                elements[elementIndex].inAnimations = new Anim(Anim.AnimationType.In);
                if (elements[elementIndex].moveIn != null)
                {
                    elements[elementIndex].inAnimations.move.moveDirection = UIAnimator.GetDirection(elements[elementIndex].moveIn.moveFrom);
                    elements[elementIndex].inAnimations.move.customPosition = elements[elementIndex].moveIn.positionFrom;
                    elements[elementIndex].inAnimations.move.enabled = elements[elementIndex].moveIn.enabled;
                    elements[elementIndex].inAnimations.move.duration = elements[elementIndex].moveIn.time;
                    elements[elementIndex].inAnimations.move.startDelay = elements[elementIndex].moveIn.delay;
                    elements[elementIndex].inAnimations.move.ease = elements[elementIndex].moveIn.easeType;
                    elements[elementIndex].inAnimations.move.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].rotationIn != null)
                {
                    elements[elementIndex].inAnimations.rotate.rotation = elements[elementIndex].rotationIn.rotateFrom;
                    elements[elementIndex].inAnimations.rotate.rotateMode = DG.Tweening.RotateMode.Fast;
                    elements[elementIndex].inAnimations.rotate.enabled = elements[elementIndex].rotationIn.enabled;
                    elements[elementIndex].inAnimations.rotate.duration = elements[elementIndex].rotationIn.time;
                    elements[elementIndex].inAnimations.rotate.startDelay = elements[elementIndex].rotationIn.delay;
                    elements[elementIndex].inAnimations.rotate.ease = elements[elementIndex].rotationIn.easeType;
                    elements[elementIndex].inAnimations.rotate.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].scaleIn != null)
                {
                    elements[elementIndex].inAnimations.scale.scale = elements[elementIndex].scaleIn.scaleBegin;
                    elements[elementIndex].inAnimations.scale.enabled = elements[elementIndex].scaleIn.enabled;
                    elements[elementIndex].inAnimations.scale.duration = elements[elementIndex].scaleIn.time;
                    elements[elementIndex].inAnimations.scale.startDelay = elements[elementIndex].scaleIn.delay;
                    elements[elementIndex].inAnimations.scale.ease = elements[elementIndex].scaleIn.easeType;
                    elements[elementIndex].inAnimations.scale.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].fadeIn != null)
                {
                    elements[elementIndex].inAnimations.fade.alpha = 1f;
                    elements[elementIndex].inAnimations.fade.enabled = elements[elementIndex].fadeIn.enabled;
                    elements[elementIndex].inAnimations.fade.duration = elements[elementIndex].fadeIn.time;
                    elements[elementIndex].inAnimations.fade.startDelay = elements[elementIndex].fadeIn.delay;
                    elements[elementIndex].inAnimations.fade.ease = elements[elementIndex].fadeIn.easeType;
                    elements[elementIndex].inAnimations.fade.easeType = UIAnimator.EaseType.Ease;
                }

                elements[elementIndex].outAnimations = new Anim(Anim.AnimationType.Out);
                if (elements[elementIndex].moveOut != null)
                {
                    elements[elementIndex].outAnimations.move.moveDirection = UIAnimator.GetDirection(elements[elementIndex].moveOut.moveTo);
                    elements[elementIndex].outAnimations.move.customPosition = elements[elementIndex].moveOut.positionTo;
                    elements[elementIndex].outAnimations.move.enabled = elements[elementIndex].moveOut.enabled;
                    elements[elementIndex].outAnimations.move.duration = elements[elementIndex].moveOut.time;
                    elements[elementIndex].outAnimations.move.startDelay = elements[elementIndex].moveOut.delay;
                    elements[elementIndex].outAnimations.move.ease = elements[elementIndex].moveOut.easeType;
                    elements[elementIndex].outAnimations.move.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].rotationOut != null)
                {
                    elements[elementIndex].outAnimations.rotate.rotation = elements[elementIndex].rotationOut.rotateTo;
                    elements[elementIndex].outAnimations.rotate.rotateMode = DG.Tweening.RotateMode.Fast;
                    elements[elementIndex].outAnimations.rotate.enabled = elements[elementIndex].rotationOut.enabled;
                    elements[elementIndex].outAnimations.rotate.duration = elements[elementIndex].rotationOut.time;
                    elements[elementIndex].outAnimations.rotate.startDelay = elements[elementIndex].rotationOut.delay;
                    elements[elementIndex].outAnimations.rotate.ease = elements[elementIndex].rotationOut.easeType;
                    elements[elementIndex].outAnimations.rotate.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].scaleOut != null)
                {
                    elements[elementIndex].outAnimations.scale.scale = elements[elementIndex].scaleOut.scaleEnd;
                    elements[elementIndex].outAnimations.scale.enabled = elements[elementIndex].scaleOut.enabled;
                    elements[elementIndex].outAnimations.scale.duration = elements[elementIndex].scaleOut.time;
                    elements[elementIndex].outAnimations.scale.startDelay = elements[elementIndex].scaleOut.delay;
                    elements[elementIndex].outAnimations.scale.ease = elements[elementIndex].scaleOut.easeType;
                    elements[elementIndex].outAnimations.scale.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].fadeOut != null)
                {
                    elements[elementIndex].outAnimations.fade.alpha = 0f;
                    elements[elementIndex].outAnimations.fade.enabled = elements[elementIndex].fadeOut.enabled;
                    elements[elementIndex].outAnimations.fade.duration = elements[elementIndex].fadeOut.time;
                    elements[elementIndex].outAnimations.fade.startDelay = elements[elementIndex].fadeOut.delay;
                    elements[elementIndex].outAnimations.fade.ease = elements[elementIndex].fadeOut.easeType;
                    elements[elementIndex].outAnimations.fade.easeType = UIAnimator.EaseType.Ease;
                }

                elements[elementIndex].loopAnimations = new Loop();
                if (elements[elementIndex].moveLoop != null)
                {
                    if (elements[elementIndex].moveLoop.autoStart) { elements[elementIndex].loopAnimations.autoStart = true; }
                    elements[elementIndex].loopAnimations.move.movement = elements[elementIndex].moveLoop.movement;
                    elements[elementIndex].loopAnimations.move.enabled = elements[elementIndex].moveLoop.enabled;
                    elements[elementIndex].loopAnimations.move.startDelay = elements[elementIndex].moveLoop.delay;
                    elements[elementIndex].loopAnimations.move.loops = elements[elementIndex].moveLoop.loops;
                    elements[elementIndex].loopAnimations.move.loopType = Loop.GetLoopType(elements[elementIndex].moveLoop.loopType);
                    elements[elementIndex].loopAnimations.move.ease = elements[elementIndex].moveLoop.easeType;
                    elements[elementIndex].loopAnimations.move.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].rotationLoop != null)
                {
                    if (elements[elementIndex].rotationLoop.autoStart) { elements[elementIndex].loopAnimations.autoStart = true; }
                    elements[elementIndex].loopAnimations.rotate.rotation = elements[elementIndex].rotationLoop.rotation;
                    elements[elementIndex].loopAnimations.rotate.enabled = elements[elementIndex].rotationLoop.enabled;
                    elements[elementIndex].loopAnimations.rotate.startDelay = elements[elementIndex].rotationLoop.delay;
                    elements[elementIndex].loopAnimations.rotate.loops = elements[elementIndex].rotationLoop.loops;
                    elements[elementIndex].loopAnimations.rotate.loopType = Loop.GetLoopType(elements[elementIndex].rotationLoop.loopType);
                    elements[elementIndex].loopAnimations.rotate.ease = elements[elementIndex].rotationLoop.easeType;
                    elements[elementIndex].loopAnimations.rotate.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].scaleLoop != null)
                {
                    if (elements[elementIndex].scaleLoop.autoStart) { elements[elementIndex].loopAnimations.autoStart = true; }
                    elements[elementIndex].loopAnimations.scale.max = elements[elementIndex].scaleLoop.max;
                    elements[elementIndex].loopAnimations.scale.min = elements[elementIndex].scaleLoop.min;
                    elements[elementIndex].loopAnimations.scale.enabled = elements[elementIndex].scaleLoop.enabled;
                    elements[elementIndex].loopAnimations.scale.startDelay = elements[elementIndex].scaleLoop.delay;
                    elements[elementIndex].loopAnimations.scale.loops = elements[elementIndex].scaleLoop.loops;
                    elements[elementIndex].loopAnimations.scale.loopType = Loop.GetLoopType(elements[elementIndex].scaleLoop.loopType);
                    elements[elementIndex].loopAnimations.scale.ease = elements[elementIndex].scaleLoop.easeType;
                    elements[elementIndex].loopAnimations.scale.easeType = UIAnimator.EaseType.Ease;
                }
                if (elements[elementIndex].fadeLoop != null)
                {
                    if (elements[elementIndex].fadeLoop.autoStart) { elements[elementIndex].loopAnimations.autoStart = true; }
                    elements[elementIndex].loopAnimations.fade.max = elements[elementIndex].fadeLoop.max;
                    elements[elementIndex].loopAnimations.fade.min = elements[elementIndex].fadeLoop.min;
                    elements[elementIndex].loopAnimations.fade.enabled = elements[elementIndex].fadeLoop.enabled;
                    elements[elementIndex].loopAnimations.fade.startDelay = elements[elementIndex].fadeLoop.delay;
                    elements[elementIndex].loopAnimations.fade.loops = elements[elementIndex].fadeLoop.loops;
                    elements[elementIndex].loopAnimations.fade.loopType = Loop.GetLoopType(elements[elementIndex].fadeLoop.loopType);
                    elements[elementIndex].loopAnimations.fade.ease = elements[elementIndex].fadeLoop.easeType;
                    elements[elementIndex].loopAnimations.fade.easeType = UIAnimator.EaseType.Ease;
                }
                FindAndRemoveMissingScripts(elements[elementIndex].gameObject);
            }
            EditorUtility.ClearProgressBar();
        }
        static void UpgradeUIButtons()
        {
            UIButton[] buttons = GameObject.FindObjectsOfType<UIButton>();
            if (buttons == null || buttons.Length == 0) { return; }
            for (int buttonIndex = 0; buttonIndex < buttons.Length; buttonIndex++)
            {
                EditorUtility.DisplayProgressBar("DoozyUI - Upgrade Manager", "Upgrading... UIButton " + buttons[buttonIndex].gameObject.name, (buttonIndex + 1) / buttons.Length);
                if (buttons[buttonIndex].GetComponent<UIAnimationManager>() != null) { GameObject.DestroyImmediate(buttons[buttonIndex].GetComponent<UIAnimationManager>()); }
                buttons[buttonIndex].useOnClickAnimations = true;
                buttons[buttonIndex].onClickNavigation = new NavigationPointerData(buttons[buttonIndex].addToNavigationHistory);

                if (buttons[buttonIndex].showElements != null && buttons[buttonIndex].showElements.Count > 0)
                {
                    buttons[buttonIndex].onClickNavigation.show = new List<NavigationPointer>();
                    for (int i = 0; i < buttons[buttonIndex].showElements.Count; i++)
                    {
                        buttons[buttonIndex].onClickNavigation.show.Add(new NavigationPointer(DUI.DEFAULT_CATEGORY_NAME, buttons[buttonIndex].showElements[i]));
                    }
                }

                if (buttons[buttonIndex].hideElements != null && buttons[buttonIndex].hideElements.Count > 0)
                {
                    buttons[buttonIndex].onClickNavigation.hide = new List<NavigationPointer>();
                    for (int i = 0; i < buttons[buttonIndex].hideElements.Count; i++)
                    {
                        buttons[buttonIndex].onClickNavigation.hide.Add(new NavigationPointer(DUI.DEFAULT_CATEGORY_NAME, buttons[buttonIndex].hideElements[i]));
                    }
                }

                if (buttons[buttonIndex].gameEvents != null && buttons[buttonIndex].gameEvents.Count > 0)
                {
                    buttons[buttonIndex].onClickGameEvents = new List<string>();
                    for (int i = 0; i < buttons[buttonIndex].gameEvents.Count; i++)
                    {
                        buttons[buttonIndex].onClickGameEvents.Add(buttons[buttonIndex].gameEvents[i]);
                    }
                }

                buttons[buttonIndex].onClickPunch = new Punch();
                if (buttons[buttonIndex].onClickAnimationSettings != null)
                {
                    buttons[buttonIndex].onClickPunch.move.enabled = buttons[buttonIndex].onClickAnimationSettings.punchPositionEnabled;
                    buttons[buttonIndex].onClickPunch.move.punch = buttons[buttonIndex].onClickAnimationSettings.punchPositionPunch;
                    buttons[buttonIndex].onClickPunch.move.duration = buttons[buttonIndex].onClickAnimationSettings.punchPositionDuration;
                    buttons[buttonIndex].onClickPunch.move.startDelay = buttons[buttonIndex].onClickAnimationSettings.punchPositionDelay;
                    buttons[buttonIndex].onClickPunch.move.vibrato = buttons[buttonIndex].onClickAnimationSettings.punchPositionVibrato;
                    buttons[buttonIndex].onClickPunch.move.elasticity = buttons[buttonIndex].onClickAnimationSettings.punchPositionElasticity;

                    buttons[buttonIndex].onClickPunch.rotate.enabled = buttons[buttonIndex].onClickAnimationSettings.punchRotationEnabled;
                    buttons[buttonIndex].onClickPunch.rotate.punch = buttons[buttonIndex].onClickAnimationSettings.punchRotationPunch;
                    buttons[buttonIndex].onClickPunch.rotate.duration = buttons[buttonIndex].onClickAnimationSettings.punchRotationDuration;
                    buttons[buttonIndex].onClickPunch.rotate.startDelay = buttons[buttonIndex].onClickAnimationSettings.punchRotationDelay;
                    buttons[buttonIndex].onClickPunch.rotate.vibrato = buttons[buttonIndex].onClickAnimationSettings.punchRotationVibrato;
                    buttons[buttonIndex].onClickPunch.rotate.elasticity = buttons[buttonIndex].onClickAnimationSettings.punchRotationElasticity;

                    buttons[buttonIndex].onClickPunch.scale.enabled = buttons[buttonIndex].onClickAnimationSettings.punchScaleEnabled;
                    buttons[buttonIndex].onClickPunch.scale.punch = buttons[buttonIndex].onClickAnimationSettings.punchScalePunch;
                    buttons[buttonIndex].onClickPunch.scale.duration = buttons[buttonIndex].onClickAnimationSettings.punchScaleDuration;
                    buttons[buttonIndex].onClickPunch.scale.startDelay = buttons[buttonIndex].onClickAnimationSettings.punchScaleDelay;
                    buttons[buttonIndex].onClickPunch.scale.vibrato = buttons[buttonIndex].onClickAnimationSettings.punchScaleVibrato;
                    buttons[buttonIndex].onClickPunch.scale.elasticity = buttons[buttonIndex].onClickAnimationSettings.punchScaleElasticity;
                }


                buttons[buttonIndex].normalLoop = new Loop();
                if (buttons[buttonIndex].normalAnimationSettings != null)
                {
                    if (buttons[buttonIndex].normalAnimationSettings.moveLoop.autoStart) { buttons[buttonIndex].normalLoop.autoStart = true; }
                    buttons[buttonIndex].normalLoop.move.enabled = buttons[buttonIndex].normalAnimationSettings.moveLoop.enabled;
                    buttons[buttonIndex].normalLoop.move.movement = buttons[buttonIndex].normalAnimationSettings.moveLoop.movement;
                    buttons[buttonIndex].normalLoop.move.duration = buttons[buttonIndex].normalAnimationSettings.moveLoop.time;
                    buttons[buttonIndex].normalLoop.move.startDelay = buttons[buttonIndex].normalAnimationSettings.moveLoop.delay;
                    buttons[buttonIndex].normalLoop.move.loops = buttons[buttonIndex].normalAnimationSettings.moveLoop.loops;
                    buttons[buttonIndex].normalLoop.move.loopType = Loop.GetLoopType(buttons[buttonIndex].normalAnimationSettings.moveLoop.loopType);
                    buttons[buttonIndex].normalLoop.move.ease = buttons[buttonIndex].normalAnimationSettings.moveLoop.easeType;
                    buttons[buttonIndex].normalLoop.move.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].normalAnimationSettings.rotationLoop.autoStart) { buttons[buttonIndex].normalLoop.autoStart = true; }
                    buttons[buttonIndex].normalLoop.rotate.enabled = buttons[buttonIndex].normalAnimationSettings.rotationLoop.enabled;
                    buttons[buttonIndex].normalLoop.rotate.rotation = buttons[buttonIndex].normalAnimationSettings.rotationLoop.rotation;
                    buttons[buttonIndex].normalLoop.rotate.duration = buttons[buttonIndex].normalAnimationSettings.rotationLoop.time;
                    buttons[buttonIndex].normalLoop.rotate.startDelay = buttons[buttonIndex].normalAnimationSettings.rotationLoop.delay;
                    buttons[buttonIndex].normalLoop.rotate.loops = buttons[buttonIndex].normalAnimationSettings.rotationLoop.loops;
                    buttons[buttonIndex].normalLoop.rotate.loopType = Loop.GetLoopType(buttons[buttonIndex].normalAnimationSettings.rotationLoop.loopType);
                    buttons[buttonIndex].normalLoop.rotate.ease = buttons[buttonIndex].normalAnimationSettings.rotationLoop.easeType;
                    buttons[buttonIndex].normalLoop.rotate.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].normalAnimationSettings.scaleLoop.autoStart) { buttons[buttonIndex].normalLoop.autoStart = true; }
                    buttons[buttonIndex].normalLoop.scale.enabled = buttons[buttonIndex].normalAnimationSettings.scaleLoop.enabled;
                    buttons[buttonIndex].normalLoop.scale.min = buttons[buttonIndex].normalAnimationSettings.scaleLoop.min;
                    buttons[buttonIndex].normalLoop.scale.max = buttons[buttonIndex].normalAnimationSettings.scaleLoop.max;
                    buttons[buttonIndex].normalLoop.scale.duration = buttons[buttonIndex].normalAnimationSettings.scaleLoop.time;
                    buttons[buttonIndex].normalLoop.scale.startDelay = buttons[buttonIndex].normalAnimationSettings.scaleLoop.delay;
                    buttons[buttonIndex].normalLoop.scale.loops = buttons[buttonIndex].normalAnimationSettings.scaleLoop.loops;
                    buttons[buttonIndex].normalLoop.scale.loopType = Loop.GetLoopType(buttons[buttonIndex].normalAnimationSettings.scaleLoop.loopType);
                    buttons[buttonIndex].normalLoop.scale.ease = buttons[buttonIndex].normalAnimationSettings.scaleLoop.easeType;
                    buttons[buttonIndex].normalLoop.scale.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].normalAnimationSettings.fadeLoop.autoStart) { buttons[buttonIndex].normalLoop.autoStart = true; }
                    buttons[buttonIndex].normalLoop.fade.enabled = buttons[buttonIndex].normalAnimationSettings.fadeLoop.enabled;
                    buttons[buttonIndex].normalLoop.fade.min = buttons[buttonIndex].normalAnimationSettings.fadeLoop.min;
                    buttons[buttonIndex].normalLoop.fade.max = buttons[buttonIndex].normalAnimationSettings.fadeLoop.max;
                    buttons[buttonIndex].normalLoop.fade.duration = buttons[buttonIndex].normalAnimationSettings.fadeLoop.time;
                    buttons[buttonIndex].normalLoop.fade.startDelay = buttons[buttonIndex].normalAnimationSettings.fadeLoop.delay;
                    buttons[buttonIndex].normalLoop.fade.loops = buttons[buttonIndex].normalAnimationSettings.fadeLoop.loops;
                    buttons[buttonIndex].normalLoop.fade.loopType = Loop.GetLoopType(buttons[buttonIndex].normalAnimationSettings.fadeLoop.loopType);
                    buttons[buttonIndex].normalLoop.fade.ease = buttons[buttonIndex].normalAnimationSettings.fadeLoop.easeType;
                    buttons[buttonIndex].normalLoop.fade.easeType = UIAnimator.EaseType.Ease;
                }

                buttons[buttonIndex].selectedLoop = new Loop();
                if (buttons[buttonIndex].highlightedAnimationSettings != null)
                {
                    if (buttons[buttonIndex].highlightedAnimationSettings.moveLoop.autoStart) { buttons[buttonIndex].selectedLoop.autoStart = true; }
                    buttons[buttonIndex].selectedLoop.move.enabled = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.enabled;
                    buttons[buttonIndex].selectedLoop.move.movement = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.movement;
                    buttons[buttonIndex].selectedLoop.move.duration = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.time;
                    buttons[buttonIndex].selectedLoop.move.startDelay = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.delay;
                    buttons[buttonIndex].selectedLoop.move.loops = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.loops;
                    buttons[buttonIndex].selectedLoop.move.loopType = Loop.GetLoopType(buttons[buttonIndex].highlightedAnimationSettings.moveLoop.loopType);
                    buttons[buttonIndex].selectedLoop.move.ease = buttons[buttonIndex].highlightedAnimationSettings.moveLoop.easeType;
                    buttons[buttonIndex].selectedLoop.move.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.autoStart) { buttons[buttonIndex].selectedLoop.autoStart = true; }
                    buttons[buttonIndex].selectedLoop.rotate.enabled = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.enabled;
                    buttons[buttonIndex].selectedLoop.rotate.rotation = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.rotation;
                    buttons[buttonIndex].selectedLoop.rotate.duration = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.time;
                    buttons[buttonIndex].selectedLoop.rotate.startDelay = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.delay;
                    buttons[buttonIndex].selectedLoop.rotate.loops = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.loops;
                    buttons[buttonIndex].selectedLoop.rotate.loopType = Loop.GetLoopType(buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.loopType);
                    buttons[buttonIndex].selectedLoop.rotate.ease = buttons[buttonIndex].highlightedAnimationSettings.rotationLoop.easeType;
                    buttons[buttonIndex].selectedLoop.rotate.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.autoStart) { buttons[buttonIndex].selectedLoop.autoStart = true; }
                    buttons[buttonIndex].selectedLoop.scale.enabled = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.enabled;
                    buttons[buttonIndex].selectedLoop.scale.min = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.min;
                    buttons[buttonIndex].selectedLoop.scale.max = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.max;
                    buttons[buttonIndex].selectedLoop.scale.duration = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.time;
                    buttons[buttonIndex].selectedLoop.scale.startDelay = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.delay;
                    buttons[buttonIndex].selectedLoop.scale.loops = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.loops;
                    buttons[buttonIndex].selectedLoop.scale.loopType = Loop.GetLoopType(buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.loopType);
                    buttons[buttonIndex].selectedLoop.scale.ease = buttons[buttonIndex].highlightedAnimationSettings.scaleLoop.easeType;
                    buttons[buttonIndex].selectedLoop.scale.easeType = UIAnimator.EaseType.Ease;

                    if (buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.autoStart) { buttons[buttonIndex].selectedLoop.autoStart = true; }
                    buttons[buttonIndex].selectedLoop.fade.enabled = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.enabled;
                    buttons[buttonIndex].selectedLoop.fade.min = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.min;
                    buttons[buttonIndex].selectedLoop.fade.max = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.max;
                    buttons[buttonIndex].selectedLoop.fade.duration = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.time;
                    buttons[buttonIndex].selectedLoop.fade.startDelay = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.delay;
                    buttons[buttonIndex].selectedLoop.fade.loops = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.loops;
                    buttons[buttonIndex].selectedLoop.fade.loopType = Loop.GetLoopType(buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.loopType);
                    buttons[buttonIndex].selectedLoop.fade.ease = buttons[buttonIndex].highlightedAnimationSettings.fadeLoop.easeType;
                    buttons[buttonIndex].selectedLoop.fade.easeType = UIAnimator.EaseType.Ease;
                }
                FindAndRemoveMissingScripts(buttons[buttonIndex].gameObject);
            }
            EditorUtility.ClearProgressBar();
        }
        static void FindAndRemoveMissingScripts(GameObject target)
        {
            Component[] components = target.GetComponents<Component>();
            var r = 0;
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != null) { continue; }
                var serializedObject = new SerializedObject(target);
                var prop = serializedObject.FindProperty("m_Component");
                prop.DeleteArrayElementAtIndex(i - r);
                r++;
                serializedObject.ApplyModifiedProperties();
            }
        }

#pragma warning restore 0612
    }
}
