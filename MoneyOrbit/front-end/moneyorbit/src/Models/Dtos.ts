import { AccountFormData } from "./Data";

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

export interface CreateAccountDto extends AccountFormData{
  token: string; // Important: So as to register an account to a user, the token is required  
}
export interface BaseRegisterBankAccountDto {
  //Important: So as to register an account to a user, the token is required
  token: string;
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