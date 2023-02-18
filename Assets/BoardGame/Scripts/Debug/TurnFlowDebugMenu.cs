using Celeste;
using Celeste.Debug.Menus;
using Celeste.DeckBuilding.Decks;
using Celeste.DeckBuilding.Events;
using UnityEngine;

namespace WOTR.BoardGame.Debug
{
    [CreateAssetMenu(fileName = nameof(TurnFlowDebugMenu), menuName = "WOTR/Board Game/Debug/Turn Flow Debug Menu")]
    public class TurnFlowDebugMenu : DebugMenu
    {
        #region Properties and Fields

        private const string PHASE_1 = "Phase 1 - Action Dice and Event Cards";
        private const string PHASE_2 = "Phase 2 - Fellowship Phase";
        private const string PHASE_3 = "Phase 3 - Hunt Allocation";
        private const string PHASE_4 = "Phase 4 - Action Roll";

        [Header(PHASE_1)]
        [SerializeField] private Deck freePeoplesCharacterEvents;
        [SerializeField] private Deck freePeoplesArmyEvents;
        [SerializeField] private Deck sauronCharacterEvents;
        [SerializeField] private Deck sauronArmyEvents;
        [SerializeField] private Celeste.Events.Event beginNewTurnEvent;
        [SerializeField] private Celeste.Events.Event resetFreePeoplesDicePositionsEvent;
        [SerializeField] private Celeste.Events.Event resetSauronDicePositionsEvent;
        [SerializeField] private DrawCardsFromDeckEvent drawFreePeoplesCardEvent;
        [SerializeField] private DrawCardsFromDeckEvent drawSauronCardEvent;

        [Header(PHASE_2)]
        [SerializeField] private Celeste.Events.Event beginFellowshipPhaseEvent;
        [SerializeField] private Celeste.Events.Event endFellowshipPhaseEvent;

        [Header(PHASE_3)]
        [SerializeField] private Celeste.Events.Event beginHuntAllocationEvent;
        [SerializeField] private Celeste.Events.Event endHuntAllocationEvent;

        [Header(PHASE_4)]
        [SerializeField] private Celeste.Events.Event rollAllFreePeoplesDiceEvent;
        [SerializeField] private Celeste.Events.Event rollAllSauronDiceEvent;
        [SerializeField] private Celeste.Events.Event moveSauronEyeDiceToHuntBoxEvent;

        #endregion

        protected override void OnDrawMenu()
        {
            if (GUILayout.Button("Begin New Turn"))
            {
                beginNewTurnEvent.Invoke();
            }

            GUILayout.Space(10);
            GUILayout.Label(PHASE_1, GUI.skin.label.New().Bold());

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Reset FP Dice"))
                {
                    resetFreePeoplesDicePositionsEvent.Invoke();
                }

                if (GUILayout.Button("Reset Sauron Dice"))
                {
                    resetSauronDicePositionsEvent.Invoke();
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Draw FP Events"))
                {
                    drawFreePeoplesCardEvent.Invoke(new DrawCardsFromDeckArgs()
                    {
                        deck = freePeoplesCharacterEvents,
                        quantity = 1
                    });

                    drawFreePeoplesCardEvent.Invoke(new DrawCardsFromDeckArgs()
                    {
                        deck = freePeoplesArmyEvents,
                        quantity = 1
                    });
                }

                if (GUILayout.Button("Draw Sauron Events"))
                {
                    drawSauronCardEvent.Invoke(new DrawCardsFromDeckArgs()
                    {
                        deck = sauronCharacterEvents,
                        quantity = 1
                    });

                    drawSauronCardEvent.Invoke(new DrawCardsFromDeckArgs()
                    {
                        deck = sauronArmyEvents,
                        quantity = 1
                    });
                }
            }

            GUILayout.Space(10);
            GUILayout.Label(PHASE_2, GUI.skin.label.New().Bold());

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Begin Fellowship Phase"))
                {
                    beginFellowshipPhaseEvent.Invoke();
                }

                if (GUILayout.Button("End Fellowship Phase"))
                {
                    endFellowshipPhaseEvent.Invoke();
                }
            }

            GUILayout.Space(10);
            GUILayout.Label(PHASE_3, GUI.skin.label.New().Bold());

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Begin Hunt Allocation"))
                {
                    beginHuntAllocationEvent.Invoke();
                }

                if (GUILayout.Button("End Hunt Allocation"))
                {
                    endHuntAllocationEvent.Invoke();
                }
            }

            GUILayout.Space(10);
            GUILayout.Label(PHASE_4, GUI.skin.label.New().Bold());

            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Roll All FP Dice"))
                {
                    rollAllFreePeoplesDiceEvent.Invoke();
                }

                if (GUILayout.Button("Roll All Sauron Dice"))
                {
                    rollAllSauronDiceEvent.Invoke();
                }

                if (GUILayout.Button("Move Eye Dice"))
                {
                    moveSauronEyeDiceToHuntBoxEvent.Invoke();
                }
            }
        }
    }
}
