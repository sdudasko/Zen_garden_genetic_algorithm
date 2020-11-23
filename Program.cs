using System;
using System.Collections.Generic;
using System.Linq;

namespace zen_garden_genetic_algorithm
{
    class MainClass
    {
        public static int Dimension_x = 8; // These are overwritten in tests
        public static int Dimension_y = 8;

        public static void Main(string[] args)
        {
            //new TestCase().TestZadanie();
            new TestCase().TestZadanieBez2Kamenov();
            //new TestCase().TestMensiaZahradka();
            //new TestCase().TestVelaKamenov();

        }
        public static int Get_obvod()
        {
            return (MainClass.Dimension_x + MainClass.Dimension_y) * 2;
        }
    }
}
