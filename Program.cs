using System;

public class BankCustomer
{
    public string name = "unknown";
    public int id = Random.Shared.Next(1, 1000);

    public BankCustomer(string name)
    {
        this.name = name;
    }
}

public class BankAccount
{
    public string name { get; set; } = "undefined";
    private int accountNumber = Random.Shared.Next(1, 1000);
    public int balance { get; private set; } = 0;
    public int getBalance()
    {
        return balance;
    }

    public BankAccount(BankCustomer customer)
    {
        this.name = customer.name;
    }
    public BankAccount(string name)
    {
        this.name = name;
    }
    public void Deposit(int amount)
    {
        if (amount <= 0) throw new Exception("Deposit amount must be positive.");
        balance += amount;
    }
    public void Withdraw(int amount)
    {
        if (amount <= 0) throw new Exception("Withdraw amount must be positive.");
        if (amount > balance) throw new Exception("Insufficient funds.");
        balance -= amount;
    }
}

static class Program
{
    static void Main()
    {
        BankCustomer customer = new BankCustomer("Diego");
        Console.WriteLine($"Customer {customer.name} with ID {customer.id} has access to the bank.");
        BankAccount account = new BankAccount(customer.name);
        Console.WriteLine($"O saldo atualmente é de: (${account.getBalance()})");

        Console.WriteLine("Voce deseja depositar na sua conta? S ou N");
        var answer = Console.ReadLine()?.Trim().ToLower();
        if (answer == "s" || answer == "y")
        {
            Console.Write("Informe o valor a depositar: $");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var amount) && amount > 0)
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposito realizado. Novo saldo: (${account.balance})");
            }
            else
            {
                Console.WriteLine("Valor invalido para deposito.");
            }
        }

        Console.WriteLine("Voce deseja efetuar um saque? S ou N");
        var withdrawAnswer = Console.ReadLine()?.Trim().ToLower();
        if (withdrawAnswer == "s" || withdrawAnswer == "y")
        {
            Console.Write("Informe o valor a sacar: $");
            var withdrawInput = Console.ReadLine();
            if (int.TryParse(withdrawInput, out var wamount) && wamount > 0)
            {
                try
                {
                    account.Withdraw(wamount);
                    Console.WriteLine($"Saque realizado. Novo saldo: (${account.balance})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Valor inválido para saque.");
            }
        }
        else
        {
            Console.WriteLine("Operacao de saque cancelada.");
        }

        Console.WriteLine("Pressione Enter para sair...");
        Console.ReadLine();
    }
}