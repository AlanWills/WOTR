using Celeste.BoardGame;
using UnityEditor;
using UnityEngine;
using WOTR.BoardGame;

namespace WOTREditor.BoardGame.Game
{
    [CustomEditor(typeof(BoardGameSetup))]
    public class BoardGameSetupEditor : Editor
    {
        #region Properties and Fields

        private Celeste.BoardGame.BoardGame boardGame;
        private BoardGameObject boardGameObject;

        #endregion

        public override void OnInspectorGUI()
        {
            using (var horizontal = new GUILayout.HorizontalScope())
            {
                boardGame = EditorGUILayout.ObjectField(boardGame, typeof(Celeste.BoardGame.BoardGame), false) as Celeste.BoardGame.BoardGame;

                if (GUILayout.Button("Use", GUILayout.ExpandWidth(false)))
                {
                    (target as BoardGameSetup).UseBoardGame(boardGame);
                }
            }

            using (var horizontal = new GUILayout.HorizontalScope())
            {
                boardGameObject = EditorGUILayout.ObjectField(boardGameObject, typeof(BoardGameObject), false) as BoardGameObject;

                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                {
                    (target as BoardGameSetup).AddBoardGameObject(boardGameObject);
                }
            }

            base.OnInspectorGUI();
        }
    }
}
