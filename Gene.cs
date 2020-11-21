using System;
namespace zen_garden_genetic_algorithm
{
    public class Gene
    {
        public int N; // Gene is nth -> Indexing from 1 (for easier matrix notation)
        public int Order;

        public int X = 0;
        public int Y = 0;
        public String Direction = "Left";

        public Chromosome Chromosome;
        public bool isARock = false;

        public Gene(int N, Chromosome Chromosome, int Order)
        {
            this.N = N;
            this.Chromosome = Chromosome;
            this.Order = Order;
        }

        public void Gene_walk()
        {
            if (this.N <= MainClass.Dimension_x)
            {
                this.Walk_from_top();
            } else if ((this.N > MainClass.Dimension_x) && (this.N <= MainClass.Dimension_x + MainClass.Dimension_y))
            {
                this.Walk_from_right();
            } else if ((this.N > MainClass.Dimension_y + MainClass.Dimension_y) && this.N <= ((MainClass.Dimension_x * 2) + MainClass.Dimension_y))
            {
                this.Walk_from_bottom();
            } else
            {
                this.Walk_from_left();
            }
        }

        public void Walk_from_top(int y = -1, int x = -1)
        {
            int i = y;
            if (y == -1 || x == -1)
            {
                x = this.N - 1;
                i = 0;
                if (this.Chromosome.Garden_map[i, x] != 0) return;
            }

            bool br = false;
            while (i < MainClass.Dimension_y)
            {
                if (this.Chromosome.Garden_map[i, x] != 0 && y != i)
                {
                    this.Rotate(i - 1, x, "Top");
                    br = true;
                    if (this.Chromosome.Garden_map[x, i - 1] != 0) return;
                }
                if (br) break;
                this.Chromosome.Garden_map[i++, x] = this.Order;
                
            }
        }

        public void Walk_from_right(int y = -1, int x = -1)
        {
            int i = x;
            if (y == -1 || x == -1)
            {
                i = MainClass.Dimension_x;
                y = this.N - MainClass.Dimension_x - 1;
                if (this.Chromosome.Garden_map[y, i - 1] != 0) return;
            }

            bool br = false;
            while (i > 0)
            {
                if (this.Chromosome.Garden_map[y, i - 1] != 0)
                {
                    this.Rotate(y, i, "Right");
                    br = true;
                }
                if (br) break;
                this.Chromosome.Garden_map[y, --i] = this.Order;
            }
        }

        public void Walk_from_bottom(int y = -1, int x = -1)
        {
            bool br = false;
            int i = y;
            if (y == -1 || x == -1)
            {
                x = MainClass.Dimension_x - (this.N - ((MainClass.Dimension_x + MainClass.Dimension_y)));
                i = MainClass.Dimension_y;
                if (this.Chromosome.Garden_map[i - 1, x] != 0) return;
            }

            while (i > 0)
            {
                if (this.Chromosome.Garden_map[i - 1, x] != 0) // TODO - toto musi niekedy crashnut
                {
                    this.Rotate(i, x, "Bottom");
                    br = true;
                }
                if (br) break;
                this.Chromosome.Garden_map[--i, x] = this.Order;
            }
        }

        public void Walk_from_left(int y = -1, int x = -1)
        {
            bool br = false;
            int i = x;
            if (y == -1 || x == -1)
            {
                y = MainClass.Get_obvod() - this.N;
                i = 0;
                if (this.Chromosome.Garden_map[y, i] != 0) return;
            }

            while (i < MainClass.Dimension_x)
            {
                if (this.Chromosome.Garden_map[y, i] != 0 && (i != x))
                {
                    this.Rotate(y, i - 1, "Left");
                    br = true;
                }

                if (br) break;
                this.Chromosome.Garden_map[y, i++] = this.Order; // TODO - vsade
            }
        }

        private void Rotate(int y, int x, String From = "")
        {
            switch (From) {
                case "Top":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_left(y, x))
                        {
                            this.Walk_from_right(y, x);
                        }
                    } else
                    {
                        if (this.Can_go_right(y, x))
                        {
                            this.Walk_from_left(y, x);
                        }

                    }
                    break;
                case "Right":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_top(y, x))
                        {
                            this.Walk_from_bottom(y, x);
                        }
                    }
                    else
                    {
                        if (this.Can_go_bottom(y, x))
                        {
                            this.Walk_from_top(y, x);
                        }

                    }
                    break;

                case "Left":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_bottom(y, x))
                        {
                            this.Walk_from_top(y, x);
                        }
                    }
                    else
                    {
                        if (this.Can_go_top(y, x))
                        {
                            this.Walk_from_bottom(y, x);
                        }

                    }
                    break;

                case "Bottom":
                    if (this.Direction == "Right")
                    {
                        if (this.Can_go_right(y, x))
                        {
                            this.Walk_from_left(y, x);
                        }
                    }
                    else
                    {
                        if (this.Can_go_left(y, x))
                        {
                            this.Walk_from_right(y, x);
                        }

                    }
                    break;
            }

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
