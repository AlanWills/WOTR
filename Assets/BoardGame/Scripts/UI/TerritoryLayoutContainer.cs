using Celeste.Events;
using Celeste.UI;
using UnityEngine;

namespace WOTR.BoardGame.UI
{
    [AddComponentMenu("WOTR/Board Game/UI/Territory Layout Container")]
    public class TerritoryLayoutContainer : MonoBehaviour, ILayoutContainer
    {
        #region Properties and Fields

        [SerializeField] private string territoryName;
        [SerializeField] private ShowTooltipEvent showTooltipEvent;
        [SerializeField] private Celeste.Events.Event hideTooltipEvent;

        #endregion

        #region Callbacks

        public void OnChildAdded(GameObject gameObject)
        {
        }

        public void OnMouseEnterTerritory(Vector2 position)
        {
            showTooltipEvent.Invoke(new TooltipArgs()
            {
                isWorldSpace = true,
                position = transform.position,
                text = territoryName
            });
        }

        public void OnMouseExitTerritory()
        {
            hideTooltipEvent.Invoke();
        }

        #endregion
    }
}
