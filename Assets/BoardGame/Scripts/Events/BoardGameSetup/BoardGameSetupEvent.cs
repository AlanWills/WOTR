using System;
using UnityEngine;
using UnityEngine.Events;
using Celeste.Events;
using Celeste.BoardGame.Runtime;

namespace WOTR.BoardGame.Events
{
    [Serializable]
    public struct BoardGameSetupArgs
    {
        public BoardGameSetup boardGameSetup;
        public BoardGameRuntime boardGameRuntime;
    }

    [Serializable]
	public class BoardGameSetupUnityEvent : UnityEvent<BoardGameSetupArgs> { }
	
	[Serializable]
	[CreateAssetMenu(fileName = nameof(BoardGameSetupEvent), menuName = "WOTR/Events/Board Game/Board Game Setup")]
	public class BoardGameSetupEvent : ParameterisedEvent<BoardGameSetupArgs>
	{
	}
}
