using Celeste.DeckBuilding;
using Celeste.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace WOTR.BoardGame.UI
{
    [AddComponentMenu("WOTR/Board Game/UI/Card Detail UI Controller")]
    public class CardDetailUIController : MonoBehaviour
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

        #region Callbacks

        public void OnShowMoreCardDetail(CardRuntime card)
        {
            image.sprite = card.CardFront;
            cardDetailUIRoot.SetActive(true);
        }

        public void OnHideMoreCardDetail()
        {
            cardDetailUIRoot.SetActive(false);
        }

        #endregion
    }
}
