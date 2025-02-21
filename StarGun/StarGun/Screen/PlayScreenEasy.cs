using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using StarGun.GameObjects;
using StarGun.Managers;

namespace StarGun.Screen
{
    class PlayScreenEasy : _GameScreen
    {
        SpriteFont _spriteFont, Arial, TextUI;
        List<GameObject> _gameObjects;

        //Player
        AjarnDam DamAvatar;

        //Enemy
        EnemyRed _EnemyRed;
        EnemyOrange _EnemyOrange;
        EnemyGreen _EnemyGreen;


        private GunEasy Gun;

        DamBullet _Damshooted, _DamNextBullet;

        //Song
        Song BGplaySound;
        Boolean PlaySong = false, PlayEffect = false, RedDead = false, OrangeDead = false, GreenDead = false;
        SoundEffectInstance ShootSound, DeadSound, GameEndSFX;

        //Time
        long time;
        float tick, Cooldown;
        int _numObject, PoXI, PoYI;

        //Texture Object
        Texture2D GunTexture;
        Texture2D Black, background, Dam, Bullet, EnemyRed, EnemyOrange, EnemyGreen;
        Texture2D minHP, maxHP;
        Vector2 mousePosition, fontLength;
        private Vector2 fontSize;
        Boolean randWind = false;

        private Color _Color;
        private int alpha = 255;

        //GameEnd 
        private bool fadeFinish = false;
        private bool gameOver = false;
        private bool gameWin = false;
        private bool GameEndSound = false; 
        private float timerPerUpdate = 0.05f;
        private float _timer = 0f;
        //private float __timer = 0f;

        Random rnd;

        public void InitialPlay()
        {
            // TODO: Add your initialization logic here
            rnd = new Random();
            _Color = new Color(255, 255, 255, alpha);
            _gameObjects = new List<GameObject>();
            Reset();
            MediaPlayer.Stop();

            if (!PlaySong)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = Singleton.Instance.BGM_MasterVolume / 2;
                MediaPlayer.Play(BGplaySound);
                PlaySong = true;
            }
        }
 
        public override void LoadContent()
        {
            base.LoadContent();

            //SplashScreen
            Black = content.Load<Texture2D>("SplashScreen/Bg");

            //BackGround PlayScreen
            background = content.Load<Texture2D>("PlayScreen/BGplay");
           
            //Dam Player Img
            Dam = content.Load<Texture2D>("PlayScreen/AJDamCharacter");

            //Catapult and Bullet
            GunTexture = content.Load<Texture2D>("PlayScreen/Catapult");
            Bullet = content.Load<Texture2D>("PlayScreen/Bullet");

            //Enemy Img
            EnemyRed = content.Load<Texture2D>("PlayScreen/EnemyRed");
            EnemyOrange = content.Load<Texture2D>("PlayScreen/EnemyOrange");
            EnemyGreen = content.Load<Texture2D>("PlayScreen/EnemyGreen");

            //Hp Bar
            maxHP = content.Load<Texture2D>("PlayScreen/maxHP");
            minHP = content.Load<Texture2D>("PlayScreen/minHP");

            //Font
            Arial = content.Load<SpriteFont>("Fonts/Arial");
            _spriteFont = content.Load<SpriteFont>("Fonts/Arial");
            TextUI = content.Load<SpriteFont>("Fonts/Arial");

            //Sound---------------
            BGplaySound = content.Load<Song>("Audios/PlaySound/BGplaySound");
            
            ShootSound = content.Load<SoundEffect>("Audios/PlaySound/ShootSound").CreateInstance();
            DeadSound = content.Load<SoundEffect>("Audios/PlaySound/DeadSound").CreateInstance();

            //Game End Sound
            GameEndSFX = content.Load<SoundEffect>("Audios/PlaySound/GameEndSound").CreateInstance();
            GameEndSFX.IsLooped = false;


            InitialPlay();

        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;
            Singleton.Instance.CurrentKey = Keyboard.GetState();

            //Escape to Exit
            if (!Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Escape))
            {
                Singleton.Instance.cmdExit = true;
            }
            if (!gameOver && !gameWin)
            {
                Cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _numObject = _gameObjects.Count;
                    
                mousePosition = new Vector2(Singleton.Instance.CurrentMouse.X, Singleton.Instance.CurrentMouse.Y);

                // TODO: Add your update logic here
                tick += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                time += gameTime.ElapsedGameTime.Ticks;
                setAnglePower();
                checkAnglePower();
                checkEnemyDead();
                CheckWin();

                //random Wind
                if (!randWind)
                {
                    Singleton.Instance.Wind = rnd.Next(11) - 5;
                    randWind = true;
                }
                //Shoot Bullet
                if ((Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released))
                {
                    {
                        ShootSound.Volume = Singleton.Instance.SFX_MasterVolume;
                        ShootSound.Play();

                        if (DamAvatar.IsActive)
                        {
                            DamBullet DamBullet = _Damshooted;
                            DamBullet.Position = new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - Singleton.BULLETWIDTH / 2, Singleton.Instance.YPositionDamAvatar - Singleton.BULLETHEIGHT / 2);
                            DamBullet.Name = "Bullet";
                            DamBullet.arrowStates = DamBullet.BulletState.FIRED;
                            DamBullet.Velocity = new Vector2((20 * Singleton.Instance.DamPower) + 1000, Singleton.Instance.DamPower + 2000);
                            //DamBullet.Velocity = new Vector2((2 * Singleton.Instance.DamPower) + 100, Singleton.Instance.DamPower + 100);
                            DamBullet.Angle = (float)((180 - Singleton.Instance.DamAngle) * (Math.PI / 180));
                            DamBullet.Resistance = new Vector2(0, Singleton.Instance.Gravity);
                            DamBullet.Rotation = 45;
                            _gameObjects.Add(_Damshooted);


                            _Damshooted = _DamNextBullet;

                            _DamNextBullet = new DamBullet(Bullet)
                            {
                                IsActive = true,
                                Name = "NextBullet",
                                Viewport = new Rectangle(0, 0, Singleton.BULLETWIDTH, Singleton.BULLETHEIGHT),
                                Position = new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - Singleton.BULLETWIDTH / 2, Singleton.Instance.YPositionDamAvatar - Singleton.BULLETHEIGHT / 2),
                        };

                            _gameObjects.Add(_DamNextBullet);
                            Cooldown = 0;
                        }
                        randWind = false;
                    }
                }

                for (int i = 0; i < _numObject; i++)
                {
                    if (_gameObjects[i].IsActive)
                    {
                        _gameObjects[i].Update(gameTime, _gameObjects);
                    }
                }
                for (int i = 0; i < _numObject; i++)
                {
                    if (!_gameObjects[i].IsActive)
                    {
                        _gameObjects.RemoveAt(i);
                        i--;
                        _numObject--;
                    }
                }
            }
            else
            {
                // play end sound 1 times
                if (!GameEndSound)
                {
                    GameEndSFX.Volume = Singleton.Instance.SFX_MasterVolume;
                    GameEndSFX.Play();
                    GameEndSound = true;
                }              
                CheckWin();
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
            //ScreenPlay Background
            spriteBatch.Draw(background, new Rectangle(0, 0, Singleton.SCREENWIDTH, Singleton.SCREENHEIGHT), Color.White);

            //Wind Draw
            fontLength = _spriteFont.MeasureString("Wind" + Singleton.Instance.Wind.ToString());
            spriteBatch.DrawString(_spriteFont, " Wind " + Singleton.Instance.Wind.ToString(), new Vector2((Singleton.SCREENWIDTH - fontLength.X) / 2, 100), Color.RoyalBlue);

            /*
            //Dam Player Hp Draw 
            if (DamAvatar.IsActive)
            {
                spriteBatch.Draw(minHP, new Rectangle(Singleton.Instance.XPositionDamAvatar, Singleton.Instance.YPositionDamAvatar - 10, Singleton.minHP, 5), Color.White); // min 
                spriteBatch.Draw(maxHP, new Rectangle(Singleton.Instance.XPositionDamAvatar, Singleton.Instance.YPositionDamAvatar - 10, (DamAvatar.Hp * Singleton.minHP) / 80, 5), Color.White); // max
            }
            */

            //Enermy Hp Draw
            if (_EnemyRed.IsActive)
            {
                spriteBatch.Draw(minHP, new Rectangle(Singleton.Instance.XPositionEnemyRed + 30, Singleton.Instance.YPositionEnemyRed- 10, Singleton.minHP, 5), Color.White); // min 
                spriteBatch.Draw(maxHP, new Rectangle(Singleton.Instance.XPositionEnemyRed + 30, Singleton.Instance.YPositionEnemyRed- 10, (_EnemyRed.Hp * Singleton.minHP) / 80, 5), Color.White); // max
            }
            if (_EnemyOrange.IsActive)
            {
                spriteBatch.Draw(minHP, new Rectangle(Singleton.Instance.XPositionEnemyOrange + 30, Singleton.Instance.YPositionEnemyOrange, Singleton.minHP, 5), Color.White); // min 
                spriteBatch.Draw(maxHP, new Rectangle(Singleton.Instance.XPositionEnemyOrange + 30, Singleton.Instance.YPositionEnemyOrange, (_EnemyOrange.Hp * Singleton.minHP) / 80, 5), Color.White); // max
            }
            if (_EnemyGreen.IsActive)
            {
                spriteBatch.Draw(minHP, new Rectangle(Singleton.Instance.XPositionEnemyGreen + 30, Singleton.Instance.YPositionEnemyGreen, Singleton.minHP, 5), Color.White); // min 
                spriteBatch.Draw(maxHP, new Rectangle(Singleton.Instance.XPositionEnemyGreen + 30, Singleton.Instance.YPositionEnemyGreen, (_EnemyGreen.Hp * Singleton.minHP) / 80, 5), Color.White); // max
            }

            //Text Power and Angle Draw
            fontLength = _spriteFont.MeasureString("Power " + Singleton.Instance.DamPower.ToString());
            spriteBatch.DrawString(TextUI, "Power " + Singleton.Instance.DamPower.ToString(), new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - GunTexture.Width / 2, 420), Color.Salmon);
            fontLength = _spriteFont.MeasureString("Angle " + Singleton.Instance.DamAngle.ToString() + "*");
            spriteBatch.DrawString(TextUI, "Angle " + Singleton.Instance.DamAngle.ToString() + "*", new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - GunTexture.Width / 2, 450), Color.BlueViolet);

            //Draw All Object
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                   _gameObjects[i].Draw(spriteBatch);
            }

            
            //GameEnd Draw
            if (gameOver)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("GameOver !!");
                spriteBatch.DrawString(Arial, "GameOver !!", Singleton.Instance.Diemensions / 2 - fontSize / 2, _Color);
            }

            if (gameWin)
            {
                spriteBatch.Draw(Black, Vector2.Zero, new Color(255, 255, 255, 210));
                fontSize = Arial.MeasureString("GameWin !!");
                spriteBatch.DrawString(Arial, "GameWin !!", Singleton.Instance.Diemensions / 2 - fontSize / 2, _Color);
            }

            // Draw fade out
            if (!fadeFinish)
            {
                spriteBatch.Draw(Black, Vector2.Zero, _Color);
            }
        }


        protected void Reset()
        {
            time = 0;
            tick = 0;
            _gameObjects.Clear();


            //Gun Object
            Gun = new GunEasy(GunTexture)
            {
                IsActive = true,
                Name = "GunEasy",
                Position = new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - GunTexture.Width / 2, Singleton.Instance.YPositionDamAvatar - GunTexture.Height / 2),
            };
            _gameObjects.Add(Gun);

            //Dam Player Object

            DamAvatar = new AjarnDam(Dam)
            {
                IsActive = true,
                Name = "Dam",
                /*
                Hp = 80,
                */
                Viewport = new Rectangle(300, 300, Singleton.AjarnDamWIDTH, Singleton.AjarnDamHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionDamAvatar - GunTexture.Width / 2, Singleton.Instance.YPositionDamAvatar - GunTexture.Height / 2),

            };
            _gameObjects.Add(DamAvatar);

            //Enermy Object
            _EnemyRed= new EnemyRed(EnemyRed)
            {
                IsActive = true,
                Name = "EnemyRed",
                Hp = 100,
                Viewport = new Rectangle(0, 0, Singleton.AjarnDamWIDTH, Singleton.AjarnDamHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionEnemyRed, Singleton.Instance.YPositionEnemyRed),

            };
            _gameObjects.Add(_EnemyRed);

            _EnemyOrange = new EnemyOrange(EnemyOrange)
            {
                IsActive = true,
                Name = "EnemyOrange",
                Hp = 100,
                Viewport = new Rectangle(0, 0, Singleton.AjarnDamWIDTH, Singleton.AjarnDamHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionEnemyOrange, Singleton.Instance.YPositionEnemyOrange),

            };
            _gameObjects.Add(_EnemyOrange);

            _EnemyGreen = new EnemyGreen(EnemyGreen)
            {
                IsActive = true,
                Name = "EnemyGreen",
                Hp = 100,
                Viewport = new Rectangle(0, 0, Singleton.AjarnDamWIDTH, Singleton.AjarnDamHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionEnemyGreen, Singleton.Instance.YPositionEnemyGreen),

            };
            _gameObjects.Add(_EnemyGreen);

            //Bullet Object
            _Damshooted = new DamBullet(Bullet)
            {
                IsActive = true,
                Name = "Bullet",
                Viewport = new Rectangle(0, 0, Singleton.BULLETWIDTH, Singleton.BULLETHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - Singleton.BULLETWIDTH / 2, Singleton.Instance.YPositionDamAvatar - Singleton.BULLETHEIGHT / 2),
            Rotation = 90,
            };
            _gameObjects.Add(_Damshooted);

            //Next Bullet Object
            _DamNextBullet = new DamBullet(Bullet)
            {
                IsActive = true,
                Name = "NextBullet",
                Viewport = new Rectangle(0, 0, Singleton.BULLETWIDTH, Singleton.BULLETHEIGHT),
                Position = new Vector2(Singleton.Instance.XPositionDamAvatar + 100 - Singleton.BULLETWIDTH / 2, Singleton.Instance.YPositionDamAvatar - Singleton.BULLETHEIGHT / 2),
                Rotation = 90,
            };
            _gameObjects.Add(_DamNextBullet);

            //--
            foreach (GameObject s in _gameObjects)
            {
                s.Reset();
            }

        }
        
        protected void checkAnglePower()
        {
            //Check Angle 15*-90*
            if (Singleton.Instance.DamAngle < Singleton.Instance.minAngle)
            {
                Singleton.Instance.DamAngle = Singleton.Instance.minAngle;
            }
            else if (Singleton.Instance.DamAngle > Singleton.Instance.maxAngle)
            {
                Singleton.Instance.DamAngle = Singleton.Instance.maxAngle;
            }
            //Check Power 0 - 100
            if (Singleton.Instance.DamPower < Singleton.Instance.minPower)
            {
                Singleton.Instance.DamPower = Singleton.Instance.minPower;
            }
            else if (Singleton.Instance.DamPower > Singleton.Instance.maxPower)
            {
                Singleton.Instance.DamPower = Singleton.Instance.maxPower;
            }
        }
        protected void setAnglePower()
        {
            //Set Angle and Power
            Singleton.Instance.DamPower = (float)(Math.Sqrt((Math.Abs(Singleton.Instance.CurrentMouse.X - 300) ^ 2) + (Math.Abs(720 - Singleton.Instance.CurrentMouse.Y - 170) ^ 2)))*3;
            Singleton.Instance.DamAngle = (float)(Math.Atan2(((720 - Singleton.Instance.CurrentMouse.Y) - 170), (Singleton.Instance.CurrentMouse.X - 300))*50);
            /*
            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right))
            {
                Singleton.Instance.DamPower++;
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left))
            {
                Singleton.Instance.DamPower--;
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up))
            {
                Singleton.Instance.DamAngle++;
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Down))
            {
                Singleton.Instance.DamAngle--;
            }
            */
        }
        protected void checkEnemyDead()
        {
            //PlaySound Enemy Dead

            if ((_EnemyRed.Hp <= 0 && RedDead == false))
            {
                PlayEffect = true;
                RedDead = true;
            }
            if ((_EnemyOrange.Hp <= 0 && OrangeDead == false))
            {
                PlayEffect = true;
                OrangeDead = true;
            }
            if ((_EnemyGreen.Hp <= 0 && GreenDead == false))
            {
                PlayEffect = true;
                GreenDead = true;
            }            
            if (PlayEffect) {
                DeadSound.Volume = Singleton.Instance.SFX_MasterVolume / 2;
                DeadSound.Play();
                PlayEffect = false;
            }
        }
        
        protected void CheckWin()
        {
            /*
            //GameOver
            if (DamAvatar.Hp <= 0)
            {
                gameOver = true;
                if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released
                    || !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Space)
                    )
                {
                    //Press Spacebar or LeftClick return MenuScreen
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                }
            }
            */

            //GameWin
            if (_EnemyRed.Hp <= 0 && _EnemyOrange.Hp <= 0 && _EnemyGreen.Hp <= 0)
            {
                gameWin = true;
                if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released
                    || !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Space)
                    )
                {
                    //Press Spacebar or LeftClick return MenuScreen
                    ScreenManager.Instance.LoadScreen(ScreenManager.GameScreenName.MenuScreen);
                }
            }
        }
    }
}
