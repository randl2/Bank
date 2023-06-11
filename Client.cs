namespace Bank;

class Client
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<BankAccount> BankAccounts { get; set; }
    public Client()
    {
        BankAccounts = new List<BankAccount>();
    }
    public Client(string name, string surname)
    {
        Name = name;
        Surname = surname;
        BankAccounts = new List<BankAccount>();
    }
    public BankAccount GetAccount(string accountName)
    {
        foreach (BankAccount account in BankAccounts)
        {
            if (account.Name == accountName)
            {
                return account;
            }
        }

        return null;
    }
    public void AddAccount(BankAccount account)
    {
        BankAccounts.Add(account);
        Console.WriteLine($"Успішно додано аккаунт під назвою \"{account.Name}\" користувачу {Name} {Surname}");
    }
    public void SendMoney(BankAccount accountOfSender)
    {
        Console.WriteLine("Скільки ви хочете надіслати грошей?");
        double amountOfMoney = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Кому ви хочете надіслати? Напишіть ім'я та, з нового рядку, прізвище отримувача.");
        string? name = Console.ReadLine();
        string? surname = Console.ReadLine();

        FindUserAgain:
        bool found = BankData.ClientFound(name, surname);
        if (found)
        {
            Client userToSend = new Client();
            if(userToSend.BankAccounts != null)
            {
                Console.WriteLine("Напишіть ім'я аккаунту, на який ви хочете надіслати кошти:");
                WritingNameOfAccountAgain:
                string accountName = Console.ReadLine();

                for(int i = 0; i <  userToSend.BankAccounts.Count; i++)
                {
                    if(accountName == userToSend.BankAccounts[i].Name)
                    {
                        accountOfSender.Balance -= amountOfMoney;

                        BankAccount accountOfReceiver = new BankAccount(accountName);
                        accountOfReceiver.Balance += amountOfMoney;

                        Client userToReceive = new Client(name, surname);

                        Transaction.AddTransactionSendMoney(userToSend, userToReceive, accountOfSender, accountOfReceiver, amountOfMoney);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Такого аккаунту не існує. Перевірте на правильність і напишіть ще раз.");
                        goto WritingNameOfAccountAgain;
                    }
                }
            }
            else
            {
                Console.WriteLine("У даного користувача немає аккаунтів.");
            }
        }
        else
        {
            Console.WriteLine("Такого користувача не знайдено. Напишіть ще раз правильно.");
            goto FindUserAgain;
        }
    }
    public void ShowBankAccounts()
    {
        if(BankAccounts.Count != 0)
        {
            Console.Write($"Список рахунків {Name} {Surname}:");
            foreach (BankAccount account in BankAccounts)
            {
                Console.Write($"\n{account.Name}");
                WatchBalance(account);
            }
        }
        else
        {
            Console.WriteLine($"У користувача {Name} {Surname} відсутні рахунки.");
        }
    }
    private void WatchBalance(BankAccount account)
    {
        Console.Write($": {account.Balance} грн.\n");
        if(account.Balance < 0)
        {
            Console.Write($"Увага! На цьому рахунку від'ємний баланс !");
        }
    }
}
