using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car { CarId = 1, CarName = "Toyota", DriverId = 1 },
            new Car { CarId = 2, CarName = "BMW", DriverId = 1 },
            new Car { CarId = 3, CarName = "Mercedes", DriverId = 2 },
            new Car { CarId = 4, CarName = "Audi", DriverId = 3 }
        };

        List<Driver> drivers = new List<Driver>
        {
            new Driver { DriverId = 1, DriverName = "John", LastNameInitial = 'S' },
            new Driver { DriverId = 2, DriverName = "Mike", LastNameInitial = 'B' },
            new Driver { DriverId = 3, DriverName = "Alice", LastNameInitial = 'C' }
        };

        // 1) Сгруппировать данные по машине, должна быть машина и все водители, которые владели этой машиной:
        var query1 = from car in cars
                     join driver in drivers on car.DriverId equals driver.DriverId
                     group driver by car into carGroup
                     select new { Car = carGroup.Key, Drivers = carGroup.ToList() };

        foreach (var item in query1)
        {
            Console.WriteLine($"Car Name: {item.Car.CarName}");
            Console.WriteLine("Drivers:");
            foreach (var driver in item.Drivers)
            {
                Console.WriteLine($"{driver.DriverName} {driver.LastNameInitial}");
            }
            Console.WriteLine();
        }

        // 2) Сгруппировать данные по владельцу, должны быть владельцы и все машины, которые им принадлежали:
        var query2 = from driver in drivers
                     join car in cars on driver.DriverId equals car.DriverId
                     group car by driver into driverGroup
                     select new { Driver = driverGroup.Key, Cars = driverGroup.ToList() };

        foreach (var item in query2)
        {
            Console.WriteLine($"Driver Name: {item.Driver.DriverName} {item.Driver.LastNameInitial}");
            Console.WriteLine("Cars:");
            foreach (var car in item.Cars)
            {
                Console.WriteLine($"{car.CarName}");
            }
            Console.WriteLine();
        }

        // 3) Выбрать водителей фамилии, которых начинаются с заданной буквы:
        char letter = 'B';
        var query3 = from driver in drivers
                     where driver.LastNameInitial == letter
                     select driver;

        Console.WriteLine($"Drivers with last name initial '{letter}':");
        foreach (var driver in query3)
        {
            Console.WriteLine($"{driver.DriverName} {driver.LastNameInitial}");
        }

        Console.ReadKey();
    }
}

class Car
{
    public int CarId { get; set; }
    public string CarName { get; set; }
    public int DriverId { get; set; }
}

class Driver
{
    public int DriverId { get; set; }
    public string DriverName { get; set; }
    public char LastNameInitial { get; set; }
}