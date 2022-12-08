namespace OnlineShop.Models.Products.Computers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OnlineShop.Models.Products.Components;
    using Common.Constants;
    using Peripherals;

    public abstract class Computer : Product, IComputer
    {
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new HashSet<IComponent>();
            peripherals = new HashSet<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => components as IReadOnlyCollection<IComponent>;

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals as IReadOnlyCollection<IPeripheral>;

        public override double OverallPerformance
        {
            get
            {
                if (components.Any())
                {
                    return base.OverallPerformance + components.Average(c => c.OverallPerformance);
                }
                else
                {
                    return base.OverallPerformance;
                }
            }
        }

        public override decimal Price
            => base.Price + components.Sum(c => c.Price) + peripherals.Sum(p => p.Price);        

        public void AddComponent(IComponent component)
        {
            if (components.Any(c => c.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }

            components.Add(component);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!components.Any() || components.All(c => c.GetType().Name != componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            var component = components.First(c => c.GetType().Name == componentType);
            components.Remove(component);

            return component;
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(p => p.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }

            peripherals.Add(peripheral);
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!peripherals.Any() || peripherals.All(c => c.GetType().Name != peripheralType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }

            var peripheral = peripherals.First(c => c.GetType().Name == peripheralType);
            peripherals.Remove(peripheral);

            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine(string.Format(SuccessMessages.ComputerComponentsToString, components.Count));

            foreach (var component in components)
            {
                sb.AppendLine("  " + component.ToString().Trim());
            }

            double averagePerformance = peripherals.Any() ? peripherals.Average(p => p.OverallPerformance) : 0;   

            sb.AppendLine(string.Format(SuccessMessages.ComputerPeripheralsToString, peripherals.Count, averagePerformance));

            foreach (var peripheral in peripherals)
            {
                sb.AppendLine("  " + peripheral.ToString().Trim());
            }

            return sb.ToString().Trim();
        }
    }
}
