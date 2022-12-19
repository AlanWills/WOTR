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
        public int availableSoldiers;
        public int availableElites;
        public int availableLeaders;
        public int removedSoldiers;
        public int removedElites;
        public int removedLeaders;

        public FactionDTO(Faction faction)
        {
            guid = faction.Guid;
            name = faction.name;
            isActive = faction.IsActive;
            diplomacyStatus = faction.DiplomacyStatus;
            availableSoldiers = faction.AvailableSoldiers;
            availableElites = faction.AvailableElites;
            availableLeaders= faction.AvailableLeaders;
            removedSoldiers = faction.RemovedSoldiers;
            removedElites = faction.RemovedElites;
            removedLeaders = faction.RemovedLeaders;
        }
    }
}
