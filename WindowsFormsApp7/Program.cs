using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;


namespace WindowsFormsApp7
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }



    public class CShape
    {
        protected float x;
        protected float y;
        protected string color;
        protected float size;
        protected GraphicsPath place = new GraphicsPath();
        public Boolean selected = false;

        public virtual float getX() { return x; }
        public virtual void changeX(int a) { x += a; }

        public virtual float getY() { return y; }
        public virtual void changeY(int a) { y += a; }

        public virtual float getSize() { return size; }
        public virtual void changeSize(float a) { size += a; }

        public virtual void addShape(int a, CShape obj) { }

        public string getColor() { return color; }
        public void changeColor(string a) { color = a; }

        public bool getSelected() { return selected; }
        public void reverseSelected()
        {
            if (selected) { selected = false; }
            else { selected = true; }
        }
        public void changeSelected(bool a) { selected = a; }
        public GraphicsPath getPath() { return place; }
        public virtual CShape getObj(int a) { return null; }


        public CShape(float _x, float _y)
        {
            x = _x;
            y = _y;
            color = "Red";
            selected = false;
            size = 100;
        }

        public CShape(float _x, float _y, float _size, string _color)
        {
            x = _x;
            y = _y;
            color = _color;
            selected = false;
            size = _size;
        }

        public CShape()
        {

        }

        public virtual void save(StreamWriter writer) { }
        public virtual CShape load(StreamReader reader, CShapeFactory factory) { return null; }
    };

    class Circle : CShape
    {

        public Circle(float _x, float _y) : base(_x, _y)
        {
            x = _x;
            y = _y;
            size = 100;
            selected = false;
            color = "Red";
            place.AddEllipse(x - size / 2, y - size / 2, size, size);
        }

        public Circle(float _x, float _y, float _size, string _color) : base(_x, _y)
        {
            x = _x;
            y = _y;
            size = _size;
            selected = false;
            color = _color;
            place.AddEllipse(x - size / 2, y - size / 2, size, size);
        }

        public override float getX() { return x; }
        public override void changeX(int a)
        {
            x += a;
            place.Reset();
            place.AddEllipse(x - size / 2, y - size / 2, size, size);
        }

        public override float getY() { return y; }
        public override void changeY(int a)
        {
            y += a;
            place.Reset();
            place.AddEllipse(x - size / 2, y - size / 2, size, size);
        }
        public override float getSize() { return size; }
        public override void changeSize(float a)
        {
            size += a;
            place.Reset();
            place.AddEllipse(x - size / 2, y - size / 2, size, size);
        }
        public override void save(StreamWriter Writer)
        {
            Writer.WriteLine("Circle");
            Writer.WriteLine(x);
            Writer.WriteLine(y);
            Writer.WriteLine(size);
            Writer.WriteLine(color);
            Writer.WriteLine();
        }

        public override CShape load(StreamReader reader, CShapeFactory factory)
        {
            int x = int.Parse((reader.ReadLine()));
            int y = int.Parse((reader.ReadLine()));
            int size = int.Parse((reader.ReadLine()));
            string color = (reader.ReadLine());
            return new Circle(x, y, size, color);
        }
    };

    class Triangle : CShape
    {
        public Triangle(float _x, float _y) : base(_x, _y)
        {

            size = 100;
            x = _x;
            y = _y;
            selected = false;
            Point[] myArray = { new Point((int)x, (int)(y - size / 2)), new Point((int)(x - size / 2), (int)(y + size / 2)), new Point((int)(x + size / 2), (int)(y + size / 2)) };
            place.AddPolygon(myArray);
            color = "Red";
        }

        public Triangle(float _x, float _y, float _size, string _color) : base(_x, _y)
        {

            size = _size;
            x = _x;
            y = _y;
            selected = false;
            Point[] myArray = { new Point((int)x, (int)(y - size / 2)), new Point((int)(x - size / 2), (int)(y + size / 2)), new Point((int)(x + size / 2), (int)(y + size / 2)) };
            place.AddPolygon(myArray);
            color = _color;
        }

        public override float getX() { return x; }
        public override void changeX(int a)
        {
            x += a;
            place.Reset();
            Point[] myArray = { new Point((int)x, (int)(y - size / 2)), new Point((int)(x - size / 2), (int)(y + size / 2)), new Point((int)(x + size / 2), (int)(y + size / 2)) };
            place.AddPolygon(myArray);

        }

        public override float getY() { return y; }
        public override void changeY(int a)
        {
            y += a;
            place.Reset();
            Point[] myArray = { new Point((int)x, (int)(y - size / 2)), new Point((int)(x - size / 2), (int)(y + size / 2)), new Point((int)(x + size / 2), (int)(y + size / 2)) };
            place.AddPolygon(myArray);
        }
        public override float getSize() { return size; }
        public override void changeSize(float a)
        {
            size += a;
            place.Reset();
            Point[] myArray = { new Point((int)x, (int)(y - size / 2)), new Point((int)(x - size / 2), (int)(y + size / 2)), new Point((int)(x + size / 2), (int)(y + size / 2)) };
            place.AddPolygon(myArray);
        }

        public override void save(StreamWriter Writer)
        {
            Writer.WriteLine("Triangle");
            Writer.WriteLine(x);
            Writer.WriteLine(y);
            Writer.WriteLine(size);
            Writer.WriteLine(color);
            Writer.WriteLine();
        }

        public override CShape load(StreamReader reader, CShapeFactory factory)
        {
            int x = int.Parse((reader.ReadLine()));
            int y = int.Parse((reader.ReadLine()));
            int size = int.Parse((reader.ReadLine()));
            string color = (reader.ReadLine());
            return new Triangle(x, y, size, color);
        }
    };

    class Square : CShape
    {
        public Square(float _x, float _y)
           : base(_x, _y)
        {
            size = 100;
            x = _x;
            y = _y;
            color = "Red";
            selected = false;
            Rectangle sq = new Rectangle((int)(x - size / 2), (int)(y - size / 2), (int)size, (int)size);
            place.AddRectangle(sq);
        }
        public Square(float _x, float _y, float _size, string _color)
           : base(_x, _y)
        {
            size = _size;
            x = _x;
            y = _y;
            color = _color;
            selected = false;
            Rectangle sq = new Rectangle((int)(x - size / 2), (int)(y - size / 2), (int)size, (int)size);
            place.AddRectangle(sq);
        }
        public override float getX() { return x; }
        public override void changeX(int a)
        {
            x += a;
            place.Reset();
            Rectangle sq = new Rectangle((int)(x - size / 2), (int)(y - size / 2), (int)size, (int)size);
            place.AddRectangle(sq);
        }

        public override float getY() { return y; }
        public override void changeY(int a)
        {
            y += a;
            place.Reset();
            Rectangle sq = new Rectangle((int)(x - size / 2), (int)(y - size / 2), (int)size, (int)size);
            place.AddRectangle(sq);
        }

        public override float getSize() { return size; }
        public override void changeSize(float a)
        {
            size += a;
            place.Reset();
            Rectangle sq = new Rectangle((int)(x - size / 2), (int)(y - size / 2), (int)size, (int)size);
            place.AddRectangle(sq);
        }

        public override void save(StreamWriter Writer)
        {
            Writer.WriteLine("Square");
            Writer.WriteLine(x);
            Writer.WriteLine(y);
            Writer.WriteLine(size);
            Writer.WriteLine(color);
            Writer.WriteLine();
        }

        public override CShape load(StreamReader reader, CShapeFactory factory)
        {
            int x = int.Parse((reader.ReadLine()));
            int y = int.Parse((reader.ReadLine()));
            int size = int.Parse((reader.ReadLine()));
            string color = (reader.ReadLine());
            return new Square(x, y, size, color);
        }
    };

    class Storage
    {
        int size;
        CShape[] arr;

        public Storage()
        {

            size = 0;
            arr = new CShape[size];

        }

        public void addShape(int a, CShape obj) // Add object method
        {
            size++;
            CShape[] newarr = arr;
            arr = new CShape[size];
            for (int i = 0; i < a; i++)
            {
                arr[i] = newarr[i];
            }
            arr[a] = obj;
            for (int i = a + 1; i < size; i++)
            {
                arr[i] = newarr[i - 1];
            }

        }

        public void delShape(int a) // Delete object method
        {
            size--;
            CShape[] newarr = arr;
            arr = new CShape[size];

            for (int i = 0; i < a; i++)
            {
                arr[i] = newarr[i];
            }
            for (int i = a; i < size; i++)
            {
                arr[i] = newarr[i + 1];
            }

        }

        public int getSize()
        {
            return size;
        }

        public CShape getObj(int a) // Get object method
        {
            return arr[a];
        }

    };
}