using Celeste.BoardGame.Interfaces;
using Celeste.BoardGame.Objects;
using Celeste.BoardGame.Runtime;
using Celeste.Constants;
using Celeste.Events;
using UnityEngine;
using WOTR.BoardGame.Events;
using WOTR.BoardGame.Interfaces;

namespace WOTR.BoardGame.Runtime
{
    [AddComponentMenu("WOTR/Board Game/Runtime/Dice Manager")]
    public class DiceManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private ID id;
        [SerializeField] private Dice dice;

        #endregion

        private void LoadCommon(BoardGameRuntime boardGameRuntime)
        {
            for (int i = 0, n = boardGameRuntime.NumBoardGameObjects; i < n; i++)
            {
                BoardGameObjectRuntime boardGameObjectRuntime = boardGameRuntime.GetBoardGameObject(i);

                // Find all dice which belong to the selected owner
                if (boardGameObjectRuntime.TryFindComponent<IBoardGameObjectDie>(out var die))
                {
                    // We either have no particular ID specified or our owner matches the id we specified
                    if (id == null ||
                        (boardGameObjectRuntime.TryFindComponent<IBoardGameObjectOwner>(out var owner) &&
                         owner.iFace.GetOwnerGuid(owner.instance) == id))
                    {
                        dice.AddDie(die);
                    }
                }
            }
        }

        #region Callbacks

        public void OnBoardGameSetup(BoardGameSetupArgs args)
        {
            LoadCommon(args.boardGameRuntime);
        }

        public void OnBoardGameLoaded(BoardGameLoadedArgs args)
        {
            LoadCommon(args.boardGameRuntime);
        }

        public void OnBoardGameShutdown(BoardGameShutdownArgs args)
        {
        }

        public void OnRollAllDice()
        {
            dice.RollAll();
        }

        #endregion
    }
}
