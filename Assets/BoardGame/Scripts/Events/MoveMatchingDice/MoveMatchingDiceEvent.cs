using System;
using UnityEngine;
using UnityEngine.Events;
using Celeste.Events;
using Celeste.Tools.Attributes.GUI;

namespace WOTR.BoardGame.Events
{
	[Serializable]
	public struct MoveMatchingDiceArgs
	{
		public int value;
		public string newLocation;
	}

	[Serializable]
	public class MoveMatchingDiceUnityEvent : UnityEvent<MoveMatchingDiceArgs> { }
	
	[Serializable]
	[CreateAssetMenu(fileName = nameof(MoveMatchingDiceEvent), menuName = "WOTR/Events/Board Game/Move Matching Dice Event")]
	public class MoveMatchingDiceEvent : ParameterisedEvent<MoveMatchingDiceArgs> 
	{
        [InlineDataInInspector] public MoveMatchingDiceArgs defaultArgs;

        public void InvokeWithDefaultArgs()
        {
            Invoke(defaultArgs);
        }
    }
	
	[Serializable]
	public class GuaranteedMoveMatchingDiceEvent : GuaranteedParameterisedEvent<MoveMatchingDiceEvent, MoveMatchingDiceArgs> { }
}
