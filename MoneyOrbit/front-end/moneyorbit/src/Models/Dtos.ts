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
  token: string;

  accountName: string;
  description?: string;

  isAsset: boolean;
  isExpense: boolean;
  isCaptial: boolean;
  isLiability: boolean;

  // BankAccount creation properties
  // Accounts are typically not bank accounts so the default is false
  isBankAccount?: boolean;

  bankAccountName?: string;
  bankAccountNumber?: string;
  bankBranchCode?: string;
  bankBranchName?: string;
  bankSwiftCode?: string;
}

export interface CreateGoalsDto {
    token: string;
    accIDs?: string[];
    startDate?: Date;
    endDate?: Date;
    suspenseTransactions?: boolean;
}
