namespace PerPixelTest.Sprites.Animation
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using PerPixelTest.Characters;

    
    class AnimationFactory
    {
        public static void CreatePlayerAnimations(Player player)
        {
            Texture2D spriteSheet = Globals.content.Load<Texture2D>("actions");
            player.PlayerAnimation = new Animation(spriteSheet, player.Position);
            player.PlayerAnimation.AnimationActions.Add("Walking", new AnimationState(new Rectangle(0, 135 * 0, 95 * 10, 135), 10, 50));
            player.PlayerAnimation.AnimationActions.Add("Running", new AnimationState(new Rectangle(0, 135 * 1, 95 * 12, 135), 12, 60));
            player.PlayerAnimation.AnimationActions.Add("Dying", new AnimationState(new Rectangle(0, 135 * 2, 95 * 4, 135), 4, 80));
            player.PlayerAnimation.CurrentAnimationState = player.PlayerAnimation.AnimationActions["Dying"];
        }
    }
}
