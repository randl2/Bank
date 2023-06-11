namespace Bank;

class BankAccount
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public List<string> Transactions { get; set; }
    public BankAccount(string name)
    {
        Name = name;
        Balance = 0;
        Transactions = new List<string>();
    }
}
