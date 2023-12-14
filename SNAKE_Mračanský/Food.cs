using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAKE_Mračanský
{
    internal class Food
    {
        public List<Circle> Foods { get; set; }

        public Food(int count)
        {
            Foods = new List<Circle>();
            for (int i = 0; i < count; i++)
            {
                Foods.Add(new Circle());
            }
        }
    }
}
