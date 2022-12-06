using Celeste.DeckBuilding;
using Celeste.DeckBuilding.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WOTR.BoardGame.UI
{
    [AddComponentMenu("WOTR/Board Game/UI/Event Card UI Controller")]
    public class EventCardUIController : MonoBehaviour, ICardUIController, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Properties and Fields

        [SerializeField] private Image image;

        private CardRuntime card;
        private bool canPlay = false;
        private bool dragging = false;
        private Vector2 startDragLocalPosition;

        #endregion

        #region Unity Methods

        private void OnDisable()
        {
            if (card != null)
            {
                card.OnCanPlayChanged.RemoveListener(OnCanPlayChanged);
            }
        }

        #endregion

        #region ICardUIController

        public void Hookup(CardRuntime card)
        {
            this.card = card;
            this.card.OnCanPlayChanged.AddListener(OnCanPlayChanged);

            image.sprite = card.CardFront;

            UpdateCanPlay(card.CanPlay);
        }

        public bool IsForCard(CardRuntime card)
        {
            return this.card == card;
        }

        #endregion

        #region UI

        private void UpdateCanPlay(bool newCanPlay)
        {
            canPlay = newCanPlay;
        }

        #endregion

        #region IDrag

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (canPlay)
            {
                dragging = true;
                startDragLocalPosition = transform.localPosition;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragging)
            {
                transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragging)
            {
                dragging = false;

                if (transform.localPosition.y <= 150 || !card.TryPlay())
                {
                    // Reset the card position
                    transform.localPosition = startDragLocalPosition;
                }
            }
        }

        #endregion

        #region Callbacks

        private void OnCanPlayChanged(bool newCanPlay)
        {
            UpdateCanPlay(newCanPlay);
        }

        #endregion
    }
}
