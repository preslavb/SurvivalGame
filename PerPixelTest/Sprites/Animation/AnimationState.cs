namespace PerPixelTest.Sprites.Animation
{
    using Microsoft.Xna.Framework;
    public class AnimationState
    {
        public Rectangle ActionDimensions { get; set; }
        public Vector2 FrameDimensions { get; set; }
        public int FrameAmmount { get; set; }
        public int AnimationSpeed { get; set; }

        public AnimationState(Rectangle actionDimenstions, int frameAmmount, int speed)
        {
            ActionDimensions = actionDimenstions;
            FrameAmmount = frameAmmount;
            FrameDimensions = new Vector2(ActionDimensions.Width / FrameAmmount, ActionDimensions.Height);
            AnimationSpeed = speed;
        }
    }
}
