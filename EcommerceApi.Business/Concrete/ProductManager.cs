using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.ProductDTOs;
using Microsoft.Extensions.Logging;


////Seri loglama arasdir

namespace EcommerceApi.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductManager> _logger;
        public ProductManager(IProductDAL productDAL, IMapper mapper, ILogger<ProductManager> logger)
        {
            _productDAL = productDAL;
            _mapper = mapper;
            _logger = logger;
        }
        //Verilmiş məhsul ID-sinə sahib məhsulun detallarını əldə edir.
        //"productId">Silinecek ürünün ID'si.
        //Silme işleminin neticesini gosteren bir IResult.

        public IDataResult<ProductDetailDTO> GetProductDetail(int productId)
        {
            try
            {
                if (productId <= 0)
                    throw new ArgumentException("Invalid productId. productId must be greater than 0.");

                var product = _productDAL.GetProduct(productId);

                // Silinecek product bulunamadı.
                if (product == null)
                    throw new ArgumentNullException(nameof(productId), $"Product with ID {productId} not found.");

                var map = _mapper.Map<ProductDetailDTO>(product);
                map.CategoryName = product.Category?.CategoryName;

                _logger.LogInformation($"Product detail retrieved successfully. ProductId: {productId}");

                return new SuccessDataResult<ProductDetailDTO>(map, "Product detail retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving product detail: {ex.Message}");
                return new ErrorDataResult<ProductDetailDTO>($"An error occurred while retrieving product detail: {ex.Message}");
            }
        }


        public IResult ProductCreate(ProductCreateDTO productCreateDTO)
        {
            try
            {
                if (productCreateDTO == null)
                    throw new ArgumentNullException(nameof(productCreateDTO), "ProductCreateDTO cannot be null");

                var map = _mapper.Map<Product>(productCreateDTO);
                map.CreatedDate = DateTime.Now;
                _productDAL.Add(map);

                _logger.LogInformation($"Product Added. ProductId: {map.Id}, ProductName: {map.ProductName}");

                return new SuccessResult("Product Added!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding the product: {ex.Message}");
                return new ErrorResult($"An error occurred while adding the product: {ex.Message}");
            }
        }

        public IResult ProductDelete(int productId)
        {
            try
            {
                //Bazaya girib silinecek mehsulun Id yoxlayir
                var result = _productDAL.Get(x => x.Id == productId);

                // Silinecek product tapilmadi.
                if (result == null)
                {
                    _logger.LogWarning($"Attempt to delete non-existing product with ID: {productId}");
                    return new ErrorResult($"Product with ID {productId} not found.");
                }

                _productDAL.Delete(result);
                _logger.LogInformation($"Product Deleted. ProductId: {productId}");
                return new SuccessResult("Product Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the product: {ex.Message}");
                return new ErrorResult("An error occurred while deleting the product. Please try again later or contact support.");
            }
        }

        //Öne çıxan ürünlerin bir listesini getirir
        //Öne çıkan ürünlerin listesini içeren bir IDataResult
        public IDataResult<List<ProductFeaturedDTO>> GetProductFeaturedList()
        {
            try
            {
                var products = _productDAL.GetFeaturedProducts();

                // Featured products not found
                if (products == null || !products.Any())
                {
                    _logger.LogInformation("No featured products found.");
                    return new SuccessDataResult<List<ProductFeaturedDTO>>(new List<ProductFeaturedDTO>(), "No featured products found.");
                }

                var map = _mapper.Map<List<ProductFeaturedDTO>>(products);
                _logger.LogInformation("Featured product list retrieved successfully.");
                return new SuccessDataResult<List<ProductFeaturedDTO>>(map, "Featured product list retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving featured product list: {ex.Message}");
                return new ErrorDataResult<List<ProductFeaturedDTO>>(ex.Message);
            }
        }

        //Belirtilen ürün ID'sine sahip ürünü update eder.
        //Update olunacaq ürünün melumatlarini içeren DTO.
        //Güncelleme işleminin sonucunu belirten bir IResult.
        public IResult ProductUpdate(ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                if (productUpdateDTO == null)
                    throw new ArgumentNullException(nameof(productUpdateDTO), "ProductUpdateDTO cannot be null");

                var product = _productDAL.Get(x => x.Id == productUpdateDTO.Id);

                if (product == null)
                {
                    _logger.LogWarning($"Attempt to update non-existing product with ID: {productUpdateDTO.Id}");
                    return new ErrorResult($"Product with ID {productUpdateDTO.Id} not found.");
                }

                var map = _mapper.Map<Product>(productUpdateDTO);

                product.Status = map.Status;
                product.ProductName = map.ProductName;
                product.Price = map.Price;
                product.Description = map.Description;
                product.Quantity = map.Quantity;
                product.CategoryId = map.CategoryId;
                product.IsFeatured = map.IsFeatured;
                product.Discount = map.Discount;
                product.PhotoUrl = map.PhotoUrl;

                _productDAL.Update(product);
                _logger.LogInformation($"Product Updated. ProductId: {productUpdateDTO.Id}");
                return new SuccessResult("Product Updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating the product: {ex.Message}");
                return new ErrorResult($"An error occurred while adding the product: {ex.Message}");
            }
        }

        //Bu metodun əsas məqsədi, ən son əlavə olunmuş məhsulları verilənlər bazasından əldə etmək
        //və istifadəçiyə və ya sistem tərəfindən nəzərdə tutulan məqsədlər üçün təqdim etməkdir.
        public IDataResult<List<ProductRecentDTO>> GetProductRecentList()
        {
            try
            {
                // Verilənlər bazasından ən son əlavə edilmiş məhsulları əldə et
                var products = _productDAL.GetRecentProducts();

                // Əgər məhsullar null-dırsa, səhv fırlat
                if (products == null)
                {
                    _logger.LogWarning("Recent products not found.");
                    return new ErrorDataResult<List<ProductRecentDTO>>(new List<ProductRecentDTO>(), "Recent products not found.");
                }

                // Məhsulları DTO-ya çevir
                var map = _mapper.Map<List<ProductRecentDTO>>(products);

                _logger.LogInformation("Recent products retrieved successfully.");

                // Uğurlu nəticəni qaytar
                return new SuccessDataResult<List<ProductRecentDTO>>(map, "Recent products retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Səhv halında, səhv mesajı ilə bir ErrorDataResult qaytar
                _logger.LogError($"An error occurred while retrieving recent products: {ex.Message}");
                return new ErrorDataResult<List<ProductRecentDTO>>($"An error occurred while retrieving recent products: {ex.Message}");
            }
        }

        public IDataResult<List<ProductFilterDTO>> ProductFilterList(int categoryId, int minPrice, int maxPrice)
        {
            try
            {
                // Geçersiz kategori ID yoxlamasi
                if (categoryId <= 0)
                {
                    _logger.LogWarning("Invalid category ID.");
                    return new ErrorDataResult<List<ProductFilterDTO>>("Invalid category ID.");
                }

                // Min ve max qiymet kontrolü, -10 falan yazarsa error vmesaji verecek
                if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                {
                    _logger.LogWarning("Invalid price range.");
                    return new ErrorDataResult<List<ProductFilterDTO>>("Invalid price range.");
                }

                // Filtreleme kodu
                var products = _productDAL.GetAll(x => x.CategoryId == categoryId && x.Price >= minPrice && x.Price <= maxPrice).ToList();

                // DTO'ya dönüştürme kodu
                var filteredProducts = _mapper.Map<List<ProductFilterDTO>>(products);

                _logger.LogInformation("Filter products retrieved successfully.");

                // Ugurlu olarsa netice  döndürme
                return new SuccessDataResult<List<ProductFilterDTO>>(filteredProducts);
            }
            catch (Exception ex)
            {
                // Error olarsa netice olaraq bunu gonderme
                _logger.LogError($"An error occurred while retrieving filter products: {ex.Message}");
                return new ErrorDataResult<List<ProductFilterDTO>>($"An error occurred while retrieving filter products: {ex.Message}");
            }
        }

        public IDataResult<bool> CheckProductCount(List<int> productIds)
        {
            try
            {
                // Geçersiz bir məhsul ID-si var mı yoxlayın
                if (productIds == null || productIds.Any(id => id <= 0))
                {
                    _logger.LogWarning("Invalid product IDs.");
                    return new ErrorDataResult<bool>("Invalid product IDs.");
                }

                // Məhsulları götürün və stok sayı sıfır olanları yoxlayın
                var productsWithZeroStock = _productDAL.GetAll(x => productIds.Contains(x.Id) && x.Quantity == 0);

                // Stok sayı sıfır olan məhsullar var mı yoxlayın
                if (productsWithZeroStock.Any())
                {
                    _logger.LogWarning("Some products have zero stock.");
                    return new ErrorDataResult<bool>("Some products have zero stock.");
                }

                // Hər hansi bir stok sayı sıfır olmayan məhsul varsa true, yoxsa false qaytarın
                _logger.LogInformation("Product count check successful.");
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception ex)
            {
                // Xəta halında uyğun şəkildə əməliyyat aparmaq
                _logger.LogError($"An error occurred while checking product count: {ex.Message}");
                return new ErrorDataResult<bool>($"An error occurred while checking product count: {ex.Message}");
            }
        }

        public IResult RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs)
        {
            try
            {
                _productDAL.RemoveProductCount(productDecrementQuantityDTOs);
                _logger.LogInformation("Product count removed successfully.");
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // Genel bir hata durumunda uygun şekilde işlem yapma
                _logger.LogError($"An error occurred while removing product count: {ex.Message}");
                return new ErrorResult($"An error occurred while removing product count: {ex.Message}");
            }
        }


    }
}
