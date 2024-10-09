using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class UserProfile
    {
        // Публічні поля
        public string username;
        public string email;
        public string status;

        // Звичайний конструктор
        public UserProfile()
        {
            username = "Anonymous";
            email = "unknown@example.com";
            status = "Active";
        }

        // Параметризований конструктор
        public UserProfile(string username, string email, int status)
        {
            this.username = username;
            this.email = email;
            this.status = GetStatus(status);
        }

        // Публічні властивості
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        // Публічні методи
        public virtual void ToString()
        {
            Console.WriteLine($"Ім'я користувача: {username}, \nЕлектронна адреса: {email}, \nСтатус: {status}.");
        }

        public virtual void SendMessage(string message)
        {
            Console.WriteLine($"{username} відправив повідомлення: {message}");
        }

        public void BlockUser()
        {
            Console.WriteLine($"Користувача {username} заблоковано.");
        }

        public virtual void DeleteAccount()
        {
            Console.WriteLine($"Користувача {username} видалено.");
        }

        public string GetStatus(int choice)
        {
            switch (choice) 
            {
                case 1:
                    return "Active";
                case 2:
                    return "Inactive";
                default:
                    throw new ArgumentException("Невірний вибір статусу, оберіть 1 або 2.");
            }
        }
    }

    public class AdminProfile : UserProfile
    {
        // Приватне поле
        private int adminLevel;

        // Конструктор з рівнем адміністратора
        public AdminProfile(string username, string email, int status, int adminLevel)
        {
            this.username = username;
            this.email = email;
            this.status = GetStatus(status);
            
            // Перевірка
            if (adminLevel <= 0)
            {
                throw new ArgumentException("Рівень адміністратора не може бути від'ємним або рівним нулю.");
            }
            else
            {
                this.adminLevel = adminLevel;
            }
        }

        // Перевизначений метод
        public override void ToString()
        {
            Console.WriteLine($"Ім'я користувача: {username}, \nЕлектронна адреса: {email}, \nСтатус: {status}, \nРівень адміністративних прав: {adminLevel}.");
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"Адміністратор {username} відправив системне повідомлення: {message}");
        }

        public override void DeleteAccount()
        {
            Console.WriteLine($"Адміністративний профіль {username} видалено.");
        }

        // Публічні методи
        public void BanUser(string username)
        {
            Console.WriteLine($"Адміністратор заблокував користувача {username}.");
        }

        public void UnbanUser(string username)
        {
            Console.WriteLine($"Адміністратор розблокував користувача {username}.");
        }
    }

    public class RegularProfile : UserProfile
    {
        // Приватне поле
        private int friendCount;

        // Конструктор з кількістю друзів
        public RegularProfile(string username, string email, int status, int friendCount)
        {
            this.username = username;
            this.email = email;
            this.status = GetStatus(status);

            // Перевірка
            if (friendCount < 0)
            {
                throw new ArgumentException("Кількість друзів не може бути від'ємним значенням.");
            }
            else
            {
                this.friendCount = friendCount;
            }
        }

        // Перевизначений метод
        public override void ToString()
        {
            Console.WriteLine($"Ім'я користувача: {username}, \nЕлектронна адреса: {email}, \nСтатус: {status}, \nКількість друзів: {friendCount}.");
        }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{username} відправив особисте повідомлення: {message}");
        }

        public override void DeleteAccount()
        {
            Console.WriteLine($"Звичайний профіль {username} видалено.");
        }

        // Публічний метод
        public void AddFriend()
        {
            friendCount += 1;
            Console.WriteLine($"{username} додав нового друга. Загальна кількість друзів: {friendCount}.");
        }
    }

    class Program
    {
        static void Main()
        {
            // Всі профілі
            UserProfile userProfile = new UserProfile();
            userProfile.ToString();
            userProfile.SendMessage("Hello World!");
            userProfile.BlockUser();
            userProfile.DeleteAccount();

            Console.WriteLine(new string('-', 40));

            Console.Write("Введіть ім'я користувача: ");
            string username = Console.ReadLine();
            Console.Write("Введіть електронну адресу: ");
            string email = Console.ReadLine();
            Console.Write("Оберіть статус 1 - Active або 2 - Inactive: ");
            int status = int.Parse(Console.ReadLine());
            Console.Write("Відправте повідомлення: ");
            string message = Console.ReadLine();

            Console.WriteLine();

            UserProfile userProfile1 = new UserProfile(username, email, status);
            userProfile1.ToString();
            userProfile1.SendMessage(message);
            userProfile1.BlockUser();
            userProfile1.DeleteAccount();

            Console.WriteLine(new string('-', 40));

            // Профіль адміністратора
            Console.Write("Введіть ім'я адміністратора: ");
            string adminUsername = Console.ReadLine();
            Console.Write("Введіть електронну адресу: ");
            string adminEmail = Console.ReadLine();
            Console.Write("Оберіть статус 1 - Active або 2 - Inactive: ");
            int adminStatus = int.Parse(Console.ReadLine());
            Console.Write("Відправте повідомлення: ");
            string adminMessage = Console.ReadLine();
            Console.Write("Введіть рівень адміністративних прав: ");
            int adminLevel = int.Parse(Console.ReadLine());
            Console.Write("Введіть ім'я користувача для заблокування: ");
            string adminBan = Console.ReadLine();
            Console.Write("Введіть ім'я користувача для розблокування: ");
            string adminUnban = Console.ReadLine();

            Console.WriteLine();

            AdminProfile adminProfile = new AdminProfile(adminUsername, adminEmail, adminStatus, adminLevel);
            adminProfile.ToString();
            adminProfile.SendMessage(adminMessage);
            adminProfile.BanUser(adminBan);
            adminProfile.UnbanUser(adminUnban);
            adminProfile.DeleteAccount();

            Console.WriteLine(new string('-', 40));

            // Профіль звичайного користувача
            Console.Write("Введіть ім'я звичайного користувача: ");
            string regularUsername = Console.ReadLine();
            Console.Write("Введіть електронну адресу: ");
            string regularEmail = Console.ReadLine();
            Console.Write("Оберіть статус 1 - Active або 2 - Inactive: ");
            int regularStatus = int.Parse(Console.ReadLine());
            Console.Write("Введіть кількість друзів: ");
            int friendCount = int.Parse(Console.ReadLine());
            Console.Write("Відправте повідомлення: ");
            string regularMessage = Console.ReadLine();

            Console.WriteLine();

            RegularProfile regularProfile = new RegularProfile(regularUsername, regularEmail, regularStatus, friendCount);
            regularProfile.ToString();
            regularProfile.SendMessage(regularMessage);
            regularProfile.AddFriend();
            regularProfile.DeleteAccount();

            Console.ReadKey();
        }
    }
}