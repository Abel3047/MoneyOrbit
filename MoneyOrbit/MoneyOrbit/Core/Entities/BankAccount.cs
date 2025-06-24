using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    /// <summary>Represents a financial account in the system.
    /// This class implements the IEntity interface, indicating that it is an entity in the domain model.
    /// </summary>
    public class BankAccount: Account, IBankAccount
    {
        //My issue with saving the BankAccountName is that I don't want to have users traced back by their names, but I believe
        //that it won't really be an issue if we have OTP services working. And even if they get access to this information,
        //they won't have access to other acccounts without the authentication
        public string BankAccountName { get; set; }

        public string BankAccountNumber { get; set; }
        public string BankBranchCode { get; set; }
        public string BankBranchName { get; set; }
        public string? BankSwiftCode { get; set;}

        new public bool isAsset { get; private set; } = true;
        new public bool isExpense { get; private set; } = false;
        new public bool isCaptial { get; private set; } = false;
        new public bool isLiability { get; private set; } = false;
    }
}
