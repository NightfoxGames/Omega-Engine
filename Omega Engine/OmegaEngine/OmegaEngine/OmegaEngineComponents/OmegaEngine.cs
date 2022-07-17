using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace OmegaEngine.OmegaEngineComponents
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class OmegaEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "Simple 2D Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        private static List<Sprite2D> AllSprites = new List<Sprite2D>();
        private static List<Shape2D> AllShapes = new List<Shape2D>();

        public Color BgColor = Color.White;
        public OmegaEngine(Vector2 ScreenSize, string Title)
        {
            Debug.ServerLog("Game Is Starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }


        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void DeleteShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }
        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void DeleteSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        void GameLoop()
        {
            Start();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                Draw();
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                Update();
                Thread.Sleep(1);
                }
                catch
                {
                    Debug.ErrorLog("Not Responding...");
                  
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BgColor);

            foreach(Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(shape.color), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }
            foreach (Sprite2D sprite in AllSprites)
            {
                g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }
        }

        public abstract void Start();
        public abstract void Draw();
        public abstract void Update();
    
    }

}
