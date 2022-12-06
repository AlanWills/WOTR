using Celeste.Objects;
using UnityEngine;

namespace WOTR.BoardGame.Catalogue
{
    [CreateAssetMenu(fileName = nameof(WOTRPlayerRecordCatalogue), menuName = "WOTR/Board Game/Catalogue/WOTR Player Record Catalogue")]
    public class WOTRPlayerRecordCatalogue : ListScriptableObject<WOTRPlayerRecord>
    {
        public WOTRPlayerRecord FindByGuid(int guid)
        {
            return FindItem(x => x.Guid == guid);
        }
    }
}
