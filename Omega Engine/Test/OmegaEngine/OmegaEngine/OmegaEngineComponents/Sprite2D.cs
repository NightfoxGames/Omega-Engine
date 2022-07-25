using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OmegaEngine.OmegaEngineComponents
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;
        public bool isReference;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;



            Debug.InfoLog($"[Sprite2D]({Tag}) - Has Been Regestered");
            OmegaEngine.RegisterSprite(this);
        }
        public Sprite2D( string Directory)
        {
            
            this.Directory = Directory;
           

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;



            Debug.InfoLog($"[Sprite2D]({Tag}) - Has Been Regestered");
            OmegaEngine.RegisterSprite(this);
        }
        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D refr, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            
            this.Tag = Tag;

            Sprite = refr.Sprite;



            Debug.InfoLog($"[Sprite2D]({Tag}) - Has Been Regestered");
            OmegaEngine.RegisterSprite(this);
        }

        public bool IsCollidingWithObject(Sprite2D a, Sprite2D b)
        {
            if(a.Position.X < b.Position.X + b.Scale.X && a.Position.X + a.Scale.X > b.Position.X && a.Position.Y < b.Position.Y + b.Scale.Y && a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }
            return false;
        }
        public Sprite2D IsCollidingWithTag(string tag)
        {
            foreach(Sprite2D b in OmegaEngine.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Position.X < b.Position.X + b.Scale.X && Position.X + Scale.X > b.Position.X && Position.Y < b.Position.Y + b.Scale.Y && Position.Y + Scale.Y > b.Position.Y)
                    {
                        return b;
                    }
                }
            }
            return null;
        }
        public void Destroy()
        {
            Debug.InfoLog($"[Shape2D]({Tag}) - Has Been Destroyed");
            OmegaEngine.DeleteSprite(this);
        }
    }
}
