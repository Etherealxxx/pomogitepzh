using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;

namespace authorization
{
    public class User
    {
        public long ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User() { }
        public User(long id, string login, string password, string role)
        {
            ID = id;
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public class Staff: User, IUserCRUD
    {
        public enum Roles { admin = 0, repositoryManager = 1, personalManager =2, casher =3 , countent =4}
        public long StaffID { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; } = "";
        public string BirthDay { get; set; }
        public string Passport { get; set; }

        public long UserID { get; set; }
        public double Salary { get; set; }
        public string Position = "";

        public Staff() { }

        public Staff(User usr)
        {
            ID = usr.ID;
            Login = usr.Login;
            Password = usr.Password;
            Role = usr.Role;
            Staff staff = ReadStaffData(ID);
            if (staff != null)
            {
                Name = staff.Name;
                StaffID = staff.StaffID;
                LastName = staff.LastName;
                Name = staff.Name;
                MiddleName = staff.MiddleName;
                BirthDay = staff.BirthDay;
                Passport = staff.Passport;
                UserID = staff.UserID;
                Salary = staff.Salary;

            }

        }

        protected Staff ReadStaffData(long UserID)
        {
            string text = File.ReadAllText("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\staffData.json");
            List<Staff> staffs = JsonSerializer.Deserialize<List<Staff>>(text);
            foreach (Staff staff in staffs)
            {
                if (staff.UserID == UserID)
                {
                    return staff;
                }
            }
            return null;
        }
    }

    public class Admin: Staff, IUserCRUD
    {
        public string Position = "admin";
        List<User> Users { get; set; }

        public Admin(User usr): base(usr)
        {
        }

        public void AdminMenu()
        {
            Console.WriteLine("  Hello," + Name);
            Console.WriteLine("  ------------------------------------");
            ShowUsers();
            Console.WriteLine("F1 - Добавить запись");
            Console.WriteLine("F2 - Найти запись");
            Root();    
        }

        private void Root()
        {
            int doing = Menu.AdminManager(startPos: 2, count: Users.Count);
            switch (doing)
            {
                case 111:
                    AddUser();
                    break;
                case 112:
                    FindUser();
                    break;
                default:
                    UserData(Users[doing]);
                    break;
            }
        }

        private void AddUser() 
        {
            Console.WriteLine("Введите ID");
            long userId = long.Parse(Console.ReadLine());
            Console.WriteLine("login");
            string login = Console.ReadLine();
            Console.WriteLine("password");
            string password = Console.ReadLine();
            Console.WriteLine("role");
            int role = Int32.Parse(Console.ReadLine());
            User newUser = new User(userId, login, password, ((Roles)role).ToString());
            CreateUser(newUser);
            Console.Clear();
            AdminMenu();
        }

        private void FindUser() 
        {
            string[] parames = { "ID", "Login", "Password", "Role"};
            foreach (string param in parames)
            {
                Console.WriteLine("  " + param);
            }
            int i = Menu.FindUser(count:parames.Length);

            Console.WriteLine(parames[i] + ":");
            string value = Console.ReadLine();
            Users = FindeUserByParam(parames[i], value);
            Console.Clear();
            if (Users.Count == 1)
                UserData(Users[0]);
            else
            {
                foreach (User usr in Users)
                {
                    Console.WriteLine("  " + usr.ID + "   " + usr.Login + "   " + usr.Password + "   " + usr.Role);
                }
                Menu.AdminManager(count: Users.Count);
            }
        }

        private List<User> FindeUserByParam(string param, string value)
        {
            List<User> result = new List<User>();
            switch (param)
            {
                case "ID":
                    int id = Int32.Parse(value);
                    foreach(User usr in Users)
                    {
                        if (id == usr.ID)
                        {
                            result.Add(usr);
                            break;
                        }
                    }
                    break;
                case "Login":
                    foreach (User usr in Users)
                    {
                        if (value == usr.Login)
                        {
                            result.Add(usr);
                            break;
                        }
                    }
                    break;
                case "Password":
                    foreach (User usr in Users)
                    {
                        if (value == usr.Password)
                        {
                            result.Add(usr);
                            break;
                        }
                    }
                    break;
                case "Role":
                    foreach (User usr in Users)
                    {
                        int role = Int32.Parse(value);

                        if (((Roles)role).ToString() == usr.Role)
                        {
                            result.Add(usr);
                        }
                    }
                    break;
            }
            return result;
        }

        private void ShowUsers()
        {
            Users = ReadUser("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\userdata.json");
            foreach (User usr in Users)
            {
                Console.WriteLine("  " + usr.ID + "   " + usr.Login + "   " + usr.Password + "   " + usr.Role);
            }
        }

        static List<User> ReadUser(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<User>>(text);
        }

        private void UserData(User user)
        {
            string[] datas = { user.ID.ToString(), user.Login, user.Password, user.Role};
            foreach(string data in datas)
            {
                Console.WriteLine("  " + data);
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("F10 - change value");
            Console.WriteLine("Delete - remove user");
            int doing = Menu.ChangeUser(count:datas.Length);
            switch (doing)
            {
                case -99:
                    DeleteUser(user);
                    break;
                case -1:
                    AdminMenu();
                    break;
                default:
                    UpdateUser(user, doing);
                    Console.Clear();
                    AdminMenu();
                    break;
            }
        }

        private void CreateUser(User user) 
        {
            Users.Add(user);
            SerializeUsers();
        }

        private void DeleteUser(User user)
        {
            Users.Remove(user);
            SerializeUsers();
            AdminMenu();
        }

        private void SerializeUsers()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\userdata.json");
            JsonSerializer.SerializeAsync(createStream, Users);
        }

        private void UpdateUser(User user, int param)
        {
            Users.Remove(user);
            Console.WriteLine("Print new value");
            string value = Console.ReadLine();
            switch (param)
            {
                case 0:
                    user.ID = long.Parse(value);
                    break;
                case 1:
                    user.Login = value;
                    break;
                case 2:
                    user.Password = value;
                    break;
                case 3:
                    user.Role = ((Roles)Int32.Parse(value)).ToString();
                    break;
            }
            Users.Add(user);
            SerializeUsers();
        }
    }
    //===============================================================================================================
    public class Personal_Manager : Staff, IUserCRUD
    {
        public string Position = "personalManager";

        List<Staff> Staffs;

        public Personal_Manager(User usr) : base(usr)
        {
        }

        public void PM_Menu() 
        {
            Console.WriteLine("  Hello," + Name);
            Console.WriteLine("  ------------------------------------");
            ShowStaffs();
            Console.WriteLine("F1 - Найти сотрудника");
            int pos = Menu.PM_Manager(count: Staffs.Count, startPos:2);
            if (pos == 111)
            {
                FindStaff();
            }
            else
            {
                ShowStaffInfo(Staffs[pos]);
            }
        }
        private void ShowStaffs()
        {
            Staffs = ReadStaff("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\staffData.json");
            foreach (Staff staff in Staffs)
            {
                Console.WriteLine("  " + staff.StaffID + "   " + staff.Name + "   " + staff.LastName + "   " + staff.Position);
            }
        }
        static List<User> ReadUser(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<User>>(text);
        }

        static List<Staff> ReadStaff(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Staff>>(text);
        }

        private void ShowStaffInfo(Staff staff)
        {
            string[] datas = { staff.StaffID.ToString(), staff.Name, staff.LastName, staff.MiddleName, staff.BirthDay, staff.Passport, staff.Salary.ToString() };
            foreach (string data in datas)
            {
                Console.WriteLine("  " + data);
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine("F1 - связать с пользователем");

            Console.WriteLine("F10 - обновить сотрудника");

            Console.WriteLine("Delete - удалить сотрудника");
            int doing = Menu.StaffInfo(count: datas.Length);
            switch (doing)
            {
                case -99:
                    DeleteStaff(staff);
                    PM_Menu();
                    break;
                case -1:
                    PM_Menu();
                    break;
                case -2:
                    RelateWithUser(staff);
                    break;
                default:
                    UpdateStaff(staff, doing);
                    Console.Clear();
                    PM_Menu();
                    break;

            }
        }
        private void DeleteStaff(Staff staff)
        {
            Staffs.Remove(staff);
            SerializeStaffs();
            PM_Menu();
        }
        private void FindStaff() 
        {
            string[] parames = { "ID", "Name", "LastName", "Passport", "Birthdate" };
            foreach (string param in parames)
            {
                Console.WriteLine("  " + param);
            }
            int i = Menu.FindStaff(count: parames.Length);

            Console.WriteLine(parames[i] + ":");
            string value = Console.ReadLine();
            Staffs = FindeStaffByParam(parames[i], value);
            Console.Clear();
            if (Staffs.Count == 1)
                ShowStaffInfo(Staffs[0]);
            else
            {
                foreach (Staff staff in Staffs)
                {
                    Console.WriteLine("  " + staff.StaffID + "   " + staff.Name + "   " + staff.LastName + "   " + staff.Position);
                }
                Menu.PM_Manager(count: Staffs.Count);
            }
        }

        private List<Staff> FindeStaffByParam(string param, string value)
        {
            List<Staff> result = new List<Staff>();
            switch (param)
            {
                case "ID":
                    int id = Int32.Parse(value);
                    foreach (Staff staff in Staffs)
                    {
                        if (id == staff.StaffID)
                        {
                            result.Add(staff);
                            break;
                        }
                    }
                    break;
                case "Name":
                    foreach (Staff staff in Staffs)
                    {
                        if (value == staff.Name)
                        {
                            result.Add(staff);
                            break;
                        }
                    }
                    break;
                case "LastName":
                    foreach (Staff staff in Staffs)
                    {
                        if (value == staff.LastName)
                        {
                            result.Add(staff);
                            break;
                        }
                    }
                    break;
                case "Passport":
                    foreach (Staff staff in Staffs)
                    {
                        if (value == staff.Password)
                        {
                            result.Add(staff);
                            break;
                        }
                    }
                    break;
                case "Birthdate":
                    foreach (Staff staff in Staffs)
                    {
                        if (value == staff.BirthDay)
                        {
                            result.Add(staff);
                            break;
                        }
                    }
                    break;
            }
            return result;
        }

        private void RelateWithUser(Staff staff)
        {
            Console.WriteLine("Введите  Id пользователя");
            long id = long.Parse( Console.ReadLine());
            Staffs.Remove(staff);
            staff.UserID = id;
            Staffs.Add(staff);
            SerializeStaffs();
            PM_Menu();
        }
        private void UpdateStaff(Staff staff, int param)
        {
                Staffs.Remove(staff);
                Console.WriteLine("Print new value");
                string value = Console.ReadLine();
                switch (param)
                {
                    case 0:
                        staff.StaffID = long.Parse(value);
                        break;
                    case 1:
                        staff.Name = value;
                        break;
                    case 2:
                        staff.LastName = value;
                        break;
                    case 3:
                        staff.MiddleName = value;
                        break;
                    case 4:
                        staff.BirthDay = value;
                        break;
                    case 5:
                        staff.Passport = value;
                        break;
                    case 6:
                        staff.Salary = Double.Parse(value);
                        break;
            }
                Staffs.Add(staff);
            SerializeStaffs();            
        }
        private void SerializeStaffs()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\autorization\\staffData.json");
            JsonSerializer.SerializeAsync(createStream, Staffs);
        }
    }

    //==================================================================================================
    public class Repository_Manager: Staff
    {
        public string Position = "repositoryManager";

        List<Items> Items;
        List<Repository> RepositoryItem;

        public Repository_Manager(User usr): base(usr){ }

        public void RM_Menu()
        {
            Console.WriteLine("  Hello," + Name);
            Console.WriteLine("  ------------------------------------");
            ShowItems();
            Console.WriteLine("F1 - Найти товар");

            int pos = Menu.RM_Manager(count: Items.Count, startPos: 2);
            if (pos == 111)
            {
                FindItem();
            }
            else
            {
                ShowItemInfo(Items[pos]);
            }
        }
        private void ShowItems()
        {
            Items = ReadItems("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\items.json");
            foreach (Items item in Items)
            {
                Console.WriteLine("  " + item.ID + "   " + item.Title + "   " + item.Price);
            }
        }

        List<Items> ReadItems(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Items>>(text);
        }

        private void FindItem() { }

        private void ShowItemInfo(Items item) 
        {
            int count = CountOfItems(item.ID);
            string[] parames = { item.ID.ToString(), item.Title, item.Price.ToString(), count.ToString() };
            foreach(string param in parames)
            {
                Console.WriteLine("  " + param);
            }
            int doing = Menu.ChangeItem(count:parames.Length);
            switch (doing)
            {
                case -99:
                    DeleteItem(item);
                    RM_Menu();
                    break;
                case -1:
                    RM_Menu();
                    break;
                default:
                    UpdateItem(item, doing);
                    Console.Clear();
                    RM_Menu();
                    break;
            }
        }

        private void DeleteItem(Items item)
        {
            Items.Remove(item);
            SerializeItems();
        }

        private void UpdateItem(Items item, int doing)
        {
            if (doing < 3)
            {
                Items.Remove(item);
                Console.WriteLine("Print new value");
                string value = Console.ReadLine();
                switch (doing)
                {
                    case 0:
                        item.ID = long.Parse(value);
                        break;
                    case 1:
                        item.Title = value;
                        break;
                    case 2:
                        item.Price = Int32.Parse(value);
                        break;

                }
                Items.Add(item);
                SerializeItems();
            }
            else
            {
                Repository repository = FindRepoByItem(item);
                RepositoryItem.Remove(repository);
                repository.Count = Int32.Parse(Console.ReadLine());
                RepositoryItem.Add(repository);
                SerializeRepos();
            }

        }

        private Repository FindRepoByItem(Items item)
        {
            ReadRepos();
            foreach (Repository repo in RepositoryItem)
            {
                if (repo.ItemID == item.ID)
                    return repo;
            }
            return null;
        }

        private int CountOfItems(long ItemId)
        {
            ReadRepos();
            foreach(Repository repo in RepositoryItem)
            {
                if (repo.ItemID == ItemId)
                    return repo.Count;
            }
            return 0;
        }
        private void ReadRepos()
        {
            RepositoryItem = ReadRepo("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\repos.json");
        }
        List<Repository> ReadRepo(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Repository>>(text);
        }
        private void SerializeItems()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\items.json");
            JsonSerializer.SerializeAsync(createStream, Items);
        }
        private void SerializeRepos()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\repos.json");
            JsonSerializer.SerializeAsync(createStream, RepositoryItem);
        }
    }

    //========================================================================================


    public class Casher: Staff // кассир
    {
        List<Items> Items;
        List<Repository> RepositoryItem;
        List<Cash> Cashes;
        int Sum = 0;

        public string Position = "casher";
        public Casher(User usr) : base(usr)
        {
        }

        public void Casher_Menu()
        {
            Console.WriteLine("  Hello," + Name);
            Console.WriteLine("  ------------------------------------");
            ShowItems();
            int pos = Menu.RM_Manager(count: Items.Count, startPos: 2);
            if (pos == 111)
            {
                SerializeBuing();
                Sum = 0;
                Casher_Menu();
            }
            else
            {
                SetCount(Items[pos]);
                Casher_Menu();
            }
        }

        private void SetCount(Items item)
        {
            int count = Int32.Parse(Console.ReadLine());
            Repository repo = FindRepoByItem(item);
            if (count < repo.Count)
            {
                Sum += item.Price;
                RepositoryItem.Remove(repo);
                repo.Count -= count;
                RepositoryItem.Add(repo);
                SerializeRepos();
            }
        }

        private void SerializeBuing() 
        {
            GetCashe();
            long id = GetMaxCashID() + 1;
            Cash cash = new Cash(id, Sum);
            Cashes.Add(cash);
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\money.json");
            JsonSerializer.SerializeAsync(createStream, Cashes);
        }
        

        private Repository FindRepoByItem(Items item)
        {
            ReadRepos();
            foreach (Repository repo in RepositoryItem)
            {
                if (repo.ItemID == item.ID)
                    return repo;
            }
            return null;
        }
        private void ShowItems()
        {
            Items = ReadItems("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\items.json");
            foreach (Items item in Items)
            {
                Console.WriteLine("  " + item.ID + "   " + item.Title + "   " + item.Price);
            }
        }

        private void GetCashe()
        {
            string text = File.ReadAllText("C:\\Users\\User\\source\\kir\\authorization\\authorization\\money.json");
            Cashes =  JsonSerializer.Deserialize<List<Cash>>(text);
        }

        List<Items> ReadItems(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Items>>(text);
        }

        private void ReadRepos()
        {
            RepositoryItem = ReadRepo("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\repos.json");
        }
        List<Repository> ReadRepo(string filepath)
        {
            string text = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Repository>>(text);
        }
        private void SerializeItems()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\items.json");
            JsonSerializer.SerializeAsync(createStream, Items);
        }
        private void SerializeRepos()
        {
            using FileStream createStream = File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\authorization\\authorization\\repos.json");
            JsonSerializer.SerializeAsync(createStream, RepositoryItem);
        }

        private long GetMaxCashID()
        {
            long max = 0;
            foreach(Cash cash in Cashes)
            {
                if (cash.ID> max) max = cash.ID;
            }
            return max;
        }
    }

    public class Countent: Staff
    {
        public string Position = "countent";
        public Countent(User usr) : base(usr)
        {
        }
    }


    public interface IUserCRUD
    {
        void CreateUser() { }
        void DeleteUser() { }

        List<User> ReadUser(string filepath) { return null; }
        Staff ReadStaffData(long UserID) { return null; }

        void UpdateUser(User user, int param) { }

    }
}
