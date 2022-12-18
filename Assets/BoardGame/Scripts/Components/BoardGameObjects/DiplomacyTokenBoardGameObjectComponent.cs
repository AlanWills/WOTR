using Celeste.BoardGame;
using Celeste.BoardGame.Components;
using Celeste.BoardGame.Interfaces;
using Celeste.Components;
using Celeste.Events;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace WOTR.BoardGame.Components
{
    [DisplayName("Diplomacy Token")]
    [CreateAssetMenu(fileName = nameof(DiplomacyTokenBoardGameObjectComponent), menuName = "WOTR/Board Game/Board Game Object Components/Diplomacy Token")]
    public class DiplomacyTokenBoardGameObjectComponent : BoardGameObjectComponent, IBoardGameObjectToken, IBoardGameObjectTooltip, IBoardGameObjectDiplomacyToken
    {
        #region Properties and Fields

        [SerializeField] private Faction faction;

        [Header("Sprites")]
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite passiveSprite;

        [Header("Events")]
        [SerializeField] private ShowTooltipEvent showTooltipEvent;
        [SerializeField] private Celeste.Events.Event hideTooltipEvent;

        #endregion

        #region IToken

        void IBoardGameObjectToken.SetFaceUp(Instance instance, bool isFaceUp)
        {
            faction.IsActive = isFaceUp;
        }

        bool IBoardGameObjectToken.IsFaceUp(Instance instance)
        {
            return faction.IsActive;
        }

        void IBoardGameObjectToken.Flip(Instance instance)
        {
            faction.IsActive = !faction.IsActive;
        }

        public Sprite GetSprite(Instance instance)
        {
            return faction.IsActive ? activeSprite : passiveSprite;
        }

        public void AddIsFaceUpChangedCallback(Instance instance, UnityAction<ValueChangedArgs<bool>> callback)
        {
            faction.AddIsActiveChangedCallback(callback);
        }

        public void RemoveIsFaceUpChangedCallback(Instance instance, UnityAction<ValueChangedArgs<bool>> callback)
        {
            faction.RemoveIsActiveChangedCallback(callback);
        }

        #endregion

        #region IDiplomacy

        public bool IsActive(Instance instance)
        {
            return faction.IsActive;
        }

        public int GetDiplomacyStatus(Instance instance)
        {
            return faction.DiplomacyStatus;
        }

        public void AddDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback)
        {
            faction.AddDiplomacyStatusChangedCallback(callback);
        }

        public void RemoveDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback)
        {
            faction.RemoveDiplomacyStatusChangedCallback(callback);
        }

        #endregion

        #region ITooltip

        public void ShowTooltip(Instance instance, Vector3 position, bool isWorldSpace)
        {
            bool isActive = faction.IsActive;
            showTooltipEvent.Invoke(new TooltipArgs()
            {
                isWorldSpace = isWorldSpace,
                position = position,
                text = $"{faction.DisplayName} - {(isActive ? "Active" : "Passive")}"
            });
        }

        public void HideTooltip(Instance instance)
        {
            hideTooltipEvent.Invoke();
        }

        #endregion
    }
}
