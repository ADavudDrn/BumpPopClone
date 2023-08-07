using System;
using ReferenceValue;
using TMPro;
using UnityEngine;

namespace Utility
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private IntRef CurrentLevel;
        [SerializeField] private TextMeshProUGUI Text;

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            Text.SetText("Level " + CurrentLevel.Value);
        }
    }
}