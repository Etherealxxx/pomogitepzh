using System.Text.Json;

namespace authorization
{
    class Program
    {
        static void Main()
        {
            Auth();
        }

        public static void Auth()
        {
            Console.WriteLine("login");
            string login = Console.ReadLine();
            Console.WriteLine("password");
            string password = Console.ReadLine();
            Authorization.Login(login, password);
            Auth();
        }
    }

    public static class Authorization
    {
        public static void Login(string login, string password)
        {
            User usr = IsUserExist(login);
            if (usr != null)
            {
                if (usr.Password == password)
                    SetUserRole(usr);
                else
                {
                    Console.Clear();
                    Console.WriteLine("password not exist");

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("login not exist");
            }
        }
        private static User IsUserExist(string login)
        {
            string text = File.ReadAllText("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\userdata.json");

            List<User> users = JsonSerializer.Deserialize<List<User>>(text);
            foreach (User user in users)
            {
                if (user.Login == login) return user;
            }
            return null;
        }

        private static void SetUserRole(User user)
        {
            Console.Clear();
            switch (user.Role)
            {
                case "admin":
                    Admin adm = new Admin(user);
                    adm.AdminMenu();
                    break;
                case "repositoryManager":
                    Repository_Manager rm = new Repository_Manager(user);
                    rm.RM_Menu();
                    break;
                case "personalManager":
                    Personal_Manager pm = new Personal_Manager(user);
                    pm.PM_Menu();
                    break;
                case "casher":
                    Casher casher = new Casher(user);
                    casher.Casher_Menu();
                    break;
                case "countent":
                    Countent countent = new Countent(user);
                    break;
            }
        }

    }

    public static class Menu
    {
        public static int AdminManager(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.F1:
                        Console.Clear();
                        return 111;
                    case ConsoleKey.F2: 
                        Console.Clear();
                        return 112;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Program.Auth();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int ChangeUser(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Delete:
                        Console.Clear();
                        return -99;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return -1;
                    case ConsoleKey.F10:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int FindUser(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i-startPos;
                }
            }
        }

        public static int PM_Manager(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.F1:
                        Console.Clear();
                        return 111;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Program.Auth();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int StaffInfo(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Delete:
                        Console.Clear();
                        return -99;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return -1;
                    case ConsoleKey.F10:
                        Console.Clear();
                        return i - startPos;
                    case ConsoleKey.F1:
                        Console.Clear();
                        return -2;
                }
            }
        }
        public static int FindStaff(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int RM_Manager(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.F1:
                        Console.Clear();
                        return 111;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Program.Auth();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int ChangeItem(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Delete:
                        Console.Clear();
                        return -99;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return -1;
                    case ConsoleKey.F10:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
        public static int CasherManager(int startPos = 0, int count = 3, string arrow = "->")
        {
            string empty = new string(' ', arrow.Length);
            int i = startPos;
            Console.SetCursorPosition(0, startPos);
            Console.Write(arrow);
            ConsoleKeyInfo key;
            for (; ; )
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (i == count + startPos - 1)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, ++i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.UpArrow:
                        if (i == startPos)
                            continue;
                        Console.SetCursorPosition(0, i);
                        Console.Write(empty);
                        Console.SetCursorPosition(0, --i);
                        Console.Write(arrow);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return i - startPos;
                }
            }
        }
    }
}

