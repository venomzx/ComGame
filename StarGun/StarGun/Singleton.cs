using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace StarGun
{
	class Singleton
	{
		//Screen Width and Height
		public Vector2 Diemensions = new Vector2(1280, 720);
		public const int SCREENWIDTH = 1280;
		public const int SCREENHEIGHT = 720;

		public float BGM_MasterVolume = 1f;
		public float SFX_MasterVolume = 1f;

		//Exit, FullSreen, ShowFPS 
		public bool cmdExit = false, cmdFullScreen = false, cmdShowFPS = false;
		public bool IsFullScreen;

		//Wind 
		public int Wind;

		//Angle and Power
		public float DamAngle;
		public float DamPower;

		public float minAngle = 15;
		public float maxAngle = 90;
		public float minPower = 0;
		public float maxPower = 100;

		//hitbox
		public const int AjarnDamWIDTH = 100;
		public const int AjarnDamHEIGHT = 100;
		public const int BULLETWIDTH = 100;
		public const int BULLETHEIGHT = 100;

		//HP min
		public const int minHP = 50;

		//Player Position
		public int XPositionDamAvatar = 110;
		public int YPositionDamAvatar = 550;

		//Enermy Position
		public int XPositionEnemyRed = 1000;
		public int YPositionEnemyRed = 350;

		public int XPositionEnemyOrange = 800;
		public int YPositionEnemyOrange = 550;

		public int XPositionEnemyGreen = 900;
		public int YPositionEnemyGreen = 450;

		//Gravity
		public int Gravity = 5;

		//Shoot
		public bool Shooting = false;

		/*public enum SelectCharecter
		{
			SelectArcher
		}

		public SelectCharecter CurrentCharecter;*/

		public KeyboardState PreviousKey, CurrentKey;
		public MouseState CurrentMouse;
		public MouseState PreviousMouse;


		private static Singleton instance;
		public static Singleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Singleton();
				}
				return instance;
			}
		}
	}
}
