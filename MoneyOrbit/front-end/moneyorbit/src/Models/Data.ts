export interface AccountFormData {
  accountName: string;
  description?: string;

  isAsset: boolean;
  isExpense: boolean;
  isCaptial: boolean;
  isLiability: boolean;

  // BankAccount creation properties
  // Accounts are typically not bank accounts so the default is false
  isBankAccount?: boolean;

  BankAccountName?: string;
  BankAccountNumber?: string;
  BankBranchCode?: string;
  BankBranchName?: string;
  BankSwiftCode?: string;
}
