using System;
using WOTR.BoardGame.Objects;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class HuntBoxManagerDTO
    {
        public HuntBoxDTO freePeoplesHuntBoxDTO;
        public HuntBoxDTO sauronHuntBoxDTO;

        public HuntBoxManagerDTO(HuntBox freePeoplesHuntBox, HuntBox sauronHuntBox)
        {
            freePeoplesHuntBoxDTO = new HuntBoxDTO(freePeoplesHuntBox);
            sauronHuntBoxDTO = new HuntBoxDTO(sauronHuntBox);
        }
    }
}
