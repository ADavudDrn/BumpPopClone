using UnityEngine;

namespace Finish
{
    public class FinishPiece : MonoBehaviour
    {
        public FinishLock Lock;
        [SerializeField]private Transform Road;
        [SerializeField]private Transform CurbLeft;
        [SerializeField]private Transform CurbRight;

        public void SetLength(float length)
        {
            Road.localPosition = new Vector3(Road.localPosition.x, Road.localPosition.y, length / 2);
            CurbLeft.localPosition = new Vector3(CurbLeft.localPosition.x, CurbLeft.localPosition.y, length / 2);
            CurbRight.localPosition = new Vector3(CurbRight.localPosition.x, CurbRight.localPosition.y, length / 2);

            Road.localScale = new Vector3(Road.localScale.x, Road.localScale.y, length);
            CurbLeft.localScale = new Vector3(CurbLeft.localScale.x, CurbLeft.localScale.y, length);
            CurbRight.localScale = new Vector3(CurbRight.localScale.x, CurbRight.localScale.y, length);

            Lock.transform.localPosition = new Vector3(Lock.transform.localPosition.x, Lock.transform.localPosition.y,
                length - (length * .1f));
        }
    }
}