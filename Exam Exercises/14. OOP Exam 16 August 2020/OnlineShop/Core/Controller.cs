namespace OnlineShop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Constants;
    using Common.Enums;
    using Models.Products.Components;
    using Models.Products.Computers;
    using Models.Products.Peripherals;

    public class Controller : IController
    {
        private readonly HashSet<IComputer> computers = new HashSet<IComputer>();
        private readonly HashSet<IComponent> components = new HashSet<IComponent>();
        private readonly HashSet<IPeripheral> peripherals = new HashSet<IPeripheral>();

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            if (!Enum.TryParse(computerType, out ComputerType type))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            IComputer computer = null;

            if (type == ComputerType.Laptop)
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else if (type == ComputerType.DesktopComputer)
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }

            computers.Add(computer);

            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            var computer = FindComputerIfExist(computerId);

            var component = components.FirstOrDefault(c => c.Id == id);

            if (component != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            if (!Enum.TryParse(componentType, out ComponentType type))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            switch (type)
            {
                case ComponentType.CentralProcessingUnit:
                    component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.Motherboard:
                    component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.PowerSupply:
                    component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.RandomAccessMemory:
                    component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.SolidStateDrive:
                    component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case ComponentType.VideoCard:
                    component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
            }

            components.Add(component);
            computer.AddComponent(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var computer = FindComputerIfExist(computerId);

            var componentToRemove = computer.RemoveComponent(componentType);
            components.Remove(componentToRemove);

            return string.Format(SuccessMessages.RemovedComponent, componentType, componentToRemove.Id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var computer = FindComputerIfExist(computerId);

            var peripheral = peripherals.FirstOrDefault(c => c.Id == id);

            if (peripheral != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            if (!Enum.TryParse(peripheralType, out PeripheralType type))
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            switch (type)
            {
                case PeripheralType.Headset:
                    peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Keyboard:
                    peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Monitor:
                    peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case PeripheralType.Mouse:
                    peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
            }

            computer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var computer = FindComputerIfExist(computerId);

            var peripheralToRemove = computer.RemovePeripheral(peripheralType);
            peripherals.Remove(peripheralToRemove);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheralToRemove.Id);
        }

        public string BuyComputer(int id)
        {
            var computerToBuy = FindComputerIfExist(id);

            computers.Remove(computerToBuy);

            return computerToBuy.ToString();
        }

        public string BuyBest(decimal budget)
        {
            var computer = computers.Where(c => c.Price <= budget).OrderByDescending(c => c.OverallPerformance).FirstOrDefault();

            if (computer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            return this.BuyComputer(computer.Id);
        }

        public string GetComputerData(int id) => FindComputerIfExist(id).ToString();

        private IComputer FindComputerIfExist(int id)
        {
            var computer = computers.FirstOrDefault(c => c.Id == id);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer;
        }
    }
}
