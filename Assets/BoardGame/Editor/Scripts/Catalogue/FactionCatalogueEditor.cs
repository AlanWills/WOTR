using CelesteEditor.DataStructures;
using UnityEditor;
using WOTR.BoardGame;
using WOTR.BoardGame.Catalogue;

namespace WOTREditor.BoardGame.Catalogue
{
    [CustomEditor(typeof(FactionCatalogue))]
    public class FactionCatalogueEditor : IIndexableItemsEditor<Faction>
    {
    }
}
