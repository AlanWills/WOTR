using Celeste.Events;
using Celeste.Input;
using Celeste.UI;
using System.Collections.Generic;
using UnityEngine;

namespace WOTR.BoardGame.UI
{
    [AddComponentMenu("WOTR/Board Game/UI/Territory Layout Container")]
    public class TerritoryLayoutContainer : MonoBehaviour, ILayoutContainer
    {
        #region Properties and Fields

        [SerializeField] private Territory territory;
        [SerializeField] private List<Transform> unitAnchors = new List<Transform>();

        [Header("Events")]
        [SerializeField] private ShowTooltipEvent showTooltipEvent;
        [SerializeField] private Celeste.Events.Event hideTooltipEvent;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (!Application.isPlaying && unitAnchors.Count != transform.childCount)
            {
                FindAnchors();
            }
        }

        #endregion

        public void FindAnchors()
        {
            unitAnchors.Clear();

            for (int i = 0, n = transform.childCount; i < n; ++i)
            {
                unitAnchors.Add(transform.GetChild(i));
            }
        }

        #region Callbacks

        public void OnChildAdded(GameObject gameObject)
        {
            for (int i = 0, n = unitAnchors.Count; i < n; i++)
            {
                Transform anchor = unitAnchors[i];

                if (anchor.childCount == 0)
                {
                    gameObject.transform.SetParent(anchor, false);
                    gameObject.transform.position = anchor.position;
                    break;
                }
            }

            // If we get here, we've run out of anchors - maybe modify the UI entirely (aka conglomerate the army/ies into tokens or just have overflow UI for the models that wouldn't fit)
        }
        
        public void OnChildRemoved(GameObject gameObject)
        {
            // No-op
        }

        public void OnMouseEnterTerritory(InputState inputState)
        {
            showTooltipEvent.Invoke(TooltipArgs.AnchoredToMouse(territory.DisplayName));
        }

        public void OnMouseExitTerritory()
        {
            hideTooltipEvent.Invoke();
        }

        #endregion
    }
}
