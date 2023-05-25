# MS-Unit Testing

This is Labb 5 in the Course Avancerad .NET at Campus Varberg in Sweden.
I will reuse an previous project called Team Lemon. It was an bank application that we made as our first group project.

## Places of Eventual Errors
I will look at the code that we have written and try to find some functions that are critical for the program to work perfectly.

### Point of Error : 1
I will do a MSUnit test for the function "InternalTransfer" inside of "AccountManagement.cs" to ensure that all transfers within the same user will go smooth and not fail.

Here is a list of things that I think can go wrong that I will test.
- [ ] MonitorAccounts so that we get a return value larger than 1 meaning that we have at least 2 accounts to transfer money between.
- [ ] Validate what account will transfer the money and to what account, aka you cant choose to transfer from account 7 to account 2 if you only have 3 accounts and so on.
- [ ] Ensure that you have enough money to transfer
- [ ] Ensure that you cant choose account or transfer amount other than an integer, aka not be able to write (" "), ("Zebra"), ("B4nana5) or similar.
- [ ] Validate that the correct amount will be transfered if there are different currencies.

### Point of Error : 2 
For the second MSUnit test I will test "MakeExternalTransfer" inside of "AccountManagement.cs" to ensure that all transfers to other accounts will go smooth and not fail.
