using Xunit;

public class BankAccountTests
{
    [Fact]
    public void Deposit_WithPositiveAmount_IncreasesBalance()
    {
        var account = new BankAccount("Teste");

        account.Deposit(100);

        Assert.Equal(100, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Deposit_WithZeroOrNegativeAmount_Throws(int amount)
    {
        var account = new BankAccount("Teste");

        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(amount));
    }

    [Fact]
    public void Withdraw_WithSufficientBalance_DecreasesBalance()
    {
        var account = new BankAccount("Teste");
        account.Deposit(200);

        account.Withdraw(70);

        Assert.Equal(130, account.Balance);
    }

    [Fact]
    public void Withdraw_WithInsufficientBalance_Throws()
    {
        var account = new BankAccount("Teste");

        Assert.Throws<InvalidOperationException>(() => account.Withdraw(1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Withdraw_WithZeroOrNegativeAmount_Throws(int amount)
    {
        var account = new BankAccount("Teste");

        Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(amount));
    }

    [Fact]
    public void AccountNumber_IsUniquePerInstance()
    {
        var account1 = new BankAccount("A");
        var account2 = new BankAccount("B");

        Assert.NotEqual(account1.AccountNumber, account2.AccountNumber);
    }
}
