using Celeste.BoardGame;
using Celeste.BoardGame.Persistence;
using Celeste.BoardGame.Runtime;
using Celeste.Components;
using Celeste.Events;
using Celeste.Persistence;
using System;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private UnityEvent<BoardGameSetupArgs> onBoardGameSetup = new UnityEvent<BoardGameSetupArgs>();
        [SerializeField] private UnityEvent<BoardGameLoadedArgs> onBoardGameLoaded = new UnityEvent<BoardGameLoadedArgs>();
        [SerializeField] private UnityEvent<BoardGameRuntime> onBoardGameReady = new UnityEvent<BoardGameRuntime>();
        [SerializeField] private UnityEvent<BoardGameShutdownArgs> onBoardGameShutdown = new UnityEvent<BoardGameShutdownArgs>();
        [SerializeField] private UnityEvent<BoardGameRuntime, BoardGameObjectRuntime> onBoardGameAdded = new UnityEvent<BoardGameRuntime, BoardGameObjectRuntime>();

        [NonSerialized] private BoardGameRuntime boardGameRuntime;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Load();
        }

        private void OnDisable()
        {
            onBoardGameShutdown.Invoke(new BoardGameShutdownArgs() { });
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

            onBoardGameLoaded.Invoke(new BoardGameLoadedArgs()
            {
                boardGameRuntime = boardGameRuntime
            });

            onBoardGameReady.Invoke(boardGameRuntime);
        }

        protected override void SetDefaultValues()
        {
            LoadCommon(boardGameSetup.startingBoardGameRuntimeState);

            onBoardGameSetup.Invoke(new BoardGameSetupArgs()
            {
                boardGameSetup = boardGameSetup,
                boardGameRuntime = boardGameRuntime
            });

            onBoardGameReady.Invoke(boardGameRuntime);
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

            onBoardGameAdded.Invoke(boardGameRuntime, boardGameObjectRuntime);
        }

        private void OnBoardGameChanged()
        {
            Save();
        }

        #endregion
    }
}
