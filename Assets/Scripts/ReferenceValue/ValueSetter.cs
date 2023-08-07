using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ReferenceValue
{
    public class ValueSetter : MonoBehaviour
    {
        private enum SetTime
        {
            Awake,
            OnEnable,
            Start,
        }
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private RefValue Reference;

        [SerializeField] private SetTime SetOn; 

        [SerializeField, ShowIf("@Reference is IntRef"), LabelText("Value"), Indent]
        private int IntValue;

        [SerializeField, ShowIf("@Reference is TransformRef"), LabelText("Value"), Indent]
        private Transform TransformValue;
        
        [SerializeField, ShowIf("@Reference is BoolRef"), LabelText("Value"), Indent]
        private bool BoolValue;
        
        [SerializeField, ShowIf("@Reference is BallCounterRef"), LabelText("Value"), Indent]
        private BallCounter BallCounterValue;

//------Private Variables-------//



#region UNITY_METHODS

        private void Awake()
        {
            if(SetOn != SetTime.Awake)
                return;
            SetValue();
        }

        private void OnEnable()
        {
            if(SetOn != SetTime.OnEnable)
                return;
            SetValue();
        }

        private void Start()
        {
            if(SetOn != SetTime.Start)
                return;
            SetValue();
        }



#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS
        private void SetValue()
        {
            switch (Reference)
            {
                case IntRef intRef:
                    intRef.Value = IntValue;
                    break;
                
                case TransformRef transformRef:
                    transformRef.Value = TransformValue;
                    break;
                
                case BoolRef boolRef:
                    boolRef.Value = BoolValue;
                    break;
                
                case BallCounterRef ballCounterRef:
                    ballCounterRef.Value = BallCounterValue;
                    break;
            }
        }
#endregion


    }
}