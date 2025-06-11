using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Infrastructure.Services
{
    public class GoalService: IGoalService
    {
        private readonly IGoalRepository<IGoal> _goalRepository;
        private readonly IAccountRepository<IAccount> _accountRepository;

        public GoalService(IGoalRepository<IGoal> goalRepository, 
            IAccountRepository<IAccount> accountRepository)
        {
            _goalRepository = goalRepository;
            _accountRepository = accountRepository;
        }
                
        public async Task<ResultObject> CreateGoal(GoalCreationDto gDTO)
        {
            if(gDTO == null)
                return new ResultObject() { Error = "Goal creation data is null." };
            if (gDTO.Amount == 0)
                return new ResultObject() { Error = "Goal creation data has to have an amount to be recorded." };
            //Checks if the important information (accountDebited) is not null or empty
            if (String.IsNullOrEmpty(gDTO.AccDebitedID))
                return new ResultObject()
                {
                    Error = "You are missing an important piece of information. Please provide  the ID of the account" +
                    " debited-'AccDebitedID'."
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
        public Task<ResultObject> AssignTransationToGoal(AssignTransationToGoalDto assignTransationToGoalDto)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}

