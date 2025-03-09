using UnityEngine;

namespace Match3Game.Pendulum
{
    public interface IPendulum
    {
        Vector2 AttachmentPoint { get; }
        Vector2 PivotPosition { get; }
        
        Transform EndPoint { get;  }
        
        void Update(float deltaTime);
    }
    
    public  class StandartPendulum : IPendulum
    {
        private readonly Transform pivot;
        private readonly Transform endPoint;
        private readonly float radius;
        private readonly float minAngle;
        private readonly float maxAngle;
        private readonly float speed;
        private readonly float objectHeight;
        
        private float time;
        
        public Vector2 AttachmentPoint { get; private set; }
        public Vector2 PivotPosition { get; private set; }
        public Transform EndPoint { get; private set;  }


        public StandartPendulum(Transform pivot, Transform endPoint, float radius, float minAngle, float maxAngle, float speed, float objectHeight)
        {
            this.pivot = pivot;
            this.endPoint = endPoint;
            this.radius = radius;
            this.minAngle = minAngle;
            this.maxAngle = maxAngle;
            this.speed = speed;
            this.objectHeight = objectHeight;

            PivotPosition = pivot.position;
            EndPoint = endPoint;
        }

        public void Update(float deltaTime)
        {
            if (pivot == null) return;

            time += Time.deltaTime * speed;
            float angle = Mathf.Lerp(minAngle, maxAngle, (Mathf.Sin(time) + 1) / 2);
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 offset = new Vector2(Mathf.Sin(angleRad), -Mathf.Cos(angleRad)) * radius;
            endPoint.position = (Vector2)pivot.position + offset;
            
            AttachmentPoint = endPoint.position + new Vector3(0, objectHeight, 0);
        }
    }    
}

