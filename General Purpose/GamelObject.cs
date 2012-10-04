using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{
    class GameObject
    {
        //Texture Manager
        protected Textures TEXTURES = Textures.GetInstance();
               

        //Sprite-Only members
        private Vector2 centerImg;
        public Textures.TextureName textureName;
        public Texture2D texture;                           //Automaticaly loaded from textureName above in LoadContent()
        public Color tint = Color.White;                    //sprite tinting color
        public SpriteEffects effects = SpriteEffects.None;  //sprite effects, horiz/vert flip
        public float depth = 0.0f;                          //drawing depth on the screen
        public Rectangle source = new Rectangle(0,0,1,1);   //source rectangle of image
        public float rotation = 0.0f;                       //Sprite rotation

        //Positioning
        public Vector2 position = Vector2.Zero;         //the position of the object on the screen
        public Rectangle bounds = Rectangle.Empty;      //The (square) region of the object on the screen

        //Moving
        public bool moving = true;
        private Vector2 moveDir;    //the direction of movement, should be 0,0 or a normalized vector
        private Vector2 faceDir;    //used for sprite rotation
        public double speed = 0.0f; //Pixels per second second

        //Collision detection
        //public bool canCollide;               //Determines whether this object chould be checked for collisions
        public float collisionRadius;           //The radius of the (OUTTER) collision detection from center of object
        private const int collisionOffset = 3; //offset from collision radius
        public float detectionRadius;           //The radius at which an object can detect another collidable object
        private const int detectionOffset = 50; //offset from collision radius


        //Tell the level that it can be removed when it does the check to see
        //which gameObjects have moved outside of the level 'viewport'
        //**** Ships start with this value initially set to false
        //since many ships will spawn outside of the viewport
        //as soon as they enter the viewport though, they are flagged as true again
        public bool canClean = true;
        

        public GameObject(Textures.TextureName name)
        {
            //texture
            textureName = name;
        }


        public virtual void LoadContent()
        {
            //Get the texture from the texture manager
            texture = TEXTURES.GetTexture(this.textureName);

            //Fix the size of the bounds according to the size of the texture (image)
            bounds.Width = texture.Width;
            bounds.Height = texture.Height;
            source.Width = bounds.Width;
            source.Height = bounds.Height;

            //Move the bounds rectangle to the correct position on screen
            MoveTo(this.position);

            //set the center of the image offset for drawing purposed
            centerImg = new Vector2((float)texture.Width/2.0f, (float)texture.Height/2.0f);

            //set the collision and detection radii
            collisionRadius = Math.Max((float)texture.Height / 2.0f, (float)texture.Width / 2.0f) - collisionOffset;
            detectionRadius = collisionRadius + detectionOffset;
        }

        private void UpdateRotation()
        {
            this.rotation = VecUtil.GetAngle(ref faceDir);
        }
        public virtual void SetRotation(Vector2 v)
        {
            v.Normalize();
            VecUtil.SetRotationNormal((double)VecUtil.GetAngle(ref v), ref faceDir);
            VecUtil.SetRotationNormal((double)VecUtil.GetAngle(ref v), ref moveDir);
            UpdateRotation();
        }
        public virtual void SetRotation(float angle)
        {
            VecUtil.SetRotationNormal((double)angle, ref faceDir);
            VecUtil.SetRotationNormal((double)angle, ref moveDir);
            UpdateRotation();
        }
        public virtual void Rotate(Vector2 v)
        {
            v.Normalize();
            VecUtil.Rotate((double)VecUtil.GetAngle(ref v), ref faceDir);
            VecUtil.Rotate((double)VecUtil.GetAngle(ref v), ref moveDir);
            UpdateRotation();
        }
        public virtual void Rotate(float angle)
        {
            VecUtil.Rotate((double)angle, ref faceDir);
            VecUtil.Rotate((double)angle, ref moveDir);
            UpdateRotation();
        }
        public virtual void SetFaceDir(float angle)
        {
            faceDir = VecUtil.GetNormAng(angle);
            UpdateRotation();
        }
        public virtual void SetFaceDir(Vector2 v)
        {
            if (v.X == 0 && v.Y == 0)
                v = VecUtil.GetNormDown();
            else
                v.Normalize();

            faceDir = v;
            UpdateRotation();
        }
        protected Vector2 GetFaceDir()
        {
            return faceDir;
        }
        protected Vector2 GetMoveDir()
        {
            return moveDir;
        }

        //default drawing routine
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, source, tint, rotation, centerImg, 1.0f, effects, depth);
        }

        //drawing routine for special cases (resized destination rectangles)
        public virtual void Draw(SpriteBatch sb, bool boundingBox)
        {
            if (boundingBox)
                sb.Draw(texture, bounds, source, tint, 0.0f, Vector2.Zero, effects, depth);
            else
                sb.Draw(texture, position, source, tint, rotation, centerImg, 1.0f, effects, depth);
        }

        //public virtual void Draw(SpriteBatch sb, float scale)
        //{
        //    sb.Draw(texture, position, source, tint, rotation, centerImg, scale, effects, depth);
        //}



        public virtual void Update(GameTime gameTime)
        {
            //Since movedir is always normalized vector, it cannot be (0,0)
            //Since player ship is not always moving
            //we set moving to false when the input is (0,0) for the move direciton
            if (!moving)
                return;

            //Make sure movedir is normalized
            if (moveDir.X == 0 && moveDir.Y == 0)   //FOR DEBUGGING
                moveDir = new Vector2(0, 1);
            moveDir.Normalize();

            //Calculate the movement offset for this frame
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds * (float)speed;
            Vector2 v = moveDir;
            v.X *= elapsed;
            v.Y *= elapsed;

            //add movement to current position
            position.X += v.X;
            position.Y += v.Y;

            //move the rectangle bounds again to match the new position
            FixBounds();
        }


        private void FixBounds()
        {
            this.bounds.X = (int)Math.Round((position.X - (float)bounds.Width / 2.0f), 0, MidpointRounding.AwayFromZero);
            this.bounds.Y = (int)Math.Round((position.Y - (float)bounds.Height / 2.0f), 0, MidpointRounding.AwayFromZero);
        }


        //move the gameobject by a change vector
        protected void Offset(Vector2 change)
        {
            Vector2 v = new Vector2(this.position.X + change.X, this.position.Y + change.Y);
            MoveTo(v);
        }


        //----------------- PROPERTIES FOR OBJECT SETUP AND MANIPULATION ---------------------------


        //Move the GameObject by its position coordinate(the center of the object)
        //to the given location
        public void MoveTo(Vector2 position)
        {
            this.position = position;

            this.bounds.X = (int)(position.X - (float)bounds.Width / 2.0f);
            this.bounds.Y = (int)(position.Y - (float)bounds.Height / 2.0f);
           
        }

        //Move this gameplay object to the center of the given rectangle
        public void CenterIn(Rectangle rec)
        {
            MoveTo(new Vector2((float)rec.X + (float)rec.Width / 2.0f,
                               (float)rec.Y + (float)rec.Height / 2.0f));
        }

        //Move this gameplay object to the center of the given rectangle
        public void CenterInHoriz(Rectangle rec)
        {
            MoveTo(new Vector2((float)rec.X + (float)rec.Width / 2.0f,
                               this.position.Y));
        }

        //Move this gameplay object to the center of the given rectangle
        public void CenterInVert(Rectangle rec)
        {
            MoveTo(new Vector2(this.position.X,
                               (float)rec.Y + (float)rec.Height / 2.0f));
        }

        //Change the size of the source and destination rectangles
        public void Stretch(Vector2 size)
        {
            //destination
            this.bounds.Width = (int)size.X;
            this.bounds.Height = (int)size.Y;

            //source
            //this.source.Width = size.X;
            //this.source.Height = size.Y;

            //will move the destination so that it is again
            //centered around the position vector
            MoveTo(this.position); 
        }

    }
}
