using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // For shuffle

namespace zen_garden_genetic_algorithm
{
    public class Chromosome
    {
        List<Gene> genes = new List<Gene>();
        public int Finess = -1;
        public int[,] Garden_map = new int[MainClass.Dimension_x, MainClass.Dimension_y];
        public int Number_of_rocks = 0;

        public Chromosome(int[,] Garden)
        {
            int[,] Cp = Garden.Clone() as int[,];
            this.Garden_map = Cp;

            this.Fill_genes();
            this.Set_fitness();
        }

        public void Fill_genes()
        {
            List<int> Genes_n = new List<int>();

            int Number_of_genes_to_generate = (MainClass.Dimension_x + MainClass.Dimension_y) * 2;
            for (int i = 1; i <= Number_of_genes_to_generate; i++)
            {
                Genes_n.Add(i);
            }

            Chromosome.Shuffle(Genes_n);

            for (int i = 0; i < Number_of_genes_to_generate / 2; i++) // TODO +k
            {
                Gene gene = new Gene(Genes_n[i], this, i+1);
                this.genes.Add(gene);
            }

            this.genes.ForEach(delegate (Gene Gene)
            {
                Gene.Gene_walk();
            });

            this.Set_fitness();

            this.Set_number_of_rocks();

            if (this.Finess == ((MainClass.Dimension_x * MainClass.Dimension_y) - this.Number_of_rocks))
            {
                Console.WriteLine("Solution found!");
            }

        }

        public void Set_fitness()
        {
            int Fitness = 0;

            int rowLength = this.Garden_map.GetLength(0);
            int colLength = this.Garden_map.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if ((this.Garden_map[i, j] != 0) && (this.Garden_map[i, j] != -1))
                    {
                        Fitness++;
                    }
                }
            }
            this.Finess = Fitness;
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
        public void Set_number_of_rocks()
        {
            int Fitness = 0;

            int rowLength = this.Garden_map.GetLength(0);
            int colLength = this.Garden_map.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (this.Garden_map[i, j] == -1)
                    {
                        this.Number_of_rocks++;
                    }
                }
            }
        }

        // https://stackoverflow.com/a/1262619/6525417 Function for array shuffle
        public static void Shuffle<T>(IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
