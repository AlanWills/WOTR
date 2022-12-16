using System;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class FactionDTO
    {
        public int guid;
        public bool isActive;
        public int diplomacyStatus;

        public FactionDTO(Faction faction)
        {
            guid = faction.Guid;
            isActive = faction.IsActive;
            diplomacyStatus = faction.DiplomacyStatus;
        }
    }
}
