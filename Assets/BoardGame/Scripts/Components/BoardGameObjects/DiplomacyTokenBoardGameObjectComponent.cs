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
    public class DiplomacyTokenBoardGameObjectComponent : BoardGameObjectComponent, IBoardGameObjectToken, IBoardGameObjectTooltip
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

        public void SetFaceUp(Instance instance, bool isFaceUp)
        {
            faction.IsActive = isFaceUp;
        }

        public bool IsFaceUp(Instance instance)
        {
            return faction.IsActive;
        }

        public void Flip(Instance instance)
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
