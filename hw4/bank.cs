using System;
using System.Collections.Generic;

class Bank
{
    private List<Client> clients;
    private List<Account> accounts;

    public Bank()
    {
        clients = new List<Client>();
        accounts = new List<Account>();
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
    }

    public void OpenAccount(Client client)
    {
        if (clients.Contains(client))
        {
            Account account = new Account();
            client.AddAccount(account);
            accounts.Add(account);
            Console.WriteLine($"The account is opened for the client: {client.FullName}");
        }
        else
        {
            Console.WriteLine($"Client {client.FullName} not registered with the bank.");
        }
    }

    public List<Client> GetAllClients()
    {
        return clients;
    }

    public List<Account> GetAllAccounts()
    {
        return accounts;
    }
}

class Client
{
    public string FullName { get; }
    private List<Account> accounts;

    public Client(string fullName)
    {
        FullName = fullName;
        accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public decimal GetBalance(Account account)
    {
        return account.Balance;
    }
}

class Account
{
    private static int accountCount = 0;
    public int AccountNumber { get; }
    public decimal Balance { get; private set; }
    private List<Transaction> transactionHistory;

    public Account()
    {
        AccountNumber = ++accountCount;
        Balance = 0;
        transactionHistory = new List<Transaction>();
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Transaction transaction = new Transaction(TransactionType.Deposit, amount);
        transactionHistory.Add(transaction);
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Transaction transaction = new Transaction(TransactionType.Withdrawal, amount);
            transactionHistory.Add(transaction);
        }
        else
        {
            Console.WriteLine("There are insufficient funds in the account.");
        }
    }

    public void Transfer(decimal amount, Account destinationAccount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            destinationAccount.Deposit(amount);

            Transaction transaction = new Transaction(TransactionType.Transfer, amount, destinationAccount.AccountNumber);
            transactionHistory.Add(transaction);
        }
        else
        {
            Console.WriteLine("There are insufficient funds in the account for the transfer.");
        }
    }

    public List<Transaction> GetTransactionHistory()
    {
        return transactionHistory;
    }
}

class Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public int DestinationAccountNumber { get; }

    public Transaction(TransactionType type, decimal amount, int destinationAccountNumber = 0)
    {
        Type = type;
        Amount = amount;
        DestinationAccountNumber = destinationAccountNumber;
    }
}

enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}
