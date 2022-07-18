using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OmegaEngine.OmegaEngineComponents;
using System.Windows.Forms;

namespace OmegaEngine
{
    class DemoGame : OmegaEngineComponents.OmegaEngine
    {
        Shape2D player;
        float dirX = 0;
        float dirY = 0;

        Vector2 lastPos = Vector2.Zero();

        List<string[,]> Rooms = new List<string[,]>();

        string[,] Level_1 = 
        {
            {"=","=","=","=","=","=","=","=","="},
            {"=",".","*",".","=","=","=","=","="},
            {"=","*","=",".","*",".","=","=","="},
            {"=",".","=","=","=",".",".",".","="},
            {"=","*","=","0","=","=","=",".","="},
            {"=",".","=",".",".",".",".",".","="},
            {"=","=","=","=","=","=","=","=","="},            
        };
        


        // Opening The Window
        public DemoGame() : base(new Vector2(615, 515), "OmegaEngine Demo")
        {
            
        }

        // Called When The System Is Starting Up The Game
        public override void Start()
        {
            Rooms.Add(Level_1);
            Rooms.Add(Level_2);
            BgColor = Color.Beige;
            CamZoom = new Vector2(1.36f, 1.36f);

            player = new Shape2D(new Vector2(70, 275), new Vector2(10, 10), "Player", Color.Red);

            for (int i = 0; i < Rooms[0].GetLength(1); i++)
            {
                for (int j = 0; j < Rooms[0].GetLength(0); j++)
                {
                    if(Rooms[0][j,i] == "=")
                    {
                        new Shape2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), "Ground", Color.Gray);
                    }
                    if (Rooms[0][j, i] == "*")
                    {
                        new Shape2D(new Vector2(i * 50 + 10, j * 50 + 10), new Vector2(20, 20), "Coin", Color.Yellow);
                    }
                    if (Rooms[0][j, i] == "0")
                    {
                        new Shape2D(new Vector2(i * 50 + 25, j * 50 + 25), new Vector2(40, 40), "End", Color.BlueViolet);
                    }
                }
            }
        }

        // Called Every Before The Window Refreshes
        public override void Draw()
        {
            
        }

        // Called Every Frame After The Window Refreshes
        public override void Update()
        {
            player.Position.X += dirX;
            player.Position.Y += dirY;


            Shape2D coin = player.IsCollidingWithTag("Coin");
            if (coin != null)
            {
                coin.Destroy();
            }
            
            if (player.IsCollidingWithTag("Ground") != null)
            {
                player.Position.X = lastPos.X;
                player.Position.Y = lastPos.Y;
            }
            else
            {
                lastPos.X = player.Position.X;
                lastPos.Y = player.Position.Y;
            }
        }

        // Called Once You Exit The Game
        public override void Exit()
        {
            
        }

        // Called Once A Key Is Pressed
        public override void GetKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W || e.KeyCode == Keys.Space) { dirY = -1f; }
            if (e.KeyCode == Keys.A) { dirX = -1f; }
            if (e.KeyCode == Keys.S) { dirY = 1f; }
            if (e.KeyCode == Keys.D) { dirX = 1f; }
        }

        // Called Once A Key Is Released
        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Space) { dirY = 0; }
            if (e.KeyCode == Keys.A) { dirX = 0; }
            if (e.KeyCode == Keys.S) { dirY = 0; }
            if (e.KeyCode == Keys.D) { dirX = 0; }
        }

    }
}
