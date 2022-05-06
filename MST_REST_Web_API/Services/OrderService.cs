using AutoMapper;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Exceptions;
using MST_REST_Web_API.Models.DTO;

namespace MST_REST_Web_API.Services
{
    public interface IOrderService
    {
        int Create(CreateOrderDto dto);
        List<Order> GetAll();

    }
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public OrderService(ApplicationDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public int Create(CreateOrderDto dto)
        {
            var address = new Address
            {
                Street = dto.Street,
                PostalCode = dto.PostalCode,
                Town = dto.Town,
                HouseNumber = dto.HouseNumber
            };
            _context.Addresses.Add(address);
            _context.SaveChanges();

            var products = _context.Products.Where(x => dto.ProductsIds.Contains(x.Id));

            if (products == null)
                throw new BadRequestException("List of products is empty");

            var order = new Order
            {
                AddressId = address.Id,
                Date = DateTime.Now,
                TotalCost = 0,
                Products = new List<Product>()
            };

            foreach (var index in dto.ProductsIds)
            {
                var product = products.FirstOrDefault(x => x.Id == index);
                if (product == null)
                    throw new NotFoundException($"Product with id: {index} not exist");
                order.Products.Add(product);
                order.TotalCost += product.Price;
            }
            
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public List<Order> GetAll() // later aligator
        {
            var listOfOrders = _context.Orders.ToList();
            return listOfOrders;

        }

    }
}
