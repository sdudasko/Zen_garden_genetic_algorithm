using System;
namespace zen_garden_genetic_algorithm
{
    public class Gene
    {
        public int X = -1;
        public int Y = -1;
        public String Direction = "Right";

        public Gene()
        {
            int RangeX = MainClass.Dimension_x;
            int RangeY = MainClass.Dimension_y;

            int Assignments_count = 0;

            this.Assign_axis_values(RangeX, RangeY, Assignments_count);
            
        }

        private void Assign_axis_values(int RangeX, int RangeY, int Assignments_count) // TODO - assignments count - if we already have such gene with the same x and y, regenerate?
        {
            Random rand = new Random();

            //if (RangeX > RangeY) // Means we have matrix is wide, not tall
            if (rand.NextDouble() >= 0.5)
            {
                int x = rand.Next(1, RangeX + 1);
                this.X = x;

                if (rand.NextDouble() >= 0.5) {
                    this.Y = RangeY;
                } else
                {
                    this.Y = 0;
                }

                if (this.X == 0 && this.Y == 0)
                {
                    this.Assign_axis_values(RangeX, RangeY, Assignments_count);
                }

            } else
            {
                int y = rand.Next(1, RangeY + 1);
                this.Y = y;

                if (rand.NextDouble() >= 0.5)  // X is either on the very left or very right
                {
                    this.X = RangeX;
                }
                else
                {
                    this.X = 0;
                }

                if (this.X == 0 && this.Y == 0) // TODO - check all 4 corners
                {
                    this.Assign_axis_values(RangeX, RangeY, Assignments_count);
                }
            }

            if (rand.NextDouble() >= 0.5)
            {
                this.Direction = "Left";
            }
        }

        private void Gene_walk()
        {
            if (this.X)
        }

        private bool Is_dimension_marginal(int Dimension_val, string Dimension)
        {
            if (Dimension == "X")
            {
                if (Dimension_val == 0 || Dimension_val == MainClass.Dimension_x - 1)
                {
                    return true;
                }
            } else
            {
                if (Dimension_val == 0 || Dimension_val == MainClass.Dimension_y - 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
