using Celeste.BoardGame.Components;
using Celeste.Components;
using System.ComponentModel;
using UnityEngine;
using WOTR.BoardGame.Interfaces;

namespace WOTR.BoardGame.Components
{
    [DisplayName("Faction Unit")]
    [CreateAssetMenu(fileName = nameof(FactionUnitBoardGameObjectComponent), menuName = "WOTR/Board Game/Board Game Object Components/Faction Unit")]
    public class FactionUnitBoardGameObjectComponent : BoardGameObjectComponent, IBoardGameObjectUnit
    {
        #region Properties and Fields

        [SerializeField] private UnitType unitType;
        [SerializeField] private bool reAddToPoolOnDeath;

        #endregion

        public UnitType GetUnitType(Instance instance)
        {
            return unitType;
        }
    }
}
