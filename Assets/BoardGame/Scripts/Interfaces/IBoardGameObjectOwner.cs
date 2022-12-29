using Celeste.Components;

namespace WOTR.BoardGame.Interfaces
{
    public interface IBoardGameObjectOwner
    {
        int GetOwnerGuid(Instance instance);
        bool IsOwnedBy(Instance instance, int ownerGuid);
    }
}
