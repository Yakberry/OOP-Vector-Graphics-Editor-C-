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
    public partial class Form1 : Form
    {     
        Storage shapes = new Storage();    
        SolidBrush sbRed = new SolidBrush(Color.Red);       
        SolidBrush sbGreen = new SolidBrush(Color.Green);    
        SolidBrush sbBlue = new SolidBrush(Color.Blue);
        Pen SelectedPen = new Pen(Color.Black, 2);
        SolidBrush sbYellow = new SolidBrush(Color.Yellow);
        SolidBrush sbBlack = new SolidBrush(Color.Black);
        int fig_type = 0;
        CShapeFactory factory = new ConcreteShapeFactory();
        List<Image> im = new List<Image>();


        public Form1()
        {
            InitializeComponent();
        }

        private void cancel()      // Отмена выделения, выключение панели манипулирования, обновление панели рисования
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).reverseSelected(); }
            }
            panel2.Enabled = false;
            panel1.Refresh();
        }

        private void draw(CShape obj)   // Отрисовка фигур сообразно их указанному цвету и обводка выделенных фигур
        {
            Graphics g = panel1.CreateGraphics();
            if (obj.getColor() == "Red") { g.FillPath(sbRed, obj.getPath()); }
            if (obj.getColor() == "Blue") { g.FillPath(sbBlue, obj.getPath()); }
            if (obj.getColor() == "Green") { g.FillPath(sbGreen, obj.getPath()); }
            if (obj.getColor() == "Yellow") { g.FillPath(sbYellow, obj.getPath()); }
            if (obj.getColor() == "Black") { g.FillPath(sbBlack, obj.getPath()); }         
            if (obj.getSelected())
            {
                Graphics f = panel1.CreateGraphics();
                f.DrawPath(SelectedPen, obj.getPath());
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)      // Событие заставляет все фигуры из хранилища нарисоваться
        {
            Graphics g = panel1.CreateGraphics();
            if (im.Count != 0) g.DrawImage(im.First(), 0, 0);
            for (int i = 0; i < shapes.getSize(); i++)
            {
                draw(shapes.getObj(i));
            }
            label1.Text = shapes.getSize().ToString();
        }

        private bool IsNotCrossingBorders(CShape obj)                   // Проверка пересечения границ панели рисования (false, ecли пересекает)
        {
            if ((obj.getPath().GetBounds().Top >= 0) && (obj.getPath().GetBounds().Left >= 0) && (obj.getPath().GetBounds().Right <= (panel1.Width)-3) && (obj.getPath().GetBounds().Bottom <= (panel1.Height)-3))
            {
                return true;
            }
            else return false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)     // Событие щелчка мыши 
        {
            bool flag = true;
            bool flag2 = true;
            Graphics g = panel1.CreateGraphics();
            if (im.Count != 0) g.DrawImage(im.First(), 0, 0);
            for (int i = 0; i < shapes.getSize(); i++)      // Если ткнули не на пустое место, происходит выделение
            {
                if (shapes.getObj(i).getPath().IsVisible(e.X, e.Y))
                {
                    panel2.Enabled = true;
                    shapes.getObj(i).reverseSelected();
                    flag = false;                    
                }
            }
            
            for (int i = 0; i < shapes.getSize(); i++)  // Если не осталось выделенных объектов, закрываем панель манипулирования
            {
                if (shapes.getObj(i).getSelected()) { flag2 = false; }
            }

            if (flag2 == true) { cancel(); } 


            if (flag == true)     // Если ткнули на пустое место, создаём новую фигуру в зависимости от положения кнопок выбора фигур
            {
                panel2.Enabled = false;
                for (int i = 0; i < shapes.getSize(); i++)
                {
                    shapes.getObj(i).changeSelected(false);
                }
                if (fig_type == 0)
                {
                    CShape circle = new Circle(e.X, e.Y);
                    if (IsNotCrossingBorders(circle)) { shapes.addShape(shapes.getSize(), circle); }; 
                }
                if (fig_type == 1)
                {
                    CShape square = new Square(e.X, e.Y);
                    if (IsNotCrossingBorders(square)) { shapes.addShape(shapes.getSize(), square); }
                }
                if (fig_type == 2)
                {
                    CShape triangle = new Triangle(e.X, e.Y);
                    if (IsNotCrossingBorders(triangle)) { shapes.addShape(shapes.getSize(), triangle); }
                }
            }           
            panel1.Refresh();
        }

        // Установка цвета всех выделенных фигур
        private void rbRed_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).changeColor("Red"); }
            }
            panel1.Refresh();
        }

        private void rbBlue_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).changeColor("Blue"); }
            }
            panel1.Refresh();
        }

        private void rbGreen_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).changeColor("Green"); }
            }
            panel1.Refresh();
        }
        private void rbYellow_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).changeColor("Yellow"); }
            }
            panel1.Refresh();
        }

        private void rbBlack_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected()) { shapes.getObj(i).changeColor("Black"); }
            }
            panel1.Refresh();
        }

        // Установка выбора фигуры пользователем
        private void rbCircle_CheckedChanged(object sender, EventArgs e)
        {
            fig_type = 0;
            cancel();
        }

        private void rbSquare_CheckedChanged(object sender, EventArgs e)
        {
            fig_type = 1;
            cancel();
        }
        private void rbTriangle_CheckedChanged(object sender, EventArgs e)
        {
            fig_type = 2;
            cancel();
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e) // Щелчок по форме вызывает метод cancel()
        {
            cancel();
        }


        // Управление с клавиатуры
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 46) // Del key
            {
                for (int i = 0; i < shapes.getSize(); i++)
                {
                    if (shapes.getObj(i).getSelected() == true)
                    {
                        shapes.delShape(i);
                        i--;
                    }
                }
                cancel();
            }

            if (e.KeyValue == 40) // Down key
            {
                for (int i = 0; i < shapes.getSize(); i++)
                {
                    if (shapes.getObj(i).getSelected() == true)
                    {
                        shapes.getObj(i).changeY(10);
                        if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                        {
                            shapes.getObj(i).changeY(-10);
                        }
                        panel1.Refresh();
                    }
                }
            }

            if (e.KeyValue == 104)  // Numpad 8
            {
                btnUp_Click(sender, e);
            }

            if (e.KeyValue == 98)  // Numpad 2
            {
                btnDown_Click(sender, e);
            }

            if (e.KeyValue == 100) // Numpad 4
            {
                btnLeft_Click(sender, e);
               

            }

            if (e.KeyValue == 102) // Numpad 6
            {
                btnRight_Click(sender, e);
            }

            if (e.KeyValue == 107 || e.KeyValue == 187) // Numpad + or keyboard +
            {
                btnExpand_Click(sender, e);
            }

            if (e.KeyValue == 109 || e.KeyValue == 189) // Numpad - or keyboard -
            {
                btnDiminish_Click(sender, e);
            }

            if (e.KeyValue == 71)           // G (Group)
            {
                for (int i = 0; i < shapes.getSize(); i++)
                {
                    if (shapes.getObj(i).getSelected() == true)
                    {
                        Group exact_group = new Group();                       
                        shapes.addShape(shapes.getSize(), exact_group);
                        for (int j = 0; j < shapes.getSize(); j++)
                        {                            
                            if (shapes.getObj(j).getSelected() == true)
                            {
                                exact_group.addShape((int)exact_group.getSize(), shapes.getObj(j));
                                shapes.delShape(j);                                
                                j--;
                            }
                        }
                        break;
                    }
                }
                cancel();
                panel1.Refresh();
            }

            if (e.KeyValue == 85)           // U  (Ungroup)
            {
                if (SelectedAreGroups() == true)
                {
                    
                    float _size = shapes.getSize();   // Переменная sz поможет определить "рамки" просмотра shapes на предмет групп, подлежащих удалению


                    for (int i = 0; i < _size; i++)
                    {
                        if ((shapes.getObj(i).getSelected() == true) && (shapes.getObj(i) is Group) )
                        {
                            for (int j = 0; j < shapes.getObj(i).getSize(); j++)
                            {
                                shapes.addShape(shapes.getSize(), shapes.getObj(i).getObj(j));
                            }
                            _size = _size - 1;       // Сдвинули границу просмотра, чтобы не удалить подгруппы, вылезшие в shapes из групп
                            shapes.delShape(i);
                            i--;               // Сдвинулись на один влево, чтобы не пропустить элемент, сдвинутый удалением
                        }
                    }
                    cancel();
                    panel1.Refresh();
                }
            }

            if (e.KeyValue == 83)            // S (Save)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                string saving_path = saveFileDialog1.FileName;
                
                StreamWriter Writer = new StreamWriter(saving_path);
                Writer.WriteLine(shapes.getSize());
                Writer.WriteLine("Число сверху - размер хранилища shapes.");
                Writer.WriteLine("Далее записано содержимое хранилища: объекты и группы объектов.");
                Writer.WriteLine("Порядок хранения данных для объектов-фигур:");
                Writer.WriteLine("Координаты (х и у), размер, цвет, selected (true/false).");
                Writer.WriteLine("Объявления и завершения описания групп выделены словами Group и GroupEnd соответственно, для каждой группы указан её размер и перечислены все входящие в неё объекты.");
                Writer.WriteLine();
                for (int i = 0; i < shapes.getSize(); i++)
                {
                    shapes.getObj(i).save(Writer);
                }
                Writer.Close();
            }

            if (e.KeyValue == 76)               // L (Load)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                string path_to_open = openFileDialog1.FileName;
                StreamReader Reader = new StreamReader(path_to_open);
                int load_size = int.Parse((Reader.ReadLine()));
                Reader.ReadLine();
                Reader.ReadLine();
                Reader.ReadLine();
                Reader.ReadLine();
                Reader.ReadLine();
                Reader.ReadLine();

                for (int i = 0; i < load_size; i++)
                {
                    string obj_type = Reader.ReadLine();
                    CShape someShape = factory.createShape(obj_type);
                    shapes.addShape(shapes.getSize(), someShape.load(Reader, factory));
                    Reader.ReadLine();
                }

                Reader.Close();
                panel1.Refresh();
            }
        }

        private bool SelectedAreGroups()
        {            
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    if (shapes.getObj(i) is Group == false)
                        return false;
                    
                }
            }
            return true;

        }


        // Обработка нажатия кнопок 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.delShape(i);
                    i--;
                }
            }
            cancel();
        }

        private void btnUp_Click(object sender, EventArgs e)    // Перемещение вверх
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeY(-10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeY(10);
                    }
                    panel1.Refresh();
                }
            }
        }


        private void btnLeft_Click(object sender, EventArgs e)  // Перемещение влево
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeX(-10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeX(10);
                    }
                    panel1.Refresh();
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e) // Перемещение вправо 
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeX(10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeX(-10);
                    }
                    panel1.Refresh();
                }
            }
        }


        private void btnDown_Click(object sender, EventArgs e)  // Перемещение вниз
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeY(10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeY(-10);
                    }
                    if (shapes.getObj(i).getSize() <= 0)
                    {
                        shapes.getObj(i).changeSize(10);
                    }
                    panel1.Refresh();
                }
            }
        }

        private void btnExpand_Click(object sender, EventArgs e) // Увеличение размера
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeSize(10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeSize(-10);
                    }
                    if (shapes.getObj(i).getSize() <= 0)
                    {
                        shapes.getObj(i).changeSize(10);
                    }
                    panel1.Refresh();
                }
            }
        }

        private void btnDiminish_Click(object sender, EventArgs e) // Уменьшение размера
        {
            for (int i = 0; i < shapes.getSize(); i++)
            {
                if (shapes.getObj(i).getSelected() == true)
                {
                    shapes.getObj(i).changeSize(-10);
                    if (IsNotCrossingBorders(shapes.getObj(i)) == false)
                    {
                        shapes.getObj(i).changeSize(10);
                    }
                    if (shapes.getObj(i).getSize() <= 0)
                    {
                        shapes.getObj(i).changeSize(10);
                    }
                    panel1.Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle r = panel1.RectangleToScreen(panel1.ClientRectangle);
            Bitmap b = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(r.Location, new Point(0, 0), r.Size);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                b.Save(saveFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Bitmap для открываемого изображения

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {

                Graphics g = panel1.CreateGraphics();
                g.DrawImage(Image.FromFile(open_dialog.FileName), 0, 0);
                if (im.Count != 0) im.Clear();
                im.Add(Image.FromFile(open_dialog.FileName));
                panel1.Refresh();
            }
        }
    }
}
