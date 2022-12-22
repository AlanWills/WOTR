using UnityEditor;
using UnityEngine;
using WOTR.BoardGame.UI;

namespace WOTREditor.BoardGame.UI
{
    [CustomEditor(typeof(TerritoryLayoutContainer))]
    public class TerritoryLayoutContainerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Find Anchors"))
            {
                (target as TerritoryLayoutContainer).FindAnchors();
            }

            base.OnInspectorGUI();
        }
    }
}
