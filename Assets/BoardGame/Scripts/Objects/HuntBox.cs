using Celeste.Parameters;
using UnityEngine;

namespace WOTR.BoardGame.Objects
{
    [CreateAssetMenu(fileName = nameof(HuntBox), menuName = "WOTR/Board Game/Objects/Hunt Box")]
    public class HuntBox : ScriptableObject
    {
        #region Properties and Fields

        public string Location => location;
        public int NumDiceInHuntBox => numDiceInHuntBox.Value;

        [SerializeField] private string location;
        [SerializeField] private IntValue numDiceInHuntBox;
        [SerializeField] private Celeste.Events.Event onChanged;

        #endregion

        public void AddDiceToHuntBox()
        {
            AddDiceToHuntBox(1);
        }

        public void AddDiceToHuntBox(int numDiceToAdd)
        {
            numDiceInHuntBox.Value += numDiceToAdd;
            onChanged.Invoke();
        }

        public void RemoveDiceFromHuntBox()
        {
            if (numDiceInHuntBox.Value > 0)
            {
                --numDiceInHuntBox.Value;
                onChanged.Invoke();
            }
        }

        public void RemoveAllDiceFromHuntBox()
        {
            if (numDiceInHuntBox != null)
            {
                numDiceInHuntBox.Value = 0;
                onChanged.Invoke();
            }
        }
    }
}
