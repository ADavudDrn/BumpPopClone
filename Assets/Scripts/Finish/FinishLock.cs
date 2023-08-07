using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using DG.Tweening;
using ReferenceValue;
using ScriptableEvent;
using TMPro;
using UnityEngine;

namespace Finish
{
    public class FinishLock : MonoBehaviour
    {
        public CinemachineVirtualCamera FinishCam;
        public Transform NextLock;
        [SerializeField] private IntRef BallPassedFinish;
        [SerializeField] private int LockBreakCount;
        [SerializeField] private List<Ball> EnteredBalls = new List<Ball>();
        [SerializeField] private Color StartColor;
        [SerializeField] private Color BreakColor;
        [SerializeField] private MeshRenderer LockMesh;
        [SerializeField] private GameEvent LevelFinish;

        [SerializeField] private TextMeshPro CountText;

        [SerializeField] private float WaitDuration = 3f;

        private bool _isLockBreak;
        private Tween _tween;


        private void Awake()
        {
            UpdateText();
            LockMesh.material.color = StartColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isLockBreak)
                return;
            
            if(!other.TryGetComponent(out Ball ball))
                return;
            
            if(EnteredBalls.Contains(ball))
                return;
            
            EnteredBalls.Add(ball);
            UpdateText();
            BreakLock();
        }

        private void OnTriggerExit(Collider other)
        {
            if(_isLockBreak)
                return;
            
            if(!other.TryGetComponent(out Ball ball))
                return;

            if (EnteredBalls.Contains(ball))
                EnteredBalls.Remove(ball);
            
            UpdateText();
            
        }

        private void BreakLock()
        {
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(WaitDuration, () =>
            {
                if (_isLockBreak)
                    return;
                _isLockBreak = true;
                LevelFinish.Raise();
            });
            
            var color = Color.Lerp(StartColor, BreakColor, (EnteredBalls.Count / (float)LockBreakCount));
            LockMesh.material.color = color;
            if (EnteredBalls.Count >= LockBreakCount)
            {
                LockMesh.gameObject.SetActive(false);
                _isLockBreak = true;
                FinishCam.m_LookAt = NextLock;
                FinishCam.m_Follow = NextLock;
                return;
            }

            if (EnteredBalls.Count >= BallPassedFinish.Value)
            {
                if(_isLockBreak)
                    return;
                _isLockBreak = true;
                LevelFinish.Raise();
                _tween?.Kill();
            }
        }

        private void UpdateText()
        {
            var count = LockBreakCount - EnteredBalls.Count;
            if (count < 0)
                count = 0;
            CountText.SetText(count.ToString());
        }

        public void SetCount(int count)
        {
            LockBreakCount = count;
        }
    }
}