using System.Linq;
using static System.Console;

namespace Amnisty
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private const string Amnisty = "1";
        private const string Exit = "0";

        public void Run()
        {
            string userInput;
            string amnistyCrime = "Антиправительственное";
            bool isExit = false;

            Database database = new Database();
            database.CreateCriminals();

            while (isExit == false)
            {
                database.ShowAllCriminals();
                WriteLine();
                WriteLine(Amnisty + " - Амнистия! Слава Асторцке!");
                WriteLine(Exit + " - Выход\n");

                userInput = ReadLine();

                switch (userInput)
                {
                    case Amnisty:
                        database.PerformAmnisty(amnistyCrime);
                        break;

                    case Exit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Criminal
    {
        public Criminal()
        {
            Name = GetName();
            Crime = GetCrime();
            isImprisoned = GetImprisonmentStatus();
        }

        public string Name { get; private set; }
        public string Crime { get; private set; }
        public bool isImprisoned { get; private set; }

        private string GetName()
        {
            string[] criminalNames =
            [
        "Толя Руль",
        "Вася Шнырь",
        "Петруха Кабан",
        "Гриша Гопник",
        "Дима Толкач",
        "Санек Бульба",
        "Женя Лапоть",
        "Коля Барсук",
        "Леша Халтурка",
        "Сергей Косой",
        "Миша Череп",
        "Олег Огурец",
        "Игорь Чебурек",
        "Витя Колотушка",
        "Юра Мясник",
        "Андрей Бычок",
        "Макс Карась",
        "Павел Колесо",
        "Саша Котлета",
        "Кирилл Бутерброд",
        "Артем Брызгало",
        "Денис Крошка",
        "Никита Пельмень",
        "Стас Пельменище",
        "Ольга Гречка",
        "Елена Блинная",
        "Таня Лапша",
        "Алиса Пирожок",
        "Вика Щи",
        "Яна Афёра",
        "Витя Застрелю",
        "Паша Кабриолет",
        "Коля Чмырь",
        "Миша Мафиозник"
            ];

            string name = criminalNames[Utils.GetRandomNumber(criminalNames.Length - 1)];
            return name;
        }

        private string GetCrime()
        {
            string[] crimes =
            [
        "Антиправительственное",
        "Угон",
        "Воровство",
        "Разбой",
        "Убийство",
        "Несмешные Мемы",
        "Кринж",
        "Инфоцыганство"
            ];

            string crime = crimes[Utils.GetRandomNumber(crimes.Length - 1)];
            return crime;
        }

        private bool GetImprisonmentStatus()
        {
            bool isImprisoned = false;

            if (Utils.GetRandomNumber(2) == 1)
            {
                isImprisoned = true;
            }

            return isImprisoned;
        }
    }

    class Database
    {
        private int ammountOfRecords = 20;
        private List<Criminal> _criminals = new List<Criminal>();

        public void ShowAllCriminals()
        {
            string imprisonmentStatus;

            WriteLine("\nСписок преступников:\n");

            for (int i = 0; i < _criminals.Count; i++)
            {
                if (_criminals[i].isImprisoned == true)
                {
                    imprisonmentStatus = "В тюрьме";
                }
                else
                {
                    imprisonmentStatus = "На свободе";
                }

                WriteLine($"{_criminals[i].Name}, {_criminals[i].Crime}, {imprisonmentStatus}");
            }
        }

        public void PerformAmnisty(string crime)
        {
            var amistiedCriminals = _criminals.Where(criminal => criminal.Crime == crime).ToList();

            WriteLine("\nАмнистированы:\n");

            foreach (var criminal in amistiedCriminals)
            {
                WriteLine($"{criminal.Name}");
            }

            for (int i = 0; i < amistiedCriminals.Count; i++)
            {
                _criminals.Remove(amistiedCriminals[i]);
            }
        }

        public void CreateCriminals()
        {
            for (int i = 0; i < ammountOfRecords; i++)
            {
                _criminals.Add(new Criminal());
            }
        }
    }

    class Utils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static int GetRandomNumber(int maxValue)
        {
            return s_random.Next(maxValue);
        }
    }
}

//В нашей великой стране Арстоцка произошла амнистия!
//Всех людей, заключенных за преступление "Антиправительственное", следует исключить из списка заключенных.
//Есть список заключенных, каждый заключенный состоит из полей: ФИО, преступление.
//Вывести список до амнистии и после. 