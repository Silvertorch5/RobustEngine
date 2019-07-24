﻿using System;
using OpenTK;
using RobustEngine.Graphics.OpenGL;

namespace RobustEngine.Graphics.Render
{
    public class Framebuffer : GLFrameBuffer, IRenderable2D
    {
        private int Width;
        private int Height;

        public Vertex[] VertexData{get;set;}
        public int[] Indicies{get;set;}
        public Matrix4 ModelMatrix {get;set;}
        public Debug DebugMode {get;set;}
        public float PointSize{get;set;}
        public float LineWidth{get;set;}
        
        public Framebuffer(TextureTarget TT = TextureTarget.Texture2D) : base(TT)
        {
            
        }


        public void Init(int width,int height)
        {
            VertexData = new Vertex[]
            {
                Vertex.One *-1,
                Vertex.UnitXNegUnitY,
                Vertex.One,
                Vertex.UnitXNegUnitY*-1
            };

            Indicies = new int[]
            {
                0,1,2,
                2,3,0
            };

            ModelMatrix = Matrix4.Identity;
            PointSize=1f;
            LineWidth=1f;
            GLCreate(width,height);            
        }

    }
}