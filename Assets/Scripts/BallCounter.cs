using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dreamteck.Splines;
using ReferenceValue;
using ScriptableEvent;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallCounter : MonoBehaviour
    {
        [SerializeField] private TransformRef SelectedBall;
        [SerializeField] private List<Ball> ActiveBalls = new List<Ball>();
        [SerializeField] private BoolRef IsMultiplied;
        [SerializeField] private TransformRef SplineComputerTransformRef;
        [SerializeField] private BoolRef IsBallsMoving;
        [SerializeField] private GameEvent LevelFailed;
        [SerializeField] private TransformRef ThrowCamera;
        [SerializeField] private TransformRef FollowCamera;

        private SplineComputer _splineComputer;
        private bool _isFinishStarted;
        
        private void Start()
        {
            _splineComputer = SplineComputerTransformRef.Value.GetComponent<SplineComputer>();
        }

        private void Update()
        {
            if(ActiveBalls.Count <= 1)
                return;

            var maxPercent = 0d;
            var followedBall = ActiveBalls.First();

            foreach (var ball in ActiveBalls)
            {
                var perc = GetSplinePercent(ball.transform);
                if (perc > maxPercent)
                {
                    maxPercent = perc;
                    followedBall = ball;
                }
            }

            if (GetSplinePercent(SelectedBall.Value) < maxPercent)
            {
                SelectedBall.Value = followedBall.transform;
            }

        }
        
        public void AddBallToList(Ball ball)
        {
            if(ActiveBalls.Contains(ball))
                return;
            
            ActiveBalls.Add(ball);
        }

        public void RemoveBallFromList(Ball ball)
        {
            if(ActiveBalls.Contains(ball))
            {
                ActiveBalls.Remove(ball);
                
                CheckBallsMovement();
            }
        }

        private void CheckBallsMovement()
        {
            if (ActiveBalls.Count > 0)
                return;

            if (IsMultiplied.Value)
            {
                IsMultiplied.Value = false;
                IsBallsMoving.Value = false;
                ThrowCamera.Value.gameObject.SetActive(true);
                FollowCamera.Value.gameObject.SetActive(false);
            }
            else
            {
                if(_isFinishStarted)
                    return;
                LevelFailed.Raise();
            }
        }


        private double GetSplinePercent(Transform ball)
        {
            SplineSample sample = new SplineSample();
            _splineComputer.Project(ball.position, ref sample);
            var percent = sample.percent;

            return percent;
        }

        public void FinishStarted()
        {
            _isFinishStarted = true;
        }
   }
}