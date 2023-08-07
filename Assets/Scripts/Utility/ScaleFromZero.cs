using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Utility
{
    public class ScaleFromZero : MonoBehaviour
    {
        [SerializeField]private Vector3 GrownScale = Vector3.one;
        [SerializeField] private float DurationInSec = 0.25f;
        [SerializeField]private float Delay = 0f;
        [SerializeField] private Ease Ease;

        private Vector3 _originalScale;
        private Vector3 _zeroScale;

        private void Awake()
        {
            _originalScale = GrownScale;
            transform.localScale = _zeroScale;
        }

        private void OnEnable()
        {
            var delay = Delay;

            transform.localScale = _zeroScale;
            PlayAnimation(delay);
        }

        private void OnDisable()
        {
            transform.localScale = _zeroScale;
        }

        private void PlayAnimation(float delay)
        {
            transform.DOScale(_originalScale, DurationInSec).SetDelay(delay).SetEase(Ease);
        }
    }
}