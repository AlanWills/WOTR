using Celeste.DeckBuilding;
using Celeste.Tools;
using Celeste.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WOTR.BoardGame.UI
{
    public class CardDetailContext : IDetailContext
    {
        public CardRuntime card;

        public CardDetailContext(CardRuntime card)
        {
            this.card = card;
        }
    }

    [AddComponentMenu("WOTR/Board Game/UI/Card Detail UI Controller")]
    public class CardDetailUIController : DetailUIController
    {
        #region Properties and Fields

        [SerializeField] private GameObject cardDetailUIRoot;
        [SerializeField] private Image image;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (cardDetailUIRoot == null)
            {
                cardDetailUIRoot = gameObject;
            }

            this.TryGetInChildren(ref image);
        }

        #endregion

        #region Detail UI Controller

        public override bool IsValidFor(IDetailContext detailContext)
        {
            return detailContext is CardDetailContext;
        }

        public override void Show(IDetailContext detailContext)
        {
            CardDetailContext cardDetailContext = detailContext as CardDetailContext;
            image.sprite = cardDetailContext.card.CardFront;
            cardDetailUIRoot.SetActive(true);
        }

        public override void Hide()
        {
            cardDetailUIRoot.SetActive(false);
        }

        #endregion
    }
}
