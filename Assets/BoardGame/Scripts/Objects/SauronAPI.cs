using Celeste.BoardGame.Interfaces;
using Celeste.BoardGame.Objects;
using Celeste.BoardGame.Runtime;
using Celeste.BoardGame.UI;
using Celeste.Events;
using UnityEngine;

namespace WOTR.BoardGame.Objects
{
    [CreateAssetMenu(fileName = nameof(SauronAPI), menuName = "WOTR/Board Game/Objects/Sauron API")]
    public class SauronAPI : ScriptableObject
    {
        #region Properties and Fields

        [SerializeField] private Dice dice;
        [SerializeField] private int eyeDiceValue = 6;
        [SerializeField] private HuntBox huntBox;

        #endregion

        public void MoveAllEyeDiceToHuntBox()
        {
            for (int i = 0, n = dice.NumDice; i < n; ++i)
            {
                BoardGameObjectRuntime die = dice.GetDie(i);

                die.TryFindComponent<IBoardGameObjectDie>(out var dieComponent);

                if (dieComponent.iFace.GetValue(dieComponent.instance) == eyeDiceValue)
                {
                    MoveToHuntBox(die);
                }
            }
        }

        public void MoveToHuntBox(BoardGameObjectRuntime runtime)
        {
            dice.MoveDiceTo(runtime, huntBox.Location);
            huntBox.AddDiceToHuntBox();
        }

        public void MoveToHuntBox(GameObjectClickEventArgs args)
        {
            BoardGameObjectUIController uiController = args.gameObject.GetComponent<BoardGameObjectUIController>();

            if (uiController != null)
            {
                MoveToHuntBox(uiController.BoardGameObjectRuntime);
            }
        }
    }
}
