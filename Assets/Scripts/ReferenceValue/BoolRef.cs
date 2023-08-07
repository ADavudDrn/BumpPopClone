using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Bool Value", menuName = "RefValues/BoolValue")]
    public class BoolRef : RefValue
    {
        [ShowInInspector]
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        private bool _value;
    }
}