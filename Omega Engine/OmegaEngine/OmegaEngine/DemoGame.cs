using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OmegaEngine.OmegaEngineComponents;

namespace OmegaEngine
{
    class DemoGame : OmegaEngineComponents.OmegaEngine
    {
        Shape2D player;
        float dirX = 0;
        float dirY = 0;
        public DemoGame() : base(new Vector2(615,515), "OmegaEngine Demo")
        {

        }

        // Called When The System Is Starting Up The Game
        public override void Start()
        {
            BgColor = Color.Beige;

            player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "Player", Color.Red);
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
        }
    }
}
