using static Final_project_class_1.PickPointDelivery;

namespace Final_project_class_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var recipient1 = new RecipientInfo("Doe", "Jane", "+156202011");
            var recipient2 = new RecipientInfo("Doe", "Jack", "+156202012");
            var recipient3 = new RecipientInfo("Doe", "James", "+156202013");

            var homeDelivery = new HomeDelivery("123 Main St.", recipient1, "Mr. Mike", DayOfWeek.Friday, DateTime.Now.AddHours(3));
            var pickPointDelivery = new PickPointDelivery("Wu Mall", recipient2, DayOfWeek.Wednesday, DateTime.Now.AddHours(1));
            var shopDelivery = new ShopDelivery("Main Shop", recipient3, DayOfWeek.Wednesday, DateTime.Now.AddHours(0.5));

            var order1 = new Order<HomeDelivery, object>(homeDelivery, 534120, "Workbook");
            var order2 = new Order<PickPointDelivery, object>(pickPointDelivery, 534121, "Notebook");
            var order3 = new Order<ShopDelivery, object>(shopDelivery, 534122, "Pen");

            order1.OrderInfo();
            order1.DeliveryProcess();
            Console.WriteLine();

            order2.OrderInfo();
            order2.DeliveryProcess();
            Console.WriteLine();

            order3.OrderInfo();
            order3.DeliveryProcess();
            Console.ReadKey();

        }
    }

    //класс для описания товара
    public class Product
    {
        public string[] ProductName;

        public Product()
        {

        }

    }

    public class RecipientInfo
    {
        public string LastName;
        public string FirstName;
        public string PhoneNumber;

        public RecipientInfo(string lastName, string firstName, string phoneNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
        }

    }

    public abstract class Delivery
    {
        public string Address;
        public DateTime DeliveryTime;
        public DayOfWeek DeliveryDayOfWeek;
        public RecipientInfo Recipient;

        //Метод Deliveryman выводит сообщение о доставке
        public abstract void DeliveryInfo();

    }

    public class HomeDelivery : Delivery
    {
        public string CourierName { get; set; }

        public HomeDelivery(string address, RecipientInfo recipient, string courierName, DayOfWeek deliveryDayOfWeek, DateTime deliveryTime) //конструктор
        {
            Address = address;
            Recipient = recipient;
            CourierName = courierName;
            DeliveryDayOfWeek = deliveryDayOfWeek;
            DeliveryTime = deliveryTime;
        }

        public override void DeliveryInfo()
        {
            Console.WriteLine($"Delivery by {CourierName} for {Recipient.LastName} {Recipient.FirstName} to address {Address} on {DeliveryDayOfWeek} at {DeliveryTime}. In a case call the number {Recipient.PhoneNumber}");
        }

    }

    public class PickPointDelivery : Delivery
    {
        public PickPointDelivery(string address, RecipientInfo recipient, DayOfWeek deliveryDayOfWeek, DateTime deliveryTime) //конструктор
        {
            Address = address;
            Recipient = recipient;
            DeliveryDayOfWeek = deliveryDayOfWeek;
            DeliveryTime = deliveryTime;
        }


        public override void DeliveryInfo()
        {
            Console.WriteLine($"Delivery for {Recipient.LastName} {Recipient.FirstName} to pick point {Address} on {DeliveryDayOfWeek} at {DeliveryTime}.");
        }


        public class ShopDelivery : Delivery
        {
            public ShopDelivery(string address, RecipientInfo recipient, DayOfWeek deliveryDayOfWeek, DateTime deliveryTime) //конструктор
            {
                Address = address;
                Recipient = recipient;
                DeliveryDayOfWeek = deliveryDayOfWeek;
                DeliveryTime = deliveryTime;
            }

            public override void DeliveryInfo()
            {
                Console.WriteLine($"Delivery for {Recipient.LastName} {Recipient.FirstName} to pick point {Address} on {DeliveryDayOfWeek} at {DeliveryTime}.");
            }
        }

        public class Order<TDelivery, TStruct> where TDelivery : Delivery
        {
            public TDelivery Delivery;

            public int Number;

            public string Description;

            public Order(TDelivery delivery, int number, string description)
            {
                Delivery = delivery;
                Number = number;
                Description = description;
            }

            public void OrderInfo()
            {
                Console.WriteLine($"Order {Number}: {Delivery.Address}. About delivery: {Description}");
            }

            public void DeliveryProcess()
            {
                Console.WriteLine($"Processing delivery for order {Number}...");
                Delivery.DeliveryInfo();
            }
        }
    }
}