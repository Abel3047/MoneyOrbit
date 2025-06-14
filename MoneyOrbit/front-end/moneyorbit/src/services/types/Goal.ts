export interface Goal {
    goalName: string;
    goalDescription: string;
    accDebitedID: string;
    relatedTransactionIDs: [];
    amountAccomplished: number;
    accCreditedID: string;
    id: string;
    date: string;
    description: string;
    amount: number;
  }

export interface ApiResponse<T> {
    result: T;
    error: string | null;
}