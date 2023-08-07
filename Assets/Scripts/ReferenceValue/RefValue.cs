using UnityEngine;

namespace ReferenceValue
{
    public abstract class RefValue : ScriptableObject
    {
        public delegate void ValueChanged();
        public event ValueChanged OnValueChanged;
        public void ValueHasChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}