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

        public void Destroy()
        {
            Debug.InfoLog($"[Shape2D]({Tag}) - Has Been Destroyed");
            OmegaEngine.DeleteShape(this);
        }
    }
}
