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
            var pickPointDelivery = new PickPointDelivery("Wu Mall", recipient2, DayOfWeek.Wednesday, DateTime.Now.AddHours(1), "9:00-18:00");
            var shopDelivery = new ShopDelivery("Main Shop", recipient3, DayOfWeek.Wednesday, DateTime.Now.AddHours(0.5), "10:00-20:00");

            var product1 = new Product("Workbook", 2);
            var product2 = new Product("Notebook", 1);
            var product3 = new Product("Pen", 5);

            var order1 = new Order<HomeDelivery, object>(homeDelivery, 534120, product1); //сейчас мы как-то используем object?
            var order2 = new Order<PickPointDelivery, object>(pickPointDelivery, 534121, product2);
            var order3 = new Order<ShopDelivery, object>(shopDelivery, 534122, product3);

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
        public string Name;
        public int Quantity;

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

    }

    public class RecipientInfo
    {
        public string LastName; //почему мы все-таки убрали авто свойства?
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

        public Delivery(string address, RecipientInfo recipient, DayOfWeek deliveryDay, DateTime deliveryTime)
        {
            Address = address;
            Recipient = recipient;
            DeliveryDayOfWeek = deliveryDay;
            DeliveryTime = deliveryTime;
        }

        //Метод DeliveryInfo выводит сообщение о доставке
        public abstract void DeliveryInfo();

    }

    public class HomeDelivery : Delivery
    {
        public string CourierName { get; set; } // а тут вот ставим свойство?

        public HomeDelivery(string address, RecipientInfo recipient, string courierName, DayOfWeek deliveryDay, DateTime deliveryTime) //конструктор
            : base(address, recipient, deliveryDay, deliveryTime)
        {
            CourierName = courierName;
        }

        public override void DeliveryInfo()
        {
            Console.WriteLine($"Delivery by {CourierName} for {Recipient.LastName} {Recipient.FirstName} to address {Address} on {DeliveryDayOfWeek} at {DeliveryTime}. In a case call the number {Recipient.PhoneNumber}");
        }

    }

    public class PickPointDelivery : Delivery
    {
        public string WorkingHours;
        public PickPointDelivery(string address, RecipientInfo recipient, DayOfWeek deliveryDay, DateTime deliveryTime, string workingHours)
             : base(address, recipient, deliveryDay, deliveryTime)
        {     
            WorkingHours = workingHours;
        }

        public override void DeliveryInfo()
        {
            Console.WriteLine($"PickPoint Delivery: {Address}, Working Hours: {WorkingHours}, Delivery Time: {DeliveryTime}");
        }

        public class ShopDelivery : Delivery
        {
            public string WorkingHours;

            public ShopDelivery(string address, RecipientInfo recipient, DayOfWeek deliveryDay, DateTime deliveryTime, string workingHours)
                : base(address, recipient, deliveryDay, deliveryTime)
            {
                WorkingHours = workingHours;
            }

            public override void DeliveryInfo()
            {
                Console.WriteLine($"Shop Delivery: {Address}, Working Hours: {WorkingHours}, Delivery Time: {DeliveryTime}");
            }
        }

        public class Order<TDelivery, TStruct> where TDelivery : Delivery
        {
            public TDelivery Delivery;
            public int Number;
            public Product Product;

            public Order(TDelivery delivery, int number, Product product)
            {
                Delivery = delivery;
                Number = number;
                Product = product;
            }

            public void OrderInfo()
            {
                Console.WriteLine($"Order {Number}: {Delivery.Address}. About delivery: {Product.Name}, Quantity: {Product.Quantity}");
            }

            public void DeliveryProcess()
            {
                Console.WriteLine($"Processing delivery for order {Number}...");
                Delivery.DeliveryInfo();
            }
        }
    }
}