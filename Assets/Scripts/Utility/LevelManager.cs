using System.Collections.Generic;
using ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utility
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private IntRef CurrentLevel;
        [SerializeField] private List<GameObject> LevelPrefabs;

        private const string CURRENT_LEVEL_SAVE_KEY = "CurrentLevel";

        private void Awake()
        {
            Load();
            InstantiateCurrentLevel();
        }

        private void InstantiateCurrentLevel()
        {
            if (LevelPrefabs.Count <= 0)
                return;

            Instantiate(LevelPrefabs[(CurrentLevel.Value-1) % LevelPrefabs.Count]);

        }

        private void Load()
        {
            CurrentLevel.Value = PlayerPrefs.HasKey(CURRENT_LEVEL_SAVE_KEY)
                ? PlayerPrefs.GetInt(CURRENT_LEVEL_SAVE_KEY, 1)
                : 1;
        }

        private void Save()
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_SAVE_KEY, CurrentLevel.Value);
            PlayerPrefs.Save();
        }

        public void LevelCompleted()
        {
            CurrentLevel.Value++;
            Save();
        }

        [Button]
        public void DeleteSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
    


