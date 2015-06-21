using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Fish
{
    class fishie
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public float mass;
        public float scale;

        public fishie(Texture2D texture, Vector2 position, Vector2 velocity, float mass, float scale) 
        {
        this.texture=texture; 
        this.position=position;
        this.mass = mass;
        this.velocity = velocity;
        this.scale = scale;
        }

        public void Draw(SpriteBatch classSprite) 
        {
            classSprite.Draw(texture, position, null, Color.White, 0, Vector2.Zero,
        scale, SpriteEffects.None, 0);
        }

        /* public void Update(GameTime gameTime) 
        {
            
            velocity = new Vector2(Mouse.GetState().X - position.X, Mouse.GetState().Y - position.Y);
            velocity.Normalize();
            velocity.X *= variation;
            velocity.Y *= variation;
            position.X += velocity.X;
            position.Y += velocity.Y;
        } */
    }
}
