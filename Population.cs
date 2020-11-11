using System;
using System.Collections.Generic;

namespace zen_garden_genetic_algorithm
{
    public class Population
    {
        List<Chromosome> chromosomes = new List<Chromosome>();

        public Population(int[,] Garden)
        {
            Chromosome Testing_chromosome = new Chromosome(Garden);
            Testing_chromosome.Print_garden();
        }
    }
}
