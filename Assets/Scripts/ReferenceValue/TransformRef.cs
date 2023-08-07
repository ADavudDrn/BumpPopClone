using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Transform Value", menuName = "RefValues/TransformValue")]
    public class TransformRef : RefValue
    {
        [ShowInInspector]
        public Transform Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        private Transform _value;
    }
}