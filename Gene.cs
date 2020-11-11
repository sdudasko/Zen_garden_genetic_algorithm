using System;
namespace zen_garden_genetic_algorithm
{
    public class Gene
    {
        public int N; // Gene is nth -> Indexing from 1 (for easier matrix notation)

        public int X = 0;
        public int Y = 0;
        public String Direction = "Right";

        public Chromosome Chromosome;

        public Gene(int N, Chromosome Chromosome)
        {
            this.N = N;
            int RangeX = MainClass.Dimension_x - 1;
            int RangeY = MainClass.Dimension_y - 1;
            this.Chromosome = Chromosome;

            int Assignments_count = 0;

            this.Assign_axis_values(RangeX, RangeY, Assignments_count);
        }

        private void Assign_axis_values(int RangeX, int RangeY, int Assignments_count) // TODO - assignments count - if we already have such gene with the same x and y, regenerate?
        {
            Random rand = new Random();

            if (rand.NextDouble() >= 0.5)
            {
                int x = rand.Next(0, RangeX);
                this.X = x;

                if (rand.NextDouble() >= 0.5) {
                    this.Y = RangeY;
                } else
                {
                    this.Y = 0;
                }

            } else
            {
                int y = rand.Next(0, RangeY);
                this.Y = y;

                if (rand.NextDouble() >= 0.5)  // X is either on the very left or very right
                {
                    this.X = RangeX;
                }
                else
                {
                    this.X = 0;
                }

            }

            if (rand.NextDouble() >= 0.99) // TODO - we have fixed rotation now, then change to .5
            {
                this.Direction = "Left";
            }
        }

        public void Gene_walk()
        {
            String Increment_direction = "";
            int Increment_by = 0;

            this.Get_dimension_and_direction_to_walk(ref Increment_direction, ref Increment_by);

            if (Increment_direction == "y")
            {
                this.Start_waking_vertical(Increment_by, 0);
            }
            
        }

        private void Start_waking_vertical(int Increment, int Other_direction_increment)
        {
            this.Chromosome.Garden_map[Other_direction_increment, this.X] = this.N;

            if (Other_direction_increment != MainClass.Dimension_y - 1)
            {
                this.Start_waking_vertical(Increment, ++Other_direction_increment);
            }
        }

        private void Get_dimension_and_direction_to_walk(ref String Increment_direction, ref int Increment_by)
        {

            if (this.X == 0 && this.Y == 0) // Top left
            {
                Increment_direction = "y";
                Increment_by = 1;

                return;

            }
            else if (this.X == 0 && this.Y == MainClass.Dimension_y - 1)
            { // Bottom left

                Increment_direction = "y";
                Increment_by = -1;

                return;

            }
            else if (this.Y == 0 && this.X == MainClass.Dimension_x - 1) // Top right
            {
                Increment_direction = "y";
                Increment_by = 1;

                return;

            }
            else if ((this.Y == MainClass.Dimension_y - 1) && (this.X == MainClass.Dimension_x - 1)) // Bottom right
            {
                Increment_direction = "y";
                Increment_by = -1;

                return;
            }


            if (this.X != 0 && this.X != MainClass.Dimension_x - 1) // Not in the corner and we know we are on the bottom or top side
            {
                if (this.Y == 0) // We are on the top side so we need to go down, so we need to increment y value
                {
                    Increment_direction = "y";
                    Increment_by = 1;

                } else // We are on the top so we need to decrement y value
                {
                    Increment_direction = "y";
                    Increment_by = -1;
                }
            } else
            {
                if (this.X == 0) // We are on the left side so we need to go to the right in x dimension
                {
                    Increment_direction = "x";
                    Increment_by = 1;

                }
                else // We are on the very right so we go to left (decrementing x)
                {
                    Increment_direction = "x";
                    Increment_by = -1;
                }
            }
        }
    }
}
