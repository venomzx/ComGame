using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarGun.Managers;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace StarGun.Screen
{
	class MenuScreen : _GameScreen
	{
		private Color _Color = new Color(250, 250, 250, 0);
		private Texture2D BG, Black, Lvlselector,About;
		private Texture2D StartH, AboutH, OptionH, ExitH, EasyH, EasyAb, apply, applyH, back, backH, checkBoxYes, checkBoxNo, Arrow;
		private SpriteFont Arial;
		private Vector2 fontSize;
		private Song BGM2;
		private SoundEffectInstance SoundClickUI;

		private bool fadeFinish = false;
		private bool showOption = false, showAbout = false, showStart = false;
		private bool mhStart = false, mhOption = false, mhAbout = false, mhExit = false, mhBack = false, mhApply, mhEasy = false;
		private bool mhsStart = false, mhsOption = false, mhsAbout = false, mhsExit = false;
		private bool mainScreen = true;
		

		private float _timer = 0.0f;
		private float timerPerUpdate = 0.03f;
		private int alpha = 255;

		// Varible On Option Screen
		private bool FullScreen = Singleton.Instance.IsFullScreen;
		private bool ShowFPS = Singleton.Instance.cmdShowFPS;
		private int MasterBGM = Convert.ToInt32(Singleton.Instance.BGM_MasterVolume* 50);
		private int MasterSFX = Convert.ToInt32(Singleton.Instance.SFX_MasterVolume * 50);

		public void Initial()
		{
			BGM2 = content.Load<Song>("Audios/MenuSound/BGMenuSound");
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = Singleton.Instance.BGM_MasterVolume;
			MediaPlayer.Play(BGM2);
		}
		public override void LoadContent()
		{
			base.LoadContent();
			// Texture2D
			BG = content.Load<Texture2D>("MenuScreen/mainmenubg");
			Black = content.Load<Texture2D>("SplashScreen/Bg");
			Lvlselector = content.Load<Texture2D>("MenuScreen/levelselector");
			StartH = content.Load<Texture2D>("MenuScreen/startbutton");
			AboutH = content.Load<Texture2D>("MenuScreen/aboutposter");
			OptionH = content.Load<Texture2D>("MenuScreen/optionbutt");
			ExitH = content.Load<Texture2D>("MenuScreen/QuitButt");
			EasyH = content.Load<Texture2D>("MenuScreen/ez");
			EasyAb = content.Load<Texture2D>("MenuScreen/ezZomb");
			checkBoxYes = content.Load<Texture2D>("MenuScreen/yes");
			checkBoxNo = content.Load<Texture2D>("MenuScreen/no");
			apply = content.Load<Texture2D>("MenuScreen/apply1");
			applyH = content.Load<Texture2D>("MenuScreen/apply2");
			backH = content.Load<Texture2D>("MenuScreen/back");
			back = content.Load<Texture2D>("MenuScreen/back2");
			Arrow = content.Load<Texture2D>("MenuScreen/right");
			About = content.Load<Texture2D>("MenuScreen/about");
			// Fonts
			Arial = content.Load<SpriteFont>("Fonts/Arial");
			// Sounds
			SoundClickUI = content.Load<SoundEffect>("Audios/MenuSound/ClickSound").CreateInstance();
			// Call Init
			Initial();
		}
		public override void UnloadContent()
		{
			base.UnloadContent();
		}
		public override void Update(GameTime gameTime)
		{
			SoundClickUI.Volume = Singleton.Instance.SFX_MasterVolume;

			Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
			Singleton.Instance.CurrentMouse = Mouse.GetState();
			if (mainScreen)
			{
				// Click start game
				if ((Singleton.Instance.CurrentMouse.X > 220 && Singleton.Instance.CurrentMouse.Y > 150) && (Singleton.Instance.CurrentMouse.X < 400 && Singleton.Instance.CurrentMouse.Y < 250))
				{
					mhStart = true;
					if (!mhsStart)
					{
						mhsStart = true;
					}
					if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
					{
						//showStart = true;
						mainScreen = false;
						SoundClickUI.Play();
						ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.PlayScreenEasy);
					}
				}
				else
				{
					mhStart = false;
					mhsStart = false;
					mhEasy = false;
				}
				// Click option
				if ((Singleton.Instance.CurrentMouse.X > 230 && Singleton.Instance.CurrentMouse.Y > 380) && (Singleton.Instance.CurrentMouse.X < 440 && Singleton.Instance.CurrentMouse.Y < 490))
				{
					mhOption = true;
					if (!mhsOption)
					{
						mhsOption = true;
					}
					if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
					{
						showOption = true;
						mainScreen = false;
						SoundClickUI.Play();
					}
				}
				else
				{
					mhsOption = false;
					mhOption = false;
				}
				// Click About
				if ((Singleton.Instance.CurrentMouse.X > 590 && Singleton.Instance.CurrentMouse.Y > 80) && (Singleton.Instance.CurrentMouse.X < 800 && Singleton.Instance.CurrentMouse.Y < 370))
				{
					mhAbout = true;
					if (!mhsAbout)
					{
						mhsAbout = true;
					}
					if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
					{
						showAbout = true;
						mainScreen = false;
						SoundClickUI.Play();
					}
				}
				else
				{
					mhsAbout = false;
					mhAbout = false;
				}
				// Click Exit
				if ((Singleton.Instance.CurrentMouse.X > 0 && Singleton.Instance.CurrentMouse.Y > 560) && (Singleton.Instance.CurrentMouse.X < 400 && Singleton.Instance.CurrentMouse.Y < 720))
				{
					mhExit = true;
					if (!mhsExit)
					{
						SoundClickUI.Play();
						mhsExit = true;
					}
					if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
					{
						Singleton.Instance.cmdExit = true;
					}
				}
				else
				{
					mhExit = false;
					mhsExit = false;
				}
			}
			else
			{
				// Click Back
				if ((Singleton.Instance.CurrentMouse.X > 0 && Singleton.Instance.CurrentMouse.Y > 530) && (Singleton.Instance.CurrentMouse.X < 400 && Singleton.Instance.CurrentMouse.Y < 720))
				{
					mhBack = true;
					if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
					{
						mainScreen = true;
						showAbout = false;
						showOption = false;
						showStart = false;
						SoundClickUI.Play();
					}
				}
				else
				{
					mhBack = false;
				}
				if (showOption)
				{
					// Click change CheckBox FullScreen
					if ((Singleton.Instance.CurrentMouse.X > 800 && Singleton.Instance.CurrentMouse.Y > 325) && (Singleton.Instance.CurrentMouse.X < (800 + checkBoxNo.Width) && Singleton.Instance.CurrentMouse.Y < (325 + checkBoxNo.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							FullScreen = !FullScreen;
						}
					}
					// Click change CheckBox ShowFPS
					if ((Singleton.Instance.CurrentMouse.X > 800 && Singleton.Instance.CurrentMouse.Y > 400) && (Singleton.Instance.CurrentMouse.X < (800 + checkBoxNo.Width) && Singleton.Instance.CurrentMouse.Y < (400 + checkBoxNo.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							ShowFPS = !ShowFPS;
						}
					}

					// Click Arrow BGM
					if ((Singleton.Instance.CurrentMouse.X > 700 && Singleton.Instance.CurrentMouse.Y > 140) && (Singleton.Instance.CurrentMouse.X < (700 + Arrow.Width) && Singleton.Instance.CurrentMouse.Y < (140 + Arrow.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							if (MasterBGM > 0) MasterBGM -= 5;
						}
					}
					else if ((Singleton.Instance.CurrentMouse.X > 900 && Singleton.Instance.CurrentMouse.Y > 140) && (Singleton.Instance.CurrentMouse.X < (900 + Arrow.Width) && Singleton.Instance.CurrentMouse.Y < (140 + Arrow.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							if (MasterBGM < 100) MasterBGM += 5;
						}
					}
					// Click Arrow SFX
					if ((Singleton.Instance.CurrentMouse.X > 700 && Singleton.Instance.CurrentMouse.Y > 215) && (Singleton.Instance.CurrentMouse.X < (700 + Arrow.Width) && Singleton.Instance.CurrentMouse.Y < (215 + Arrow.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							if (MasterSFX > 0) MasterSFX -= 5;
						}
					}
					else if ((Singleton.Instance.CurrentMouse.X > 900 && Singleton.Instance.CurrentMouse.Y > 215) && (Singleton.Instance.CurrentMouse.X < (900 + Arrow.Width) && Singleton.Instance.CurrentMouse.Y < (215 + Arrow.Height)))
					{
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							if (MasterSFX < 100) MasterSFX += 5;
						}
					}
					// Apply Option to Game
					if ((Singleton.Instance.CurrentMouse.X > 760 && Singleton.Instance.CurrentMouse.Y > 570) && (Singleton.Instance.CurrentMouse.X < 1000 && Singleton.Instance.CurrentMouse.Y < 700))
					{
						mhApply = true;
						if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
						{
							if (Singleton.Instance.IsFullScreen != FullScreen) Singleton.Instance.cmdFullScreen = true;
							SoundClickUI.Play();
							Singleton.Instance.cmdShowFPS = ShowFPS;
							Singleton.Instance.BGM_MasterVolume = MasterBGM / 100f;
							Singleton.Instance.SFX_MasterVolume = MasterSFX / 100f;
						}
					}
					else
					{
						mhApply = false;
					}
				}
			}

			// fade out
			if (!fadeFinish)
			{
				_timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
				if (_timer >= timerPerUpdate)
				{
					alpha -= 5;
					_timer -= timerPerUpdate;
					if (alpha <= 5)
					{
						fadeFinish = true;
					}
					_Color.A = (byte)alpha;
				}
			}
			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(BG, Vector2.Zero, Color.White);
			// Draw mouse onHover button
			if (mhAbout)
			{
				spriteBatch.Draw(AboutH, new Vector2(450, 130), Color.White);
			}
			if (mhExit)
			{
				spriteBatch.Draw(ExitH, new Vector2(0, 520), Color.White);
			}
			if (mhOption)
			{
				spriteBatch.Draw(OptionH, new Vector2(211, 363), Color.White);
			}
			if (mhStart)
			{
				spriteBatch.Draw(StartH, new Vector2(180, 130), Color.White);
			}
			// Draw UI when is NOT MainMenu
			if (!mainScreen)
			{
				spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
				spriteBatch.Draw(back, new Vector2(0, 521), Color.White);
				if (mhBack)
				{
						spriteBatch.Draw(backH, new Vector2(0, 521), Color.White);
				}
				// Draw Start Screen
				if (showStart)
				{
					spriteBatch.Draw(Lvlselector, Vector2.Zero, new Color(255, 255, 255, 210));
					if (mhEasy == true)
					{
						spriteBatch.Draw(EasyH, new Vector2(207, 126), Color.White);
						spriteBatch.Draw(EasyAb, new Vector2(635, 118), Color.White);
					}
				}
				// Draw Option Screen
				if (showOption)
				{
					fontSize = Arial.MeasureString("Option");
					spriteBatch.DrawString(Arial, "Option", new Vector2(Singleton.Instance.Diemensions.X / 2 - fontSize.X / 2, 55), Color.White);

					spriteBatch.DrawString(Arial, "BGM Volume", new Vector2(300, 150), Color.White);
					spriteBatch.Draw(Arrow, new Vector2(700, 140), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
					spriteBatch.DrawString(Arial, MasterBGM.ToString(), new Vector2(800, 150), Color.White);
					spriteBatch.Draw(Arrow, new Vector2(900, 140), Color.White);

					spriteBatch.DrawString(Arial, "SFX Volume", new Vector2(300, 225), Color.White);
					spriteBatch.Draw(Arrow, new Vector2(700, 215), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
					spriteBatch.DrawString(Arial, MasterSFX.ToString(), new Vector2(800, 225), Color.White);
					spriteBatch.Draw(Arrow, new Vector2(900, 215), Color.White);

					spriteBatch.DrawString(Arial, "Full Screen", new Vector2(300, 325), Color.White);
					if (!FullScreen)
					{
						spriteBatch.Draw(checkBoxNo, new Vector2(800, 325), Color.White);
					}
					else
					{
						spriteBatch.Draw(checkBoxYes, new Vector2(800, 325), Color.White);
					}

					spriteBatch.DrawString(Arial, "Show FPS", new Vector2(300, 400), Color.White);
					if (!ShowFPS)
					{
						spriteBatch.Draw(checkBoxNo, new Vector2(800, 400), Color.White);
					}
					else
					{
						spriteBatch.Draw(checkBoxYes, new Vector2(800, 400), Color.White);
					}

					if (mhApply)
					{
						spriteBatch.Draw(applyH, new Vector2(730, 535), Color.White);
					}
					else
					{
						spriteBatch.Draw(apply, new Vector2(730, 535), Color.White);
					}
				}
				// Draw About Screen
				if (showAbout)
				{
					spriteBatch.Draw(About, Vector2.Zero, new Color(255, 255, 255, 210));
				}
			}
			// Draw fade out
			if (!fadeFinish)
			{
				spriteBatch.Draw(Black, Vector2.Zero, _Color);
			}
		}
	}
}
