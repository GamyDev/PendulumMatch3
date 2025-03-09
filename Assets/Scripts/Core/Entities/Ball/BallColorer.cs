using UnityEngine;

namespace Match3Game.Balls
{
    public class BallColorer : IColorable
    {
        private SpriteRenderer spriteRenderer;
        public int ColorID { get; private set; }
        public Color[] Colors { get; private set; } = new Color[3] { Color.red, Color.blue, Color.green };


        public BallColorer(SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
        }

        public void SetColor(int colorID)
        {
            if (spriteRenderer != null && colorID >= 0 && colorID < Colors.Length)
            {
                spriteRenderer.color = Colors[colorID];
                ColorID = colorID;
            }
        }
    }
}