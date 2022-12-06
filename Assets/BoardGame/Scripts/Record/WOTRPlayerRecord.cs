using Celeste.Components;
using Celeste.DeckBuilding;
using Celeste.DeckBuilding.Cards;
using Celeste.DeckBuilding.Catalogue;
using Celeste.DeckBuilding.Decks;
using Celeste.DeckBuilding.Extensions;
using Celeste.DeckBuilding.Persistence;
using Celeste.Objects;
using UnityEngine;
using WOTR.BoardGame.Persistence;

namespace WOTR.BoardGame
{
    [CreateAssetMenu(fileName = nameof(WOTRPlayerRecord), menuName = "WOTR/Board Game/Record/WOTR Player Record")]
    public class WOTRPlayerRecord : ScriptableObject, IGuid
    {
        #region Properties and Fields

        public int Guid
        {
            get { return guid; }
            set
            {
                guid = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

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

        public int NumCardsInHand => currentHand.NumCards;
        public int NumCardsOnStage => stage.NumCards;

        [SerializeField] private int guid;

        [Header("Cards")]
        [SerializeField] private CurrentHand currentHand;
        [SerializeField] private Stage stage;

        [Header("Events")]
        [SerializeField] private Celeste.Events.Event becameActiveEvent;
        [SerializeField] private Celeste.Events.Event becameInactiveEvent;
        [SerializeField] private Celeste.Events.Event save;

        private bool isActive = false;

        #endregion

        public void Load(
            WOTRPlayerRecordDTO dto, 
            DeckCatalogue deckCatalogue, 
            CardCatalogue cardCatalogue)
        {
            foreach (CardRuntimeDTO cardDTO in dto.cardsInHand)
            {
                Card card = cardCatalogue.FindByGuid(cardDTO.cardGuid);
                Debug.Assert(card != null, $"Could not find card with guid {cardDTO.cardGuid} in card catalogue.");
                
                Deck deck = deckCatalogue.FindByGuid(cardDTO.deckGuid);
                Debug.Assert(deck != null, $"Could not find deck with guid {cardDTO.deckGuid} in deck catalogue.");

                if (card != null && deck != null)
                {
                    CardRuntime cardRuntime = new CardRuntime(deck, card);
                    cardRuntime.LoadComponents(cardDTO.components.ToLookup());
                    currentHand.AddCard(cardRuntime);
                }
            }

            foreach (CardRuntimeDTO cardDTO in dto.cardsOnStage)
            {
                Card card = cardCatalogue.FindByGuid(cardDTO.cardGuid);
                Debug.Assert(card != null, $"Could not find card with guid {cardDTO.cardGuid} in card catalogue.");

                Deck deck = deckCatalogue.FindByGuid(cardDTO.deckGuid);
                Debug.Assert(deck != null, $"Could not find deck with guid {cardDTO.deckGuid} in deck catalogue.");

                if (card != null && deck != null)
                {
                    CardRuntime cardRuntime = new CardRuntime(deck, card);
                    cardRuntime.LoadComponents(cardDTO.components.ToLookup());
                    stage.AddCard(cardRuntime);
                }
            }
        }

        #region Card Management

        private void UpdateCanPlayCards()
        {
            for (int i = 0, n = currentHand.NumCards; i < n; ++i)
            {
                UpdateCanPlayCard(currentHand.GetCard(i));
            }
        }

        private void UpdateCanPlayCard(CardRuntime card)
        {
            card.CanPlay = !card.HasCardStatus(CardStatus.CannotPlay);
        }

        #endregion
        
        public CardRuntime GetCardFromHand(int index)
        {
            return currentHand.GetCard(index);
        }

        public CardRuntime GetCardFromStage(int index)
        {
            return stage.GetCard(index);
        }
    }
}
