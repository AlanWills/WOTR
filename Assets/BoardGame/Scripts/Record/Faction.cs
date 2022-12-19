using Celeste.Events;
using Celeste.Objects;
using Celeste.Parameters;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace WOTR.BoardGame
{
    [CreateAssetMenu(fileName = nameof(Faction), menuName = "WOTR/Board Game/Faction")]
    public class Faction : ScriptableObject, IInitializable, IGuid
    {
        #region Properties and Fields

        public int Guid
        {
            get => guid;
            set
            {
                guid = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        public string DisplayName => displayName;

        public bool IsActive
        {
            get => isActive.Value;
            set => isActive.Value = value;
        }

        public int DiplomacyStatus
        {
            get => diplomacyStatus.Value;
            set => diplomacyStatus.Value = value;
        }

        public int AvailableSoldiers
        { 
            get => availableSoldiers.Value;
            set => availableSoldiers.Value = value;
        }

        public int AvailableElites
        {
            get => availableElites.Value;
            set => availableElites.Value = value;
        }

        public int AvailableLeaders
        {
            get => availableLeaders.Value;
            set => availableLeaders.Value = value;
        }

        public int RemovedSoldiers
        {
            get => removedSoldiers.Value;
            set => removedSoldiers.Value = value;
        }

        public int RemovedElites
        {
            get => removedElites.Value;
            set => removedElites.Value = value;
        }

        public int RemovedLeaders
        {
            get => removedLeaders.Value;
            set => removedLeaders.Value = value;
        }

        [SerializeField] private int guid;
        [SerializeField] private string displayName;
        [SerializeField] private BoolValue isActive;
        [SerializeField] private IntValue diplomacyStatus;

        [Header("Available Army")]
        [SerializeField] private IntValue availableSoldiers;
        [SerializeField] private IntValue availableElites;
        [SerializeField] private IntValue availableLeaders;

        [Header("Removed Army")]
        [SerializeField] private IntValue removedSoldiers;
        [SerializeField] private IntValue removedElites;
        [SerializeField] private IntValue removedLeaders;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }

#if UNITY_EDITOR
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            int indexOfSlash = assetPath.LastIndexOf('/');
            string parentFolder = indexOfSlash > 0 ? assetPath.Substring(0, indexOfSlash) : assetPath;

            availableSoldiers = EditorOnly_CreateIfNecessary(availableSoldiers, $"{name}-AvailableSoldiers", parentFolder);
            availableElites = EditorOnly_CreateIfNecessary(availableElites, $"{name}-AvailableElites", parentFolder);
            availableLeaders = EditorOnly_CreateIfNecessary(availableLeaders, $"{name}-AvailableLeaders", parentFolder);
            removedSoldiers = EditorOnly_CreateIfNecessary(removedSoldiers, $"{name}-RemovedSoldiers", parentFolder);
            removedElites = EditorOnly_CreateIfNecessary(removedElites, $"{name}-RemovedElites", parentFolder);
            removedLeaders = EditorOnly_CreateIfNecessary(removedLeaders, $"{name}-RemovedLeaders", parentFolder);
#endif
        }

#if UNITY_EDITOR
        private IntValue EditorOnly_CreateIfNecessary(IntValue intValue, string name, string parentFolder)
        {
            if (intValue == null)
            {
                intValue = ScriptableObject.CreateInstance<IntValue>();
                intValue.name = name;
                UnityEditor.AssetDatabase.CreateAsset(intValue, $"{parentFolder}/{intValue.name}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
            }

            return intValue;
        }
#endif

#endregion

        public void Initialize()
        {
            if (isActive == null)
            {
                isActive = CreateInstance<BoolValue>();
                isActive.name = $"{name}_IsActive";
#if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                DirectoryInfo parentDirectory = Directory.GetParent(assetPath);
                string parentDirectoryPath = parentDirectory.FullName.Remove(0, Application.dataPath.Length + 1);
                UnityEditor.AssetDatabase.CreateAsset(isActive, $"Assets/{parentDirectoryPath}/{isActive.name}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
#endif
            }

            if (diplomacyStatus == null)
            {
                diplomacyStatus = CreateInstance<IntValue>();
                diplomacyStatus.name = $"{name}_DiplomacyStatus";
#if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                DirectoryInfo parentDirectory = Directory.GetParent(assetPath);
                string parentDirectoryPath = parentDirectory.FullName.Remove(0, Application.dataPath.Length + 1);
                UnityEditor.AssetDatabase.CreateAsset(diplomacyStatus, $"Assets/{parentDirectoryPath}/{diplomacyStatus.name}.asset");
                UnityEditor.AssetDatabase.SaveAssets();
#endif
            }
        }

        public void AddIsActiveChangedCallback(UnityAction<ValueChangedArgs<bool>> callback)
        {
            isActive.AddValueChangedCallback(callback);
        }

        public void RemoveIsActiveChangedCallback(UnityAction<ValueChangedArgs<bool>> callback)
        {
            isActive.RemoveValueChangedCallback(callback);
        }

        public void AddDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback)
        {
            diplomacyStatus.AddValueChangedCallback(callback);
        }

        public void RemoveDiplomacyStatusChangedCallback(UnityAction<ValueChangedArgs<int>> callback)
        {
            diplomacyStatus.RemoveValueChangedCallback(callback);
        }
    }
}
