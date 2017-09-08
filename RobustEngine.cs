﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics;
using RobustEngine.Graphics.Render;
using RobustEngine.Graphics.Shaders;
using RobustEngine.Graphics.Sprites;
using RobustEngine.System;
using RobustEngine.System.Settings;
using RobustEngine.System.Time;
using RobustEngine.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using RobustEngine.Graphics.Shapes;

namespace RobustEngine
{
    public class RobustEngine
    {
        //Render engine. Handles all rendering.      
        public int version;
        public string GLINFO;




        //Add this to Atmos Engine.
        public delegate void OnAtmosUpdate();


        //Rendering
        public Dictionary<string, NativeWindow> Windows;

        public RenderTarget[] Rendertargets; // This may be unused due to Multiple Windows.
        public GameWindow GameScreen;
        public GameWindow AltScreens; // All other non gamescreen windows
        public static Shader CurrentShader;
        public GraphicsContext Context;
        public DisplayDevice SelectedMonitor;

        public VideoSettings VSettings;



        public int Frame;
        public int FrameTime;

        private bool ReadyToRun;


        //Testing
        public View PlayerView;
        public Clock Timekeeper;
        public SpriteBatch Spritebatch;
        public Texture2D Texture;
        public Triangle2D TriangleTest;
        public Line2D LineTest;

        public Sprite Sprite;

        public RobustEngine()
        {

        }

        public void Init()
        {
            RobustConsole.ClearConsole();
            RobustConsole.SetLogLevel(LogLevel.Debug);
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Init() Intializing...");

            Timekeeper = new Clock();
            VSettings = new VideoSettings(); //TODO import video settings here

            GameScreen = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Debug);

            //GameScreen = new GameWindow();
            //GameScreen.Size = VSettings.Size;
            //GameScreen.WindowBorder = VSettings.Border;
            //GameScreen.Title = "Space Station 14";
            //GameScreen.Visible = true;

            // GameScreen.VSync = VSyncMode.Off;

            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE

            GLINFO += "\n\n------------------------------------------------------------------";
            GLINFO += "\n OpenGL Version: " + GL.GetString(StringName.Version);
            GLINFO += "\n Vendor: " + GL.GetString(StringName.Vendor);
            GLINFO += "\n GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n------------------------------------------------------------------\n";

            RobustConsole.Write(LogLevel.Info, this, GLINFO);
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.ClearColor(0, 0, 0, 0);            //GL.Enable(EnableCap.VertexArray);

            var ImageTestFile = Path.Combine(Environment.CurrentDirectory, "Graphics", "Shaders", "ImageTest");

            CurrentShader = new Shader(ImageTestFile + ".vert", ImageTestFile + ".frag");


            //TESTING
            Texture = new Texture2D("Devtexture_Floor.png");
            LineTest = new Line2D(0, 0, 0, 0, 1);
            //LineTest = new Line(0, 0, 1, 1, 1);

            TriangleTest = new Triangle2D(0, 0, 256, 256);
            Sprite = new Sprite("test", Texture);
            Sprite.SetPosition(new Vector2(.4f, .4f));

            PlayerView = new View(Vector2.One, 0, 10);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.DimGray);
            //  GL.Viewport(0, 0, -1500, -1500);



            //Context = new GraphicsContext(GraphicsMode.Default, GameScreen.WindowInfo,4,4,GraphicsContextFlags.Default);
            //Context.MakeCurrent(GameScreen.WindowInfo);
            //(Context as IGraphicsContextInternal).LoadAll();
            //GL.Enable(EnableCap.Blend);

            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Init() Done.");
            ReadyToRun = true;
        }

        #region State 
        public void Run()
        {

            RobustConsole.Write(LogLevel.Warning, "RobustEngine", "Starting Run loop...");

            Timekeeper.Start();
            GameScreen.Run(60);

        }

        public void Update(object Sender, FrameEventArgs E)
        {
            GameScreen.ProcessEvents();
            // Sprite.Update();
            mov += .01f;

        }

        float mov = .002f;

        float scale = 1 / 256f;

        int frames;
        Vector2 scal = new Vector2(1 / 256f, 1 / 256f);

        public void Render(object Sender, FrameEventArgs E)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //   PlayerView.Setup(800, 800);
            //   PlayerView.Update();
            //   Spritebatch.Begin();


            CurrentShader.Enable();
            // Sprite.Rect.DebugMode = Debug.None;
            // Sprite.Rect.SetFillColor(Color.Blue);

            LineTest.SetFillColor(Color.Red);
            LineTest.SetOrigin(LineTest.Center);
            //    LineTest.SetPosition(new Vector2(.10f * x, .10f * y));
            // LineTest.SetRotation(mov, Axis.Z);
            LineTest.Update();
            LineTest.Draw();

            //LineTest.SetFillColor(Color.Blue);
            //LineTest.SetOrigin(LineTest.Center);
            ////    LineTest.SetPosition(new Vector2(.10f * x, .10f * y));
            //LineTest.SetRotation(90f, Axis.Z);
            //LineTest.Update();
            //LineTest.Draw();

            //TriangleTest.SetFillColor(Color.Blue);
            //TriangleTest.SetOrigin(TriangleTest.BottomLeft);
            //TriangleTest.SetScale(scal);
            //TriangleTest.SetRotation(-mov, Axis.Z);
            //TriangleTest.SetPosition(new Vector2(scale, scale));
            //TriangleTest.Update();
            //TriangleTest.Draw();


            //Sprite.SetOrigin(Sprite.Rect.Center);
            //Sprite.SetScale(new Vector2(scale, scale)); //+ (.001f * x), scale + (.001f * y)));
            //Sprite.SetRotation(mov, Axis.Z);
            //Sprite.SetPosition(new Vector2(scale, scale));
            //Sprite.Update();
            //Sprite.Draw();

            ////    Sprite.SetOrigin(new Vector2(-.5f, -.5f));
            //Sprite.SetScale(new Vector2(scale, scale));
            //Sprite.SetRotation(mov);
            //Sprite.SetPosition(new Vector2(scale * x, -scale * y));
            //Sprite.Update();
            //Sprite.Draw();

            ////   Sprite.SetOrigin(new Vector2(-.5f, -.5f));
            //Sprite.SetScale(new Vector2(scale, scale));
            //Sprite.SetRotation(mov);
            //Sprite.SetPosition(new Vector2(-scale * x, scale * y));
            //Sprite.Update();
            //Sprite.Draw();

            ////  Sprite.Rect.DebugMode = Debug.Wireframe;
            ////    Sprite.SetOrigin(new Vector2(-.5f, -.5f));
            //Sprite.SetScale(new Vector2(scale, scale)); ;
            //Sprite.SetRotation(mov);
            //Sprite.SetPosition(new Vector2(scale * x, scale * y));
            //Sprite.Update();
            //Sprite.Draw();


            //  Texture.Unbind();
            CurrentShader.Disable();


            GameScreen.SwapBuffers();



            frames++;
            if (Timekeeper.GetElapsed().Seconds >= 1)
            {
                RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() FPS " + frames);
                Timekeeper.Start();
                frames = 0;
            }
            // RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() MS " + Timekeeper.GetTime().Milliseconds.ToString());
        }

        public void Stop()
        {
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Stopping...");
        }

        #endregion State

        public void setCurrentRenderTarget(RenderTarget RT)
        {

        }

        public void setScreenTarget(NativeWindow NW)
        {

        }

        #region Global Error Checking
        public static void CheckGLErrors()
        {
            var FBOEC = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            while ((FBOEC == FramebufferErrorCode.FramebufferComplete))
            {
                RobustConsole.Write(LogLevel.Verbose, "RobustEngine", FBOEC.ToString()); //TODO expand error message
                break;
            }

            var EC = GL.GetError();
            while (EC != ErrorCode.NoError)
            {
                RobustConsole.Write(LogLevel.Fatal, "RobustEngine", EC.ToString()); // TODO expand error message
            }
        }
        # endregion Global Error Checking

    }


    public enum Debug
    {
        None,
        Points,
        Wireframe,

    }


}