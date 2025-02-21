using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarGun.GameObjects
{
    class DamBullet : GameObject
    {
        Random rnd = new Random();
        int i = 0;
        Vector2 Firstpo;
        public enum BulletState
        {
            WAIT,
            FIRED,
            DIE
        }
        public BulletState arrowStates;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                            Position,
                            Viewport,
                            Color.White);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
        public DamBullet(Texture2D texture) : base(texture)
        {

        }


        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            switch (arrowStates)
            {

                case BulletState.WAIT:
                    {
                        break;
                    }
                case BulletState.FIRED:
                    {


                        if (Position.X <= 0 || Position.X >= Singleton.SCREENWIDTH || Position.Y <= 0 || Position.Y >= Singleton.SCREENHEIGHT)
                        {

                            IsActive = false;
                        }
                        if (IsActive && i % 2 == 0)
                        {
                            Position.Y += 1;
                            Velocity.Y -= 1 * (float)Math.PI;

                        }
                        if (IsActive && i % 5 == 0)
                        {
                            Velocity.X += Singleton.Instance.Wind * 0.5f;
                            i = 0;
                        }
                        BDirection.X = (float)Math.Cos(Angle);
                        BDirection.Y = (float)Math.Sin(Angle);
                        Position -= BDirection * Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

                        foreach (GameObject s in gameObjects)
                        {
                            if (IsTouching(s) && s.Name.Equals("EnemyRed"))
                            {
                                s.Hp -= rnd.Next(4) + 30;
                                arrowStates = BulletState.DIE;
                            }
                            if (IsTouching(s) && s.Name.Equals("EnemyOrange"))
                            {
                                s.Hp -= rnd.Next(4) + 30;
                                arrowStates = BulletState.DIE;
                            }
                            if (IsTouching(s) && s.Name.Equals("EnemyGreen"))
                            {
                                s.Hp -= rnd.Next(4) + 30;
                                arrowStates = BulletState.DIE;
                            }
                        }
                        i++;
                        break;
                    }
                case BulletState.DIE:
                    {
                        IsActive = false;
                        break;
                    }
                default:
                    break;
            }
            base.Update(gameTime, gameObjects);
        }
    }

}
