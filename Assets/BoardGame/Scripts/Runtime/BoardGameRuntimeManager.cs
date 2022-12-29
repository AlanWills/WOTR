using Celeste.BoardGame;
using Celeste.BoardGame.Persistence;
using Celeste.BoardGame.Runtime;
using Celeste.Components;
using Celeste.Events;
using Celeste.Persistence;
using System;
using UnityEngine;
using WOTR.BoardGame.Events;

namespace WOTR.BoardGame.Runtime
{
    public class BoardGameRuntimeManager : PersistentSceneManager<BoardGameRuntimeManager, BoardGameRuntimeDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "BoardGame.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private Celeste.BoardGame.BoardGame boardGame;
        [SerializeField] private BoardGameSetup boardGameSetup;

        [Header("Events")]
        [SerializeField] private BoardGameSetupEvent onBoardGameSetupEvent;
        [SerializeField] private BoardGameLoadedEvent onBoardGameLoadedEvent;
        [SerializeField] private BoardGameReadyEvent onBoardGameReadyEvent;
        [SerializeField] private BoardGameShutdownEvent onBoardGameShutdownEvent;
        [SerializeField] private BoardGameObjectAddedEvent onBoardGameObjectAddedEvent;

        [NonSerialized] private BoardGameRuntime boardGameRuntime;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Load();
        }

        private void OnDisable()
        {
            onBoardGameShutdownEvent.Invoke(new BoardGameShutdownArgs() { });
            boardGameRuntime.Shutdown();
        }

        #endregion

        #region Save/Load

        protected override BoardGameRuntimeDTO Serialize()
        {
            return new BoardGameRuntimeDTO(boardGameRuntime);
        }

        protected override void Deserialize(BoardGameRuntimeDTO dto)
        {
            LoadCommon(dto);

            onBoardGameLoadedEvent.Invoke(new BoardGameLoadedArgs()
            {
                boardGameRuntime = boardGameRuntime
            });

            onBoardGameReadyEvent.Invoke(new BoardGameReadyArgs()
            {
                boardGameRuntime = boardGameRuntime
            });
        }

        protected override void SetDefaultValues()
        {
            LoadCommon(boardGameSetup.startingBoardGameRuntimeState);

            onBoardGameSetupEvent.Invoke(new BoardGameSetupArgs()
            {
                boardGameSetup = boardGameSetup,
                boardGameRuntime = boardGameRuntime
            });

            onBoardGameReadyEvent.Invoke(new BoardGameReadyArgs()
            {
                boardGameRuntime = boardGameRuntime
            });
        }

        private void LoadCommon(BoardGameRuntimeDTO dto)
        {
            boardGameRuntime = new BoardGameRuntime(boardGame);
            boardGameRuntime.LoadComponents(dto.components.ToLookup());

            foreach (var boardGameObjectRuntimeDTO in dto.boardGameObjectRuntimes)
            {
                boardGameRuntime.AddBoardGameObject(boardGameObjectRuntimeDTO);
            }

            boardGameRuntime.ComponentDataChanged.AddListener(OnBoardGameChanged);
        }

        #endregion

        #region Callbacks

        public void OnAddBoardGameObject(AddBoardGameObjectArgs args)
        {
            BoardGameObject boardGameObject = boardGame.FindBoardGameObject(args.boardGameObjectGuid);
            UnityEngine.Debug.Assert(boardGameObject != null, $"Could not find board game object with guid {args.boardGameObjectGuid}.");
            BoardGameObjectRuntime boardGameObjectRuntime = boardGameRuntime.AddBoardGameObject(boardGameObject);

            if (boardGameObjectRuntime.TryFindComponent<IBoardGameObjectActor>(out var actor))
            {
                actor.iFace.SetCurrentLocationName(actor.instance, args.location);
            }

            onBoardGameObjectAddedEvent.Invoke(new BoardGameObjectAddedArgs()
            {
                boardGameRuntime = boardGameRuntime, 
                boardGameObjectRuntime = boardGameObjectRuntime
            });
        }

        private void OnBoardGameChanged()
        {
            Save();
        }

        #endregion
    }
}
