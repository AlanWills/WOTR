using Celeste.BoardGame;
using System.Collections.Generic;
using UnityEngine;
using WOTR.BoardGame;

namespace WOTREditor.BoardGame.Game
{
    [CreateAssetMenu(fileName = nameof(BoardGameSetupTemplate), menuName = "WOTR/Board Game/Game/Board Game Setup Template")]
    public class BoardGameSetupTemplate : ScriptableObject
    {
        #region Properties and Fields

        [SerializeField] private BoardGameSetup boardGameSetup;
        [SerializeField] private List<BoardGameObject> boardGameObjects = new List<BoardGameObject>();

        #endregion
    }
}
