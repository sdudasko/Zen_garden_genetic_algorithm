using System;
using System.Collections.Generic;
using System.Linq;

namespace zen_garden_genetic_algorithm
{
    public class Population
    {
        List<Chromosome> chromosomes = new List<Chromosome>();
        public int[,] Garden;

        public Population(int[,] Garden)
        {
            this.Garden = Garden;

            Chromosome first = new Chromosome(Garden);
            Chromosome second = new Chromosome(Garden);
            Chromosome third = new Chromosome(Garden);
            Chromosome fourth = new Chromosome(Garden);

            this.chromosomes.Add(first);
            this.chromosomes.Add(second);
            this.chromosomes.Add(third);
            this.chromosomes.Add(fourth);

            Console.WriteLine("First:");
            first.Print_garden();
            Console.WriteLine("Second:");
            second.Print_garden();
            Console.WriteLine("Third:");
            third.Print_garden();
            Console.WriteLine("Fourth:");
            fourth.Print_garden();

            List<Chromosome> Selection_for_mixing = this.Select_chromosomes_for_mixing_by_roulette();

            Console.WriteLine("First':");
            Selection_for_mixing[0].Print_garden();
            Console.WriteLine("Second':");
            Selection_for_mixing[1].Print_garden();
            Console.WriteLine("Third':");
            Selection_for_mixing[2].Print_garden();
            Console.WriteLine("Fourth':");
            Selection_for_mixing[3].Print_garden();

            this.Mix_chromosomes(first, second);
        }

        public void Mix_chromosomes(Chromosome Chromosome_1, Chromosome Chromosome_2)
        {
            Console.WriteLine();
            List<Chromosome> Chromosomes_for_next_population = new List<Chromosome>();

            List<int> Mixed_genes = new List<int>();

            int len = (Chromosome_1.Genes_n.Count) / 2; // Pouzivame iba prvu polovicu vygenerovanych genov
            int c = 0;

            for (int i = 0; i < len; i++)
            {
                if (i < (len / 2))
                {
                    Mixed_genes.Add(Chromosome_1.Genes_n[i]);
                }
                else
                {
                    Mixed_genes.Add(Chromosome_2.Genes_n[c]);
                    c++;
                }

            }
            for (int i = 0; i < len; i++)
            {

                Console.Write(Mixed_genes[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Chromosome ch = new Chromosome(this.Garden, Mixed_genes);

            ch.Print_garden();

            //new Population();
        }

        public List<Chromosome> Select_chromosomes_for_mixing_by_roulette()
        {
            double Sum_fitnesses = this.chromosomes.Sum(item => item.Fitness);
            double Fitness_cent = 100 / Sum_fitnesses;
            List<Chromosome> Selection_for_mixing = new List<Chromosome>();

            double lastDistribution = 0;
            foreach(Chromosome chromosome in this.chromosomes)
            {
                double ads = ((chromosome.Fitness * Fitness_cent) / 100) + lastDistribution;
                chromosome.Fitness_cent = ads;
                lastDistribution = ads;
                Console.WriteLine("Distribution: " + ads);
            }

            this.chromosomes = this.chromosomes.OrderBy(item => item.Fitness_cent).ToList();

            for (int i = 0; i < this.chromosomes.Count; i++)
            {
                var rnd = new Random();
                var probability = rnd.NextDouble();
                var selected = this.chromosomes.SkipWhile(item => item.Fitness_cent < probability).First();

                Console.WriteLine("Probability: " + probability);
                Selection_for_mixing.Add(selected);
                
            }

            return Selection_for_mixing;
        }
    }
}
