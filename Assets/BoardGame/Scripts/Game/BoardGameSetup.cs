using Celeste.BoardGame;
using Celeste.BoardGame.Persistence;
using UnityEngine;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame
{
    [CreateAssetMenu(fileName = nameof(BoardGameSetup), menuName = "WOTR/Board Game/Game/Setup")]
    public class BoardGameSetup : ScriptableObject
    {
        #region Properties and Fields

        public BoardGameRuntimeDTO startingBoardGameRuntimeState;
        public FactionsManagerDTO startingFactionsState;
        public PlayerRecordManagerDTO startingPlayersState;

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
