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
