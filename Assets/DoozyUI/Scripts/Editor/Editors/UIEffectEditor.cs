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
    [CustomEditor(typeof(UIEffect), true)]
    [CanEditMultipleObjects]
    public class UIEffectEditor : QEditor
    {
        private const string MISSING_UIELEMENT = " --- ";

        UIEffect uiEffect { get { return (UIEffect)target; } }

        SerializedProperty
            targetParticleSystem, targetUIElement,
            startDelay, playOnAwake, stopInstantly,
            useCustomSortingLayerName, customSortingLayerName,
            useCustomOrderInLayer, customOrderInLayer,
            effectPosition, sortingOrderStep;

        Canvas m_targetCanvas;
        Canvas TargetCanvas
        {
            get
            {
                if (m_targetCanvas == null)
                {
                    m_targetCanvas = uiEffect.targetUIElement.GetComponent<Canvas>() == null
                                     ? uiEffect.targetUIElement.gameObject.AddComponent<Canvas>()
                                     : uiEffect.targetUIElement.GetComponent<Canvas>();
                }
                return m_targetCanvas;
            }
        }
        string targetSortingLayerName;
        int targetOrderInLayer;

        void SerializedObjectFindProperties()
        {
            targetParticleSystem = serializedObject.FindProperty("targetParticleSystem");
            targetUIElement = serializedObject.FindProperty("targetUIElement");
            startDelay = serializedObject.FindProperty("startDelay");
            playOnAwake = serializedObject.FindProperty("playOnAwake");
            stopInstantly = serializedObject.FindProperty("stopInstantly");
            useCustomSortingLayerName = serializedObject.FindProperty("useCustomSortingLayerName");
            customSortingLayerName = serializedObject.FindProperty("customSortingLayerName");
            useCustomOrderInLayer = serializedObject.FindProperty("useCustomOrderInLayer");
            customOrderInLayer = serializedObject.FindProperty("customOrderInLayer");
            effectPosition = serializedObject.FindProperty("effectPosition");
            sortingOrderStep = serializedObject.FindProperty("sortingOrderStep");
        }

        void GenerateInfoMessages()
        {
            infoMessage = new Dictionary<string, InfoMessage>
            {
                { "ParticleSystemDisabled", new InfoMessage() { title = "Missing ParticleSystem", message = "Add a ParticleSystem to this gameObject or set the target ParticleSystem manually", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) } },
                { "UIElementDisabled", new InfoMessage() { title = "Missing UIElement", message = "Set the target UIElement that manages this UIEffect", type = InfoMessageType.Error, show = new AnimBool(false, Repaint) } }
            };
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
            GenerateInfoMessages();

            if (uiEffect.targetParticleSystem == null)
            {
                uiEffect.targetParticleSystem = uiEffect.GetComponent<ParticleSystem>();
            }
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUIEffect.texture, WIDTH_420, HEIGHT_42);
            serializedObject.Update();

            infoMessage["ParticleSystemDisabled"].show.target = uiEffect.targetParticleSystem == null;
            infoMessage["UIElementDisabled"].show.target = uiEffect.targetUIElement == null;
            if (infoMessage["ParticleSystemDisabled"].show.target || infoMessage["UIElementDisabled"].show.target)
            {
                QUI.Space(-SPACE_4);
                DrawInfoMessage("ParticleSystemDisabled", WIDTH_420);
                DrawInfoMessage("UIElementDisabled", WIDTH_420);
            }

            DrawRenameGameObjectButton();
            DrawAddParticleSystemButton();
            DrawTargetParticleSystem();
            DrawTargetUIElement();
            if (uiEffect.targetParticleSystem != null && uiEffect.targetUIElement != null)
            {
                DrawSortingLayerName();
                DrawSortingOrder();
                DrawUpdateSortingButton();
                DrawSettings();
            }
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawRenameGameObjectButton()
        {
            if (!DUI.DUISettings.UIEffect_Inspector_ShowButtonRenameGameObject || (uiEffect.targetUIElement != null && uiEffect.targetUIElement.linkedToNotification)) { return; }
            QUI.Space(SPACE_2);
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("Rename GameObject to target Element Name"))
                {
                    if (serializedObject.isEditingMultipleObjects)
                    {
                        Undo.RecordObjects(targets, "Renamed Multiple Objects");
                        for (int i = 0; i < targets.Length; i++)
                        {
                            UIEffect iTarget = (UIEffect)targets[i];
                            iTarget.gameObject.name = iTarget.targetUIElement != null
                                                      ? (DUI.DUISettings.UIEffect_Inspector_RenameGameObjectPrefix + iTarget.targetUIElement.elementName + DUI.DUISettings.UIEffect_Inspector_RenameGameObjectSuffix)
                                                      : "UIEffect DISABLED";
                        }
                    }
                    else
                    {

                    }
                    RenameGameObject();
                }
            }
            QUI.EndHorizontal();
        }
        void RenameGameObject()
        {
            uiEffect.gameObject.name = uiEffect.targetUIElement != null
                                       ? (DUI.DUISettings.UIEffect_Inspector_RenameGameObjectPrefix + uiEffect.targetUIElement.elementName + DUI.DUISettings.UIEffect_Inspector_RenameGameObjectSuffix)
                                       : "UIEffect DISABLED";
        }
        void DrawAddParticleSystemButton()
        {
            if (uiEffect.targetParticleSystem != null) { return; }
            QUI.BeginHorizontal(WIDTH_420);
            {
                if (QUI.Button("Add a ParticleSystem to this gameObject"))
                {
                    targetParticleSystem.objectReferenceValue = uiEffect.GetComponent<ParticleSystem>() == null ? uiEffect.gameObject.AddComponent<ParticleSystem>() : uiEffect.GetComponent<ParticleSystem>();
                    serializedObject.ApplyModifiedProperties();
                    uiEffect.targetParticleSystem.GetComponent<ParticleSystemRenderer>().material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Particle.mat");
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }
        void DrawTargetParticleSystem()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Target ParticleSystem", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                QUI.PropertyField(targetParticleSystem, 290);
            }
            QUI.EndHorizontal();
        }
        void DrawTargetUIElement()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Target UIElement", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                QUI.BeginChangeCheck();
                {
                    QUI.PropertyField(targetUIElement, 290);
                }
                if (QUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    RenameGameObject();
                }
            }
            QUI.EndHorizontal();
            QUI.Space(-SPACE_2);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(130);
                QUI.Label("Element Category", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 100);
                if (uiEffect.targetUIElement != null)
                {
                    QUI.Label(uiEffect.targetUIElement.elementCategory, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 190);
                }
                else
                {
                    QUI.Label(MISSING_UIELEMENT, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 190);
                }
            }
            QUI.EndHorizontal();
            QUI.Space(-SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(130);
                QUI.Label("Element Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 100);
                if (uiEffect.targetUIElement != null)
                {
                    QUI.Label(uiEffect.targetUIElement.elementName, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 190);
                }
                else
                {
                    QUI.Label(MISSING_UIELEMENT, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmallItalic), 190);
                }
            }
            QUI.EndHorizontal();
        }
        void DrawSortingLayerName()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Sorting Layer Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                if (!useCustomSortingLayerName.boolValue)
                {
                    targetSortingLayerName = TargetCanvas.overrideSorting
                                             ? TargetCanvas.sortingLayerName
                                             : TargetCanvas.rootCanvas.sortingLayerName;

                    QUI.Label(targetSortingLayerName, DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), 140);
                    QUI.Toggle(useCustomSortingLayerName);
                    QUI.Label("use a custom layer name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 200);
                    customSortingLayerName.stringValue = targetSortingLayerName;
                }
                else
                {
                    if (customSortingLayerName.stringValue == MISSING_UIELEMENT)
                    {
                        customSortingLayerName.stringValue = "Default";
                    }
                    QUI.PropertyField(customSortingLayerName, 140);
                    QUI.Toggle(useCustomSortingLayerName);
                    QUI.Label("use a custom layer name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 200);
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
        }
        void DrawSortingOrder()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("Order in Layer", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                if (!useCustomOrderInLayer.boolValue)
                {
                    targetOrderInLayer = TargetCanvas.overrideSorting
                                         ? TargetCanvas.sortingOrder
                                         : TargetCanvas.rootCanvas.sortingOrder;

                    targetOrderInLayer = uiEffect.effectPosition == UIEffect.EffectPosition.InFrontOfTarget
                                         ? targetOrderInLayer + uiEffect.sortingOrderStep
                                         : targetOrderInLayer - uiEffect.sortingOrderStep;

                    QUI.Label(targetOrderInLayer.ToString(), DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), 140);
                    QUI.Toggle(useCustomOrderInLayer);
                    QUI.Label("use a custom order in layer", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 200);
                    customOrderInLayer.intValue = targetOrderInLayer;
                }
                else
                {
                    QUI.PropertyField(customOrderInLayer, 140);
                    QUI.Toggle(useCustomOrderInLayer);
                    QUI.Label("use a custom order in layer", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 200);
                }
            }
            QUI.EndHorizontal();
            if (!useCustomOrderInLayer.boolValue)
            {
                QUI.BeginHorizontal(WIDTH_420);
                {
                    QUI.Space(130);
                    QUI.Label("Set the effect", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 68);
                    QUI.PropertyField(effectPosition, 110);
                    QUI.Label("by", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 16);
                    QUI.PropertyField(sortingOrderStep, 40);
                    QUI.Label("step" + (sortingOrderStep.intValue == 1 ? "" : "s"), DUIStyles.GetStyle(DUIStyles.TextStyle.LabelSmall), 40);
                }
                QUI.EndHorizontal();
            }
            QUI.Space(SPACE_2);
        }
        void DrawUpdateSortingButton()
        {
            QUI.Space(SPACE_4);
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(130);
                if (QUI.Button("Update Sorting", WIDTH_420 - 130, HEIGHT_24))
                {
                    uiEffect.UpdateSorting();
                }
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_4);
        }
        void DrawSettings()
        {
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Label("start delay on show", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormal), 126);
                QUI.PropertyField(startDelay, 290);
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Toggle(playOnAwake);
                QUI.Label("play on awake", 130);
            }
            QUI.EndHorizontal();
            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Toggle(stopInstantly);
                QUI.Label("stop instantly on hide", 130);
            }
            QUI.EndHorizontal();
        }
    }
}
