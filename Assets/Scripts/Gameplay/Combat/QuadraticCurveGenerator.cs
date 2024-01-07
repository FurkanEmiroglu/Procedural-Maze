using UnityEngine;

namespace GameName.Gameplay.Combat
{
    public struct QuadraticCurveGenerator
    {
        private Transform m_aTransform;
        private Transform m_bTransform;
        
        private Vector3 m_controlPoint;

        public QuadraticCurveGenerator(Transform aTransform, Transform bTransform, float heightFactor)
        {
            m_aTransform = aTransform;
            m_bTransform = bTransform;

            Vector3 a = m_aTransform.position;
            Vector3 b = m_bTransform.position;
                
            float distanceSqrMagnitude = (a - b).sqrMagnitude;
            float height = distanceSqrMagnitude * heightFactor / 100f;
            m_controlPoint = (a + b) / 2f + Vector3.up * height;
        }

        public Vector3 Evaluate(float t)
        {
            Vector3 ac = Vector3.Lerp(m_aTransform.position, m_controlPoint, t);
            Vector3 cb = Vector3.Lerp(m_controlPoint, m_bTransform.position, t);
            return Vector3.Lerp(ac, cb, t);
        }
        
        public void UpdateGenerator(Transform aTransform, Transform bTransform, float heightFactor)
        {
            m_aTransform = aTransform;
            m_bTransform = bTransform;

            Vector3 a = m_aTransform.position;
            Vector3 b = m_bTransform.position;
                
            float distanceSqrMagnitude = (a - b).sqrMagnitude;
            float height = distanceSqrMagnitude * heightFactor / 100f;
            m_controlPoint = (a + b) / 2f + Vector3.up * height;
        }
    }
}