using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Response;
using FlooringOrder.Model.Interface;

namespace FlooringOrder.BLL
{
    public class TaxManager
    {
        ITaxRepository _taxRepository;
        public TaxManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public DisplayTaxResponse DisplayTaxResponse(string state)
        {
            DisplayTaxResponse taxResponse = new DisplayTaxResponse();
            taxResponse.Tax = _taxRepository.DisplayTax(state);
            if(taxResponse.Tax is null)
            {
                taxResponse.Success = false;
                taxResponse.Message = $"{state} is an invalid state.";
            }
            else
            {
                taxResponse.Success = true;
            }
            return taxResponse;
        }
    }
}
