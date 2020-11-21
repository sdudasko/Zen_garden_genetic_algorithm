using System;
using System.Linq;
using System.Collections.Generic;

namespace zen_garden_genetic_algorithm
{
    public class TestCase
    {

        public void TestZadanie()
        {
            //MainClass.Dimension_x = 12;
            //MainClass.Dimension_y = 10;

            //int[,] Zens_garden = new int[10, 12]
            //{
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, },
            //    { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            //};

            MainClass.Dimension_x = 4;
            MainClass.Dimension_y = 3;

            int[,] Zens_garden = new int[3, 4]
            {
                { -1, -1, -1, -1 },
                { 0, 0, -1, -1 },
                { -1, -1, -1, 0 },
            };

            Population First_population = new Population(Zens_garden, 0);

            List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_roulette();

            First_population.Mix_chromosomes(Selection_for_mixing);

            
        }
    }
}
