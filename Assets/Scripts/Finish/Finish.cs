using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DefaultNamespace;
using Lean.Pool;
using ReferenceValue;
using ScriptableEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Finish
{
    [ExecuteInEditMode]
    public class Finish : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Generation")] private FinishPiece FinishPiecePrefab;
        [SerializeField, FoldoutGroup("Generation"), MinValue(5f)] private float FinishPieceLength;
        [SerializeField, FoldoutGroup("Generation")] private int FinishPieceCount;
        [SerializeField, FoldoutGroup("Generation")] private int FinishPieceBallCountStart;
        [SerializeField, FoldoutGroup("Generation")] private int FinishPieceBallIncrease;

        [SerializeField, FoldoutGroup("Generation")] private List<FinishPiece> SpawnedFinishPieces = new List<FinishPiece>();

        [SerializeField] private GameEvent FinishStart;
        [SerializeField] private IntRef BallPassedFinish;
        [SerializeField] private TransformRef FollowCamera;
        [SerializeField] private TransformRef ThrowCamera;
        [SerializeField] private CinemachineVirtualCamera FinishCamera;

        private bool _isPassed;
        private FinishPiece _prevPiece;
        

        [Button]
        private void GenerateFinish()
        {
            ClearFinish();
            
            for (int i = 0; i < FinishPieceCount; i++)
            {
                var piece = Instantiate(FinishPiecePrefab, transform.position, FinishPiecePrefab.transform.rotation,
                    transform);
                
                piece.transform.localPosition = Vector3.zero + piece.transform.forward * FinishPieceLength * i;
                piece.SetLength(FinishPieceLength);
                var count = FinishPieceBallCountStart + FinishPieceBallIncrease * i;
                piece.Lock.SetCount(count);
                piece.Lock.FinishCam = FinishCamera;
                
                if (i >= 1)
                    _prevPiece.Lock.NextLock = piece.Lock.transform;
                
                SpawnedFinishPieces.Add(piece);
                _prevPiece = piece;

            }
        }

        [Button]
        private void ClearFinish()
        {
            SpawnedFinishPieces.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetComponent(out Ball ball))
                return;
            
            ball.FinishStart();
            
            if (!_isPassed)
            {
                _isPassed = true;
                FinishStart.Raise();
                FollowCamera.Value.gameObject.SetActive(false);
                ThrowCamera.Value.gameObject.SetActive(false);
                FinishCamera.m_LookAt = SpawnedFinishPieces.First().Lock.transform;
                FinishCamera.m_Follow = SpawnedFinishPieces.First().Lock.transform;
                FinishCamera.gameObject.SetActive(true);
            }

            BallPassedFinish.Value++;
        }
    }
}