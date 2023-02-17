using Celeste.DeckBuilding.Catalogue;
using Celeste.Events;
using Celeste.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;
using WOTR.BoardGame.Catalogue;
using WOTR.BoardGame.Events;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame.Runtime
{
    [AddComponentMenu("WOTR/Board Game/Runtime/Player Record Manager")]
    public class PlayerRecordManager : PersistentSceneManager<PlayerRecordManager, PlayerRecordManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "PlayerRecords.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private DeckCatalogue deckCatalogue;
        [SerializeField] private CardCatalogue cardCatalogue;
        [SerializeField] private WOTRPlayerRecordCatalogue playerCatalogue;

        [NonSerialized] private List<PlayerRecord> activePlayers = new List<PlayerRecord>();

        #endregion

        #region Save/Load

        protected override PlayerRecordManagerDTO Serialize()
        {
            return new PlayerRecordManagerDTO(activePlayers);
        }

        protected override void Deserialize(PlayerRecordManagerDTO dto)
        {
            LoadCommon(dto);
        }

        protected override void SetDefaultValues() { }

        private void LoadCommon(PlayerRecordManagerDTO dto)
        {
            activePlayers.Clear();

            foreach (var playerDTO in dto.playerRecords)
            {
                PlayerRecord playerRecord = playerCatalogue.FindByGuid(playerDTO.guid);
                UnityEngine.Debug.Assert(playerRecord != null, $"Could not find player record with GUID {playerDTO.guid}.");
                playerRecord.Load(playerDTO, deckCatalogue, cardCatalogue);
                activePlayers.Add(playerRecord);
            }
        }

        #endregion

        #region Callbacks

        public void OnBoardGameSetup(BoardGameSetupArgs args)
        {
            LoadCommon(args.boardGameSetup.DeserializeData<PlayerRecordManagerDTO>(FILE_NAME));
        }

        public void OnBoardGameLoaded(BoardGameLoadedArgs args)
        {
            Load();
        }

        public void OnBoardGameShutdown(BoardGameShutdownArgs args)
        {
        }

        #endregion
    }
}
