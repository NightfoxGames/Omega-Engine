using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace OmegaEngine.OmegaEngineComponents
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";
        public Color color = Color.Gray;

        public Shape2D(Vector2 Position, Vector2 Scale, string Tag, Color color)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.color = color;

            Debug.InfoLog($"[Shape2D]({Tag}) - Has Been Regestered");
            OmegaEngine.RegisterShape(this);
        }
        public bool IsCollidingWithObject(Shape2D a, Shape2D b)
        {
            if (a.Position.X < b.Position.X + b.Scale.X && a.Position.X + a.Scale.X > b.Position.X && a.Position.Y < b.Position.Y + b.Scale.Y && a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }
            return false;
        }

        public Shape2D IsCollidingWithTag(string tag)
        {
            foreach (Shape2D b in OmegaEngine.AllShapes)
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
            OmegaEngine.DeleteShape(this);
        }
    }
}
