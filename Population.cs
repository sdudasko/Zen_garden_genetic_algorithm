using System;
using System.Collections.Generic;
using System.Linq;

namespace zen_garden_genetic_algorithm
{
    public class Population
    {
        List<Chromosome> chromosomes = new List<Chromosome>();
        public int[,] Garden;
        public int N;

        public Population(int[,] Garden, int N)
        {
            this.Garden = Garden;
            this.N = N;

            for (int i = 0; i < Config.POCET_JEDINCOV_V_GENERACII; i++)
            {
                this.chromosomes.Add(new Chromosome(Garden, this));
            }
        }

        public void Mix_chromosomes(List<Chromosome> Given_chromosomes)
        {
            List<Chromosome> Chromosomes_for_next_population = new List<Chromosome>();

            List<int> Mixed_genes = new List<int>();

            int len = (Given_chromosomes[0].Genes_n.Count) / 2; // Pouzivame iba prvu polovicu vygenerovanych genov
            int c = 0;

            Console.WriteLine("Creating new population: " + this.N);
            Population New_population = new Population(this.Garden, ++this.N);

            for (int Chromosome_couple_counter = 0; Chromosome_couple_counter < Given_chromosomes.Count / 2; Chromosome_couple_counter++)
            {
                for (int i = 0; i < len; i++)
                {
                    if (i < (len / 2))
                    {
                        Mixed_genes.Add(Given_chromosomes[Chromosome_couple_counter].Genes_n[i]);
                    }
                    else
                    {
                        Mixed_genes.Add(Given_chromosomes[Chromosome_couple_counter + 1].Genes_n[c]);
                        c++;
                    }
                }

                Chromosome Freshly_mixxed_chromosome = new Chromosome(this.Garden, this, Mixed_genes);

                New_population.chromosomes.Add(Freshly_mixxed_chromosome);

                c = 0;
            }


            // Mame vytvorene dvojice, ideme znova krizit
            this.Mix_chromosomes(New_population.chromosomes);
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
            }

            this.chromosomes = this.chromosomes.OrderBy(item => item.Fitness_cent).ToList();

            for (int i = 0; i < this.chromosomes.Count; i++)
            {
                var rnd = new Random();
                var probability = rnd.NextDouble();
                var selected = this.chromosomes.SkipWhile(item => item.Fitness_cent < probability).First();

                Selection_for_mixing.Add(selected);
                
            }

            return Selection_for_mixing;
        }
    }
}
