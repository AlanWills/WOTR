using Celeste.Debug.Menus;
using Celeste.Events;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WOTR.BoardGame.Debug
{
    [CreateAssetMenu(fileName = nameof(FactionsDebugMenu), menuName = "WOTR/Board Game/Debug/Factions Debug Menu")]
    public class FactionsDebugMenu : DebugMenu
    {
        #region Properties and Fields

        [SerializeField] private List<Faction> factions = new List<Faction>();

        [Header("Events")]
        [SerializeField] private IntEvent addFactionToGameEvent;

        [NonSerialized] private List<bool> factionsVisible = new List<bool>();

        #endregion

        protected override void OnDrawMenu()
        {
            SynchronizeVisibility();

            for (int i = 0, n = factions.Count; i < n; ++i)
            {
                factionsVisible[i] = DrawFactionDebugMenu(factions[i], factionsVisible[i]);
            }
        }

        private bool DrawFactionDebugMenu(Faction faction, bool isVisible)
        {
            if (GUILayout.Button(faction.DisplayName))
            {
                isVisible = !isVisible;
            }

            if (isVisible)
            {
                using (var horizontal = new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Add To Game", GUILayout.ExpandWidth(false)))
                    {
                        addFactionToGameEvent.Invoke(faction.Guid);
                    }
                    
                    faction.IsActive = GUILayout.Toggle(faction.IsActive, "Is Active");
                }

                GUILayout.Label("Diplomacy Status");

                using (var horizontal = new GUILayout.HorizontalScope())
                {
                    var normalStyle = new GUIStyle(GUI.skin.button);
                    var highlightedStyle = new GUIStyle(GUI.skin.button);
                    highlightedStyle.normal = highlightedStyle.active;

                    for (int i = 0;  i < 4; ++i)
                    {
                        DrawDiplomacyButton(faction, i, normalStyle, highlightedStyle);
                    }
                }

                faction.AvailableSoldiers = DrawUnitCountField(faction.AvailableSoldiers, "Available Soldiers");
                faction.AvailableElites = DrawUnitCountField(faction.AvailableElites, "Available Elites");
                faction.AvailableLeaders = DrawUnitCountField(faction.AvailableLeaders, "Available Leaders");
                faction.RemovedSoldiers = DrawUnitCountField(faction.RemovedSoldiers, "Removed Soldiers");
                faction.RemovedElites = DrawUnitCountField(faction.RemovedElites, "Removed Elites");
                faction.RemovedLeaders = DrawUnitCountField(faction.RemovedLeaders, "Removed Leaders");
            }

            return isVisible;
        }

        private void SynchronizeVisibility()
        {
            if (factionsVisible.Count != factions.Count)
            {
                factionsVisible.Clear();

                for (int i = 0, n = factions.Count; i < n; i++)
                {
                    factionsVisible.Add(false);
                }
            }
        }

        private void DrawDiplomacyButton(
            Faction faction,
            int diplomacyLevel,
            GUIStyle normalStyle,
            GUIStyle highlightedStyle)
        {
            if (GUILayout.Button(diplomacyLevel.ToString(), faction.DiplomacyStatus == diplomacyLevel ? highlightedStyle : normalStyle))
            {
                faction.DiplomacyStatus = diplomacyLevel;
            }
        }

        private int DrawUnitCountField(int availableUnits, string label)
        {
            using (var horizontal = new GUILayout.HorizontalScope())
            {
                GUILayout.Label($"{label}: {availableUnits}");

                if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
                {
                    ++availableUnits;
                }

                if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
                {
                    --availableUnits;
                }
            }

            return availableUnits;
        }
    }
}
