using Celeste.BoardGame;
using Celeste.BoardGame.Events;
using Celeste.BoardGame.Persistence;
using Celeste.BoardGame.Runtime;
using Celeste.Components;
using Celeste.Events;
using Celeste.Persistence;
using Celeste.Persistence.Snapshots;
using System;
using UnityEngine;
using WOTR.BoardGame.Events;

namespace WOTR.BoardGame.Managers
{
    public class BoardGameRuntimeManager : PersistentSceneManager<BoardGameRuntimeManager, BoardGameRuntimeDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "BoardGame.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private Celeste.BoardGame.BoardGame boardGame;
        [SerializeField] private DataSnapshot boardGameSetup;

        [Header("Events")]
        [SerializeField] private BoardGameSetupEvent onBoardGameSetupEvent;
        [SerializeField] private BoardGameLoadedEvent onBoardGameLoadedEvent;
        [SerializeField] private BoardGameReadyEvent onBoardGameReadyEvent;
        [SerializeField] private BoardGameShutdownEvent onBoardGameShutdownEvent;
        [SerializeField] private BoardGameObjectAddedEvent onBoardGameObjectAddedEvent;
        [SerializeField] private BoardGameObjectMovedEvent onBoardGameObjectMovedEvent;

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
            // Unpack the setup snapshot into the filesystem so it'll be there without us having to save
            boardGameSetup.UnpackItems();

            // Grab the data for our board game runtime and use it to load
            LoadCommon(boardGameSetup.DeserializeData<BoardGameRuntimeDTO>(FILE_NAME));

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
                boardGameRuntime.AddBoardGameObjectRuntime(boardGameObjectRuntimeDTO);
            }

            boardGameRuntime.ComponentDataChanged.AddListener(OnBoardGameChanged);
        }

        #endregion

        #region Callbacks

        public void OnAddBoardGameObject(AddBoardGameObjectArgs args)
        {
            BoardGameObject boardGameObject = boardGame.FindBoardGameObject(args.boardGameObjectGuid);
            UnityEngine.Debug.Assert(boardGameObject != null, $"Could not find board game object with guid {args.boardGameObjectGuid}.");
            BoardGameObjectRuntime boardGameObjectRuntime = boardGameRuntime.AddBoardGameObjectRuntime(boardGameObject);

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

        public void OnMoveBoardGameObject(MoveBoardGameObjectArgs args)
        {
            BoardGameObjectRuntime runtime = args.boardGameObjectRuntime;

            if (runtime == null)
            {
                if (args.boardGameObjectRuntimeInstanceId != 0)
                {
                    runtime = boardGameRuntime.FindBoardGameObjectRuntime(args.boardGameObjectRuntimeInstanceId);
                }
                else if (!string.IsNullOrEmpty(args.boardGameObjectRuntimeName))
                {
                    runtime = boardGameRuntime.FindBoardGameObjectRuntime(args.boardGameObjectRuntimeName);
                }
            }

            UnityEngine.Debug.Assert(runtime != null, $"No runtime could be found for moving board game object.");
            if (runtime != null && runtime.TryFindComponent<IBoardGameObjectActor>(out var actor))
            {
                string oldLocationName = actor.iFace.GetCurrentLocationName(actor.instance);
                actor.iFace.SetCurrentLocationName(actor.instance, args.newLocation);
                onBoardGameObjectMovedEvent.Invoke(new BoardGameObjectMovedArgs()
                {
                    boardGameRuntime = boardGameRuntime,
                    boardGameObjectRuntime = runtime,
                    oldLocation = oldLocationName,
                    newLocation = args.newLocation
                });
            }
        }

        private void OnBoardGameChanged()
        {
            Save();
        }

        #endregion
    }
}
