using System;
using System.Collections.Generic;
using DG.Tweening;
using ReferenceValue;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody Rigidbody;
        [SerializeField] private Collider Collider;
        [SerializeField] private List<Color> Colors = new List<Color>();
        [SerializeField] private MeshRenderer MeshRenderer;

        [SerializeField] private BallCounterRef BallCounterRef;

        private bool _isBallMoving;
        private bool _ballThrown;
        private bool _finishStarted;
        
        private void OnEnable()
        {
            DOVirtual.DelayedCall(.5f, ()=>Collider.enabled = true);
            SetColor(Colors[Random.Range(0,Colors.Count)]);
        }
        
        private void Update()
        {
            if(_finishStarted)
                return;

            if (Rigidbody.velocity.y > 0)
                Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0, Rigidbody.velocity.z);
            if (!_ballThrown) 
                return;
            
            CheckBallStopped();
        }

        public void ThrowBall(Vector3 force)
        {
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(force);
            _isBallMoving = true;
            BallCounterRef.Value.AddBallToList(this);
            DOVirtual.DelayedCall(.05f, ()=>_ballThrown = true);
        }

        public void BounceBall(Vector3 force)
        {
            Rigidbody.AddForce(force);
        }

        public void CheckBallStopped()
        {
            if (_isBallMoving)
            {
                if(Rigidbody.angularVelocity.magnitude < 0.2f)
                    Rigidbody.angularVelocity = Vector3.zero;
                if (Rigidbody.velocity.magnitude < 0.2f)
                {
                    Rigidbody.velocity = Vector3.zero;
                    Rigidbody.isKinematic = true;
                    _isBallMoving = false;
                    _ballThrown = false;
                    BallCounterRef.Value.RemoveBallFromList(this);
                }
            }
        }

        public void SetColor(Color color)
        {
            MeshRenderer.material.color = color;
        }

        public void FinishStart()
        {
            _finishStarted = true;
            Rigidbody.drag = .1f;
        }
    }
}