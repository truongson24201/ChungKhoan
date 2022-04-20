using BangGiaTrucTuyen.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace BangGiaTrucTuyen.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            productRepository = new ProductRepository(connectionString);
        }

        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts",products);//bất đồng bộ ngắt sử lý 

            var productsForGraph = productRepository.GetProductsForGraph();
            await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);
        }
    }
}
