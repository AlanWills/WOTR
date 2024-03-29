﻿using Celeste.BoardGame.Components;
using Celeste.Components;
using Celeste.Constants;
using System.ComponentModel;
using UnityEngine;
using WOTR.BoardGame.Interfaces;

namespace WOTR.BoardGame.Components
{
    [DisplayName("Static Owner")]
    [CreateAssetMenu(fileName = nameof(StaticOwnerBoardGameObjectComponent), menuName = "WOTR/Board Game/Board Game Object Components/Static Owner")]
    public class StaticOwnerBoardGameObjectComponent : BoardGameObjectComponent, IBoardGameObjectOwner
    {
        #region Properties and Fields

        [SerializeField] private ID owner;

        #endregion

        public int GetOwnerGuid(Instance instance)
        {
            return owner.GetHashCode();
        }

        public bool IsOwnedBy(Instance instance, int ownerGuid)
        {
            return owner == ownerGuid;
        }
    }
}
