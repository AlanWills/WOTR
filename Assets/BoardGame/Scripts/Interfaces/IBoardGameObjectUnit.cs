using Celeste.Components;
using System;

namespace WOTR.BoardGame.Interfaces
{
    [Serializable]
    public enum UnitType
    {
        Soldier,
        Elite,
        Leader
    }

    public interface IBoardGameObjectUnit
    {
        UnitType GetUnitType(Instance instance);
    }
}
