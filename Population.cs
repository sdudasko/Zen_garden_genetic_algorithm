using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace zen_garden_genetic_algorithm
{
    public class Population
    {
        List<Chromosome> chromosomes = new List<Chromosome>(); // hasMany()
        public int[,] Garden;
        public int N;
        public int Sum_fitness = 0;

        public Population(int[,] Garden, int N) // In first population we seed data from base population, then just adding new blood
        {
            this.Garden = Garden;
            this.N = N;
            int To_generate = Config.POCET_POCIATOCNYCH_JEDINCOV;

            if (N != 0) { To_generate = Config.POCET_JEDINCOV_NOVEJ_KRVI; }
            
            for (int i = 0; i < To_generate; i++)
            {
                this.chromosomes.Add(new Chromosome(Garden, this));
            }
            
        }

        public void Mix_chromosomes(List<Chromosome> Given_chromosomes) // Mixing chromosomes based on half of 1st chromosome genes and second half on the first half of second chromosome genes
        {
            List<Chromosome> Chromosomes_for_next_population = new List<Chromosome>();

            List<int> Mixed_genes = new List<int>();
            List<String> Directions = new List<String>();

            int len = (Given_chromosomes[0].Genes_n.Count) / 2; // Pouzivame iba prvu polovicu vygenerovanych genov
            int c = 0;

            foreach(Chromosome ch in Given_chromosomes) // Print for zadanie so we know when we are close to solution
            {
                if (ch.Fitness > 110)
                {
                    Console.WriteLine("Creating new population: " + this.N);
                    Console.WriteLine("GCH: " + ch.Fitness);
                    ch.Print_garden();
                }
            }

            Population New_population = new Population(this.Garden, ++this.N); // and creating new population of new genes

            for (int Chromosome_couple_counter = 0; Chromosome_couple_counter < Given_chromosomes.Count / 2; Chromosome_couple_counter++)
            {
                for (int i = 0; i < len; i++) // Vytvarame dvojicky na krizenie
                {
                    if (i < (len / 2))
                    {
                        Chromosome Ch1 = Given_chromosomes[Chromosome_couple_counter];
                        Mixed_genes.Add(Ch1.Genes_n[i]);
                        if (i < Ch1.genes.Count)
                            Directions.Add(Ch1.genes[i].Direction);
                    }
                    else
                    {
                        Chromosome Ch2 = Given_chromosomes[Chromosome_couple_counter + 1];
                        Mixed_genes.Add(Ch2.Genes_n[c]);
                        if (c < Ch2.genes.Count)
                            Directions.Add(Ch2.genes[c].Direction);
                        c++;
                    }
                }

                Chromosome Freshly_mixxed_chromosome = new Chromosome(this.Garden, this, Mixed_genes, Directions);

                New_population.chromosomes.Add(Freshly_mixxed_chromosome);

                c = 0;
            }

            this.Mix_chromosomes(New_population.chromosomes); // Mame vytvorene dvojice, ideme znova krizit
        }

        public List<Chromosome> Select_chromosomes_for_mixing_by_roulette() // RULETA
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

        public List<Chromosome> Select_chromosomes_for_mixing_by_elitarstvo() // ELITARSTVO
        {
            double Sum_fitnesses = this.chromosomes.Sum(item => item.Fitness);
            double Fitness_cent = 100 / Sum_fitnesses;
            List<Chromosome> Selection_for_mixing = new List<Chromosome>();

            double lastDistribution = 0;
            foreach (Chromosome chromosome in this.chromosomes)
            {
                double ads = ((chromosome.Fitness * Fitness_cent) / 100) + lastDistribution;
                chromosome.Fitness_cent = ads;
                lastDistribution = ads;
            }


            this.chromosomes = this.chromosomes.OrderByDescending(item => item.Fitness).ToList();
            Chromosome Best0 = this.chromosomes[0];
            Chromosome Best1 = this.chromosomes[1];
            Chromosome Best2 = this.chromosomes[2];
            Chromosome Best3 = this.chromosomes[3];

            Selection_for_mixing.Add(Best0);
            Selection_for_mixing.Add(Best1);
            Selection_for_mixing.Add(Best2);
            Selection_for_mixing.Add(Best3);

            for (int i = 0; i < this.chromosomes.Count - 4; i++)
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
