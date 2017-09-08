namespace MagicFire
{
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class OpenSceneMenuItemScript{
static void OpenScene(string name)
{
if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true)
{
   EditorSceneManager.OpenScene("Assets/_Scenes/" + name + ".unity");
}
}}
}
