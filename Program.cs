using System.Globalization;
using System.Net;

namespace Final_project_class_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var homeDelivery = new HomeDelivery("123 Main St.", "Doe", "Jane", "+156202011", "Mr. Mike", DateTime.Now.AddHours(3));
            var pickPointDelivery = new PickPointDelivery("Wu Mall", "Doe", "Jack", "Mr. Andrew", DayOfWeek.Wednesday, DateTime.Now.AddHours(1));
            var shopDelivery = new ShopDelivery("Main Shop", "Doe", "James", "Mr. Carl", DayOfWeek.Wednesday, DateTime.Now.AddHours(0.5));

            var order1 = new Order<HomeDelivery, object>(homeDelivery, 534120, "Workbook");
            var order2 = new Order<PickPointDelivery, object>(pickPointDelivery, 534121, "Notebook");
            var order3 = new Order<ShopDelivery, object>(shopDelivery, 534122, "Pen");

            order1.DisplayAddress();
            order1.DeliveryProcess();
            Console.WriteLine();

            order2.DisplayAddress();
            order2.DeliveryProcess();
            Console.WriteLine();

            order3.DisplayAddress();
            order3.DeliveryProcess();
            Console.WriteLine();

        }
    }
    public abstract class Delivery
    {
        public string Address;
        public string LastName;
        public string FirstName;
        public string PhoneNumber;

        //Метод Deliveryman выводит сообщение о доставке
        public abstract void Deliveryman();
        public abstract void Pickpoint();
        public abstract void DeliveryToShop();
    }

    public class HomeDelivery : Delivery
    {
        public string CourierName { get; set; }
        public DateTime DeliveryTime { get; set; }

        public HomeDelivery(string address, string lastName, string firstName, string phoneNumber, string courierName, DateTime deliveryTime) //конструктор
        {
            Address = address;
            LastName = lastName;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
            CourierName = courierName;
            DeliveryTime = deliveryTime;

        }

        public override void Deliveryman()
        {
            Console.WriteLine($"Delivery by {CourierName} for {LastName} {FirstName} to address {Address} at {DeliveryTime}. In a case call the number {PhoneNumber}");
        }
        public override void Pickpoint()
        {
            // Реализация не требуется, оставим пустой
        }
        public override void DeliveryToShop()
        {

        }
    }

    public class PickPointDelivery : Delivery
    {
        public string CourierName { get; set; }
        public string PickPointAddress { get; set; }
        public DayOfWeek DeliveryDayOfWeek { get; set; }
        public DateTime DeliveryTime { get; set; }

        public PickPointDelivery(string address, string lastName, string firstName, string courierName, DayOfWeek deliveryDayOfWeek, DateTime deliveryTime) //конструктор
        {
            PickPointAddress = address;
            LastName = lastName;
            FirstName = firstName;
            CourierName = courierName;
            DeliveryDayOfWeek = deliveryDayOfWeek;
            DeliveryTime = deliveryTime;
        }

        public override void Pickpoint()
        {
            Console.WriteLine($"Delivery by {CourierName} for {LastName} {FirstName} to pick point {PickPointAddress} on {DeliveryDayOfWeek} at {DeliveryTime}.");
        }
        public override void Deliveryman()
        {
            // Реализация не требуется, оставим пустой
        }
        public override void DeliveryToShop()
        {

        }
    }

    public class ShopDelivery : Delivery
    {
        public string CourierName { get; set; }
        public string ShopAddress { get; set; }
        public DayOfWeek DeliveryDayOfWeek { get; set; }
        public DateTime DeliveryTime { get; set; }
        public ShopDelivery(string address, string lastName, string firstName, string courierName, DayOfWeek deliveryDayOfWeek, DateTime deliveryTime) //конструктор
        {
            ShopAddress = address;
            LastName = lastName;
            FirstName = firstName;
            CourierName = courierName;
            DeliveryDayOfWeek = deliveryDayOfWeek;
            DeliveryTime = deliveryTime;
        }

        public override void DeliveryToShop()
        {
            Console.WriteLine($"Delivery by {CourierName} for {LastName} {FirstName} to pick point {ShopAddress} on {DeliveryDayOfWeek} at {DeliveryTime}.");
        }
        public override void Deliveryman()
        {

        }
        public override void Pickpoint()
        {

        }

    }

    public class Order<TDelivery, TStruct> where TDelivery : Delivery //какая еще раз тут логика?
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public Order(TDelivery delivery, int number, string description)
        {
            Delivery = delivery;
            Number = number;
            Description = description; // где выводится description?
        }

        public void DisplayAddress()
        {
            Console.WriteLine($"Order {Number}: {Delivery.Address}");
        }

        public void DeliveryProcess()
        {
            Console.WriteLine($"Processing delivery for order {Number}...");
            Delivery.Deliveryman();
            Delivery.Pickpoint();
            Delivery.DeliveryToShop();
        }
    }
}