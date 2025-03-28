namespace task5
{
    internal class Program
    {
        static void Main()
        {
            var accounts = new List<Account>
        {
            new Account(),
            new Account("Larry"),
            new Account("Moe", 2000),
            new Account("Curly", 5000)
        };

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            var savAccounts = new List<SavingsAccount>
        {
            new SavingsAccount(),
            new SavingsAccount("Superman"),
            new SavingsAccount("Batman", 2000),
            new SavingsAccount("Wonderwoman", 5000, 5.0)
        };

            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            var checAccounts = new List<CheckingAccount>
        {
            new CheckingAccount(),
            new CheckingAccount("Larry2"),
            new CheckingAccount("Moe2", 2000),
            new CheckingAccount("Curly2", 5000)
        };

            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);

            var trustAccounts = new List<TrustAccount>
        {
            new TrustAccount(),
            new TrustAccount("Superman2"),
            new TrustAccount("Batman2", 2000),
            new TrustAccount("Wonderwoman2", 5000, 5.0)
        };

            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Withdraw(trustAccounts, 2000);
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Default", double balance = 0)
        {
            Name = name;
            Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name}: {Balance:C}";
        }
    }

    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string name = "Default", double balance = 0, double interestRate = 3.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            return base.Deposit(amount + (amount * InterestRate / 100));
        }
    }

    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.5;

        public CheckingAccount(string name = "Default", double balance = 0)
            : base(name, balance) { }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + WithdrawalFee);
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int WithdrawalCount = 0;
        private const int MaxWithdrawals = 3;
        private const double BonusThreshold = 5000;
        private const double BonusAmount = 50;

        public TrustAccount(string name = "Default", double balance = 0, double interestRate = 3.0)
            : base(name, balance, interestRate) { }

        public override bool Deposit(double amount)
        {
            if (amount >= BonusThreshold)
                amount += BonusAmount;
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (WithdrawalCount >= MaxWithdrawals || amount > Balance * 0.2)
                return false;
            WithdrawalCount++;
            return base.Withdraw(amount);
        }
    }

    public static class AccountUtil
    {
        public static void Deposit<T>(List<T> accounts, double amount) where T : Account
        {
            Console.WriteLine("\n=== Depositing to Accounts ===");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed to deposit {amount} to {acc}");
            }
        }

        public static void Withdraw<T>(List<T> accounts, double amount) where T : Account
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ===");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed to withdraw {amount} from {acc}");
            }
        }
    }
}