namespace Bank;

class Logic
{
    public void Start()
    {
        Console.WriteLine("Вітаю вас у Монобанк!\nТепер ви адміністратор.\n");

        bool isWorking = true;

        while(isWorking)
        {
            Console.WriteLine("Напишіть число від 1 до 9, щоб виконати певну дію.\n");
            Console.WriteLine("1 - добавити клієнта\n2 - показати всіх клієнтів\n3 - створити рахунок\n4 - показати всі рахунки певного клієнта\n" +
                "5 - поповнити рахунок\n6 - зняти кошти\n7 - надіслати кошти на інший рахунок\n8 - показати список транзакцій на одному рахунку\n" +
                "9 - завершити роботу");
            TypingAgain:
            int choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    Console.WriteLine("Введіть ім'я клієнта, якого хочете зареєструвати:");
                    string nameOfClient = Console.ReadLine();

                    Console.WriteLine("Введіть прізвище клієнта, якого хочете зареєструвати:");
                    string surnameOfClient = Console.ReadLine();

                    BankData.AddClient(nameOfClient, surnameOfClient);
                    break;
                case 2:
                    BankData.ShowClients();
                    break;
                case 3:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, якому будете добавляти рахунок:");
                    string nameOfClientForAccount = Console.ReadLine();
                    string surnameOfClientForAccount = Console.ReadLine();

                    if(BankData.ClientFound(nameOfClientForAccount, surnameOfClientForAccount))
                    {
                        Console.WriteLine("Введіть назву рахунку:");
                        string accountName = Console.ReadLine();

                        Client clientForAccount = BankData.GetClient(nameOfClientForAccount, surnameOfClientForAccount);
                        BankAccount account = new BankAccount(accountName);

                        clientForAccount.AddAccount(account);
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }
                    break;
                case 4:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, у якого хочете подивитись список рахунків:");
                    string nameOfClientToShowAccounts = Console.ReadLine();
                    string surnameOfClientToShowAccounts = Console.ReadLine();

                    if(BankData.ClientFound(nameOfClientToShowAccounts, surnameOfClientToShowAccounts))
                    {
                        Client clientToShowAccounts = BankData.GetClient(nameOfClientToShowAccounts, surnameOfClientToShowAccounts);
                        clientToShowAccounts.ShowBankAccounts();
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }
                    break;
                case 5:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, якому хочете поповнити рахунок:");
                    string nameOfClientToAddMoney = Console.ReadLine();
                    string surnameOfClientToAddMoney = Console.ReadLine();

                    if (BankData.ClientFound(nameOfClientToAddMoney, surnameOfClientToAddMoney))
                    {
                        Client clientToAddMoney = BankData.GetClient(nameOfClientToAddMoney, surnameOfClientToAddMoney);

                        Console.WriteLine("Введіть назву рахунку, на який хочете поповнити рахунок:");
                        AccountToBeAddedAgain:
                        string accountToBeAdded = Console.ReadLine();

                        if (clientToAddMoney.BankAccounts.Count > 0)
                        {
                            if (clientToAddMoney.FoundAccount(accountToBeAdded))
                            {
                                BankAccount accountAddMoney = clientToAddMoney.GetAccount(accountToBeAdded);

                                Console.WriteLine("Введіть суму, на яку хочете поповнити:");
                                MoneyToAddAgain:
                                double moneyToAdd = Convert.ToDouble(Console.ReadLine());

                                if(moneyToAdd > 0)
                                {
                                    Transaction.AddTransactionPlusMoney(clientToAddMoney, accountAddMoney, moneyToAdd);
                                    Console.WriteLine($"Рахунок \"{accountToBeAdded}\" було успішно поповнено на {moneyToAdd} грн!");
                                }
                                else if (moneyToAdd < 0)
                                {
                                    Console.WriteLine("Ви ввели від'ємне число. Напишіть додатнє.");
                                    goto MoneyToAddAgain;
                                }
                                else if(moneyToAdd == 0)
                                {
                                    Console.WriteLine("Ви ввели 0. Напишіть додатнє число.");
                                    goto MoneyToAddAgain;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ви ввели назву рахунку неправильно. Перевірте на правильність і напишіть ще раз.");
                                goto AccountToBeAddedAgain;
                            }
                        }
                        else
                        {
                            BankData.AccountNotExisting();
                        }
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }
                    break;
                case 6:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, з рахунку якого хочете зняти кошти:");
                    string nameOfClientToWithdrawMoney = Console.ReadLine();
                    string surnameOfClientToWithdrawMoney = Console.ReadLine();

                    if(BankData.ClientFound(nameOfClientToWithdrawMoney, surnameOfClientToWithdrawMoney))
                    {
                        Client clientToWithdrawMoney = BankData.GetClient(nameOfClientToWithdrawMoney, surnameOfClientToWithdrawMoney);

                        Console.WriteLine("Введіть назву рахунку, з якого хочете зняти кошти:");
                        AccountToBeWithdrawnAgain:
                        string accountToBeWithdrawn = Console.ReadLine();

                        if(clientToWithdrawMoney.BankAccounts.Count > 0)
                        {
                            if(clientToWithdrawMoney.FoundAccount(accountToBeWithdrawn))
                            {
                                BankAccount accountWithdrawMoney = clientToWithdrawMoney.GetAccount(accountToBeWithdrawn);

                                Console.WriteLine("Введіть суму, яку хочете зняти з рахунку:");
                                MoneyToWithdrawAgain:
                                double moneyToWithdraw = Convert.ToDouble(Console.ReadLine());

                                if(moneyToWithdraw > 0)
                                {
                                    Transaction.AddTransactionMinusMoney(clientToWithdrawMoney, accountWithdrawMoney, moneyToWithdraw);
                                    Console.WriteLine($"З рахунку \"{accountToBeWithdrawn}\" було успішно знято {moneyToWithdraw} грн!");
                                }
                                else if(moneyToWithdraw  < 0)
                                {
                                    Console.WriteLine("Ви ввели від'ємне число. Напишіть додатнє.");
                                    goto MoneyToWithdrawAgain;
                                }
                                else if(moneyToWithdraw == 0)
                                {
                                    Console.WriteLine("Ви ввели 0. Напишіть додатнє число.");
                                    goto MoneyToWithdrawAgain;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ви ввели назву рахунку неправильно. Перевірте на правильність і напишіть ще раз.");
                                goto AccountToBeWithdrawnAgain;
                            }
                        }
                        else
                        {
                            BankData.AccountNotExisting();
                        }
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }
                    break;
                case 7:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, з рахунку якого хочете надіслати кошти:");
                    string nameOfSender = Console.ReadLine();
                    string surnameOfSender = Console.ReadLine();

                    if(BankData.ClientFound(nameOfSender, surnameOfSender))
                    {
                        Client sender = BankData.GetClient(nameOfSender, surnameOfSender);

                        if(sender.BankAccounts.Count > 0)
                        {
                            Console.WriteLine("Введіть назву рахунку, з якого хочете зняти кошти:");
                            AccountNameOfSenderAgain:
                            string accountNameOfSender = Console.ReadLine();

                            if (sender.FoundAccount(accountNameOfSender))
                            {
                                BankAccount senderAccount = sender.GetAccount(accountNameOfSender);
                                Console.WriteLine("Введіть суму, яку хочете надіслати:");
                                MoneyToSendAgain:
                                double moneyToSend = Convert.ToDouble(Console.ReadLine());

                                if(moneyToSend > 0)
                                {

                                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, на рахунок якого хочете надіслати кошти:");
                                    string nameOfReceiver = Console.ReadLine();
                                    string surnameOfReceiver = Console.ReadLine();

                                    if (BankData.ClientFound(nameOfReceiver, surnameOfReceiver))
                                    {
                                        Client receiver = BankData.GetClient(nameOfReceiver, surnameOfReceiver);

                                        if (receiver.BankAccounts.Count > 0)
                                        {
                                            Console.WriteLine("Введіть назву рахунку, на який хочете надіслати кошти:");
                                        AccountNameOfReceiverAgain:
                                            string accountNameOfReceiver = Console.ReadLine();

                                            if (receiver.FoundAccount(accountNameOfReceiver))
                                            {
                                                BankAccount receiverAccount = receiver.GetAccount(accountNameOfReceiver);

                                                Transaction.AddTransactionSendMoney(sender, receiver, senderAccount, receiverAccount, moneyToSend);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ви ввели назву рахунку неправильно. Перевірте на правильність і напишіть ще раз.");
                                                goto AccountNameOfReceiverAgain;
                                            }
                                        }
                                        else
                                        {
                                            BankData.AccountNotExisting();
                                        }
                                    }
                                }
                                else if(moneyToSend < 0)
                                {
                                    Console.WriteLine("Ви ввели від'ємне число. Напишіть додатнє.");
                                    goto MoneyToSendAgain;
                                }
                                else if(moneyToSend == 0)
                                {
                                    Console.WriteLine("Ви ввели 0. Введіть додатнє число.");
                                    goto MoneyToSendAgain;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ви ввели назву рахунку неправильно. Перевірте на правильність і напишіть ще раз.");
                                goto AccountNameOfSenderAgain;
                            }
                        }
                        else
                        {
                            BankData.AccountNotExisting();
                        }
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }

                    break;
                case 8:
                    Console.WriteLine("Введіть ім'я та, з нового рядка, прізвище клієнта, у якого хочете подивитись список транзакцій:");
                    string nameToSeeTransactions = Console.ReadLine();
                    string surnameToSeeTransactions = Console.ReadLine();

                    if(BankData.ClientFound(nameToSeeTransactions, surnameToSeeTransactions))
                    {
                        Client clientToSeeTransactions = BankData.GetClient(nameToSeeTransactions, surnameToSeeTransactions);
                        if (clientToSeeTransactions.BankAccounts.Count == 0)
                        {
                            BankData.AccountNotExisting();
                        }
                        else
                        {
                            Console.WriteLine("Введіть назву рахунку:");
                            NameToSeeTransactionsAgain:
                            string accountNameToSeeTransactions = Console.ReadLine();
                            if (clientToSeeTransactions.FoundAccount(accountNameToSeeTransactions))
                            {
                                BankAccount accountToSeeTransactions = clientToSeeTransactions.GetAccount(accountNameToSeeTransactions);
                                if (accountToSeeTransactions.Transactions.Count > 0)
                                {
                                    accountToSeeTransactions.ShowTransactionInfo(accountToSeeTransactions.Transactions);
                                }
                                else
                                {
                                    Console.WriteLine("На цьому рахунку не проводились транзакції.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Даного рахунку не існує. Перевірте не правильність і введіть ще раз.");
                                goto NameToSeeTransactionsAgain;
                            }
                        }
                    }
                    else
                    {
                        BankData.ClientNotExisting();
                    }
                    break;
                case 9:
                    isWorking = false;
                    Console.WriteLine("\nРоботу завершено.");
                    break;
                default:
                    Console.WriteLine($"Ви ввели {choice}, а треба число від 1 до 9. Введіть ще раз правильно.");
                    goto TypingAgain;
            }
        }
    }
}
