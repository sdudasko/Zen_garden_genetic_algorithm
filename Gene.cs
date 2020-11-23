using System;
namespace zen_garden_genetic_algorithm
{
    public class Gene
    {
        public int N; // Gene is nth -> Indexing from 1 (for easier matrix notation) (je to index, z ktoreho vstupujeme)
        public int Order; // Order of the gene for notation to matrix
        public String Direction = ""; // We are remembering direction in gene unless its the first population so we generate it random
        public Chromosome Chromosome; // belongsTo()

        public Gene(int N, Chromosome Chromosome, int Order, String Direction = "")
        {
            this.N = N;
            this.Chromosome = Chromosome;
            this.Order = Order;

            if (Direction != "") { this.Direction = Direction; } else { // First generation directions is random
                Random r = new Random();
                if (r.NextDouble() > 0.5) { this.Direction = "Left"; }
                else { this.Direction = "Right"; }
            }

        }

        public bool Gene_walk() // Walking from N to the end of garden
        {
            if (this.N <= MainClass.Dimension_x)
            {
                if (!this.Walk_from_top()) return false;
            } else if ((this.N > MainClass.Dimension_x) && (this.N <= MainClass.Dimension_x + MainClass.Dimension_y))
            {
                if (!this.Walk_from_right()) return false;
            } else if ((this.N > MainClass.Dimension_y + MainClass.Dimension_y) && this.N <= ((MainClass.Dimension_x * 2) + MainClass.Dimension_y))
            {
                if (!this.Walk_from_bottom()) return false;
            } else
            {
                if (!this.Walk_from_left()) return false;
            }
            return true;
        }

        /**
         *  We are walking from the top and trying to get on the bottom. Similar for all directions
         *  This function has initial "nullable" args, set for -1
         *  The function can be through Rotate() be recursively called
         *  The same for all directions
         */ 
        public bool Walk_from_top(int y = -1, int x = -1)
        {
            int i = y;
            if (y == -1 || x == -1) // If starting from the rim 
            {
                x = this.N - 1;
                i = 0;
                if (this.Chromosome.Garden_map[i, x] != 0) return true;
            }

            bool br = false;
            while (i < MainClass.Dimension_y)
            {
                if (this.Chromosome.Garden_map[i, x] != 0 && y != i)
                {
                    if (!this.Rotate(i - 1, x, "Top")) return false;
                    br = true;
                }
                if (br) break;
                this.Chromosome.Garden_map[i++, x] = this.Order;
                
            }
            return true;
        }

        public bool Walk_from_right(int y = -1, int x = -1)
        {
            int i = x;
            if (y == -1 || x == -1)
            {
                i = MainClass.Dimension_x;
                y = this.N - MainClass.Dimension_x - 1;
                if (this.Chromosome.Garden_map[y, i - 1] != 0) return true;
            }

            bool br = false;
            while (i > 0)
            {
                if (this.Chromosome.Garden_map[y, i - 1] != 0)
                {
                    if (!this.Rotate(y, i, "Right")) return false;
                    br = true;
                }
                if (br) break;
                this.Chromosome.Garden_map[y, --i] = this.Order;
            }

            return true;
        }

        public bool Walk_from_bottom(int y = -1, int x = -1)
        {
            bool br = false;
            int i = y;
            if (y == -1 || x == -1)
            {
                x = MainClass.Dimension_x - (this.N - ((MainClass.Dimension_x + MainClass.Dimension_y)));
                i = MainClass.Dimension_y;
                if (this.Chromosome.Garden_map[i - 1, x] != 0) return true;
            }

            while (i > 0)
            {
                if (this.Chromosome.Garden_map[i - 1, x] != 0)
                {
                    if (!this.Rotate(i, x, "Bottom")) return false;
                    br = true;
                }
                if (br) break;
                this.Chromosome.Garden_map[--i, x] = this.Order;
            }
            return true;
        }

        public bool Walk_from_left(int y = -1, int x = -1)
        {
            bool br = false;
            int i = x;
            if (y == -1 || x == -1)
            {
                y = MainClass.Get_obvod() - this.N;
                i = 0;
                if (this.Chromosome.Garden_map[y, i] != 0) return true;
            }

            while (i < MainClass.Dimension_x)
            {
                if (this.Chromosome.Garden_map[y, i] != 0 && (i != x))
                {
                    if (!this.Rotate(y, i - 1, "Left")) return false;
                    br = true;
                }

                if (br) break;
                this.Chromosome.Garden_map[y, i++] = this.Order;
            }
            return true;
        }

        /**
         * Rotating the movement based on where we are walking from and the direction set for the gene.
         */
        private bool Rotate(int y, int x, String From = "")
        {
            switch (From) {
                case "Top":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_left(y, x)) this.Walk_from_right(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        

                    } else
                    {
                        if (this.Can_go_right(y, x)) this.Walk_from_left(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        

                    }
                    break;
                case "Right":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_top(y, x)) this.Walk_from_bottom(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        
                    }
                    else
                    {
                        if (this.Can_go_bottom(y, x)) this.Walk_from_top(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        

                    }
                    break;

                case "Left":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_bottom(y, x)) this.Walk_from_top(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        
                    }
                    else
                    {
                        if (this.Can_go_top(y, x)) this.Walk_from_bottom(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        

                    }
                    break;

                case "Bottom":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_right(y, x)) this.Walk_from_left(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        
                    }
                    else
                    {
                        if (this.Can_go_left(y, x)) this.Walk_from_right(y, x);
                        else {
                            if (this.Is_on_rim(y, x)) return true;
                            return false;
                        }
                        

                    }
                    break;
            }

            return true;

        }

        /**
         * Helper checks
         */
        private bool Is_on_rim(int y, int x)
        {
            if ((y == 0) || (y == MainClass.Dimension_y - 1) || (x == 0) || (x == MainClass.Dimension_x - 1)) return true;
            else return false;
        }

        private bool Can_go_right(int y, int x)
        {
            return ((x != MainClass.Dimension_x - 1) && this.Chromosome.Garden_map[y, x + 1] == 0);
        }

        private bool Can_go_left(int y, int x)
        {
            return ((x != 0) && this.Chromosome.Garden_map[y, x - 1] == 0);
        }

        private bool Can_go_top(int y, int x)
        {
            return ((y != 0) && this.Chromosome.Garden_map[y - 1, x] == 0);
        }

        private bool Can_go_bottom(int y, int x)
        {
            return ((y != MainClass.Dimension_y - 1) && this.Chromosome.Garden_map[y + 1, x] == 0);
        }

    }
}
