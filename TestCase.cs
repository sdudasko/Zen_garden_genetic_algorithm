using System;
using System.Linq;
using System.Collections.Generic;

namespace zen_garden_genetic_algorithm
{
    public class TestCase
    {

        public void TestZadanie()
        {
            MainClass.Dimension_x = 12;
            MainClass.Dimension_y = 10;

            int[,] Zens_garden = new int[10, 12]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, },
                { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            };


            Population First_population = new Population(Zens_garden, 0);

            List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_roulette();

            First_population.Mix_chromosomes(Selection_for_mixing);

        }

        public void TestZadanieBez2Kamenov()
        {
            MainClass.Dimension_x = 12;
            MainClass.Dimension_y = 10;

            int[,] Zens_garden = new int[10, 12]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, },
                { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            };


            Population First_population = new Population(Zens_garden, 0);

            List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_elitarstvo();

            First_population.Mix_chromosomes(Selection_for_mixing);

        }

        public void TestMensiaZahradka()
        {
            MainClass.Dimension_x = 10;
            MainClass.Dimension_y = 10;

            int[,] Zens_garden = new int[10, 10]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, },
                { 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            };


            Population First_population = new Population(Zens_garden, 0);

            List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_elitarstvo();

            First_population.Mix_chromosomes(Selection_for_mixing);

        }

        public void TestVelaKamenov()
        {
            MainClass.Dimension_x = 7;
            MainClass.Dimension_y = 7;

            int[,] Zens_garden = new int[7, 7]
            {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, -1, 0},
                { 0, -1, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, -1, 0, 0},
                { 0, 0, 0, 0, 0, 0, -1},
                { 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, -1, 0, 0, 0},
                
            };


            Population First_population = new Population(Zens_garden, 0);

            List<Chromosome> Selection_for_mixing = First_population.Select_chromosomes_for_mixing_by_elitarstvo();

            First_population.Mix_chromosomes(Selection_for_mixing);

        }

    }
}
