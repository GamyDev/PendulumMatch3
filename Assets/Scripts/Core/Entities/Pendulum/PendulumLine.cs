using UnityEngine;

namespace Match3Game.Pendulum  
{
    public interface IPendulumLine
    {
        IPendulum Pendulum { get; }
        void UpdateLine();
    }
    
    public class PendulumLine : IPendulumLine
    {
        private LineRenderer _lineRenderer;
        public  IPendulum Pendulum { get; private set; }

        public PendulumLine(LineRenderer lineRenderer, IPendulum pendulum)
        {
            _lineRenderer = lineRenderer;
            Pendulum = pendulum;
            
            Init(lineRenderer);
        }

        private void Init(LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.useWorldSpace = true;
        }

        public void UpdateLine()
        {
            _lineRenderer.SetPosition(0, Pendulum.PivotPosition);
            _lineRenderer.SetPosition(1, Pendulum.AttachmentPoint);
        }
    }    
}

