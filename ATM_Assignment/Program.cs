using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATM_Assignment
{
    [Serializable]
    public class Person
    {
        string Fname;
        string Lname;

        public Person(string fname, string lname)
        {
            Fname = fname;
            Lname = lname;
        }
        public Person()
        {
            Fname = "";
            Lname = "";
        }
        public void setPerson(Person x)
        {
            Fname = x.Fname;
            Lname = x.Fname;
        }
        public string getFirstName()
        {
            return Fname;
        }
        public string getLastName()
        {
            return Lname;
        }
    }

    public class BankAccount : Person
    {
        string email;
        string cardNumber;
        string pinCode;
        double accountBalance;

        public BankAccount(Person x, string email, string cardNumber, string pinCode, double accountBalance)
        {
            base.setPerson(x);
            this.email = email;
            this.cardNumber = cardNumber;
            this.pinCode = pinCode;
            this.accountBalance = accountBalance;
        }
        public void setBalance(double NewBalance)
        {
            this.accountBalance = NewBalance;
        }
        public string getCardNumber()
        {
            return cardNumber;
        }
        public string getPinCode()
        {
            return pinCode;
        }
        public double getBalance()
        {
            return accountBalance;
        }

        public string getEmail()
        {
            return email;
        }


    }

    public class Bank
    {
        int bankCapacity;
        static int NumberOfCustomers = 0;
        static BankAccount[] arrayFoAccount = new BankAccount[100];

        public Bank(int bankCapacity)
        {
            this.bankCapacity = bankCapacity;

        }

        public int getNumberOfCustomers()
        {
            return NumberOfCustomers;
        }

        public BankAccount[] getArrayAccounts()
        {
            return arrayFoAccount;
        }
        public void AddNewAccount(BankAccount x)
        {

            arrayFoAccount[NumberOfCustomers++] = x;

        }
        public bool IsBankUser(string cardNumber, string pinCode)
        {
            foreach (BankAccount i in arrayFoAccount)
            {
                if ((i.getCardNumber() == cardNumber) && (i.getPinCode() == pinCode))
                    return true;
            }
            return false;
        }

        public bool CheckBalance(BankAccount x, double exampleAmount)
        {
            if (IsBankUser(x.getCardNumber(), x.getPinCode()))
                if (x.getBalance() >= exampleAmount)
                {
                    return true;
                }
            return false;

        }
        public void Withdraw(BankAccount x, double withdrawAmount)
        {
            if (IsBankUser(x.getCardNumber(), x.getPinCode()))
            {


                if (CheckBalance(x, withdrawAmount))
                {
                    x.setBalance((x.getBalance() - withdrawAmount));
                }

            }

        }
        public void Deposit(BankAccount x, double depositAmount)
        {
            if (depositAmount > 0)
            {
                if (IsBankUser(x.getCardNumber(), x.getPinCode()))
                    x.setBalance((x.getBalance() + depositAmount));
            }
        }

        public void Load()
        {
            String line;
            try
            {

                StreamReader streamReaderFile = new StreamReader("C:/Users/asus/source/repos/ATM/bankAccounts.txt");

                line = streamReaderFile.ReadLine();
                Bank bank = new Bank(100);

                while (line != null)
                {

                    // each line like this format  =>    firstname,lastname,email,cardnumber,pincode,balance
                    // i will split with "," to ge a parts of string
                    String[] accountParts = line.Split(',');
                    // Console.WriteLine(accountParts[0] + " have " + accountParts[5] + " JOD");
                    Person newPerson = new Person(accountParts[0], accountParts[1]);
                    BankAccount newBanckAcc = new BankAccount(newPerson, accountParts[2], accountParts[3], accountParts[4], Convert.ToDouble(accountParts[5]));

                    bank.AddNewAccount(newBanckAcc);

                    line = streamReaderFile.ReadLine();
                }

                streamReaderFile.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block");
            }
        }

        public void Save()
        {

            try
            {

                StreamWriter streamWriterFile = new StreamWriter("C:/Users/asus/source/repos/ATM/bankAccounts.txt");
                Bank bank = new Bank(100);
                BankAccount[] arrayOfAccounts = bank.getArrayAccounts();
                foreach (BankAccount banckAccount in arrayOfAccounts)
                {
                    String newLine =
                        banckAccount.getFirstName() + ","
                        + banckAccount.getLastName() + ","
                        + banckAccount.getEmail() + ","
                        + banckAccount.getCardNumber() + ","
                        + banckAccount.getPinCode() + ","
                        + banckAccount.getBalance();
                    streamWriterFile.WriteLine(newLine);
                }



                streamWriterFile.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
