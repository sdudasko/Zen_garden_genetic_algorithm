using System;
using System.Collections.Generic;
using System.Linq;

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
        public static int Dimension_x = 8;
        public static int Dimension_y = 8;

        public static void Main(string[] args)
        {

            //int[,] Zens_garden = new int[Dimension_x, Dimension_y]
            //{
            //    { 0, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, -1, 0, 0, 0, -1, 0, 0 },
            //    { 0, 0, 0, -1, 0, 0, 0, 0 },
            //    { 0, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, 0, 0, 0, -1, 0, 0, 0 },
            //    { 0, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, 0, 0, 0, 0, 0, 0, 0 },
            //    { 0, 0, 0, 0, -1, 0, 0, 0 },
            //};

            //Population First_population = new Population(Zens_garden, 0);

            //List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_roulette();

            //First_population.Mix_chromosomes(Selection_for_mixing);

            new TestCase().TestZadanie();

        }
        public static int Get_obvod()
        {
            return (MainClass.Dimension_x + MainClass.Dimension_y) * 2;
        }
    }
}
