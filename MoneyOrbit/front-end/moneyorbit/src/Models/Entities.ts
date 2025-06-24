export interface Goal {
  goalName: string;
  goalDescription: string;
  accDebitedID: string;
  relatedTransactionIDs: string[];
  amountAccomplished: number;
  id: string;
  date: Date;
  description: string;
  accCreditedID: string;
  amount: number;
}
export interface User {
  token: string;
  accessLevel: string;
}
