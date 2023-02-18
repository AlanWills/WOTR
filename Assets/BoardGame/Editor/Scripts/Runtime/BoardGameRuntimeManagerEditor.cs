using CelesteEditor.Persistence;
using UnityEditor;
using WOTR.BoardGame.Managers;

namespace WOTREditor.BoardGame.Runtime
{
    [CustomEditor(typeof(BoardGameRuntimeManager))]
    public class BoardGameRuntimeManagerEditor : IPersistentSceneManagerEditor
    {
    }
}
