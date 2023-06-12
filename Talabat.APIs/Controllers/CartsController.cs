using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class CartsController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerCart>> GetCart(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return cart ?? new CustomerCart(id);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCartDto cart)
        {
            var mappedCart = _mapper.Map<CustomerCartDto, CustomerCart>(cart);
            var createdOrUpdatedCart = await _cartRepository.UpdateCartAsync(mappedCart);
            if (cart is null) return BadRequest(new ApiResponse(400));
            return createdOrUpdatedCart;
        }



        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCart(string id)
        {
            return await _cartRepository.DeleteCartAsync(id);
        }
    }
}
