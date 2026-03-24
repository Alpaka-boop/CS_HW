using System;

namespace CarDescriber
{
    public interface ICar
    {
        string GetDescription();
    }

    public interface IElectric { }

    public interface IMechanical { }

    public interface IAutomatic { }

    public interface IManual { }

    public enum CarType
    {
        Tesla,
        Bmw,
        Audi
    }

    public abstract class ACar : ICar
    {
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Transmission { get; set; }
        public int Seats { get; set; }
        public string Features { get; set; }

        public string GetDescription()
        {
            return $"{Brand}: {Type} car with {Transmission} transmission, {Seats} seats, {Features}";
        }
    }

    public class TeslaCar : ACar, IElectric, IAutomatic
    {
        public TeslaCar()
        {
            Brand = "Tesla";
            Type = "electrical";
            Transmission = "automatic";
            Seats = 5;
            Features = "Android on board";
        }
    }

    public class BmwCar : ACar, IMechanical, IManual
    {
        public BmwCar()
        {
            Brand = "BMW";
            Type = "mechanical";
            Transmission = "manual";
            Seats = 5;
            Features = "leather interior";
        }
    }

    public class AudiCar : ACar, IMechanical, IAutomatic
    {
        public AudiCar()
        {
            Brand = "Audi";
            Type = "mechanical";
            Transmission = "automatic";
            Seats = 5;
            Features = "premium sound system";
        }
    }

    public class CarFactory
    {
        public ICar CreateCar(CarType type)
        {
            switch (type)
            {
                case CarType.Tesla:
                    return new TeslaCar();
                case CarType.Bmw:
                    return new BmwCar();
                case CarType.Audi:
                    return new AudiCar();
                default:
                    throw new ArgumentException("Unknown car type");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CarFactory factory = new();
            while (true)
            {
                Console.Write("Введите марку автомобиля или done для остановки ввода: ");
                string input = Console.ReadLine();
                if (input.Equals("done", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
                if (Enum.TryParse(input, true, out CarType carType))
                {
                    ICar car = factory.CreateCar(carType);
                    Console.WriteLine(car.GetDescription());
                }
                else
                {
                    Console.WriteLine("Неизвестная марка автомобиля.");
                }
            }
        }
    }
}
