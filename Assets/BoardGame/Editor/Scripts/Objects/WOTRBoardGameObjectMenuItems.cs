using Celeste.BoardGame;
using Celeste.BoardGame.Components;
using CelesteEditor.Tools;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WOTR.BoardGame.Components;

namespace WOTREditor.BoardGame.Objects
{
    public static class WOTRBoardGameObjectMenuItems
    {
        [MenuItem("Assets/Create/WOTR/Board Game/Preset Objects/Unit", validate = true)]
        public static bool ValidateCreateUnitMenuItem()
        {
            return Selection.objects != null &&
                   Array.Exists(Selection.objects, x => x is GameObject && !(x as GameObject).scene.IsValid());
        }

        [MenuItem("Assets/Create/WOTR/Board Game/Preset Objects/Unit", validate = false)]
        public static void CreateUnitMenuItem()
        {
            // Cache the objects in a new list here in case creating any new objects changes the selection
            List<UnityEngine.Object> cachedObjects = new List<UnityEngine.Object>(Selection.objects);

            for (int i = 0, n = cachedObjects.Count; i < n; i++)
            {
                GameObject prefab = cachedObjects[i] as GameObject;

                if (prefab == null ||
                    prefab.scene.IsValid())
                {
                    // Not a project prefab, so we can't use it to create a prefab actor
                    continue;
                }

                string prefabFolder = AssetUtility.GetAssetFolderPath(prefab);

                BoardGameObject boardGameObject = ScriptableObject.CreateInstance<BoardGameObject>();
                boardGameObject.name = $"{prefab.name}BoardGameObject";

                AssetUtility.CreateAssetInFolderAndSave(boardGameObject, prefabFolder);

                PrefabActorBoardGameObjectComponent prefabActor = boardGameObject.CreateComponent<PrefabActorBoardGameObjectComponent>();
                prefabActor.Prefab = prefab;
                prefabActor.CustomiseScale = false;

                boardGameObject.CreateComponent<FactionUnitBoardGameObjectComponent>();

                AssetDatabase.SaveAssetIfDirty(prefabActor);
                AssetDatabase.SaveAssetIfDirty(boardGameObject);
            }
        }
    }
}
