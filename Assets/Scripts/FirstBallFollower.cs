using ReferenceValue;
using UnityEngine;

namespace DefaultNamespace
{
    public class FirstBallFollower : MonoBehaviour
    {
        [SerializeField] private TransformRef Ball;
        [SerializeField] private BoolRef IsBallMoving;

        private void Update()
        {
            if (IsBallMoving.Value)
                transform.position = Vector3.Lerp(transform.position, Ball.Value.position, 8f * Time.deltaTime);
            else
                transform.position = Ball.Value.position;
        }
    }
}