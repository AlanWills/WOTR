using Celeste.FSM.Nodes.Events.Conditions;
using System.ComponentModel;
using WOTR.BoardGame.Events;

namespace WOTR.FSM.Nodes.Events.Conditions
{
    [DisplayName("Board Game Setup")]
    public class BoardGameSetupEventCondition : ParameterizedEventCondition<BoardGameSetupArgs, BoardGameSetupEvent>
    {
    }
}
