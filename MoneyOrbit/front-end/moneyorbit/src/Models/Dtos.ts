export interface GetGoalsForUserDto {
  Token: string;
  StartDate?: Date;
  EndDate?: Date;
}

export interface GoalAmountAccomplishedDto {
  GoalID: string;
}
export interface GetGoalDto {
  GoalID: string;
}

export interface GoalUpdateDto {
  ID: string;
  Date?: Date;
  GoalName?: string;
  GoalDescription?: string;
  Amount: number;
}

export interface GoalCreationDto {
  Date: Date;
  GoalName: string;
  GoalDescription: string;
  AccDebitedID: string;
  Amount: number;
}
export interface LoginDto {
  UserName: string;
  Password: string;
}

export interface CreateAccountDto {
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
export interface BaseRegisterBankAccountDto {
  AccountName: string;
  description?: string;

  BankAccountName: string;
  BankAccountNumber: string;
  BankBranchCode: string;
  BankBranchName: string;
  BankSwiftCode?: string;
}

export interface CreateGoalsDto {
  date: string;
  goalName: string;
  goalDescription: string;
  accDebitedID: string;
  amount: number;
}
export interface UserCreationDto {
  UserName: string;
  password: string;
  FirstName: string;
  LastName: string;
  AccessLevel: string;
  Email: string;
  PhoneNumber: string;
} 