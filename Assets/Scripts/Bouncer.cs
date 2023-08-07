using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bouncer : MonoBehaviour
    {
        [SerializeField] private float BounceForce;

        private Tween _punchTween;
        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.transform.TryGetComponent(out Ball ball))
                return;

            var direction = (ball.transform.position - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
            ball.BounceBall(direction * BounceForce);
            _punchTween?.Kill();
            transform.localScale = Vector3.one;
            _punchTween = transform.DOPunchScale(Vector3.one * .2f, .25f);
        }
    }
}