using Core.DataAccess;
using Entities.Entities;
using Entities.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFoodListDal : IEntityRepository<FoodList>
    {
       
    }

}
