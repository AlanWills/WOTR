using UnityEditor;
using UnityEngine;
using WOTR.BoardGame.Map;

namespace WOTREditor.BoardGame.Map
{
    [CustomEditor(typeof(TerritoryTopology))]
    public class TerritoryTopologyEditor : Editor
    {
        private SerializedProperty expandedTerritoryConnectionsProperty;

        private void OnEnable()
        {
            expandedTerritoryConnectionsProperty = serializedObject.FindProperty("expandedTerritoryConnections");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            using (var disabled = new EditorGUI.DisabledScope(true))
            {
                DrawPropertiesExcluding(serializedObject, "m_Name", "expandedTerritoryConnections");
            }

            EditorGUILayout.PropertyField(expandedTerritoryConnectionsProperty);

            serializedObject.ApplyModifiedProperties();

            TerritoryTopology territoryTopology = target as TerritoryTopology;

            using (var horizontal = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Apply Symmetrically"))
                {
                    territoryTopology.ApplyExpandedTerritoryConnectionsSymmetrically();
                }

                if (GUILayout.Button("Collapse Expanded Territories"))
                {
                    territoryTopology.CollapseExpandedTerritoryConnections();
                }
            }
        }
    }
}
