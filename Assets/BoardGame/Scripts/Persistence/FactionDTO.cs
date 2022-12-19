using System;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class FactionDTO
    {
        public string name;
        public int guid;
        public bool isActive;
        public int diplomacyStatus;

        public FactionDTO(Faction faction)
        {
            guid = faction.Guid;
            name = faction.name;
            isActive = faction.IsActive;
            diplomacyStatus = faction.DiplomacyStatus;
        }
    }
}
