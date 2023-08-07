using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    [CreateAssetMenu(fileName = "Int Value", menuName = "RefValues/IntValue")]
    public class IntRef : RefValue
    {
        [ShowInInspector]
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueHasChanged();
            }
        }
        private int _value;
    }
}