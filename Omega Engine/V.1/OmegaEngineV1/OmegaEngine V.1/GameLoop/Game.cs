using GLFW;
using OmegaEngine_V._1.Rendering.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaEngine_V._1.GameLoop
{
    abstract class Game
    {

        protected int InitWidth { get; set; }
        protected int InitHeight { get; set; }
        protected string InitTitle { get; set; }
        public Game(int initWidth, int initHeight, string initTitle)
        {
            InitWidth = initWidth;
            InitHeight = initHeight;
            InitTitle = initTitle;
        }
        public void Run()
        {
            Init();

            DisplayManager.CreateWindow(InitWidth,InitHeight, InitTitle);

            LoadContent();

            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedTime;
                GameTime.TotalElapsedTime = (float)Glfw.Time;

                Update();

                Glfw.PollEvents();

                Render();
            }
            DisplayManager.CloseWindow();
        }

        protected abstract void Init();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();
    }
}
