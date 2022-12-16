using Celeste.BoardGame;
using Celeste.BoardGame.Persistence;
using Celeste.BoardGame.Runtime;
using System;
using UnityEngine;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame
{
    [Serializable]
    public struct BoardGameSetupArgs
    {
        public BoardGameSetup boardGameSetup;
        public BoardGameRuntime boardGameRuntime;
    }

    [CreateAssetMenu(fileName = nameof(BoardGameSetup), menuName = "WOTR/Board Game/Game/Setup")]
    public class BoardGameSetup : ScriptableObject
    {
        #region Properties and Fields

        public BoardGameRuntimeDTO StartingBoardGameRuntimeState => startingBoardGameRuntimeState;
        public FactionsManagerDTO StartingFactionsState => startingFactionsState;
        public PlayerRecordManagerDTO StartingPlayersState => startingPlayersState;

        [SerializeField] private BoardGameRuntimeDTO startingBoardGameRuntimeState;
        [SerializeField] private FactionsManagerDTO startingFactionsState;
        [SerializeField] private PlayerRecordManagerDTO startingPlayersState;

        #endregion

        public void UseBoardGame(Celeste.BoardGame.BoardGame boardGame)
        {
            startingBoardGameRuntimeState = new BoardGameRuntimeDTO(boardGame);
        }

        public void AddBoardGameObject(BoardGameObject boardGameObject)
        {
            startingBoardGameRuntimeState.boardGameObjectRuntimes.Add(new BoardGameObjectRuntimeDTO(boardGameObject));
        }
    }
}
