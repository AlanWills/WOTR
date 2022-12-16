using Celeste.Components;
using Celeste.Constants;

namespace WOTR.BoardGame.Interfaces
{
    public interface IBoardGameObjectOwner
    {
        ID GetOwner(Instance instance);
    }
}
