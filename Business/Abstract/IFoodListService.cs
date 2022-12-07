using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFoodListService
    {
        IDataResult<List<FoodList>> GetAll();
        IDataResult<List<FoodList>> GetAllWithoutAllergens(int ClientId);
        IDataResult<FoodList> GetById(int id);
    }
}
