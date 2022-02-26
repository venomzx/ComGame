﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GP_Midterm_BubblePuzzle {
	 class Singleton {
		public Vector2 Diemensions = new Vector2(1280,720);
		public int BGM_MasterVolume = 100;
        public int Score = 0;
        public float SFX_MasterVolume = 1f;
		public bool Shooting = false;
		public List<Vector2> removeBubble = new List<Vector2>();
		public bool cmdExit = false, cmdFullScreen = false, cmdShowFPS = false;
		public bool IsFullScreen;
		public string BestTime, BestScore;

		public MouseState MousePrevious, MouseCurrent;

		private static Singleton instance;
		public static Singleton Instance {
			get {
				if (instance == null) {
					instance = new Singleton();
				}
				return instance;
			}
		}
	}
}
