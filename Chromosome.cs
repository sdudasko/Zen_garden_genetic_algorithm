using System;
using System.Collections.Generic;
using System.Linq;

namespace zen_garden_genetic_algorithm
{
    public class Chromosome
    {
        List<Gene> genes = new List<Gene>();
        public int Finess = -1;
        public int[,] Garden_map = new int[MainClass.Dimension_x, MainClass.Dimension_y];

        public Chromosome(int[,] Garden)
        {
            int[,] Cp = Garden.Clone() as int[,];
            this.Garden_map = Cp;

            this.Fill_genes();
            this.Set_fitness();
        }

        public void Fill_genes()
        {
            for (int i = 0; i < (MainClass.Dimension_x - 1 + MainClass.Dimension_y - 1); i++) // TODO - o/2 + k, -1s because of indexing for both dimensions
            {
                Gene gene = new Gene(i + 1, this);
                this.genes.Add(gene);
            }

            this.genes.ForEach(delegate (Gene Gene)
            {
                Gene.Gene_walk();
            });
        }

        public void Set_fitness()
        {

        }

        /**
         * Just for testing purposes
         * Code used from:
         * https://stackoverflow.com/a/12827010/6525417
         */
        public void Print_garden()
        {

            int rowLength = this.Garden_map.GetLength(0);
            int colLength = this.Garden_map.GetLength(1);
            string arrayString = "";
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    arrayString += string.Format("{0} ", this.Garden_map[i, j]);
                }
                arrayString += System.Environment.NewLine + System.Environment.NewLine;
            }

            Console.Write(arrayString);
        }
    }
}
