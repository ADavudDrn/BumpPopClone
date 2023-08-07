using System;
using Cinemachine;
using ReferenceValue;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private float ThrowPower;
        [SerializeField] private TransformRef SelectedBall;
        [SerializeField] private BoolRef IsBallsMoving;
        [SerializeField] private TransformRef BallFollower;
        [SerializeField] private LineRenderer LineRenderer;
        [SerializeField] private float LineLength;
        [SerializeField] private LayerMask LineBreakLayer;
        [SerializeField] private TransformRef FollowCamera;
        [SerializeField] private TransformRef ThrowCamera;
        [SerializeField] private float CameraZDistance = 15f;
        
        private Vector2 _mouseStartPos;
        private Vector2 _mouseEndPos;
        private Vector3 _direction;

        private CinemachineVirtualCamera _throwCamera;
        private CinemachineTransposer _transposer;
        private bool _isClicked;
        private bool _isFirstThrow;
        
        private void Awake()
        {
            _mouseStartPos = Vector2.zero;
            _mouseEndPos = Vector2.zero;
            _direction = Vector3.zero;
            LineRenderer.gameObject.SetActive(false);
            _isFirstThrow = true;
            _throwCamera = ThrowCamera.Value.GetComponent<CinemachineVirtualCamera>();
            _transposer = _throwCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Update()
        {
            if(IsBallsMoving.Value)
                return;

            if (!_isClicked)
            {
                var direction = BallFollower.Value.forward;
                _transposer.m_FollowOffset =
                    new Vector3(-direction.x * CameraZDistance, _transposer.m_FollowOffset.y, -direction.z * CameraZDistance);
                
                DrawLine();
            }
            
            if (Input.GetMouseButtonDown(0) && !_isClicked)
            {
                OnMouseButtonDown();
            }

            if (Input.GetMouseButtonUp(0) && _isClicked)
            {
                OnMouseButtonUp();
            }
            
            if (_isClicked)
            {
                OnMouseButton();
            }
        }

        private void OnMouseButtonDown()
        {
            _isClicked = true;
            _mouseStartPos = Input.mousePosition;
        }

        private void OnMouseButton()
        {
            var delta = Input.mousePosition.x - _mouseStartPos.x;

            var eulerAng = (BallFollower.Value.rotation.eulerAngles.y+ delta) * Mathf.Deg2Rad;

            _direction = new Vector3(Mathf.Sin(eulerAng),0,Mathf.Cos(eulerAng));
            _transposer.m_FollowOffset =
                new Vector3(-_direction.x * CameraZDistance, _transposer.m_FollowOffset.y, -_direction.z * CameraZDistance);

            DrawLine();
        }

        private void OnMouseButtonUp()
        {
            _isClicked = false;
            _mouseEndPos = Input.mousePosition;

            var delta = _mouseEndPos.x - _mouseStartPos.x;

            var eulerAng = (BallFollower.Value.rotation.eulerAngles.y+ delta) * Mathf.Deg2Rad;

            _direction = new Vector3(Mathf.Sin(eulerAng),0,Mathf.Cos(eulerAng));
            
            LineRenderer.gameObject.SetActive(false);
            
            SelectedBall.Value.GetComponent<Ball>().ThrowBall(_direction.normalized*(ThrowPower));
            IsBallsMoving.Value = true;
            FollowCamera.Value.gameObject.SetActive(true);
            ThrowCamera.Value.gameObject.SetActive(false);
            _mouseEndPos = Vector2.zero;
            _mouseStartPos = Vector2.zero;
        }

        private void DrawLine()
        {
            LineRenderer.gameObject.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(SelectedBall.Value.transform.position, _direction, out hit, LineLength, LineBreakLayer))
            {
                var length = (hit.point - SelectedBall.Value.transform.position).magnitude;
                var remainingLength = LineLength - length;
                var newDir = Vector3.Reflect(hit.point - SelectedBall.Value.transform.position, hit.normal);
                LineRenderer.SetPosition(0,SelectedBall.Value.transform.position);
                LineRenderer.SetPosition(1,SelectedBall.Value.transform.position + _direction.normalized*(length-.5f));
                LineRenderer.SetPosition(2,hit.point + newDir.normalized*remainingLength);
                return;
            }
            LineRenderer.SetPosition(0,SelectedBall.Value.transform.position);
            LineRenderer.SetPosition(1,SelectedBall.Value.transform.position + _direction.normalized*LineLength);
            LineRenderer.SetPosition(2,SelectedBall.Value.transform.position + _direction.normalized*LineLength);
        }
    }
}
