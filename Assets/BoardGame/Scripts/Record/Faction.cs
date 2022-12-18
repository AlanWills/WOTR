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

        [SerializeField] private int guid;
        [SerializeField] private string displayName;
        [SerializeField] private BoolValue isActive;
        [SerializeField] private IntValue diplomacyStatus;

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
        }

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
