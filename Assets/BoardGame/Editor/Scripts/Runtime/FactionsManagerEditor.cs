using CelesteEditor.Persistence;
using UnityEditor;
using WOTR.BoardGame.Runtime;

namespace WOTREditor.BoardGame.Runtime
{
    [CustomEditor(typeof(FactionsManager))]
    public class FactionsManagerEditor : IPersistentSceneManagerEditor
    {
    }
}
