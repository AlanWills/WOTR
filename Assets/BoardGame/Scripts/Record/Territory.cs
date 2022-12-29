using System;
using System.Collections.Generic;
using UnityEngine;
using WOTR.BoardGame.Interfaces;

namespace WOTR.BoardGame
{
    [CreateAssetMenu(fileName = nameof(Territory), menuName = "WOTR/Board Game/Territory")]
    public class Territory : ScriptableObject
    {
        #region Utility Structs

        private class ArmyInfo
        {
            public int numSoldiers;
            public int numElites;
            public int numLeaders;
        }

        #endregion

        #region Properties and Fields

        public string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        [SerializeField] private string displayName;

        [NonSerialized] private Dictionary<int, ArmyInfo> armyInfoLookup = new Dictionary<int, ArmyInfo>();

        #endregion

        public void AddUnits(int ownerGuid, UnitType unitType, int unitQuantity)
        {
            if (!armyInfoLookup.TryGetValue(ownerGuid, out ArmyInfo info))
            {
                info = new ArmyInfo();
                armyInfoLookup.Add(ownerGuid, info);
            }

            switch (unitType)
            {
                case UnitType.Soldier:
                    info.numSoldiers += unitQuantity;
                    break;

                case UnitType.Elite:
                    info.numElites += unitQuantity;
                    break;

                case UnitType.Leader:
                    info.numLeaders += unitQuantity;
                    break;
            }
        }

        public void AddUnits(Faction faction, UnitType unitType, int unitQuantity)
        {
            AddUnits(faction.Guid, unitType, unitQuantity);
        }

        public void AddUnit(int factionGuid, UnitType unitType)
        {
            AddUnits(factionGuid, unitType, 1);
        }

        public void AddUnit(Faction faction, UnitType unitType)
        {
            AddUnits(faction.Guid, unitType, 1);
        }

        public void RemoveUnits(int factionGuid, UnitType unitType, int unitQuantity)
        {
            UnityEngine.Debug.Assert(armyInfoLookup.ContainsKey(factionGuid), $"Failed to find faction army info in territory {displayName} for faction {factionGuid}.");
            if (armyInfoLookup.TryGetValue(factionGuid, out ArmyInfo info))
            {
                switch (unitType)
                {
                    case UnitType.Soldier:
                        info.numSoldiers -= unitQuantity;
                        break;

                    case UnitType.Elite:
                        info.numElites -= unitQuantity;
                        break;

                    case UnitType.Leader:
                        info.numLeaders -= unitQuantity;
                        break;
                }
            }
        }

        public void RemoveUnits(Faction faction, UnitType unitType, int unitQuantity)
        {
            RemoveUnits(faction.Guid, unitType, unitQuantity);
        }

        public void RemoveUnit(int factionGuid, UnitType unitType)
        {
            RemoveUnits(factionGuid, unitType, 1);
        }

        public void RemoveUnit(Faction faction, UnitType unitType)
        {
            RemoveUnits(faction.Guid, unitType, 1);
        }
    }
}
