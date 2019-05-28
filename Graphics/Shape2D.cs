using RobustEngine.Graphics.Interfaces;
using OpenTK;

namespace RobustEngine.Graphics
{
    public class Shape2D : ITransformable2D, IRenderable2D
    { 
        private Matrix4 modelmatrix;       
        private Vector2 origin;
        private Vector2 position;
        private Vector2 scale;
        private Vector2 size;
        private Vector2 rotation;

        private Color fillcolor;
        private Debug debugmode;
        private Vertex[] vertexdata;
        private int[] indicies;        
   
        #region Accessors     

        public Matrix4 ModelMatrix
        { 
            get { return modelmatrix; }
            protected set { modelmatrix = value; }
        }  

        public Vector2 Origin 
        {
            get { return origin; }
            set { SetOrigin(value); origin=value; }
        }

        public Vector2 Position 
        {
            get { return position; }
            set { SetPosition(value); position=value; }
        }
        
        public Vector2 Scale 
        {
            get { return scale; }
            set { SetScale(value); scale=value; }
        }

        public Vector2 Size 
        {
            get { return size; }
            set { SetSize(value); size=value; }
        }     

        public Vector2 Rotation
        {
            get { return rotation; }
            set { SetRotation(value); rotation=value;}
        }
 
        public Color FillColor 
        {
            get { return fillcolor; }
            set { SetFillColor(value);}
        }

        public Debug DebugMode 
        {
            get { return debugmode; }
            set { debugmode = value; }
        }

        public Vertex[] VertexData 
        {
            get { return vertexdata; }
            protected set { vertexdata = value; }
        }

        public int[] Indicies
        {
            get { return indicies; }
            protected set { indicies = value; }
        }

        public float PointSize {get;set;}
        public float LineWidth {get;set;}
        
        #endregion

        public void SetOrigin(Vector2 newOrigin)
        {
            if (newOrigin == Vector2.Zero)
            {
                return;
            }
            modelmatrix *= Matrix4.CreateTranslation(-newOrigin.X, -newOrigin.Y, 0);
        }

        public void SetPosition(Vector2 newPosition)
        {
            if (newPosition == Vector2.Zero)
            {
                return;
            }
            modelmatrix *= Matrix4.CreateTranslation(newPosition.X, newPosition.Y, 0);
        }

        public void SetRotation(Vector2 newRotation)
        {
          
            rotation.X = MathHelper.DegreesToRadians(newRotation.X);
            rotation.Y = MathHelper.DegreesToRadians(newRotation.Y);

            modelmatrix *= Matrix4.CreateRotationX(newRotation.X);
            modelmatrix *= Matrix4.CreateRotationY(newRotation.Y);             
        }

        public void SetScale(Vector2 newScale)
        {
            modelmatrix *= Matrix4.CreateScale(newScale.X, newScale.Y, 1);
        }   

        public virtual void SetSize(Vector2 newSize)
        {
         
        }
   
        public void SetFillColor(Color newColor)
        {
            fillcolor = newColor;
            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].SetColor(newColor);
            }
        }

        public void PushMatrix(Matrix4 mat)
        {
            modelmatrix *= mat;
        }

        public void PopMatrix()
        {
            modelmatrix = Matrix4.Identity;
        }
     
    }
}