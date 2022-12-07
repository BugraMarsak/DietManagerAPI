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
    public interface IClientDietListService
    {
        IDataResult<List<ClientDietList>> GetAll();
        IResult Add(ClientDietList clientDietList);
        IResult Update(ClientDietList clientDietList);
        IResult Delete(ClientDietList clientDietList);
        IDataResult<List<ClientDietList>> GetByUserId(int userId);
        IDataResult<List<ClientDietList>> GetByUserIdAndDietianId(int userId,int dietianId);
    }
}
