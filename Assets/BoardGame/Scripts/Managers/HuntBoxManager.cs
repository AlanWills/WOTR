using Celeste.Persistence;
using UnityEngine;
using WOTR.BoardGame.Objects;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame.Managers
{
    [AddComponentMenu("WOTR/Board Game/Managers/Hunt Box Manager")]
    public class HuntBoxManager : PersistentSceneManager<HuntBoxManager, HuntBoxManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "HuntBox.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private HuntBox freePeoplesHuntBox;
        [SerializeField] private HuntBox sauronHuntBox;

        #endregion

        #region Save/Load

        protected override HuntBoxManagerDTO Serialize()
        {
            return new HuntBoxManagerDTO(freePeoplesHuntBox, sauronHuntBox);
        }

        protected override void Deserialize(HuntBoxManagerDTO dto)
        {
            freePeoplesHuntBox.AddDiceToHuntBox(dto.freePeoplesHuntBoxDTO.numDiceInHuntBox);
            sauronHuntBox.AddDiceToHuntBox(dto.sauronHuntBoxDTO.numDiceInHuntBox);
        }

        protected override void SetDefaultValues()
        {
            freePeoplesHuntBox.RemoveAllDiceFromHuntBox();
            sauronHuntBox.RemoveAllDiceFromHuntBox();
        }

        #endregion

        #region Callbacks

        public void OnRemoveAllFreePeoplesDiceFromHuntBox()
        {
            freePeoplesHuntBox.RemoveAllDiceFromHuntBox();
        }

        public void OnRemoveAllSauronDiceFromHuntBox()
        {
            sauronHuntBox.RemoveAllDiceFromHuntBox();
        }

        #endregion
    }
}
