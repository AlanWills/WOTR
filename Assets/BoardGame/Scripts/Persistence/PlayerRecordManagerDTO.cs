using System;
using System.Collections.Generic;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class PlayerRecordManagerDTO
    {
        public List<WOTRPlayerRecordDTO> playerRecords = new List<WOTRPlayerRecordDTO>();

        public PlayerRecordManagerDTO(List<WOTRPlayerRecord> playerRecords)
        {
            this.playerRecords.Capacity = playerRecords.Count;

            for (int i = 0, n = playerRecords.Count; i < n; ++i)
            {
                this.playerRecords.Add(new WOTRPlayerRecordDTO(playerRecords[i]));
            }
        }
    }
}
