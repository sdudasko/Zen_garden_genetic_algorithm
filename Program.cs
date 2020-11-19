using System;

/**
 * Rodicovske stavy, ktore vytvaraju potomkov
 * potomci budu mat nejake vlastnosti rodicov
 * Chromozom - stav - list genov
 * Populacia - skupina chromozomov
 * Fitness - funkcia na ohodnotenie chromozomu
 * 
 * Gen
 *  - je to index policka, kde ma vstupit, ked vojde do zahradky, 1 chromozom moze mat tychto genov teda o/2 + k
 *  - moze obsahovat informaciu, kde sa otoci
 * 
 *
 * Postup:
 * 
 * 1. Vytvorime pociatocnu populaciu (skupinu chromozomov, ktore maju nahodne vygenerovane cisla)
 * 2. Ohodnotime chromozomy populacie
 * 3. Zo sucasnesnej pozicie sa vyberu dvojice chromozomov - rodicia
 * 4. Ideme krizit - teda vyberieme tu dvojicu rodicov, spravime RULETU
 *    - Kazdemu chromozomu sa priradi nejake policko podla jeho Fitness funkcie.
 *    - Budeme mat ruletu, kde policka budu chromozom, a policko bude take velke, aky velky ma fitness
*/

namespace zen_garden_genetic_algorithm
{
    class MainClass
    {
        public const int Dimension_x = 4;
        public const int Dimension_y = 4;

        public static void Main(string[] args)
        {

            int[,] Zens_garden = new int[Dimension_x, Dimension_y]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 2, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 2, 0 },
            };

            new Population(Zens_garden);

        }
        public static int Get_obvod()
        {
            return (MainClass.Dimension_x + MainClass.Dimension_y) * 2;
        }
    }
}
