using System.Collections.Generic;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.OrderDataAccess;

namespace WebBanHang.Business_Layer
{
    public class OrderBusinessLogic
    {
        private readonly OrderDataAccess orderDataAccess;

        public OrderBusinessLogic()
        {
            orderDataAccess = new OrderDataAccess();
        }

        public List<Order> GetOrders()
        {
            return orderDataAccess.GetOrders();
        }
    }
}
