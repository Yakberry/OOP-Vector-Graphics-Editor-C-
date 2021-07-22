using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp7
{
    class Group : CShape
    {
        private CShape[] shapes_in_group;
        public Group()
        {
            size = 0;
            shapes_in_group = new CShape[(int)size];
            selected = false;
            color = "Red";
        }
        public override void addShape(int a, CShape obj)
        {
            size = (int)size + 1;
            CShape[] copy = shapes_in_group;
            shapes_in_group = new CShape[(int)size];
            for (int i = 0; i < a; i++)
            {
                shapes_in_group[i] = copy[i];
            }
            shapes_in_group[a] = obj;
            place.AddPath(shapes_in_group[a].getPath(), false);
            for (int i = a + 1; i < (int)size; i++)
            {
                shapes_in_group[i] = copy[i - 1];
            }
        }
        public override void changeX(int a)
        {
            place.Reset();

            for (int i = 0; i < (int)size; i++)
            {
                shapes_in_group[i].changeX(a);
                place.AddPath(shapes_in_group[i].getPath(), false);

            }

        }
        public override void changeY(int a)
        {
            place.Reset();

            for (int i = 0; i < (int)size; i++)
            {
                shapes_in_group[i].changeY(a);
                place.AddPath(shapes_in_group[i].getPath(), false);

            }

        }
      
        public override float getSize()
        {
            return size;
        }



        public override void changeSize(float a)
        {
            place.Reset();

            if (a > 0)
            {
                
                {
                    for (int i = 0; i < size; i++)
                    {
                        shapes_in_group[i].changeSize(10);
                       
                    }
                }
            }


            if (a < 0)
            {
                
                {
                    for (int i = 0; i < size; i++)
                    {
                        shapes_in_group[i].changeSize(-10);
                        if  (shapes_in_group[i].getSize() <= 0)
                        {
                            shapes_in_group[i].changeSize(10);
                        }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                place.AddPath(shapes_in_group[i].getPath(), false);

            }

        }

        new public void changeColor(string a)
        {
            place.Reset();

            for (int i = 0; i < size; i++)
            {
                shapes_in_group[i].changeColor(a);
                place.AddPath(shapes_in_group[i].getPath(), false);

            }
            color = a;
        }

        public override CShape getObj(int a) { return shapes_in_group[a]; }
        new public GraphicsPath getPath() { return place; }

        public override void save(StreamWriter Writer)
        {
            Writer.WriteLine("Group");
            Writer.WriteLine(size);
            Writer.WriteLine(color);
            Writer.WriteLine();
            for (int i = 0; i < size; i++)
            {
                shapes_in_group[i].save(Writer);
            }
            Writer.WriteLine("GroupEnd");
            Writer.WriteLine();
        }

        public override CShape load(StreamReader reader, CShapeFactory factory)
        {
           int _size = int.Parse(reader.ReadLine());
            string _color = reader.ReadLine();
            CShape new_group = new Group();
            new_group.changeColor(_color);
            reader.ReadLine();

            for (int i = 0; i < _size; i++)
            {
                string obj_type = reader.ReadLine();
                CShape someShape = factory.createShape(obj_type);
                new_group.addShape((int)new_group.getSize(), someShape.load(reader, factory));
                reader.ReadLine();
            }
            reader.ReadLine();
            return new_group;
        }
    }

   
}
