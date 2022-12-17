using CelesteEditor.Persistence;
using UnityEditor;
using WOTR.BoardGame.Runtime;

namespace WOTREditor.BoardGame.Runtime
{
    [CustomEditor(typeof(PlayerRecordManager))]
    public class PlayerRecordManagerEditor : IPersistentSceneManagerEditor
    {
    }
}
