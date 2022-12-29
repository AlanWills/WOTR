using Celeste.BoardGame;
using Celeste.BoardGame.Runtime;
using Celeste.Events;
using UnityEngine;
using WOTR.BoardGame.Catalogue;
using WOTR.BoardGame.Events;
using WOTR.BoardGame.Interfaces;

namespace WOTR.BoardGame.Runtime
{
    [AddComponentMenu("WOTR/Board Game/Runtime/Territories Manager")]
    public class TerritoriesManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TerritoryCatalogue territoryCatalogue;

        #endregion

        private void LoadCommon(BoardGameRuntime boardGameRuntime)
        {
            for (int i = 0, n = boardGameRuntime.NumBoardGameObjects; i < n; i++)
            {
                BoardGameObjectRuntime boardGameObjectRuntime = boardGameRuntime.GetBoardGameObject(i);

                // Find if a unit is currently in our territory
                TryAddUnitToTerritory(boardGameObjectRuntime);
            }
        }

        private void TryAddUnitToTerritory(BoardGameObjectRuntime boardGameObjectRuntime)
        {
            if (boardGameObjectRuntime.TryFindComponent<IBoardGameObjectUnit>(out var unit) &&
                boardGameObjectRuntime.TryFindComponent<IBoardGameObjectOwner>(out var owner) &&
                boardGameObjectRuntime.TryFindComponent<IBoardGameObjectActor>(out var actor))
            {
                string location = actor.iFace.GetCurrentLocationName(actor.instance);
                int ownerGuid = owner.iFace.GetOwnerGuid(owner.instance);
                UnitType unitType = unit.iFace.GetUnitType(actor.instance);
                Territory territory = territoryCatalogue.GetItem(location);

                UnityEngine.Debug.Assert(territory != null, $"Failed to find territory with name {location} when adding unit to territory.");
                if (territory != null)
                {
                    territory.AddUnit(ownerGuid, unitType);
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

        public void OnBoardGameObjectAdded(BoardGameRuntime boardGameRuntime, BoardGameObjectRuntime boardGameObjectRuntime)
        {
            TryAddUnitToTerritory(boardGameObjectRuntime);
        }

        #endregion
    }
}
