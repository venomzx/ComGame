using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarGun.Managers;

namespace StarGun.Screen
{
	class SplashScreen : _GameScreen
	{
		private Vector2 fontSize;
		private Color _Color; // for update color alpha
		private SpriteFont Arial;
		private Texture2D Logo,Bg;
		private int alpha; // Value of alpha in color for fade logo and text
		private int Scene; // order of index to display splash screen
		private float _timer; // Elapsed time in game
		private float _timePerUpdate; // Will do update function when _timer > _timePerUpdate
		private bool Show; // true will fade in and false will fade out
		//private String Scene = Singleton.Instance.Scene;
		public SplashScreen()
		{
			Show = true;
			_timePerUpdate = 0.05f;
			Scene = 0;
			alpha = 250;
			_Color = new Color(255, 255, 255, alpha);
		}
		public override void LoadContent()
		{
			base.LoadContent();
			Logo = content.Load<Texture2D>("SplashScreen/Logo");
			Bg = content.Load<Texture2D>("SplashScreen/Bg");
			Arial = content.Load<SpriteFont>("Fonts/Arial");
		}
		public override void UnloadContent() { base.UnloadContent(); }
		public override void Update(GameTime gameTime)
		{
			// Add elapsed time to _timer
			_timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
			if (_timer >= _timePerUpdate)
			{
				if (Show)
				{
					//_Color = (red, green, blue, alpha);
					//fade in
					alpha -= 5;
					// when fade in finish
					if (alpha <= 0)
					{
						Show = false;
						// transition screen
						if (Scene == 2)
						{
							ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
							//Singleton.Scene = "MenuScreen";
						}
						
					}
				}
				else
				{
					// fade out
					alpha += 5;
					// whene fade out finish
					if (alpha >= 250)
					{
						Show = true;
						// Change display index and set next display
						Scene++;
						if (Scene == 1)
						{
							_Color = Color.Black;
							_timePerUpdate -= 0.015f;
						}
					}
				}
				_timer -= _timePerUpdate;
				_Color.A = (byte)alpha;
			}
			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			switch (Scene)
			{
				case 0:
					spriteBatch.Draw(Bg, Vector2.Zero, Color.Black);
					spriteBatch.Draw(Logo, new Vector2((Singleton.Instance.Diemensions.X - Logo.Width) / 2, (Singleton.Instance.Diemensions.Y - Logo.Height) / 4), _Color);
					fontSize = Arial.MeasureString("StarGun");
					spriteBatch.DrawString(Arial, "StarGun", new Vector2(460,530), Color.White);
					spriteBatch.Draw(Bg, Vector2.Zero, _Color);
					break;
				case 1:
					spriteBatch.Draw(Bg, Vector2.Zero, Color.Black);
					fontSize = Arial.MeasureString("pepodev REFERENCE");
					spriteBatch.DrawString(Arial, "pepodev REFERENCE", new Vector2((Singleton.Instance.Diemensions.X - fontSize.X) / 2, (Singleton.Instance.Diemensions.Y - fontSize.Y) / 2), Color.White);
					spriteBatch.Draw(Bg, Vector2.Zero, _Color);
					break;
				case 2:
					spriteBatch.Draw(Bg, Vector2.Zero, Color.Black);
					break;
			}

		}
	}
}
