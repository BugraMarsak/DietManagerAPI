using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FoodListManager : IFoodListService
    {
        IFoodListDal _foodListDal;

        public FoodListManager(IFoodListDal foodListDal)
        {
            _foodListDal = foodListDal;
        }

        public IDataResult<List<FoodList>> GetAll()
        {
            return new SuccessDataResult<List<FoodList>>(_foodListDal.GetAll(), Messages.Listed);
            //return new SuccessDataResult<List<FoodList>>(context.FoodLists.ToList(), Messages.Listed);
        }

        public IDataResult<List<FoodList>> GetAllWithoutAllergens(int ClientId)
        {
            List<FoodList> foodLists;
            ClientAllergies ClientAllergies;
            string[] temp;
            using (DietManagerContext context = new DietManagerContext())
            {
                ClientAllergies = context.ClientAllergies.FirstOrDefault(c => c.ClientId == ClientId);
                foodLists = context.FoodList.ToList();
                if (ClientAllergies != null)
                {
                    temp = ClientAllergies.AllergiesList.Split(",");
                    foreach (var item in temp)
                    {
                        foodLists = foodLists.Where(f => !(f.AllergenArray.Any(a => (a == item)))).ToList();
                    }
                }
                

            }
            return new SuccessDataResult<List<FoodList>>(foodLists, Messages.Listed);
        }

        public IDataResult<FoodList> GetById(int id)
        {
            return new SuccessDataResult<FoodList>(_foodListDal.Get(p => p.Id == id));

        }
        private void getAllergen()
        {
           
        }
    }
}
