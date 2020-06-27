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
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(
      IGenericRepository<Product> productsRepo,
      IGenericRepository<ProductBrand> productBrandRepo,
      IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
    {
      _productsRepo = productsRepo;
      _productBrandRepo = productBrandRepo;
      _productTypeRepo = productTypeRepo;
      _mapper = mapper;
    }

    // Get/Products
    [Cached(600)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
      var countSpec = new ProductWithFiltersForCountSpecification(productParams);
      var totalItems = await _productsRepo.CountAsync(countSpec);
      var products = await _productsRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
      return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data  ));
    }

    // Get/Products/:id
    [Cached(600)]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      var product = await _productsRepo.GetEntitiyWithSpec(spec);
      
      if (product == null) return NotFound(new ApiResponse(404));
      
      return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    // Get/ProductsBrands
    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _productBrandRepo.ListAllAsync());
    }

    // Get/ProductsTypes
    [Cached(600)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
    {
      return Ok(await _productTypeRepo.ListAllAsync());
    }
  }
}