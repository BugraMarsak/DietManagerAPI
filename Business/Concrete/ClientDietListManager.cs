using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ClientDietListManager : IClientDietListService
    {
        IClientDietListDal _clientDietList;

        public ClientDietListManager(IClientDietListDal clientDietList)
        {
            _clientDietList = clientDietList;
        }

        public IResult Add(ClientDietList clientDietList)
        {
            _clientDietList.Add(clientDietList);
            return new SuccessResult(Messages.added);
        }

        public IResult Delete(ClientDietList clientDietList)
        {
            _clientDietList.Delete(clientDietList);
            return new SuccessResult(Messages.deleted);
        }

        public IDataResult<List<ClientDietList>> GetAll()
        {
            return new SuccessDataResult<List<ClientDietList>>(_clientDietList.GetAll(), Messages.Listed);
            //return new SuccessDataResult<List<FoodList>>(context.FoodLists.ToList(), Messages.Listed);
        }

        public IDataResult<List<ClientDietList>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<ClientDietList>>(_clientDietList.GetAll(p => p.ClientId == userId));
        }

        public IDataResult<List<ClientDietList>> GetByUserIdAndDietianId(int userId, int dietianId)
        {
            return new SuccessDataResult<List<ClientDietList>>(_clientDietList.GetAll(p => p.ClientId == userId && p.DietitianId ==dietianId));
        }

        public IResult Update(ClientDietList clientDietList)
        {
            _clientDietList.Update(clientDietList);
            return new SuccessResult(Messages.changed);
        }

    }
}
