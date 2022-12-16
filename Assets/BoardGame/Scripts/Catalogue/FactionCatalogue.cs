using Celeste.Objects;
using UnityEngine;

namespace WOTR.BoardGame.Catalogue
{
    [CreateAssetMenu(fileName = nameof(FactionCatalogue), menuName = "WOTR/Board Game/Catalogue/Faction Catalogue")]
    public class FactionCatalogue : ListScriptableObject<Faction>
    {
        public Faction FindByGuid(int guid)
        {
            return FindItem(x => x.Guid == guid);
        }
    }
}
