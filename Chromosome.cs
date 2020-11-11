using System;
using System.Collections.Generic;

namespace zen_garden_genetic_algorithm
{
    public class Chromosome
    {
        List<Gene> genes = new List<Gene>();

        public Chromosome()
        {

            this.Fill_genes();
            this.Perform_fitness();
        }

        public void Fill_genes()
        {
            for (int i = 0; i < (MainClass.Dimension_x - 1 + MainClass.Dimension_y - 1); i++) // TODO - o/2 + k, -1s because of indexing for both dimensions
            {
                Gene gene = new Gene();
                this.genes.Add(gene);
            }
        }

        public void Perform_fitness()
        {

        }
    }
}
