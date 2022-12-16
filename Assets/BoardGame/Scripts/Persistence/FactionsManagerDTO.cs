using System;
using System.Collections.Generic;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class FactionsManagerDTO
    {
        public List<FactionDTO> factions = new List<FactionDTO>();

        public FactionsManagerDTO(List<Faction> factions)
        {
            this.factions.Capacity = factions.Count;

            for (int i = 0, n = factions.Count; i < n; ++i)
            {
                this.factions.Add(new FactionDTO(factions[i]));
            }
        }
    }
}
