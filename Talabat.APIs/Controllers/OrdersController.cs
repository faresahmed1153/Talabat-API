﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,
           IMapper mapper
            )
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var shippingAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.CartId, orderDto.DeliveryMethodId, shippingAddress);

            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }



        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForUserAsync(email, id);

            if (order is null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));

        }

        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {


            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}

