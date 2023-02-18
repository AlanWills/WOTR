using Celeste.FSM.Nodes.Events;
using WOTR.BoardGame.Events;

namespace WOTR.BoardGame.Nodes
{
    [CreateNodeMenu("WOTR/Events/Raisers/Move Matching Dice Event Raiser")]
    public class MoveMatchingDiceEventRaiserNode : ParameterisedEventRaiserNode<MoveMatchingDiceArgs, MoveMatchingDiceEvent>
    {
    }
}
