using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "BallCounter Value", menuName = "RefValues/BallCounterValue")]
    public class BallCounterRef : RefValue
    {
        [ShowInInspector]
        public BallCounter Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        private BallCounter _value;
    }
}