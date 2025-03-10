﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGun.GameObjects
{
    public class GameObject : ICloneable
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public float Rotation,Angle;
        public Vector2 Scale;
        public Vector2 Velocity;
        public Vector2 Resistance;
        public string Name;
        public int Hp;
        public Vector2 BDirection , RDirection;
        public bool IsActive;
        public Rectangle Viewport;
        public Dictionary<string, SoundEffectInstance> SoundEffects;
        

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public GameObject(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            //IsActive = true;

        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Reset()
        {

        }



        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Collision
        protected bool IsTouching(GameObject g)
        {
            return IsTouchingLeft(g) ||
                IsTouchingTop(g) ||
                IsTouchingRight(g) ||
                IsTouchingBottom(g);
        }

        protected bool IsTouchingLeft(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Left &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Right &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Top;
        }

        protected bool IsTouchingBottom(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Bottom &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion





    }
}
