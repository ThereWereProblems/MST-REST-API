using AutoMapper;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Exceptions;

namespace MST_REST_Web_API.Services
{
    public interface IProductService
    {
        void AddProduct(Product dto);
        void DeleteProduct(int id);
        void UpdateProduct(int id, Product dto);
        List<Product> GetAll();
    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ProductService(ApplicationDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public void AddProduct(Product dto)
        {

            var productExist = _context.Products.FirstOrDefault(x => x.Name == dto.Name);
            if (productExist != null)
                throw new ConflictException("Product with that name exists");
            if (dto.Price < 0)
                throw new BadRequestException("The price cannot be less than 0");

            var product = new Product
            {
                Price = dto.Price,
                Name = dto.Name,
                Description = dto.Description,
                PhotoURL = dto.PhotoURL
            };
            _context.Products.Add(product);

            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                throw new BadRequestException($"Product with id: {id} not exist");

            var result = _context.Products.Remove(product);

            _context.SaveChanges();
        }

        public void UpdateProduct(int id, Product dto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                throw new BadRequestException($"Product with id: {id} not exist");
            if (dto.Price < 0)
                throw new BadRequestException("The price cannot be less than 0");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.PhotoURL = dto.PhotoURL;

            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            var products = _context.Products.ToList();
            return products;
        }
    }
}
