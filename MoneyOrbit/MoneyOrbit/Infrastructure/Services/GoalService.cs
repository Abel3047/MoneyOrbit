using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;
using System.Collections.ObjectModel;

namespace MoneyOrbit.Infrastructure.Services
{
    public class GoalService: IGoalService
    {
        private readonly IGoalRepository<IGoal> _goalRepository;
        private readonly IAccountRepository<IAccount> _accountRepository;
        private readonly ITransactionRepository<ITransaction> _transactionRepository;
        private readonly IUserRepository<IUser> _userRepository;

        public GoalService(IGoalRepository<IGoal> goalRepository, 
            IAccountRepository<IAccount> accountRepository,
            ITransactionRepository<ITransaction> transactionRepository,
            IUserRepository<IUser> userRepository)
        {
            _goalRepository = goalRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }
                
        public async Task<ResultObject> CreateGoal(GoalCreationDto gDTO)
        {
            if(gDTO == null|| NullGuard.IsNull(gDTO))
                return new ResultObject() { Error = "Goal creation data is null." };
            if (gDTO.Amount == 0)
                return new ResultObject() { Error = "Goal creation data has to have an amount to be recorded." };
            //Checks if the important information (accountDebited) is not null or empty
            if (String.IsNullOrEmpty(gDTO.AccDebitedID)|| NullGuard.IsNull(gDTO.Date))
                return new ResultObject()
                {
                    Error = "You are missing an important piece of information. Please provide  the ID of the account" +
                    " debited-'AccDebitedID'/Date."
                };
            //Checks if the account exists in the database
            if (!await DoesAccountExist(gDTO.AccDebitedID))
                return new ResultObject() { Error = "The debited account for the goal does not exist in the database," +
                    " and is a requisite parameter" };

            //Creates a goal object from the DTO
            var goal = new GoalFactory()
                .CreateGoal(gDTO.GoalName,gDTO.GoalDescription,gDTO.AccDebitedID,gDTO.Amount, gDTO.Date);
            //Records the goal in the database
            await _goalRepository.UpdateData(goal.ID,goal);

            return new ResultObject() { Result = "success" };
        }
        public async Task<ResultObject> UpdateGoal(GoalUpdateDto guDto)
        {
            //Checks if the goal update data is null
            if (guDto == null)
                return new ResultObject() { Error = "Goal creation data is null." };
            if(String.IsNullOrEmpty(guDto.ID))
                return new ResultObject() { Error = "Goal ID is required to update a goal." };
            
            var goal = await _goalRepository.GetInstanceOfType<Goal>(guDto.ID);
            //Checks if the goal exists in the database
            if (NullGuard.IsNull(goal))
                return new ResultObject() { Error = "The goal does not exist in the database, and is a requisite parameter" };
            
            //Sets the goal properties from the DTO
            goal.Date = guDto.Date ?? goal.Date;
            goal.GoalName = guDto.GoalName ?? goal.GoalName;
            goal.GoalDescription = guDto.GoalDescription ?? goal.GoalDescription;
            //If the amount is not set, it will get the amount from the goal in the database
            if (guDto.Amount <= 0) guDto.Amount = goal.Amount;
            else goal.Amount = guDto.Amount;

            await _goalRepository.UpdateData(goal.ID, goal);

            return new ResultObject() { Result = "success" };

        }
        public async Task<ResultObject> AssignTransationToGoal(AssignTransationToGoalDto aTGDto)
        {
            //check if the transactionID or goalID is null or empty
            if (String.IsNullOrEmpty(aTGDto.TransactionID) || String.IsNullOrEmpty(aTGDto.GoalID))
                return new ResultObject() { Error = "Transaction ID/ Goal ID  is required to assign a the transaction to a goal." };

            //checks if the transaction exists in the database
            var transaction = await _transactionRepository.GetInstanceOfType<Transaction>(aTGDto.TransactionID);
            if (NullGuard.IsNull(transaction))
                return new ResultObject() { Error = "The transaction does not exist in the database, and is a requisite parameter" };
            //checks if the goal exists in the database
            Goal goal = await _goalRepository.GetInstanceOfType<Goal>(aTGDto.GoalID);
            if (NullGuard.IsNull(goal))
                return new ResultObject() { Error = "The goal does not exist in the database, and is a requisite parameter" };

            //Checks if the transaction relates to the goal
            if (!goal.RelatesToTransaction<Goal>(transaction))
                return new ResultObject() { Error = "This transaction doesn't relate to the goal provided" };
            //Checks if the transaction is already assigned to the goal
            if (goal.RelatedTransactionIDs.Any(t => t == aTGDto.TransactionID))
                return new ResultObject() { Error = "The transaction is already assigned to the goal." };

            //If the transaction relates to the goal, it will be assigned to the goal and stored in the database
            await setTransactionIntoGoal(transaction, goal);

            return new ResultObject() { Result = "success" };
        }

        public async Task<ResultObject> GetGoal(GetGoalDto getGoalDto)
        {
            var goal = await GetGoalfromID(getGoalDto.GoalID);
            //Returns the amount accomplished by the goal
            return new ResultObject() { Result = goal };
        }
        public async Task<ResultObject> GetGoalAmountAccomplished(GoalAmountAccomplishedDto gAADto)
        {
            var goal = await GetGoalfromID(gAADto.GoalID);
            //Returns the amount accomplished by the goal
            return new ResultObject() { Result = goal.AmountAccomplished };
        }
        public async Task<ResultObject> GetGoalsForUser(GetGoalsForUserDto gGFUDto)
        {
            //Checks if the Token is null or empty, which is a required parameter to get goals
            if (String.IsNullOrEmpty(gGFUDto.Token)) return new ResultObject() { Error = "User Token is required to get Goals." };

            //Gets the user from the Token
            var user = await _userRepository.GetInstanceOfType<User>(gGFUDto.Token);
            //Checks if the user exists in the database
            if (NullGuard.IsNull(user)) return new ResultObject() { Error = $"User was not found with the Token {gGFUDto.Token}." };

            //Gets the goals from the acountIDs from the user
            List<Goal> goals = new List<Goal>();
            foreach (var accID in user.AccountIDs)
            {
                var gs = await _goalRepository.GetCollectionWithIdenticalProperty<Goal>(accID);
                if (gs != null && gs.Any())
                {
                    goals.AddRange(gs);
                }
            }

            #region Filtering options
            // Checks if the start date and end date are both set, and filters the goals by date range
            if (gGFUDto.StartDate.HasValue && gGFUDto.EndDate.HasValue && gGFUDto.StartDate <= gGFUDto.EndDate)
            {
                goals = goals
                    .Where(t => t.Date >= gGFUDto.StartDate.Value && t.Date <= gGFUDto.EndDate.Value).ToList();
            }
            else if (gGFUDto.StartDate.HasValue)
            {
                goals = goals
                    .Where(t => t.Date >= gGFUDto.StartDate.Value).ToList();
            }
            else if (gGFUDto.EndDate.HasValue)
            {
                goals = goals
                    .Where(t => t.Date <= gGFUDto.EndDate.Value).ToList();
            }           

            #endregion

            if (NullGuard.IsNull(goals) || !goals.Any())
                return new ResultObject()
                {
                    Error = "No goals were found for the user. Either adjust your filtering options " +
                    "or give a different user"
                };

            return new ResultObject()
            {
                Result = new Collection<Goal>(goals)
            };

        }
        #region Support methods
        /// <summary>
        /// Checks if the account already exists in the database by simply running the typical path and if account!=null it will 
        /// return true
        /// </summary>
        /// <param name="accID"></param>
        /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesAccountExist(string accID)
        {
            var account = await _accountRepository.GetInstanceOfType<Account>(accID);
            return account != null;
        }
        private async Task setTransactionIntoGoal(Transaction transaction, Goal goal)
        {
            //Gives the goal the transactionID that is being assigned to it
            goal.RelatedTransactionIDs = goal.RelatedTransactionIDs.Append(transaction.ID).ToArray();
            //Updates the amount accomplished by the goal
            goal.AddToAmountAccomplished(transaction.Amount);

            //Updates the goal in the database
            await _goalRepository.UpdateData(goal.ID, goal);
        }
        private async Task<Goal> GetGoalfromID(string goalID)
        {
            //check if the goalID is null or empty
            if (String.IsNullOrEmpty(goalID))
                throw new NullReferenceException("Goal ID is required to get the amount accomplished.");
            //checks if the goal exists in the database
            var goal = await _goalRepository.GetInstanceOfType<Goal>(goalID);
            if (NullGuard.IsNull(goal))
                throw new NullReferenceException("The goal does not exist in the database, and is a requisite parameter");
            return goal;
        }

        #endregion
    }
}

