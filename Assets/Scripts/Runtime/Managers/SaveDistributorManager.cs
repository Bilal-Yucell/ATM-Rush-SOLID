using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SaveDistributorManager : MonoBehaviour
    {
        private static GameData _gameData;
        private static readonly SaveManager _saveManager = new SaveManager();
        [SerializeField] private bool autoSave = true;

        #region EventSubscribtion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += SaveData;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= SaveData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            GetSaveData();
        }

        public static GameData GetSaveData()
        {
            GameData GetData()
            {
                return _saveManager.PreLoadData(new GameData());
            }
            if (_gameData is null)
            {
                _gameData = GetData();
            }
            return _gameData;
        }

        public void SaveData()
        {
            if (_gameData is null)GetSaveData();
            _saveManager.PreSaveData(_gameData);
        }

#if UNITY_EDITOR

        private void OnApplicationQuit()
        {
            if(autoSave)SaveData();
        }  
#endif

#if UNITY_ANDROID && UNITY_IOS

        private void OnApplicationPause()
        {
            if(pauseStatus && autoSave)SaveData();
        }  
#endif
    }
}