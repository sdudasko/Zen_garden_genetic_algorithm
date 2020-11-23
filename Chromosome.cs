using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // For shuffle

namespace zen_garden_genetic_algorithm
{
    public class Chromosome
    {
        public List<Gene> genes = new List<Gene>(); // hasMany()
        public int Fitness = -1; ///
        public double Fitness_cent = 0; // Helper for selection, should be virtual
        public int[,] Garden_map = new int[MainClass.Dimension_x, MainClass.Dimension_y];
        public int Number_of_rocks = 0;
        public Population Population; // belongsTo()
        
        public List<int> Genes_n = new List<int>();
        public List<String> Directions = new List<String>(); // We pass directions as param for mixing genes

        public Chromosome(int[,] Garden, Population Population, List<int> Genes_n = null, List<String> Directions = null)
        {
            int[,] Cp = Garden.Clone() as int[,];
            this.Garden_map = Cp;
            this.Population = Population;

            if (Genes_n != null)
            {
                this.Genes_n = Genes_n;
            }
            if (Directions != null)
            {
                this.Directions = Directions;
            }

            this.Fill_genes();
            this.Set_fitness();

        }

        /**
         * Creating new genes or taking genes from parameter and then walking the garden from them.
         * Then setting fitness and checking if solution was found.
         */
        public void Fill_genes()
        {

            int Number_of_genes_to_generate = (MainClass.Dimension_x + MainClass.Dimension_y) * 2;
            Random r = new Random();

            if (this.Genes_n.Count == 0)
            {

                for (int i = 1; i <= Number_of_genes_to_generate; i++)
                {
                    this.Genes_n.Add(i);
                }
                Chromosome.Shuffle(Genes_n);

                for (int i = 0; i < Number_of_genes_to_generate / 2; i++)
                {
                    Gene gene = new Gene(Genes_n[i], this, i + 1);

                    if (Config.SANCA_NA_MUTACIU_GENU_V_PERCENTACH/100 > r.NextDouble())
                    {
                        int mutovanyGen = r.Next(i, Number_of_genes_to_generate / 2);
                        gene.N = mutovanyGen;
                    }

                    this.genes.Add(gene);
                }

            } else
            {
                this.genes = new List<Gene>();

                for (int i = 0; i < Number_of_genes_to_generate / 2; i++)
                {
                    Gene gene = new Gene(Genes_n[i], this, i + 1);

                    gene.Direction = this.Directions[i];

                    if (Config.SANCA_NA_MUTACIU_GENU_V_PERCENTACH / 100 > r.NextDouble())
                    {
                        int mutovanyGen = r.Next(i, Number_of_genes_to_generate / 2);
                        gene.N = mutovanyGen;
                    }

                    this.genes.Add(gene);
                }
            }

            foreach (Gene Gene in this.genes) {
                if (!Gene.Gene_walk()) break;        
            }

            this.Set_fitness();
            this.Set_number_of_rocks();

            if (this.Fitness == ((MainClass.Dimension_x * MainClass.Dimension_y) - this.Number_of_rocks)) // Kontorla, ci mame riesenie
            {
                Console.WriteLine("Solution found!: " + this.Population.N);
                this.Print_garden();

                Environment.Exit(0);
                return;
            }

        }

        public void Set_fitness() // Fitness je pocet pohrabanych policok
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
            this.Fitness = Fitness;
        }

        /**
         * Just for testing purposes
         * Code used from:
         * https://stackoverflow.com/a/12827010/6525417
         */
        public void Print_garden() // Printing garden
        {

            int rowLength = this.Garden_map.GetLength(0);
            int colLength = this.Garden_map.GetLength(1);
            var arrayString = "";
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    arrayString += (this.Garden_map[i, j].ToString().PadLeft(2, '0') + " ");
                }
                arrayString += System.Environment.NewLine + System.Environment.NewLine;
            }

            Console.Write(arrayString);
        }
        public void Set_number_of_rocks() // We want to be flexible with rocks and have a single source of truth
        {
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
