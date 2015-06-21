using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fish
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        Texture2D fish;
        static int numPart = 550;
        List<fishie> joes = new List<fishie>();
        float ax, ay, dx, dy, xi, yi, r, invr, invr3, f;
        float eps = 550.0f;
        float dt = 1.5f;
        float G = 6.67384f * (float)Math.Pow(10.0,-11.0);
        SpriteFont font;
        float T = 0.0f;
        //float G = 1.0f;
        MouseState ms;
        public float avgM = (float)Math.Pow(10, 7);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
           
        }
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
            ms = Mouse.GetState();
            for (int i = 0; i < numPart/25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    joes.Add(new fishie(fish, new Vector2(250 + 20 * i, 250 + 20*j), new Vector2(0, 0), /*(float)rand.NextDouble()* */ (float)Math.Pow(10, 9) + 1, 0.2f));
                }
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fish = Content.Load<Texture2D>(@"fish");
            font = Content.Load<SpriteFont>(@"myFont");
            Song song = Content.Load<Song>("padsman");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.5f;
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState newState = Mouse.GetState();

            if(newState.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Released)
            {
                joes.Add(new fishie(fish, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Vector2(0,0), 600*(float)Math.Pow(10,9), 1.5f));
                numPart++;
            }
            //Parallel.For(0, numPart, i =>

            for (int i = 0; i < numPart; i++)
            {
                xi = joes[i].position.X;
                yi = joes[i].position.Y;
                ax = 0.0f;
                ay = 0.0f;

                for (int j = 0; j < numPart; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    dx = joes[j].position.X - xi;
                    dy = joes[j].position.Y - yi;
                    r = dx * dx + dy * dy + eps;
                    invr = 1.0f / (float)Math.Sqrt(dx * dx + dy * dy + eps);
                    invr3 = invr * invr * invr;
                    f = G * joes[j].mass * invr3;
                    //printf("G %f, mass %f, invr3 %f", G, particles[j].mass, invr3);
                    //printf("f: %f\n", f * pow(10, 8));
                    ax += f * dx;
                    ay += f * dy;
                }
                joes[i].velocity.X += dt * ax;
                joes[i].velocity.Y += dt * ay;

                joes[i].position.X += (float)0.5 * dt * joes[i].velocity.X;
                joes[i].position.Y += (float)0.5 * dt * joes[i].velocity.Y;
            }
            T += dt;
            ms = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Num Particles: " + joes.Count().ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Time (sec): " + (T).ToString(), new Vector2(0, 20), Color.White);

            for (int i = 0; i < numPart; i++)
            {
                joes[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
