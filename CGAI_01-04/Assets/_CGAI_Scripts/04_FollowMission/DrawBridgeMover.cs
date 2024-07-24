using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
    public class DrawBridgeMover : MonoBehaviour
    {
        [SerializeField]
        private Vector3 endPosition;
        [SerializeField]
        private float timeToMove;
        [SerializeField]
        private NavMeshBaker navMeshBaker;
        public UnityEvent BridgeExtended;

        public void MoveDrawBridge()
        {
            StartCoroutine(LerpDrawbridge());
            Debug.Log("Draw bridge moving...");
        }

        private IEnumerator LerpDrawbridge()
        {
            Vector3 startPosition = transform.position;
            float timer = 0;
            while (timer < timeToMove)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, timer/timeToMove);
                yield return null;
            }
            StartCoroutine(OnBridgeExtended());
        }

        private IEnumerator OnBridgeExtended()
        {
            navMeshBaker.BakeSurface();
            yield return new WaitForSeconds(1);
            BridgeExtended.Invoke();
        }
    }
}
