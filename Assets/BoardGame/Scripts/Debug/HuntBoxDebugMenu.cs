using Celeste.Debug.Menus;
using UnityEngine;
using WOTR.BoardGame.Objects;
using static UnityEngine.GUILayout;

namespace WOTR.BoardGame.Debug
{
    [CreateAssetMenu(fileName = nameof(HuntBoxDebugMenu), menuName = "WOTR/Board Game/Debug/Hunt Box Debug Menu")]
    public class HuntBoxDebugMenu : DebugMenu
    {
        #region Properties and Fields

        [SerializeField] private HuntBox huntBox;

        #endregion

        protected override void OnDrawMenu()
        {
            using (new HorizontalScope())
            {
                Label($"Num Dice In Box: {huntBox.NumDiceInHuntBox}");

                if (Button("+", ExpandWidth(false)))
                {
                    huntBox.AddDiceToHuntBox();
                }

                if (Button("-", ExpandWidth(false)))
                {
                    huntBox.RemoveDiceFromHuntBox();
                }

                if (Button("Empty", ExpandWidth(false)))
                {
                    huntBox.RemoveAllDiceFromHuntBox();
                }
            }
        }
    }
}
