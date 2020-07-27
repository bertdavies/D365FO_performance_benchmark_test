This is a Selnium based script used to benchmark, test and record D365FO performance against an instances own historical performance records.

The test by default runs 10 separate instances of the Selenium ChromeDriver to access the Bank Accounts module, list all of the account items and then return 
to the F&O dashboard.  The time it takes to execute all parallel tests from within the on-prem environment is recorded, along with the percentage of time-out 
errors; An ICMP test is also carried out to identify any speed or packet loss issues across the infrastructure before reaching the Microsoft managed environment.
