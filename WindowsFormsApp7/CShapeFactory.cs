using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7
{
    abstract public class CShapeFactory
    {
        public abstract CShape createShape(string code);
    }

    public class ConcreteShapeFactory : CShapeFactory
    {
        public override CShape createShape(string code)
        {
            CShape ret = null;
            if (code == "Group") { ret = new Group(); }
            if (code == "Circle") { ret = new Circle(0, 0); }
            if (code == "Square") { ret = new Square(0, 0); }
            if (code == "Triangle") { ret = new Triangle(0, 0); }
            return ret;
        }
    }
}
