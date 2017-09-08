// Copyright (c) 2015 - 2017 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;
using UnityEngine;

namespace DoozyUI
{
    [CustomEditor(typeof(UINotificationManager))]
    [DisallowMultipleComponent]
    public class UINotificationManagerEditor : QEditor
    {
        UINotificationManager uiNotificationManager { get { return (UINotificationManager)target; } }

        SerializedProperty
            NotificationItems;

        void SerializedObjectFindProperties()
        {
            NotificationItems = serializedObject.FindProperty("NotificationItems");
        }

        protected override void OnEnable()
        {
            requiresContantRepaint = true;
            SerializedObjectFindProperties();
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerUINotificationManager.texture, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            DrawNotificationItems();
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }

        void DrawNotificationItems()
        {
            if (NotificationItems.arraySize == 0)
            {
                QUI.BeginHorizontal(WIDTH_420);
                {
                    QUI.Label("No UINotification prefabs referenced... Click [+] to start...", WIDTH_420 - 23);
                    QUI.BeginVertical(18);
                    {
                        QUI.Space(-1);
                        if (QUI.ButtonPlus()) { NotificationItems.InsertArrayElementAtIndex(0); }
                    }
                    QUI.EndVertical();
                }
                QUI.EndHorizontal();
                return;
            }

            QUI.BeginHorizontal(WIDTH_420);
            {
                QUI.Space(18);
                QUI.Label("Notification Name", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), (WIDTH_420 - 18 - 18 - 9) / 2f);
                QUI.Label("Notification Prefab", DUIStyles.GetStyle(DUIStyles.TextStyle.LabelNormalItalic), (WIDTH_420 - 18 - 18 - 9) / 2f);
            }
            QUI.EndHorizontal();

            QUI.BeginVertical(WIDTH_420);
            {
                for (int i = 0; i < NotificationItems.arraySize; i++)
                {
                    SaveColors();
                    QUI.BeginHorizontal(WIDTH_420);
                    {
                        QUI.Space(-4);
                        QUI.Label(" " + i.ToString(), 18);
                        if (NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationPrefab").objectReferenceValue != null)
                        {
                            NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationName").stringValue = NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationPrefab").objectReferenceValue.name;
                        }
                        else
                        {
                            QUI.SetGUIBackgroundColor(DUIColors.RedLight.Color);
                            QUI.SetGUIColor(DUIColors.RedLight.Color);
                            NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationName").stringValue = "Missing UINotification prefab";
                        }
                        QUI.Label(NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationName").stringValue, (WIDTH_420 - 18 - 18 - 9) / 2f);
                        QUI.PropertyField(NotificationItems.GetArrayElementAtIndex(i).FindPropertyRelative("notificationPrefab"), true, (WIDTH_420 - 18 - 18 - 9) / 2f);
                        QUI.BeginVertical(18);
                        {
                            QUI.Space(-1);
                            if (QUI.ButtonMinus()) { NotificationItems.DeleteArrayElementAtIndex(i); }
                        }
                        QUI.EndVertical();
                    }
                    QUI.EndHorizontal();
                    RestoreColors();
                }

                QUI.BeginHorizontal(WIDTH_420);
                {
                    QUI.Space(WIDTH_420 - 19);
                    QUI.BeginVertical(18);
                    {
                        QUI.Space(-1);
                        if (QUI.ButtonPlus()) { NotificationItems.InsertArrayElementAtIndex(NotificationItems.arraySize); }
                    }
                    QUI.EndVertical();
                    QUI.Space(2);
                }
                QUI.EndHorizontal();
            }
            QUI.EndVertical();
        }
    }
}
