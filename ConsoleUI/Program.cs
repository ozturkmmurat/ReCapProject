using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCar());
            foreach (var item in carManager.GetById(1))
            {
                Console.WriteLine(item.Description);
            }
        }
    }
}
