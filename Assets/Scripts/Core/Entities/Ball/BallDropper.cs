using UnityEngine;

namespace Match3Game.Balls
{
    public class BallDropper : IDropable
    {
        private Rigidbody2D rigidbody;
        private bool isFalling;    
        

        public BallDropper(Rigidbody2D rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        public void Drop()
        {
            if (isFalling) return;
            
            isFalling = true;
            
            rigidbody.isKinematic = false; 
            rigidbody.velocity = Vector2.zero;
        }
    }
}