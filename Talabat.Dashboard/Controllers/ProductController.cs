using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Specifications;
using Talabat.Dashboard.Helpers;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
	[Authorize]

	public class ProductController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var spec = new ProductWithBrandAndTypeSpecification();
			var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
			var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products);
			return View(mappedProducts);
		}
		public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProductViewModel model)
		{

			if (ModelState.IsValid)
			{
				if (model.Image != null)
					model.PictureUrl = await PictureSettings.UploadFile(model.Image, "products");

				else
					model.PictureUrl = "images/products/default.png";

				var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
				await unitOfWork.Repository<Product>().Add(mappedProduct);
				await unitOfWork.Complete();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
			var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
			return View(mappedProduct);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, ProductViewModel model)
		{
			if (id != model.Id)
				return NotFound();
			if (ModelState.IsValid)
			{
				if (model.Image != null)
				{
					if (model.PictureUrl != null)
					{
						PictureSettings.DeleteFile(model.PictureUrl, "products");
						model.PictureUrl = await PictureSettings.UploadFile(model.Image, "products");
					}
					else
					{
						model.PictureUrl = await PictureSettings.UploadFile(model.Image, "products");

					}

				}
				var mappedProduct = mapper.Map<ProductViewModel, Product>(model);
				unitOfWork.Repository<Product>().Update(mappedProduct);
				var result = await unitOfWork.Complete();
				if (result > 0)
					return RedirectToAction("Index");
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
			var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
			return View(mappedProduct);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int id, ProductViewModel model)
		{
			if (id != model.Id)
				return NotFound();
			try
			{
				var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
				if (product.PictureUrl != null)
					PictureSettings.DeleteFile(product.PictureUrl, "products");

				unitOfWork.Repository<Product>().Delete(product);
				await unitOfWork.Complete();
				return RedirectToAction("Index");

			}
			catch (System.Exception)
			{

				return View(model);
			}
		}
	}
}
