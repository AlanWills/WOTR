using Celeste.BoardGame.UI;
using Celeste.Events;
using Celeste.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using static PlasticGui.LaunchDiffParameters;

namespace WOTR.BoardGame.UI
{
    [AddComponentMenu("WOTR/Board Game/UI/Diplomacy Token Layout Container")]
    public class DiplomacyTokenLayoutContainer : MonoBehaviour, ILayoutContainer
    {
        #region Properties and Fields

        [SerializeField] private List<Transform> diplomacyStatusContainers = new List<Transform>();

        [NonSerialized] private List<BoardGameObjectUIController> diplomacyTokenControllers = new List<BoardGameObjectUIController>();

        #endregion

        #region Unity Methods

        private void OnDisable()
        {
            foreach (var controller in diplomacyTokenControllers)
            {
                if (controller.BoardGameObjectRuntime.TryFindComponent<IBoardGameObjectDiplomacyToken>(out var diplomacy))
                {
                    diplomacy.iFace.RemoveDiplomacyStatusChangedCallback(OnDiplomacyStatusChanged);
                }
            }
        }

        #endregion

        public void OnChildAdded(GameObject gameObject)
        {
            BoardGameObjectUIController uiController = gameObject.GetComponent<BoardGameObjectUIController>();

            if (uiController != null && 
                uiController.BoardGameObjectRuntime.TryFindComponent<IBoardGameObjectDiplomacyToken>(out var diplomacy))
            {
                diplomacy.iFace.AddDiplomacyStatusChangedCallback(OnDiplomacyStatusChanged);
                diplomacyTokenControllers.Add(uiController);
            }

            Layout();
        }

        private void Layout()
        {
            for (int i = 0, n = diplomacyTokenControllers.Count; i < n; ++i)
            {
                var diplomacyTokenController = diplomacyTokenControllers[i];
                diplomacyTokenController.BoardGameObjectRuntime.TryFindComponent<IBoardGameObjectDiplomacyToken>(out var diplomacyToken);
                
                int diplomacyStatus = diplomacyToken.iFace.GetDiplomacyStatus(diplomacyToken.instance);

                if (diplomacyStatus < diplomacyStatusContainers.Count)
                {
                    // Add each token to the appropriate container based on it's diplomacy status and notify them of that
                    diplomacyTokenController.transform.SetParent(diplomacyStatusContainers[diplomacyStatus]);
                    diplomacyStatusContainers[diplomacyStatus].GetComponent<ILayoutContainer>().OnChildAdded(diplomacyTokenController.gameObject);
                }
            }
        }

        private void OnDiplomacyStatusChanged(ValueChangedArgs<int> args)
        {
            Layout();
        }
    }
}
