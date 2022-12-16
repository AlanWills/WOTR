using Celeste.BoardGame.Runtime;
using Celeste.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;
using WOTR.BoardGame.Catalogue;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame.Runtime
{
    [AddComponentMenu("WOTR/Board Game/Runtime/Factions Manager")]
    public class FactionsManager : PersistentSceneManager<FactionsManager, FactionsManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "Factions.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private FactionCatalogue factionCatalogue;

        [NonSerialized] private List<Faction> factions = new List<Faction>();

        #endregion

        protected override FactionsManagerDTO Serialize()
        {
            return new FactionsManagerDTO(factions);
        }

        protected override void Deserialize(FactionsManagerDTO dto)
        {
            LoadCommon(dto);
        }

        protected override void SetDefaultValues() { }

        private void LoadCommon(FactionsManagerDTO dto)
        {
            factions.Clear();

            foreach (FactionDTO factionDTO in dto.factions)
            {
                Faction faction = factionCatalogue.FindByGuid(factionDTO.guid);
                UnityEngine.Debug.Assert(faction != null, $"Could not find {nameof(Faction)} with guid {factionDTO.guid}.");

                faction.IsActive = factionDTO.isActive;
                faction.DiplomacyStatus = factionDTO.diplomacyStatus;
                factions.Add(faction);
            }
        }

        #region Callbacks

        public void OnBoardGameSetup(BoardGameSetupArgs args)
        {
            LoadCommon(args.boardGameSetup.StartingFactionsState);
        }

        public void OnBoardGameLoaded(BoardGameLoadedArgs args)
        {
            Load();
        }

        public void OnBoardGameRuntimeShutdown(BoardGameShutdownArgs args)
        {
        }

        #endregion
    }
}
