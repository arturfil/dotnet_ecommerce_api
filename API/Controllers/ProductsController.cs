using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using System.Linq;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{
  public class ProductsController : BaseApiController
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      this._unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    // Get/Products
    // [Cached(600)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
      var countSpec = new ProductWithFiltersForCountSpecification(productParams);
      var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
      var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
      return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    // Get/Products/:id
    // [Cached(600)]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      var product = await _unitOfWork.Repository<Product>().GetEntitiyWithSpec(spec);

      if (product == null) return NotFound(new ApiResponse(404));

      return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    // Get/ProductsBrands
    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _unitOfWork.Repository<ProductBrand>().ListAllAsync());
    }

    // Get/ProductsTypes
    [Cached(600)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
    {
      return Ok(await _unitOfWork.Repository<ProductType>().ListAllAsync());
    }

    // Post/Product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto productToCreate) {
      var product = _mapper.Map<ProductCreateDto, Product>(productToCreate);
      product.PictureUrl = "images/products/placeholder.png";
      _unitOfWork.Repository<Product>().Add(product);
      var result = await _unitOfWork.Complete();
      if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating product"));
      return Ok(product);
    }

    // Put/Product/:id
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, ProductCreateDto productToUpdate) {
      var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
      _mapper.Map(productToUpdate, product);
      _unitOfWork.Repository<Product>().Update(product);
      var result = await _unitOfWork.Complete();
      if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating product"));
      return Ok(product);
    }

    // Delete/Product/:id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id) {
      var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
      _unitOfWork.Repository<Product>().Delete(product);
      var result = await _unitOfWork.Complete();
      if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting product"));
      return Ok();
    }
  }
}