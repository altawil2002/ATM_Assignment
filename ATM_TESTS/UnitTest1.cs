using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
namespace ATM_TESTS
{
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
        public string email;
        public string CardNumber;
        public string PinCode;
        public double accountBalance;

        public BankAccount(Person x, string email, string cardNumber, string pinCode, double accountBalance)
        {
            base.setPerson(x);
            this.email = email;
            this.CardNumber = cardNumber;
            this.PinCode = pinCode;
            this.accountBalance = accountBalance;
        }
        public void setBalance(double NewBalance)
        {
            this.accountBalance = NewBalance;
        }
        public string getCardNumber()
        {
            return CardNumber;
        }
        public string getPinCode()
        {
            return PinCode;
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
        public int NumberOfCustomers = 0;
        BankAccount[] arrayFoAccount = new BankAccount[100];

        public Bank(int bankCapacity = 100)
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
               
                if (((i.getCardNumber() == cardNumber) && (i.getPinCode() == pinCode)))
                    return true;
            }
            return false;
        }

        public double CheckBalance(string cardNumber, string pinCode)
        {

            foreach (BankAccount i in arrayFoAccount)
            {

                if ((i.getCardNumber() == cardNumber) && (i.getPinCode() == pinCode))
                    return i.getBalance();
            }
            return 0;

        }
        public void Withdraw(BankAccount x, double withdrawAmount)
        {
            if (IsBankUser(x.getCardNumber(), x.getPinCode()))
            {


                if ( CheckBalance(x.getCardNumber(), x.getPinCode()) > 0)
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

                StreamReader streamReaderFile = new StreamReader("C:/Users/asus/Downloads/ATM_Assignment/bankAccounts.txt");

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

                StreamWriter streamWriterFile = new StreamWriter("C:/Users/asus/Downloads/ATM_Assignment/bankAccounts.txt");
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

    [TestClass]
    public class UnitTest1
    {
            [TestMethod]
            public void AddBankUser_AddNewUser_OneBankAccountCreated()
            {
                int bankCapacity = 10;
                Person p1 = new Person();

                string pinCode = "1234";
                string cardNumber = "12345789";
                int accountBalance = 100;
                BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


                Bank testBank = new Bank(bankCapacity);

                testBank.AddNewAccount(tmpAccount);

                Assert.AreEqual(testBank.NumberOfCustomers, 1);


            }


        [TestMethod]
        public void IsBankUser_ValidAccount_AccountFound()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            Assert.IsTrue(testBank.IsBankUser(cardNumber, pinCode));


        }


        [TestMethod]
        public void FindAccount_InValidAccount_AccountNotFound()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);


            string fakeCardNumber = "1222345789";
            testBank.AddNewAccount(tmpAccount);

            Assert.IsFalse(testBank.IsBankUser(fakeCardNumber, pinCode));


        }




        [TestMethod]
        public void CheckBalance_AskForBlance_GetBalanceAmount()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);



            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 100);


        }


        [TestMethod]
        public void Withdraw_WithVaildAmount_BalanceUpdated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            int withdrawAmount = 50;

            testBank.Withdraw(tmpAccount, withdrawAmount);

            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 50);


        }


        [TestMethod]
        public void Deposit_WithVaildAmount_BalanceUpdated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            int withdrawAmount = 50;

            testBank.Deposit(tmpAccount, withdrawAmount);

            Assert.AreEqual(testBank.CheckBalance(tmpAccount.CardNumber, tmpAccount.PinCode), 150);


        }



        [TestMethod]
        public void SaveBankAccounts_AddOneBankAccount_DataSavedWithAccountsOnFile()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "12345789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            testBank.Save();

            Bank newTestBank = new Bank();
            newTestBank.Load();

            Assert.AreEqual(testBank.NumberOfCustomers, 1);



        }
















    }
}
