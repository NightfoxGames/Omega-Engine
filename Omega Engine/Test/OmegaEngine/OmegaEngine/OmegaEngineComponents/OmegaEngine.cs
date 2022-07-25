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

        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Shape2D> AllShapes = new List<Shape2D>();

        public Vector2 CamZoom = new Vector2(1,1);
        public Vector2 CamPosition = Vector2.Zero();
        public float CamAngle = 0;

        public Color BgColor = Color.White;
        public OmegaEngine(Vector2 ScreenSize, string Title)
        {
            Debug.ServerLog("Game Is Starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y);
            Window.Text = this.Title;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.Paint += Renderer;
            Window.FormClosed += Window_FormClosing;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosedEventArgs e)
        {
            Exit();
            GameLoopThread.Abort();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
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
            g.TranslateTransform(CamPosition.X, CamPosition.Y);
            g.RotateTransform(CamAngle);
            g.ScaleTransform(CamZoom.X, CamZoom.Y);
            try
            {
                foreach (Shape2D shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(shape.color), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
                }
                foreach (Sprite2D sprite in AllSprites)
                {
                    if (!sprite.isReference)
                    {
                        g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                    }                    
                }
            }
            catch
            {
                Debug.WarningLog("Not Loading...");
            }
        }

        public abstract void Start();
        public abstract void Draw();
        public abstract void Update();
        public abstract void Exit();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }

}
