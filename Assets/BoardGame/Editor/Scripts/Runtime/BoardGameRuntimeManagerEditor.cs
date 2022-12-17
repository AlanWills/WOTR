using Celeste.BoardGame.Persistence;
using CelesteEditor.Persistence;
using UnityEditor;
using WOTR.BoardGame.Runtime;

namespace WOTREditor.BoardGame.Runtime
{
    [CustomEditor(typeof(BoardGameRuntimeManager))]
    public class BoardGameRuntimeManagerEditor : IPersistentSceneManagerEditor
    {
    }
}
