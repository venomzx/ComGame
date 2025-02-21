using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarGun.GameObjects
{
    class EnemyOrange : GameObject
    {

        Vector2 mousePosition;

        public EnemyOrange(Texture2D texture) : base(texture)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                            Position,
                            null,
                            Color.White,
                            Rotation,
                            new Vector2(0 ,0),
                            1f,
                            SpriteEffects.None,
                            0
                            );
            Reset();
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {

            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            //rotaion
            mousePosition = new Vector2(Singleton.Instance.CurrentMouse.X, Singleton.Instance.CurrentMouse.Y);
            Vector2 direction = mousePosition - Position;
            direction.Normalize();
            
            if (Hp <=0)
            {
                IsActive = false;
            }
            base.Update(gameTime, gameObjects);
        }

    }
}
