using Celeste.BoardGame;
using Celeste.BoardGame.Persistence;
using CelesteEditor;
using UnityEditor;
using UnityEngine;
using WOTR.BoardGame;
using WOTR.BoardGame.Persistence;

namespace WOTREditor.BoardGame.Game
{
    [CustomEditor(typeof(BoardGameSetup))]
    public class BoardGameSetupEditor : Editor
    {
        #region Properties and Fields

        private Celeste.BoardGame.BoardGame boardGame;
        private BoardGameObject boardGameObject;
        private string json;

        #endregion

        public override void OnInspectorGUI()
        {
            json = EditorGUILayout.TextArea(json);

            if (GUILayout.Button("Use As Starting Board Game Runtime State"))
            {
                (target as BoardGameSetup).startingBoardGameRuntimeState = JsonUtility.FromJson<BoardGameRuntimeDTO>(json);
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Use As Starting Factions State"))
            {
                (target as BoardGameSetup).startingFactionsState = JsonUtility.FromJson<FactionsManagerDTO>(json);
                EditorUtility.SetDirty(target);
            }

            if (GUILayout.Button("Use As Starting Players State"))
            {
                (target as BoardGameSetup).startingPlayersState = JsonUtility.FromJson<PlayerRecordManagerDTO>(json);
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.Space();
            CelesteEditorGUILayout.HorizontalLine();
            EditorGUILayout.Space();

            using (var horizontal = new GUILayout.HorizontalScope())
            {
                boardGame = EditorGUILayout.ObjectField(boardGame, typeof(Celeste.BoardGame.BoardGame), false) as Celeste.BoardGame.BoardGame;

                if (GUILayout.Button("Use", GUILayout.ExpandWidth(false)))
                {
                    (target as BoardGameSetup).UseBoardGame(boardGame);
                    EditorUtility.SetDirty(target);
                }
            }

            using (var horizontal = new GUILayout.HorizontalScope())
            {
                boardGameObject = EditorGUILayout.ObjectField(boardGameObject, typeof(BoardGameObject), false) as BoardGameObject;

                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                {
                    (target as BoardGameSetup).AddBoardGameObject(boardGameObject);
                    EditorUtility.SetDirty(target);
                }
            }

            base.OnInspectorGUI();
        }
    }
}
