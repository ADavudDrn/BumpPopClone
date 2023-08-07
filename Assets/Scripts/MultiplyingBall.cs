using System;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using ReferenceValue;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class MultiplyingBall : MonoBehaviour
    {
        [SerializeField] private int BallMultiplyCount;
        [SerializeField] private Vector2 BallThrowDirectionMinMax;
        [SerializeField] private Vector2 BallThrowForceMinMax;
        [SerializeField] private Collider Collider;

        [SerializeField] private BoolRef IsMultiplied;

        [SerializeField] private Ball BallObject;

        [SerializeField] private List<Color> Colors = new List<Color>();
        [SerializeField] private MeshRenderer ModelMesh;

        [SerializeField] private bool IsRainbow;

        private Color _choosenColor;

        private bool _isUsed;


        private void Awake()
        {
            _choosenColor = Colors[Random.Range(0, Colors.Count)];
            if (!IsRainbow)
                ModelMesh.material.color = _choosenColor;
            else
            {
                ColorCycle();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isUsed)
                return;

            if(!other.TryGetComponent(out Ball ball))
                return;

            _isUsed = true;
            Collider.enabled = false;

            IsMultiplied.Value = true;

            var count = BallMultiplyCount;
            if (IsRainbow)
                count = BallMultiplyCount * 2;
            
            for (int i = 0; i < count; i++)
            {
                var dir = Quaternion.LookRotation(transform.position-ball.transform.position);
                var randomAngle = Random.Range(BallThrowDirectionMinMax.x, BallThrowDirectionMinMax.y);
                var direction = Quaternion.Euler((dir.eulerAngles + new Vector3(0,randomAngle,0)));
                var randomForce = Random.Range(BallThrowForceMinMax.x, BallThrowForceMinMax.y);

                var spawnRandom = Random.insideUnitCircle * 1.5f;
                
                var spawnPos = transform.position + new Vector3(spawnRandom.x, 0, spawnRandom.y);
                
                var spawnedBall = LeanPool.Spawn(BallObject, spawnPos, direction);
                Color ballColor;
                if (IsRainbow)
                    ballColor = Colors[Random.Range(0, Colors.Count)];
                else
                    ballColor = _choosenColor;
                
                spawnedBall.SetColor(ballColor);
                spawnedBall.ThrowBall(spawnedBall.transform.forward * randomForce);
            }
            
            gameObject.SetActive(false);
        }

        private void ColorCycle()
        {
            var color = Colors[Random.Range(0, Colors.Count)];
            DOVirtual.Color(ModelMesh.material.color, color, 1f, val => ModelMesh.material.color = val).OnComplete(ColorCycle);
        }
    }
}