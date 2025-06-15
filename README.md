This is a Web Solution that presents a financial advisor to Motswana nation wide.

It is not limited to who it is intended for, and if one may so chose to use the source code, by cloning or forking or using any part of the repository, they are agreeing to the terms this solution was developed/ maintained by and is found in the following link: https://creativecommons.org/licenses/by/4.0/. 

The solution works by reading the transactions and goals set by the user's activity and intentional submissions, and feed a prompt to an Ai model.
The Ai model will then give suggestions on how best to achieve the goals and preferences a user sets.

This solution also exposes a user of the application to a global portfolio that they can opt-in to. We call it 'Humanity', or 'Humanity'fund.
This portfolio will be managed by the banks that opt-in to providing their services to maintaining the portfolio.
When a user links their bank accounts to through our app, they are essentially making a request to the bank to become their broker into this portfolio.
If the users banks accepts the request, the bank can then section off part of the users funds, to be managed by the bank explicitly for the 'Humanity'fund.
All returns and capital gains are fed back into 'Humanity' and a user can make claims for social security on that fund.

To have all transactions unchanging and fully transparent, the financial transactions the bank takes, using the 'Humanity' capital invested, will have to make records in a blockchain. This is so there is complete accountability and transparency between all banks worldwide. This also serves to protect the rights to claim for anyone who has a bank account, but has had their data lost by the site running a clone of our software. Only registered banks can have access to this portfolio, (note this excluded insurance companies and brokers).This is because banks are uniquely positioned to be the right balance of accountable, liquid and independant and monitored.

To have this application set up, you first need to:
1. clone/fork this repo.
2. create a launchSettings.json file in the MoneyOrbit/MoneyOrbit/Properties directory. (Create the Properties folder if it doesn't exist)
3. Populate the launchSettings.json with environment variables that reference a database that will remain in persistence.
4. The default database is Firebase, but if you so chose to use a different one, you have to have the implementation of the databaseclient inherit from IDataService. This new Dataservice would then have to be recognized by the API by...
5. adding the service in the MoneyOrbit/Application/Extensions/ApplicationServiceExtension.cs. This would be as a scoped service, not as a singleton. Though if your implementation requires it, please bare in mind the connetations.

Once again, we appreciate your consideration joining our community and vision, to live in a world where basic needs are always cared for. Please comment, query and raise tickets with respect and positivity at all times, for a better future for all.
From Team Gama.....

MoneyOrbit

Terrence Titus
Abel Tshimbalanga
Tamtonkhe Nkambule
Nderitu Ndungo
Katlego Cathy Makiwa

