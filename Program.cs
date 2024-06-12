using System;
using System.Runtime.InteropServices;
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

            var order1 = new Order<HomeDelivery, List<Product>>(homeDelivery, 534120, new List<Product> {product1, product2 });
            var order2 = new Order<PickPointDelivery, List<Product>>(pickPointDelivery, 534121, new List<Product> { product3 });
            var order3 = new Order<ShopDelivery, List<Product>>(shopDelivery, 534122, new List<Product> { product1, product1});

            order1.OrderInfo();
            order1.DeliveryProcess();
            Console.WriteLine();

            order2.OrderInfo();
            order2.DeliveryProcess();
            Console.WriteLine();

            order3.OrderInfo();
            order3.DeliveryProcess();
            Console.WriteLine();

            order1.AddProduct(new Product("Pencil", 4));
            order1.OrderInfo();
            Console.WriteLine();

            order2.RemoveProduct(product2);
            order2.OrderInfo();
            Console.WriteLine();

            Console.ReadKey();
        }
    }

    //класс для описания товара
    public class Product
    {
        private string _name;
        private int _quantity;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Product name can not be null or empty.");
                _name = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Quantity can not be negative");
                _quantity = value;
            }
        }

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public static Product operator +(Product product1, Product product2) //перегрузка оператора сложения для объединения продуктов
        {
            if (product1.Name != product2.Name)
                throw new InvalidOperationException("Can not combine products with different names");

            return new Product(product1.Name, product1.Quantity + product2.Quantity);
        }
    }

    public class RecipientInfo
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string PhoneNumber { get; private set; }

        public RecipientInfo(string lastName, string firstName, string phoneNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
        }
    }

    public abstract class Delivery
    {
        public string Address { get; protected set; }
        public DateTime DeliveryTime { get; protected set; }
        public DayOfWeek DeliveryDayOfWeek { get; protected set; }
        public RecipientInfo Recipient { get; protected set; }

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
        public string CourierName { get; private set; }

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
        public string WorkingHours { get; private set; }
        public PickPointDelivery(string address, RecipientInfo recipient, DayOfWeek deliveryDay, DateTime deliveryTime, string workingHours)
             : base(address, recipient, deliveryDay, deliveryTime)
        {
            WorkingHours = workingHours;
        }

        public override void DeliveryInfo()
        {
            Console.WriteLine($"PickPoint Delivery: {Address}, Working Hours: {WorkingHours}, Delivery Time: {DeliveryTime}");
        }
    }

    public class ShopDelivery : Delivery
    {
        public string WorkingHours { get; private set; }

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
        public TDelivery Delivery { get; private set; }
        public int Number { get; private set; }
        public TStruct Products { get; private set; }

        public Order(TDelivery delivery, int number, TStruct products)
        {
            Delivery = delivery;
            Number = number;
            Products = products;
        }

        public void OrderInfo()
        {
            Console.WriteLine($"Order {Number}: {Delivery.Address}. About delivery: ");
            if (Products is List<Product> productList)
            {
                foreach (var product in productList)
                {
                    Console.WriteLine($"Product - {product.Name}, Quantity  - {product.Quantity}");
                }
            }
        }

        public void DeliveryProcess()
        {
            Console.WriteLine($"Processing delivery for order {Number}...");
            Delivery.DeliveryInfo();
        }

        public Product this[int index]
        {
            get
            {
                if (Products is List<Product> productsList)
                {
                    return productsList[index];
                }
                throw new InvalidOperationException("Product is not a List<Product>.");
            }

            set
            {
                if (Products is List<Product> productsList)
                {
                    productsList[index] = value;
                }
                else
                {
                    throw new InvalidOperationException("Product is not a List<Product>.");
                }
            }
        }

        public void AddProduct(Product product) //обобщенные методы
        {
            if (Products is List<Product> productsList)
            {
                productsList.Add(product);
            }
            else
            {
                throw new InvalidOperationException("Product is not a List<Product>.");
            }
        }

        public void RemoveProduct(Product product)
        {
            if (Products is List<Product> productsList)
            {
                productsList.Remove(product);
            }
            else
            {
                throw new InvalidOperationException("Product is not a List<Product>.");
            }
        }
    }
}   
