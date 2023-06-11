namespace Bank;

static class Transaction
{
    public static void ShowTransactionInfo(this BankAccount account, List<string> transactions)
    {
        Console.WriteLine($"Список транзакцій на рахунку \"{account.Name}\":");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }
    public static void AddTransactionPlusMoney(this Client client, BankAccount account, double money)
    {
        account.Balance += money;
        string transaction = $"{DateTime.Now} {client.Name} {client.Surname} \"{account.Name}\" +{money} грн";
        account.Transactions.Add(transaction);
    }
    public static void AddTransactionMinusMoney(this Client client, BankAccount account, double money)
    {
        account.Balance -= money;
        string transaction = $"{DateTime.Now} {client.Name} {client.Surname} \"{account.Name}\" -{money} грн";
        account.Transactions.Add(transaction);
    }
    public static void AddTransactionSendMoney(Client sender, Client receiver, BankAccount accountOfSender, BankAccount accountOfReceiver, double money)
    {
        accountOfSender.Balance -= money;
        accountOfReceiver.Balance += money;

        string transactionOfSender = $"{DateTime.Now} {sender.Name} {sender.Surname} -> {receiver.Name} {receiver.Surname} -{money} грн";
        string transactionOfReceiver = $"{DateTime.Now} {receiver.Name} {receiver.Surname} <- {sender.Name} {sender.Surname} +{money} грн";

        accountOfSender.Transactions.Add(transactionOfSender);
        accountOfReceiver.Transactions.Add(transactionOfReceiver);

        Console.WriteLine($"{sender.Name} {sender.Surname} з рахунку {accountOfSender.Name} успішно надіслав {money} грн {receiver.Name}" +
            $" {receiver.Surname} на рахунок {accountOfReceiver.Name}");
    }
}
