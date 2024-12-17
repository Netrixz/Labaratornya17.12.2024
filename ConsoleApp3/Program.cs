using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class BankAk
{
    public int AccountNumber { get; private set; }
    private string pinHash;
    private float balance;

    public BankAk(string pin)
    {
        if (!invalid(pin))
            throw new ArgumentException("Неправильный пин код");

        pinHash = YoPin(pin);
        AccountNumber = GenerateAccountNumber();
        balance = 0.0f;
    }

    public void Zamenitel(float amount)
    {
        
        if (amount <= 0)
        {
            string message = "Ошибка: сумма должна быть больше нуля.";
            throw new ArgumentException(message);
        }

        
        balance = UpdateBalance(balance, amount);
    }
    private float UpdateBalance(float currentBalance, float amountToAdd)
    {
        return currentBalance + amountToAdd;
    }
    public static bool invalid(string pin)
    {
        return pin.Length == 4 && int.TryParse(pin, out _);
    }

    private string YoPin(string pin)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
            return Convert.ToBase64String(bytes);
        }
    }

    private int GenerateAccountNumber()
    {
        Random random = new Random();
        return random.Next(000000, 999999);
    }


    public float Balance()
    {
        return balance;
    }
    private static void DovaitAc()
    {
        string pin = PromptForPin();

        try
        {
            CreateNewAccount(pin);
        }
        catch (Exception ex)
        {
            HandleAccountCreationError(ex);
        }
    }
    private static string PromptForPin()
    {
        Console.Write("Введите пин код: ");
        return Console.ReadLine();
    }
    private static void CreateNewAccount(string pin)
    {
        BankAk newAccount = new BankAk(pin);
        accounts.Add(newAccount);
        Console.WriteLine($"Новая учетная запись, созданная с помощью номера счета: {newAccount.AccountNumber}");
    }
    private static void HandleAccountCreationError(Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }


    private static void Pokazatel()
    {
        Console.WriteLine("Номера счетов:");
        foreach (var account in accounts)
        {
            Console.WriteLine(account.AccountNumber);
        }
    }

    private static void ZamenitelAccount()
    {
        Console.Write("Введите номер счета для пополнения: ");
        int accountNumber = int.Parse(Console.ReadLine());
        Console.Write("Введи число что бы добавить: ");
        float amount = float.Parse(Console.ReadLine());
        var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
        if (account != null)
        {
            account.Zamenitel(amount);
            Console.WriteLine($"Номер акаунта{accountNumber} пополнен {amount}. Новый счёт: {account.Balance()}");
        }
        else
        {
            Console.WriteLine("Акаунт не найден");
        }
    }

    private static void Pokazatelbalance()
    {
        Console.Write(": ");
        int accountNumber = int.Parse(Console.ReadLine());
        var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
        if (account != null)
        {
            Console.WriteLine($"Балланс {accountNumber}: {account.Balance()}");
        }
        else
        {
            Console.WriteLine("Аккаунт не найден.");
        }
    }
    private static List<BankAk> accounts = new List<BankAk>();

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Банк");
            Console.WriteLine("1. Добавить новую учетную запись");
            Console.WriteLine("2. Отобразить все учетные записи");
            Console.WriteLine("3. Пополнение счета");
            Console.WriteLine("4. Отображение баланса счета");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DovaitAc();
                    break;
                case "2":
                    Pokazatel();
                    break;
                case "3":
                    ZamenitelAccount();
                    break;
                case "4":
                    Pokazatelbalance();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Неправильный выбор");
                    break;
            }
        }
    }

}
