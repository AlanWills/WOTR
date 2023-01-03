using UnityEditor;
using UnityEngine;
using WOTR.BoardGame.Map;

namespace WOTREditor.BoardGame.Map
{
    [CustomPropertyDrawer(typeof(TerritoryConnection))]
    public class TerritoryConnectionPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (var propertyScope = new EditorGUI.PropertyScope(position, label, property))
            {
                {
                    Rect territory1Rect = new Rect(position);
                    territory1Rect.width *= 0.5f;

                    SerializedProperty territory1Property = property.FindPropertyRelative(nameof(TerritoryConnection.territory1));
                    EditorGUI.PropertyField(territory1Rect, territory1Property, GUIContent.none);
                }

                {
                    Rect territory2Rect = new Rect(position);
                    territory2Rect.width *= 0.5f;
                    territory2Rect.x += territory2Rect.width;

                    SerializedProperty territory2Property = property.FindPropertyRelative(nameof(TerritoryConnection.territory2));
                    EditorGUI.PropertyField(territory2Rect, territory2Property, GUIContent.none);
                }
            }
        }
    }
}
