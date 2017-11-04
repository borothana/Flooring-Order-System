using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrder.Model.Response;
using FlooringOrder.Model.Interface;

namespace FlooringOrder.BLL
{
    public class ProductManager
    {
        IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public DisplayProductResponse DisplayProductResponse(string productType)
        {
            DisplayProductResponse productResponse = new DisplayProductResponse();
            productResponse.Products = _productRepository.DisplayProduct(productType);
            if (productResponse.Products is null)
            {
                productResponse.Success = false;
                productResponse.Message = $"{productType} is an invalid product type.";
            }
            else
            {
                productResponse.Success = true;
            }
            return productResponse;
        }

    }
}
