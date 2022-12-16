using System;
using System.Collections.Generic;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class PlayerRecordManagerDTO
    {
        public List<PlayerRecordDTO> playerRecords = new List<PlayerRecordDTO>();

        public PlayerRecordManagerDTO(List<PlayerRecord> playerRecords)
        {
            this.playerRecords.Capacity = playerRecords.Count;

            for (int i = 0, n = playerRecords.Count; i < n; ++i)
            {
                this.playerRecords.Add(new PlayerRecordDTO(playerRecords[i]));
            }
        }
    }
}
