using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace StarGun.GameObjects
{
	class GunEasy : GameObject
	{
		private float angle;

		public GunEasy(Texture2D texture) : base(texture)
		{

		}

		public override void Reset()
		{
			base.Reset();
		}

		public override void Update(GameTime gameTime, List<GameObject> gameObjects)
		{
			Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
			Singleton.Instance.CurrentMouse = Mouse.GetState();
			//Y < 500 , X > 210 260
			if (Singleton.Instance.CurrentMouse.Y < Singleton.Instance.YPositionDamAvatar - 50 && Singleton.Instance.CurrentMouse.X > Singleton.Instance.XPositionDamAvatar + 190)
			{
				angle = (float)Math.Atan2((Position.Y + _texture.Height / 2) - Singleton.Instance.CurrentMouse.Y, (Position.X + _texture.Width / 2) - Singleton.Instance.CurrentMouse.X);

			}
			base.Update(gameTime, gameObjects);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, Position + new Vector2(50, 50), null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(50, 50), 1.5f, SpriteEffects.None, 0f);
			Reset();
			base.Draw(spriteBatch);
		}
	}
}
