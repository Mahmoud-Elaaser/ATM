namespace ATM
{
    public class CardHolder
    {
        private string firstName;
        private string lastName;
        private int cardNumber;
        private double balance;

        public CardHolder(string firstName, string lastName, int cardNumber, double balance)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.cardNumber = cardNumber;
            this.balance = balance;
        }

        public string FirstName { get { return firstName; } } // accessed so, it return firstName
        public string LastName { get { return lastName; } } // not accessed so, no return
        public int CardNumber { get { return cardNumber; } } // accessed so, it return CardNumber
        public double Balance { get { return balance; } } // accessed so, it return balance

        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Deposit amount cannot be negative.");
            }

            balance += amount;
        }

        public bool Withdraw(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Withdrawal amount cannot be negative.");
            }

            if (amount > balance)
            {
                Console.WriteLine("You must enter value less than or equal balance");
                return false;
            }

            balance -= amount;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<CardHolder> cards = new List<CardHolder>();
            cards.Add(new CardHolder("Mahmoud", "Diab", 453277, 15000.0));
            cards.Add(new CardHolder("Ahmed", "Khairy", 224477, 12000.0));
            cards.Add(new CardHolder("Mohamed", "Esmail", 335577, 10000.0));
            cards.Add(new CardHolder("Ali", "Mohamed", 553377, 13000.0));
            cards.Add(new CardHolder("Youssef", "Samy", 654477, 18000.0));
            cards.Add(new CardHolder("Abdo", "Nasser", 751177, 20000.0));

            Console.WriteLine("Welcome to my simple ATM Project.");

            CardHolder currentUser;
            while (true)
            {
                try
                {
                    Console.Write("Enter your card number: ");
                    int debitCardnumber = int.Parse(Console.ReadLine());
                    currentUser = cards.FirstOrDefault(x => x.CardNumber == debitCardnumber);  // Linq Query

                    if (currentUser != null)  // card number exists ==> Selected by linq Query 
                    {
                        break;
                    }
                    else  // == null  ==> not found
                    {
                        Console.WriteLine("The card number you entered is not valid.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid card number format. Please enter a valid integer.");
                }
            }

            Console.WriteLine($"Welcome, {currentUser.FirstName}!");

            int option = 0;
            do
            {
                PrintOptions();

                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option. Enter 1, 2, 3, or 4.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Deposit(currentUser);
                        break;
                    case 2:
                        Withdraw(currentUser);
                        break;
                    case 3:
                        ShowBalance(currentUser);  
                        break;
                    case 4:
                        Console.WriteLine($"Thank you, {currentUser.FirstName}! Have a nice day.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Enter 1, 2, 3, or 4.");
                        break;
                }

            } while (option != 4);
        }

        static void PrintOptions()
        {
            Console.WriteLine("Choose one option from the following:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        static void Deposit(CardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to deposit?");
            double depositAmount;
            if (double.TryParse(Console.ReadLine(), out depositAmount)) //If the input is valid, it calls the Deposit method of the currentUser object with the parsed deposit amount.
            {
                currentUser.Deposit(depositAmount);
                Console.WriteLine($"Your new balance is: {currentUser.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount format. Please enter a valid number.");
            }
        }

        static void Withdraw(CardHolder currentUser)
        {
            Console.WriteLine("How much money would you like to withdraw?");
            double withdrawAmount;
            if (double.TryParse(Console.ReadLine(), out withdrawAmount))  // out ==> pass by reference
            {
                if (currentUser.Withdraw(withdrawAmount) == true)
                {
                    Console.WriteLine($"Your new balance is: {currentUser.Balance}");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount format. Please enter a valid number.");
            }
        }

        static void ShowBalance(CardHolder currentUser)
        {
            Console.WriteLine($"Your current balance is: {currentUser.Balance}");
        }
    }
}
