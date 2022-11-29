using Celeste.DeckBuilding;
using Celeste.DeckBuilding.Cards;
using Celeste.DeckBuilding.Decks;
using Celeste.DeckBuilding.Events;
using Celeste.DeckBuilding.Extensions;
using UnityEngine;

namespace WOTR.BoardGame.Runtime
{
    [AddComponentMenu("WOTR/Board Game/Runtime/Player Event Card Decks Runtime")]
    public class PlayerEventCardDecksRuntime : MonoBehaviour
    {
        #region Properties and Fields

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;

                if (isActive)
                {
                    becameActiveEvent.Invoke();
                }
                else
                {
                    becameInactiveEvent.Invoke();
                }
            }
        }

        public CurrentHand CurrentHand => currentHand;

        [Header("Deck Elements")]
        [SerializeField] private CurrentHand currentHand;
        [SerializeField] private Stage stage;

        [Header("Events")]
        [SerializeField] private Celeste.Events.Event becameActiveEvent;
        [SerializeField] private Celeste.Events.Event becameInactiveEvent;
        [SerializeField] private CardRuntimeEvent cardPlayedEvent;

        private Deck characterEventCardsDeck = default;
        private Deck armyEventCardsDeck = default;
        private bool isActive = false;

        #endregion

        public void Hookup(Deck characterEventCards, Deck armyEventCards)
        {
        }

        #region Card Management

        private void UpdateCanPlayCards()
        {
            for (int i = 0, n = CurrentHand.NumCards; i < n; ++i)
            {
                UpdateCanPlayCard(CurrentHand.GetCard(i));
            }
        }

        private void UpdateCanPlayCard(CardRuntime card)
        {
            card.CanPlay = !card.HasCardStatus(CardStatus.CannotPlay);
        }

        #endregion

        #region Callbacks

        public void OnCardAddedToHand(CardRuntime cardRuntime)
        {
            cardRuntime.OnPlayCardSuccess.AddListener(OnPlayCardSuccess);
        }

        public void OnCardRemovedFromHand(CardRuntime cardRuntime)
        {
            cardRuntime.OnPlayCardSuccess.RemoveListener(OnPlayCardSuccess);
        }

        public void OnResourcesChanged(int newResources)
        {
            UpdateCanPlayCards();
        }

        private void OnCostChanged(CostChangedArgs costChangedArgs)
        {
            UpdateCanPlayCard(costChangedArgs.card);
        }

        private void OnPlayCardSuccess(PlayCardSuccessArgs playArgs)
        {
            CardRuntime card = playArgs.cardRuntime;

            CurrentHand.RemoveCard(card);

            if (card.IsRemovedFromDeckWhenPlayed())
            {
                //deck.AddCardToRemovedPile(card);
            }
            else
            {
                //deck.AddCardToDiscardPile(card);
            }

            cardPlayedEvent.Invoke(card);
        }

        #endregion
    }
}
