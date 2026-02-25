using System;
using System.Threading;

public class BankCustomer
{
    private static int _nextCustomerId = 0;

    public string Name { get; }
    public int Id { get; }

    public BankCustomer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do cliente é obrigatório.", nameof(name));

        Name = name.Trim();
        Id = Interlocked.Increment(ref _nextCustomerId);
    }
}

public class BankAccount
{
    private static int _nextAccountNumber = 1000;

    public string HolderName { get; }
    public int AccountNumber { get; }
    public int Balance { get; private set; }

    public BankAccount(BankCustomer customer)
        : this(customer?.Name ?? throw new ArgumentNullException(nameof(customer)))
    {
    }

    public BankAccount(string holderName)
    {
        if (string.IsNullOrWhiteSpace(holderName))
            throw new ArgumentException("O nome do titular é obrigatório.", nameof(holderName));

        HolderName = holderName.Trim();
        AccountNumber = Interlocked.Increment(ref _nextAccountNumber);
        Balance = 0;
    }

    public void Deposit(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "O valor de depósito deve ser maior que zero.");

        checked
        {
            Balance += amount;
        }
    }

    public void Withdraw(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "O valor de saque deve ser maior que zero.");

        if (amount > Balance)
            throw new InvalidOperationException("Saldo insuficiente.");

        Balance -= amount;
    }
}

static class Program
{
    static void Main()
    {
        var customer = new BankCustomer("Diego");
        var account = new BankAccount(customer);

        Console.WriteLine($"Cliente {customer.Name} (ID {customer.Id}) acessou o banco.");
        Console.WriteLine($"Conta #{account.AccountNumber} criada para {account.HolderName}.");
        Console.WriteLine($"Saldo atual: R$ {account.Balance}");

        TryReadAndDeposit(account);
        TryReadAndWithdraw(account);

        Console.WriteLine($"Saldo final: R$ {account.Balance}");
        Console.WriteLine("Pressione Enter para sair...");
        Console.ReadLine();
    }

    private static void TryReadAndDeposit(BankAccount account)
    {
        Console.WriteLine("Você deseja depositar na sua conta? (S/N)");
        var answer = Console.ReadLine()?.Trim().ToLowerInvariant();

        if (answer is not ("s" or "y"))
            return;

        Console.Write("Informe o valor a depositar: R$ ");
        if (!int.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Valor inválido para depósito.");
            return;
        }

        try
        {
            account.Deposit(amount);
            Console.WriteLine($"Depósito realizado. Novo saldo: R$ {account.Balance}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao depositar: {ex.Message}");
        }
    }

    private static void TryReadAndWithdraw(BankAccount account)
    {
        Console.WriteLine("Você deseja efetuar um saque? (S/N)");
        var answer = Console.ReadLine()?.Trim().ToLowerInvariant();

        if (answer is not ("s" or "y"))
        {
            Console.WriteLine("Operação de saque cancelada.");
            return;
        }

        Console.Write("Informe o valor a sacar: R$ ");
        if (!int.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Valor inválido para saque.");
            return;
        }

        try
        {
            account.Withdraw(amount);
            Console.WriteLine($"Saque realizado. Novo saldo: R$ {account.Balance}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao sacar: {ex.Message}");
        }
    }
}
