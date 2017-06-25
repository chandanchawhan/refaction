using refactor_me.Core.Domain.Models;
using System;
using System.Web.Http;

namespace refactor_me.Core.Domain.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork ;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_unitOfWork.Products.GetAll());
        }

        [Route]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            var product = _unitOfWork.Products.Find(p => String.Equals(p.Name, name) );

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
            var product = _unitOfWork.Products.Get(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Route]
        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if(product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();

            return  Created<Product>(Request.RequestUri + product.Id.ToString(), product);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.Id != id)
            {
                return BadRequest("Product ID in the request body: " + product.Id + " must match Product ID in the URL: " + id);
            }

            var existingProduct = _unitOfWork.Products.Get(id);

            if (existingProduct == null)
                return NotFound();
            
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.DeliveryPrice = product.DeliveryPrice;

            _unitOfWork.Complete();

            return Ok(product);

        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var existingProduct = _unitOfWork.Products.Get(id);

            if (existingProduct == null)
                return NotFound();

            _unitOfWork.Products.Remove(existingProduct);
            _unitOfWork.Complete();

            return Ok();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            var existingProduct = _unitOfWork.Products.Get(productId);

            if (existingProduct == null)
                return NotFound();

            return Ok(_unitOfWork.Products.GetProductOptionsOfProduct(existingProduct));
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            var existingProduct = _unitOfWork.Products.Get(productId);

            if (existingProduct == null)
                return NotFound();

            return Ok(_unitOfWork.Products.GetProductOptionsOfProductByProductOptionId(existingProduct, id));
        }

        [Route("{productId}/options")]
        [HttpPost]
        public IHttpActionResult CreateOption(Guid productId, ProductOption productOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = _unitOfWork.Products.Get(productId);

            if (existingProduct == null)
                return NotFound();

            if (productOption.ProductId != productId)
                return BadRequest("Product ID in the request body: " + productOption.ProductId + " must match Product ID in the URL: " + productId);

            if (productOption.Id == Guid.Empty)
                productOption.Id = Guid.NewGuid();

            var existingProductOption = _unitOfWork.Products.GetProductOptionsOfProductByProductOptionId(existingProduct, productOption.Id);

            if (existingProductOption != null)
                return BadRequest("Cannot create Product Option with ID : " + productOption.Id + " as ID already exists.");

            var newProductOption = new ProductOption()
                {
                    Id = productOption.Id,
                    Name = productOption.Name,
                    Description = productOption.Description,
                    ProductId = productId
                };

            _unitOfWork.ProductOptions.Add(newProductOption);
            _unitOfWork.Complete();

            return Ok(newProductOption);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateOption(Guid productId, Guid id, ProductOption productOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = _unitOfWork.Products.Get(productId);

            if (existingProduct == null)
                return NotFound();

            if (productOption.ProductId != productId)
                return BadRequest("Product ID in the request body: " + productOption.ProductId + " must Product match ID in the URL: " + productId);

            if (productOption.Id == Guid.Empty)
                return BadRequest("Product Option ID cannot be : " + productOption.Id + ". Must be supplied.");

            var existingProductOption = _unitOfWork.Products.GetProductOptionsOfProductByProductOptionId(existingProduct, productOption.Id);

            if (existingProductOption == null)
                return NotFound();

            existingProductOption.Name = productOption.Name;
            existingProductOption.Description = productOption.Description;

            _unitOfWork.Complete();
            return Ok(existingProductOption);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOption(Guid productId, Guid id)
        {
            var existingProduct = _unitOfWork.Products.Get(productId);

            if (existingProduct == null)
                return NotFound();

            var existingProductOption = _unitOfWork.Products.GetProductOptionsOfProductByProductOptionId(existingProduct, id);

            if (existingProductOption == null)
                return NotFound();

            _unitOfWork.ProductOptions.Remove(existingProductOption);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
