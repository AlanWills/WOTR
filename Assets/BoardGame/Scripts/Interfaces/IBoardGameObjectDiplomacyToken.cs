using Celeste.Components;
using Celeste.Events;
using UnityEngine.Events;

namespace WOTR.BoardGame
{
    public interface IBoardGameObjectDiplomacyToken
    {
        bool IsActive(Instance instance);
        int GetDiplomacyStatus(Instance instance);

        void AddDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback);
        void RemoveDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback);
    }
}
