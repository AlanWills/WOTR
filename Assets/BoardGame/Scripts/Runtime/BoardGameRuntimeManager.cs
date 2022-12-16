using Celeste.BoardGame.Persistence;
using Celeste.BoardGame.Runtime;
using Celeste.Components;
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
            LoadCommon(boardGameSetup.StartingBoardGameRuntimeState);

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
                BoardGameObjectRuntime runtime = boardGameRuntime.AddBoardGameObject(boardGameObjectRuntimeDTO.guid);
                runtime.LoadComponents(dto.components.ToLookup());
            }

            boardGameRuntime.ComponentDataChanged.AddListener(OnBoardGameChanged);
        }

        #endregion

        #region Callbacks

        private void OnBoardGameChanged()
        {
            Save();
        }

        #endregion
    }
}
