using System;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість працівників");
            Int32.TryParse(Console.ReadLine(), out int n);
            Worker []workers = ReadWorkersArray(n);
            bool checkExitMenu = false;
            while (!checkExitMenu)
            {
                Console.WriteLine("\n\n1.Переглянути масив\n" +
                    "2.Сортувати за спаданням заробітньої плати\n" +
                    "3.Сортувати  за зростанням стажу роботи\n" +
                    "4.Отримати дані про найменшу та найбільшу заробітню плату працівників\n" +
                    "5.Вихід\n" +
                    "Ваш вибір:");
                if (!Int32.TryParse(Console.ReadLine(),out n))
                    Console.WriteLine("Некоректний ввід, повторіть спробу!");
                else
                    switch (n)
                    {
                        case 1:
                            PrintWorkers(workers);
                            break;
                        case 2:
                            SortWorkersBySalary(ref workers);
                            break;
                        case 3:
                            SortWorkersByWorkexpiriense(ref workers);
                            break;
                        case 4:
                            GetWorkersInfo(workers, out int minSalary, out int maxSalary);
                            Console.WriteLine("\nНайменша заробітня плата - " + minSalary + 
                                "\nНайбільша заробітня плата - " + maxSalary);
                            break;
                        case 5:
                            checkExitMenu = true;
                            break;
                        default:
                            Console.WriteLine("Некоректний ввід, повторіть спробу!");
                            break;
                    }
            }
        }

        static Worker[] ReadWorkersArray(int n)
        {
            Worker[] arr = new Worker[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введіть дані про працівника № " + (i + 1) + "\nІм'я ");
                arr[i].Name = Console.ReadLine();
                Console.WriteLine("Рік початку роботи ");
                Int32.TryParse(Console.ReadLine(), out arr[i].Year);
                Console.WriteLine("Місяць початку роботи ");
                Int32.TryParse(Console.ReadLine(), out arr[i].Month);
                Console.WriteLine("Місце роботи\nНазва компанії ");
                arr[i].WorkPlace.Name = Console.ReadLine();
                Console.WriteLine("Посада працівника ");
                arr[i].WorkPlace.Position = Console.ReadLine();
                Console.WriteLine("Заробітня плата  ");
                Int32.TryParse(Console.ReadLine(), out arr[i].WorkPlace.Salary);

            }
            return arr;
        }
        static void PrintWorker(Worker worker)
        {
            Console.WriteLine("Ім'я  - " + worker.Name);
            Console.WriteLine("Рік початку роботи  - " + worker.Year);
            Console.WriteLine("Місяць початку роботи  - " + worker.Month);
            Console.WriteLine("Компанія:\nІм'я компанії  - " + worker.WorkPlace.Name);
            Console.WriteLine("Посада  - " + worker.WorkPlace.Position);
            Console.WriteLine("Заробітня плата  - " + worker.WorkPlace.Salary);
        }
        static void PrintWorkers(Worker []workers)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine(i+1 + " працівник");
                PrintWorker(workers[i]);
            }
        }
        static void GetWorkersInfo(Worker []workers, out int minSalary, out int maxSalary)
        {
            minSalary = maxSalary = workers[0].WorkPlace.Salary;
            for (int i = 1; i < workers.Length; i++)
            {
                if (workers[i].WorkPlace.Salary > maxSalary)
                    maxSalary = workers[i].WorkPlace.Salary;
                else if (workers[i].WorkPlace.Salary < minSalary)
                    minSalary = workers[i].WorkPlace.Salary;
            }
        }
        static void SortWorkersBySalary(ref Worker []workers)
        {
            Array.Sort(workers, SortInfoBySalary);
        }
        static int SortInfoBySalary(Worker a, Worker b)
        {
            if (a.WorkPlace.Salary > b.WorkPlace.Salary)
                return 1;
            if (a.WorkPlace.Salary < b.WorkPlace.Salary)
                return -1;
            return 0;
        }
        static void SortWorkersByWorkexpiriense(ref Worker[] workers)
        {
            Array.Sort(workers, SortInfoBySalary);
        }
        static int SortInfoByWorkexpiriense(Worker a, Worker b)
        {
            if (a.GetWorkExpiriense() > b.GetWorkExpiriense())
                return -1;
            if (a.GetWorkExpiriense() < b.GetWorkExpiriense())
                return 1;
            return 0;
        }
    }
    struct Worker
    {
        public string Name;
        public int Year;
        public int Month;
        public Company WorkPlace;
        public Worker(string name, int year, int month, Company workPlace)
        {
            Name = name;
            Year = year;
            Month = month;
            WorkPlace = workPlace;
        }
        public int GetWorkExpiriense()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            year = year - Year;
            month = month - Month;
            return year * 12 + month;
        }
        public int GetTotalMoney()
        {
            return GetWorkExpiriense() * WorkPlace.Salary;
        }
    }

    struct Company
    {
        public string Name;
        public string Position;
        public int Salary;
        public Company(string name, string position, int salary)
        {
            Name = name;
            Position = position;
            Salary = salary;
        }
    }
}
