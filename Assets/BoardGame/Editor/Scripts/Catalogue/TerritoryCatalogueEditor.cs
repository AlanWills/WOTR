using CelesteEditor.Objects;
using UnityEditor;
using WOTR.BoardGame;
using WOTR.BoardGame.Catalogue;

namespace WOTREditor.BoardGame.Catalogue
{
    [CustomEditor(typeof(TerritoryCatalogue))]
    public class TerritoryCatalogueEditor : DictionaryScriptableObjectEditor<string, Territory>
    {
        protected override string GetKey(Territory item)
        {
            return item.name;
        }
    }
}
