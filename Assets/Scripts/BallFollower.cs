using System;
using Dreamteck.Splines;
using ReferenceValue;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BallFollower : MonoBehaviour
    {
        [SerializeField] private TransformRef SplineComputerTransformRef;

        [SerializeField] private TransformRef Ball;

        private SplineComputer _splineComputer;


        private void Awake()
        {
            _splineComputer = SplineComputerTransformRef.Value.GetComponent<SplineComputer>();
        }

        private void Update()
        {
            SplineSample sample = new SplineSample();
            _splineComputer.Project(Ball.Value.position, ref sample);
            transform.position = sample.position;
            transform.rotation = sample.rotation;
        }
    }
}