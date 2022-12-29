using Celeste.Objects;
using UnityEngine;

namespace WOTR.BoardGame.Catalogue
{
    [CreateAssetMenu(fileName = nameof(TerritoryCatalogue), menuName = "WOTR/Board Game/Catalogue/Territory Catalogue")]
    public class TerritoryCatalogue : DictionaryScriptableObject<string, Territory>
    {
    }
}
