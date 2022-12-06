using Celeste.DeckBuilding.Cards;
using Celeste.DeckBuilding.Catalogue;
using Celeste.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;
using WOTR.BoardGame.Catalogue;
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

        [NonSerialized] private List<WOTRPlayerRecord> activePlayers = new List<WOTRPlayerRecord>();

        #endregion

        #region Save/Load

        protected override PlayerRecordManagerDTO Serialize()
        {
            return new PlayerRecordManagerDTO(activePlayers);
        }

        protected override void Deserialize(PlayerRecordManagerDTO dto)
        {
            activePlayers.Clear();

            foreach (var playerDTO in dto.playerRecords)
            {
                WOTRPlayerRecord playerRecord = playerCatalogue.FindByGuid(playerDTO.guid);
                Debug.Assert(playerRecord != null, $"Could not find player record with GUID {playerDTO.guid}.");
                playerRecord.Load(playerDTO, deckCatalogue, cardCatalogue);
                activePlayers.Add(playerRecord);
            }
        }

        protected override void SetDefaultValues()
        {
            activePlayers.Add(playerCatalogue.FindByGuid(1));
            activePlayers.Add(playerCatalogue.FindByGuid(2));
        }

        #endregion
    }
}
