namespace Bank;

static class BankData
{
    private static List<Client> clients = new List<Client>() { new("Oleksandr", "Yalovitsa"), new("Tyler", "Durden"), new("Eren", "Yeager"), new("", "") };
    public static void AddClient(string name, string surname)
    {
        clients.Add(new Client() { Name = name, Surname = surname });
        Console.WriteLine($"Вітаю! Тепер у нас новий клієнт: {name} {surname}.");
    }
    public static Client GetClient(string name, string surname)
    {
        return clients.FirstOrDefault(c => c.Name == name && c.Surname == surname);
    }
    public static void ShowClients()
    {
        Console.WriteLine("Список клієнтів:");
        foreach (Client client in clients)
        {
            Console.WriteLine($"{client.Name} {client.Surname}");
        }
    }
    public static bool ClientFound(string name, string surname)
    {
        foreach (Client client in clients)
        {
            if (client.Name == name && client.Surname == surname)
            {
                Console.WriteLine();
                return true;
            }
        }
        return false;
    }
    public static bool FoundAccount(this Client client, string name)
    {
        for (int i = 0; i < client.BankAccounts.Count; i++)
        {
            if (client.BankAccounts[i].Name == name)
            {
                return true;
            }
        }
        return false;
    }
    public static void ClientNotExisting()
    {
        Console.WriteLine("Такого клієнта не було знайдено.");
    }
    public static void AccountNotExisting()
    {
        Console.WriteLine("У цього клієнта немає рахунків.");
    }
}
