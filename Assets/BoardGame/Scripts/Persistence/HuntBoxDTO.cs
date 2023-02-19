using System;
using WOTR.BoardGame.Objects;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class HuntBoxDTO
    {
        public int numDiceInHuntBox;

        public HuntBoxDTO(HuntBox huntBox)
        {
            numDiceInHuntBox = huntBox.NumDiceInHuntBox;
        }
    }
}
